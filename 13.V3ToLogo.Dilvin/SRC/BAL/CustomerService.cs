using System.Collections.Generic;
using System.Linq;
using System;
using V3ToLogo.MODEL.DATABASE;
using System.Threading.Tasks;
using OpenApi.Customer;
using System.Data.SqlClient;
using V3ToLogo.MODEL.ACCESSDB;

namespace V3ToLogo.BAL
{
    public class CustomerService : BaseService
    {
        private static CustomerService instance = null;
        private CustomerService() : base() { }
        public static CustomerService GetInstance()
        {
            if (instance == null)
                instance = new CustomerService();
            return instance;
        }
        public static int CustomerNotTransferred(string customerCode)
        {
            // aktarım logunu sil. kayıt sonraki aktarımlarda tekrar aktarım listesine gelir.
            using (DBEntities entity = new DBEntities())
            {
                var param1 = new SqlParameter("customerCode", customerCode);

                return entity.Database.ExecuteSqlCommand("sp_CustomerNotTransferred @customerCode", param1);
            }
        }
        private static void InsertCustomerErrorLog(string _customerNumber, string _errLog)
        {
            DBEntities entity = new DBEntities();
            IVME_TRANSFER_CUSTOMER_ERR customerErr = new IVME_TRANSFER_CUSTOMER_ERR();
            customerErr.CurrAccCode = _customerNumber;
            customerErr.ERRLOG = _errLog;
            customerErr.ERRDATE = DateTime.Now;
            entity.IVME_TRANSFER_CUSTOMER_ERR.Add(customerErr);
            entity.SaveChanges();
        }
        private Customer CreateCustomerItem(DBEntities _logoEntities, sp_IvmeCustomerList_Result spResItem, out string returnValue)
        {
            returnValue = GeneralBussines.SUCCESS_MSG;
            Customer customerItem = new Customer();

            customerItem.AccountType = AccountTypeEnum._3;
            customerItem.CustomerCode = accessDb.GetLogoCode(3, spResItem.CurrAccCode, out returnValue);
            customerItem.SpecialCode = customerItem.CustomerCode;
            customerItem.Definition = spResItem.CurrAccDescription;
            customerItem.EBusinessCode = "V3";
            customerItem.UseEXPBRWS = 1;
            customerItem.UseFINBRWS = 1;
            customerItem.UseIMPBRWS = 1;
            customerItem.UsePURCHBRWS = 1;
            customerItem.UseSALESBRWS = 1;
            customerItem.IsPersonCompany = 0;
            customerItem.Name = "";
            customerItem.Surname = "";
            if (spResItem.IsIndividualAcc)
            {
                customerItem.IsPersonCompany = 1;
                customerItem.Name  = spResItem.FirstName;
                customerItem.Surname = spResItem.LastName;
            }
            customerItem.TaxNr = spResItem.TaxNumber;

            customerItem.TckNo = spResItem.IdentityNum;
            customerItem.Contact = "YOK";
            customerItem.FaxCode = "";
            customerItem.AuthCode = "";
            customerItem.Contact2 = "";
            customerItem.Contact3 = "";
            customerItem.PostCode = "";
            customerItem.PhoneCode1 = "";
            customerItem.PhoneCode2 = "";
            customerItem.Definition2 = "";
            customerItem.FaxExtNumber = "";
            customerItem.SpecialCode2 = "";
            customerItem.SpecialCode3 = "";
            customerItem.SpecialCode4 = "";
            customerItem.SpecialCode5 = "";
            customerItem.DistrictCode1 = spResItem.DistrictCode;
            customerItem.DistrictCode2 = "";
            customerItem.PhoneExtNumber1 = "";
            customerItem.PhoneExtNumber2 = "";
            customerItem.GroupCustomerCode = "";

            customerItem.TaxOffice = spResItem.TaxOffice;
            customerItem.TaxOfficeCode = spResItem.TaxOfficeCode;

            customerItem.AddressLine1 = spResItem.Address;
            customerItem.AddressLine2 = $"{spResItem.CityDescription} / {spResItem.CountryDescription}";
            customerItem.City = spResItem.CityDescription;
            customerItem.Town = spResItem.DistrictDescription;
            customerItem.Country = spResItem.CountryDescription;
            customerItem.EMailAddress = spResItem.EMail;
            customerItem.FaxNumber = "YOK";
            customerItem.PhoneNumber1 = spResItem.Phone1;
            customerItem.PhoneNumber2 = spResItem.Phone2;

            return customerItem;
        }
        public async Task<string> CustomerTransfer()
        {
            string returnValue = GeneralBussines.SUCCESS_MSG;

            string log_customerCode = "";
            string sessionId;
            if (LoginService.GetInstance().HasSession())
                sessionId = LoginService.GetInstance().lastSession.SessionId;
            else return "Login Session Yok";

            try
            {
                GeneralBussines.activeTransfer = ActiveTransferType.Cari;
                using (DBEntities entity = new DBEntities())
                {
                    List<sp_IvmeCustomerList_Result> spResList = new List<sp_IvmeCustomerList_Result>();
                    spResList = entity.sp_IvmeCustomerList().ToList();
                    RecordCount = spResList.Count();
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());
                    foreach (var spResItem in spResList)
                    {
                        try
                        {
                            Customer customerItem = CreateCustomerItem(entity, spResItem, out returnValue);
                            CustomerServiceResult res = await client.SaveCustomerAsync(sessionId, customerItem);
                            if (res.ReturnCode == 100)
                            {
                                var param1 = new SqlParameter("customerCode", customerItem.CustomerCode);

                                int affectedRows = entity.Database.ExecuteSqlCommand("sp_CustomerTransferred @customerCode", param1);

                                TransferCount_Success += 1;
                            }
                            else
                            {
                                InsertCustomerErrorLog(customerItem.CustomerCode, res.Description);
                                TransferCount_Error += 1;
                                returnValue = res.Description;
                            }
                        }
                        catch (Exception ex)
                        {
                            InsertCustomerErrorLog(log_customerCode, ex.Message);
                            TransferCount_Error += 1;
                            returnValue = ex.Message;
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }

                }
            }
            catch (Exception ex)
            {
                TransferCount_Error += 1;
                returnValue = ex.Message;
                InsertCustomerErrorLog(log_customerCode, returnValue);
            }
            return returnValue;
        }

