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

                    var slipListData = spResList.Select(x => new { x.BankTransNumber, x.DueDate, x.Description, x.BankTransTypeCode, x.BankTransTypeDescription, x.DocumentDate }).Distinct();
                    RecordCount = slipListData.Count();
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());
                    foreach (var slipData in slipListData)
                    {
                        try
                        {
                            log_Ref = slipData.BankTransNumber;
                            BankFiche bankFiche = new BankFiche();
                            bankFiche.SatirTutarGirisTuru = 1; // öndeğer yerel para birimi ile tutar girişi
                            bankFiche.Date = slipData.DocumentDate;
                            bankFiche.DepartmentNr = 0;
                            bankFiche.DivisionNr = 0;
                            bankFiche.SlipNumber = slipData.BankTransNumber;
                            bankFiche.AuthCode = "";
                            bankFiche.Description = slipData.Description;
                            bankFiche.SpecialCode = "V3";
                            switch (ficheType.Trim())
                            {
                                case "gelenHavale":
                                    {
                                        bankFiche.TrCode = BankFicheTypeEnum._3;
                                    }
                                    break;
                                case "gonderilenHavale":
                                    {
                                        bankFiche.TrCode = BankFicheTypeEnum._4;
                                    }
                                    break;
                                default: 
                                    break;
                            }
                            bankFiche.BankFicheLines = new List<BankFicheLine>();
                            

                            List<sp_BankFicheList_Result> linesData = spResList.Where(x => x.BankTransNumber == slipData.BankTransNumber).ToList();
                            for (int i = 0; i < linesData.Count; i++)
                            {

                                BankFicheLine line= new BankFicheLine();
                                line.ApproveNr = linesData[i].RefNumber;
                                line.DocNumber = linesData[i].DocumentNumber;
                                line.SpecialCode = "";
                                line.CustomerCode = linesData[i].CariKod;
                                line.BankCode = linesData[i].LogoBankCode;
                                line.BankAccountCode = linesData[i].BankCurrAccCode;
                                line.Description = linesData[i].Description;
                                line.Amount = (double)linesData[i].Doc_Amount;
                                line.Amount_TL = (double)linesData[i].Com_Amount;
                                line.XRate = (double)linesData[i].Com_ExchangeRate;
                                if (linesData[i].Doc_CurrencyCode!="TRY")
                                    bankFiche.SatirTutarGirisTuru = 2; // eğer dövizli işlem ise, satırlar döviz hesaplaması ile yapılsın.

                                bankFiche.BankFicheLines.Add(line);
                            }

                            BankFicheServiceResult res = null;
                            res = await client.CreateBankSlipAsync(sessionId, bankFiche);

                            if (res.ReturnCode == 100)
                            {
                                var param1 = new SqlParameter("slipNr", slipData.BankTransNumber);
                                var param2 = new SqlParameter("ficheType", (int)GeneralBussines.activeTransfer);

                                int affectedRows = entity.Database.ExecuteSqlCommand("sp_BankFicheTransferred @slipNr, @ficheType", param1, param2);

                                TransferCount_Success += 1;
                            }
                            else
                            {
                                InsertBankErrorLog(slipData.BankTransNumber, res.Description);
                                TransferCount_Error += 1;
                                returnValue = res.Description;
                            }

                        }
                        catch (Exception ex)
                        {
                            returnValue = ex.Message;
                            InsertBankErrorLog(log_Ref, returnValue);
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
