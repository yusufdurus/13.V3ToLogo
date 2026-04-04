using System.Collections.Generic;
using System.Linq;
using System;
using ImportToLogo.MODEL.LOGODB;
using ImportToLogo.DAL.ACCESSDB;
using System.Threading.Tasks;
using ImportToLogoCustomerService;

namespace ImportToLogo.BAL
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
        private static void InsertCustomerErrorLog(string _customerNumber, string _errLog)
        {
            LogoEntities entity = new LogoEntities();
            IVME_TRANSFER_CUSTOMER_ERR customerErr = new IVME_TRANSFER_CUSTOMER_ERR();
            customerErr.CurrAccCode = _customerNumber;
            customerErr.ERRLOG = _errLog;
            customerErr.ERRDATE = DateTime.Now;
            entity.IVME_TRANSFER_CUSTOMER_ERR.Add(customerErr);
            entity.SaveChanges();
        }
        private Customer CreateCustomerItem(LogoEntities _logoEntities, sp_IvmeCustomerList_Result spResItem, out string returnValue)
        {
            returnValue = GeneralBussines.SUCCESS_MSG;
            Customer customerItem = new Customer();

            List<sp_IvmeCustomerCommList_Result> commList = new List<sp_IvmeCustomerCommList_Result>();
            commList = _logoEntities.sp_IvmeCustomerCommList(spResItem.CurrAccCode).ToList();

            customerItem.CustomerCode = accessDb.GetLogoCode(3, spResItem.CurrAccCode, out returnValue);
            customerItem.SpecialCode = customerItem.CustomerCode;
            customerItem.Definition = spResItem.cdCurrAccDesc;
            customerItem.EBusinessCode = "V3";
            customerItem.UseEXPBRWS = 1;
            customerItem.UseFINBRWS = 1;
            customerItem.UseIMPBRWS = 1;
            customerItem.UsePURCHBRWS = 1;
            customerItem.UseSALESBRWS = 1;

            if (commList.Any())
            {
                var primaryComm = commList.First();
                customerItem.TaxNr = primaryComm.CurrAccPostalAddress_TaxNumber;
                customerItem.TaxOffice = primaryComm.TaxOfficeDescription;
                customerItem.TaxOfficeCode = primaryComm.CurrAccPostalAddress_TaxOfficeCode;
                customerItem.AddressLine1 = primaryComm.Address;
                customerItem.AddressLine2 = $"{primaryComm.CityDescription} / {primaryComm.CountryDescription}";
                customerItem.City = primaryComm.CityDescription;
                customerItem.Country = primaryComm.CountryDescription;
                customerItem.EMailAddress = commList.FirstOrDefault(x => x.CommunicationTypeCode == "3")?.CommAddress;
                customerItem.FaxNumber = commList.FirstOrDefault(x => x.CommunicationTypeCode == "2")?.CommAddress;
                customerItem.PhoneNumber1 = commList.FirstOrDefault(x => x.CommunicationTypeCode == "1")?.CommAddress;
                customerItem.PhoneNumber2 = commList.FirstOrDefault(x => x.CommunicationTypeCode == "5")?.CommAddress;
            }

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
                using (LogoEntities entity = new LogoEntities())
                {
                    List<sp_IvmeCustomerList_Result> spResList = new List<sp_IvmeCustomerList_Result>();
                    spResList = entity.sp_IvmeCustomerList().ToList();
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());
                    foreach (var spResItem in spResList)
                    {
                        try
                        {
                            Customer customerItem = CreateCustomerItem(entity, spResItem, out returnValue);
                            CustomerServiceResult res = await client.SaveCustomerByControlForSpecodeAsync(sessionId, customerItem);
                            if (res.ReturnCode == 100)
                            {
                                IVME_TRANSFER_CUSTOMER currentCustomer = new IVME_TRANSFER_CUSTOMER();
                                currentCustomer.CurrAccCode = spResItem.CurrAccCode;
                                currentCustomer.TransferDate = DateTime.Now;
                                entity.IVME_TRANSFER_CUSTOMER.Add(currentCustomer);
                                entity.SaveChanges();
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
    }
}