        private void InsertCLFicheErrorLog(string _slipNumber, string _errLog)
        {
            DBEntities entity = new DBEntities();
            IVME_TRANSFER_CLFICHE_ERR err = new IVME_TRANSFER_CLFICHE_ERR();
            err.FicheNumber = _slipNumber;
            err.ERRLOG = _errLog;
            err.ERRDATE = DateTime.Now;
            entity.IVME_TRANSFER_CLFICHE_ERR.Add(err);
            entity.SaveChanges();
        }
        public static int CHFKrediKartNotTransferred(string invoiceType, string invoiceNr)
        {
            // aktarım logunu sil. kayıt sonraki aktarımlarda tekrar aktarım listesine gelir.
            using (DBEntities entity = new DBEntities())
            {
                var param1 = new SqlParameter("ficheNr", invoiceNr);

                return entity.Database.ExecuteSqlCommand("sp_CHFKrediKartNotTransferred @invoiceNr", param1);
            }
        }
        public async Task<string> CHFKrediKartTransfer(string startDate, string endDate, string _ficheNr)
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
                GeneralBussines.activeTransfer = ActiveTransferType.Kredikarti;
                using (DBEntities entity = new DBEntities())
                {
                    string sql = "EXEC sp_CHF_KrediKarti @_startDate, @_endDate, @_ficheNr";

                    List<SqlParameter> parms = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@_startDate", Value = startDate },
                        new SqlParameter { ParameterName = "@_endDate", Value = endDate },
                        new SqlParameter { ParameterName = "@_ficheNr", Value = _ficheNr }
                    };

                    // Dönüş türünü belirtin
                    List<sp_CHF_KrediKarti_Result> spResList = entity.Database.SqlQuery<sp_CHF_KrediKarti_Result>(sql, parms.ToArray()).ToList();
                    var slipListData = spResList.Select(x => new { x.CreditCardPaymentNumber, x.PaymentDate, x.CariKod, x.BankCode }).Distinct();
                    RecordCount = slipListData.Count();
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());
                    foreach (var slipData in slipListData)
                    {
                        try
                        {
                            log_Ref = slipData.CreditCardPaymentNumber;
                            CustomerSlip slip = new CustomerSlip();
                            slip.TrCode = CustomerSlipTypeEnum._70;
                            slip.Date = slipData.PaymentDate;
                            slip.Department = "0";
                            slip.Division = "0";
                            slip.SlipNumber = slipData.CreditCardPaymentNumber;
                            slip.AuthCode = "";
                            slip.Description = "";
                            slip.Division = "0";
                            slip.SpecialCode = "V3-" + slipData.CreditCardPaymentNumber.ToString();
                            
                            slip.CustomerSlipLines = new List<CustomerSlipLine>();


                            List<sp_CHF_KrediKarti_Result> slipLinesData = spResList.Where(x => x.CreditCardPaymentNumber == slipData.CreditCardPaymentNumber).ToList();
                            for (int i = 0; i < slipLinesData.Count; i++)
                            {
                                CustomerSlipLine slipLine = new CustomerSlipLine();
                                slipLine.ApproveNr = "";
                                slipLine.SpecialCode = "v3ToLogo - "+ slipLinesData[i].RefNumber;
                                slipLine.BankCode = slipLinesData[i].BankCode;
                                slipLine.BankAccountCode = "firstline";
                                slipLine.DocNumber = slipLinesData[i].DocumentNumber;
                                slipLine.PaymentCode = slipLinesData[i].CreditCardTypeCode;
                                slipLine.CreditCardNo = "";
                                slipLine.CustomerCode = slipLinesData[i].CariKod;
                                slipLine.Credit = (double)slipLinesData[i].CurrAccAmount;
                                slipLine.Debit = 0;

                                slip.CustomerSlipLines.Add(slipLine);
                            }

                            CustomerSlipServiceResult res = null;

                            
                            res = await client.CreateCustomerSlipAsync(sessionId, slip);

                            if (res.ReturnCode == 100)
                            {
                                var param1 = new SqlParameter("slipNr", slip.SlipNumber);

                                int affectedRows = entity.Database.ExecuteSqlCommand("sp_CHFKrediKartTransferred @slipNr", param1);

                                TransferCount_Success += 1;
                            }
                            else
                            {
                                InsertCLFicheErrorLog(slipData.CreditCardPaymentNumber, res.Description);
                                TransferCount_Error += 1;
                                returnValue = res.Description;
                            }

                        }
                        catch (Exception ex)
                        {
                            returnValue = ex.Message;
                            InsertCLFicheErrorLog(log_Ref, returnValue);
                            TransferCount_Error += 1;
                        }
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
                InsertCLFicheErrorLog(log_Ref, returnValue);
                TransferCount_Error += 1;
            }
            return returnValue;
        }
    }
}
