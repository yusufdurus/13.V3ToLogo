using V3ToLogo.MODEL.ACCESSDB;
using V3ToLogo.MODEL.DATABASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenApi.Bank;
using System.Data.SqlClient;

namespace V3ToLogo.BAL
{
    internal class BankService : BaseService
    {
        private static BankService instance = null;
        private BankService() : base() { }
        public static BankService GetInstance()
        {
            if (instance == null)
                instance = new BankService();
            return instance;
        }
        private void InsertBankErrorLog(string _ficheNumber, string _errLog)
        {
            DBEntities entity = new DBEntities();
            IVME_TRANSFER_BANKFICHE_ERR bnFicheErr = new IVME_TRANSFER_BANKFICHE_ERR();
            bnFicheErr.FicheNumber = _ficheNumber;
            bnFicheErr.ERRLOG = _errLog;
            bnFicheErr.ERRDATE = DateTime.Now;
            entity.IVME_TRANSFER_BANKFICHE_ERR.Add(bnFicheErr);
            entity.SaveChanges();
        }
        public static void SetVariableBasedOnBankFicheType(string bankFicheType)
        {
            switch (bankFicheType)
            {
                case "gelenHavale":
                    GeneralBussines.activeTransfer = ActiveTransferType.Gelenhavale;
                    break;
                case "gonderilenHavale":
                    GeneralBussines.activeTransfer = ActiveTransferType.Gonderilenhavale;
                    break;
                default:
                    GeneralBussines.activeTransfer = ActiveTransferType.None;
                    break;
            }
        }
        public static int BankFicheNotTransferred(string bankFicheType, string ficheNr)
        {
            // aktarım logunu sil. kayıt sonraki aktarımlarda tekrar aktarım listesine gelir.
            using (DBEntities entity = new DBEntities())
            {
                var param1 = new SqlParameter("ficheNr", ficheNr);
                var param2 = new SqlParameter("bankFicheType", bankFicheType);

                return entity.Database.ExecuteSqlCommand("sp_BankFicheNotTransferred @ficheNr, @bankFicheType", param1, param2);
            }
        }

        public async Task<string> BankFicheTransfer(string ficheType, string startDate, string endDate, string _ficheNr)
        {
            string returnValue = GeneralBussines.SUCCESS_MSG;

            string log_Ref = "";
            string sessionId;
            if (LoginService.GetInstance().HasSession())
                sessionId = LoginService.GetInstance().lastSession.SessionId;
            else return "Login Session Yok";

            ConnectionSettings connectionSettings = SettingManager.GetInstance().GetConnectionSettings(out returnValue);
            try
            {
                int ficheTypeInt;
                switch (ficheType.Trim())
                {
                    case "virman":
                        ficheTypeInt = 3;
                        break;
                    case "gelenHavale":
                        ficheTypeInt = 4;
                        break;
                    case "gonderilenHavale":
                        ficheTypeInt = 5;
                        break;
                    default:
                        ficheTypeInt = 4;
                        break;
                }
                SetVariableBasedOnBankFicheType(ficheType);

                using (DBEntities entity = new DBEntities())
                {
                    string sql = "EXEC sp_BankFicheList @ficheType, @_startDate, @_endDate, @_ficheNr";

                    List<SqlParameter> parms = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@ficheType", Value = ficheTypeInt },
                        new SqlParameter { ParameterName = "@_startDate", Value = startDate },
                        new SqlParameter { ParameterName = "@_endDate", Value = endDate },
                        new SqlParameter { ParameterName = "@_ficheNr", Value = _ficheNr }
                    };

                    // Dönüş türünü belirtin
                    List<sp_BankFicheList_Result> spResList = entity.Database.SqlQuery<sp_BankFicheList_Result>(sql, parms.ToArray()).ToList();

                    RecordCount = spResList.Count;
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());

                    // BankTransNumber bazlı sayaç: aynı BankTransNumber için 1, 2, 3... şeklinde artan suffix
                    var slipCounters = new Dictionary<string, int>();

                    foreach (var row in spResList)
                    {
                        try
                        {
                            log_Ref = row.BankLineID.ToString();

                            // SlipNumber suffix hesapla
                            if (!slipCounters.ContainsKey(row.BankTransNumber))
                                slipCounters[row.BankTransNumber] = 0;
                            slipCounters[row.BankTransNumber]++;
                            string slipNumber = row.BankTransNumber + "-" + slipCounters[row.BankTransNumber];

                            BankFiche bankFiche = new BankFiche();
                            bankFiche.SatirTutarGirisTuru = row.Doc_CurrencyCode != "TRY" ? 2 : 1;
                            bankFiche.Date = row.DocumentDate;
                            bankFiche.DepartmentNr = 0;
                            bankFiche.DivisionNr = 0;
                            bankFiche.SlipNumber = slipNumber;
                            bankFiche.AuthCode = "";
                            bankFiche.Description = row.LineDescription;
                            bankFiche.SpecialCode = "V3";
                            switch (ficheType.Trim())
                            {
                                case "gelenHavale":
                                    bankFiche.TrCode = BankFicheTypeEnum._3;
                                    break;
                                case "gonderilenHavale":
                                    bankFiche.TrCode = BankFicheTypeEnum._4;
                                    break;
                                default:
                                    break;
                            }

                            BankFicheLine line = new BankFicheLine();
                            line.ApproveNr = row.RefNumber;
                            line.DocNumber = row.BankTransNumber;
                            line.SpecialCode = "";
                            line.CustomerCode = row.CariKod;
                            line.BankCode = row.LogoBankCode;
                            line.BankAccountCode = row.BankCurrAccCode;
                            line.Description = row.LineDescription;
                            line.Amount = (double)row.Doc_Amount;
                            line.Amount_TL = (double)row.Com_Amount;
                            line.XRate = (double)row.Com_ExchangeRate;

                            bankFiche.BankFicheLines = new List<BankFicheLine> { line };

                            BankFicheServiceResult res = await client.CreateBankSlipAsync(sessionId, bankFiche);

                            if (res.ReturnCode == 100)
                            {
                                var param1 = new SqlParameter("bankLineId", row.BankLineID);
                                var param2 = new SqlParameter("ficheType", (int)GeneralBussines.activeTransfer);

                                int affectedRows = entity.Database.ExecuteSqlCommand("sp_BankFicheTransferred @bankLineId, @ficheType", param1, param2);

                                TransferCount_Success += 1;
                            }
                            else
                            {
                                InsertBankErrorLog(slipNumber, res.Description);
                                ErrorList.Add(new MODEL.GENERAL.TransferErrorItem
                                {
                                    Id = row.BankTransNumber,
                                    Kod = row.CariKod,
                                    Aciklama = row.LineDescription,
                                    Log = res.Description
                                });
                                TransferCount_Error += 1;
                                returnValue = res.Description;
                            }

                        }
                        catch (Exception ex)
                        {
                            returnValue = ex.Message;
                            InsertBankErrorLog(log_Ref, returnValue);
                            ErrorList.Add(new MODEL.GENERAL.TransferErrorItem
                            {
                                Id = row.BankTransNumber,
                                Kod = row.CariKod,
                                Aciklama = row.LineDescription,
                                Log = ex.Message
                            });
                            TransferCount_Error += 1;
                        }
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
                InsertBankErrorLog(log_Ref, returnValue);
                TransferCount_Error += 1;
            }
            return returnValue;
        }
    }
}
