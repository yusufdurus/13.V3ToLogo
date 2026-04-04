using ImportToLogo.MODEL.ACCESSDB;
using ImportToLogo.MODEL.LOGODB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenAPIs.InvoiceApiReference;
using System.Data.SqlClient;

namespace ImportToLogo.BAL
{
    internal class InvoiceService : BaseService
    {
        private static InvoiceService instance = null;
        private InvoiceService() : base() { }
        public static InvoiceService GetInstance()
        {
            if (instance == null)
                instance = new InvoiceService();
            return instance;
        }
        private void InsertInvoiceErrorLog(string _invoiceNumber, string _errLog)
        {
            LogoEntities entity = new LogoEntities();
            IVME_TRANSFER_INVOICE_ERR invErr = new IVME_TRANSFER_INVOICE_ERR();
            invErr.InvoiceNumber = _invoiceNumber;
            invErr.ERRLOG = _errLog;
            invErr.ERRDATE = DateTime.Now;
            entity.IVME_TRANSFER_INVOICE_ERR.Add(invErr);
            entity.SaveChanges();
        }
        public async Task<string> InvoiceTransfer()
        {
            string returnValue = GeneralBussines.SUCCESS_MSG;
            string log_invoiceRef = "";
            string sessionId;
            if (LoginService.GetInstance().HasSession())
                sessionId = LoginService.GetInstance().lastSession.SessionId;
            else return "Login Session Yok";

            ConnectionSettings connectionSettings = SettingManager.GetInstance().GetConnectionSettings(out returnValue);
            try
            {
                using (LogoEntities entity = new LogoEntities())
                {
                    List<sp_IvmeInvoiceList_Result> spResList = new List<sp_IvmeInvoiceList_Result>();
                    spResList = entity.sp_IvmeInvoiceList().ToList();
                    var invoiceMasterList = spResList.Select(x => new { x.InvoiceRef, x.IsReturn, x.InvoiceSeries, x.InvoiceSeriesNumber, x.InvoiceDate, x.TaxType, x.CustomerCode, x.InvoiceType }).Distinct();
                    RecordCount = invoiceMasterList.Count();
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());
                    foreach (var masteritem in invoiceMasterList)
                    {
                        try
                        {
                            log_invoiceRef = masteritem.InvoiceRef;
                            Invoice invoiceItem = new Invoice();
                            invoiceItem.Date = masteritem.InvoiceDate;

                            if (masteritem.InvoiceType == "R")
                                invoiceItem.CustomerCode = connectionSettings.LogoAccountCode;
                            else
                                invoiceItem.CustomerCode = masteritem.CustomerCode;

                            string invoiceNr = masteritem.InvoiceSeries + "" + masteritem.InvoiceSeriesNumber.ToString();// 29-09-2018 eskiden boşluk vardı
                            if ((String.IsNullOrEmpty(masteritem.InvoiceSeries)) || (masteritem.InvoiceSeries == "0") || (String.IsNullOrEmpty(masteritem.InvoiceSeriesNumber.ToString())))
                            {
                                invoiceNr = masteritem.InvoiceRef;
                            }

                            invoiceItem.DivisionNr = 0;
                            invoiceItem.DocNumber = invoiceNr;
                            invoiceItem.SlipNumber = invoiceNr;
                            invoiceItem.TrCurrency = 0;
                            invoiceItem.TrRate = 0;
                            invoiceItem.FactoryNr = 0;
                            invoiceItem.WareHouseNr = 0;
                            invoiceItem.SpecialCode = "V3-"+ masteritem.InvoiceRef.Substring(6);
                            invoiceItem.InvoiceLines = new List<InvoiceLine>();

                            invoiceItem.PaymentCode = "";
                            invoiceItem.ProjectCode = "";
                            invoiceItem.Description_ = "";
                            invoiceItem.SalesmanCode = "";
                            invoiceItem.AuthCode = "";

                            List<sp_IvmeInvoiceList_Result> InvoiceLines = spResList.Where(x => x.InvoiceRef == masteritem.InvoiceRef).ToList();
                            for (int i = 0; i < InvoiceLines.Count; i++)
                            {
                                if (InvoiceLines[i].LineType == 0)
                                {
                                    InvoiceLine ProductLine = new InvoiceLine();
                                    ProductLine.Type = (int)InvoiceLineTypeEnum.ProductLine;
                                    ProductLine.Quantity = InvoiceLines[i].Quantity.GetValueOrDefault(0);
                                    double price = Convert.ToDouble((InvoiceLines[i].Price.GetValueOrDefault(0) - InvoiceLines[i].TotalDiscount)) / InvoiceLines[i].Quantity.GetValueOrDefault(1);
                                    ProductLine.Price = price * (1 + (InvoiceLines[i].ProductTax.GetValueOrDefault(0) / 100.00));
                                    ProductLine.MasterCode = accessDb.GetLogoCode(2, InvoiceLines[i].ProductHierarchyLevel04, out returnValue);
                                    ProductLine.UnitCode = accessDb.GetLogoCode(1, InvoiceLines[i].UnitSet, out returnValue);
                                    ProductLine.UnitConv1 = 1;
                                    ProductLine.UnitConv2 = 1;
                                    ProductLine.VatIncluded = true;
                                    ProductLine.VatRate = InvoiceLines[i].ProductTax.GetValueOrDefault(0);

                                    ProductLine.DiscountRate = 0;
                                    ProductLine.Discount = 0;
                                    ProductLine.VariantCode = "";
                                    ProductLine.VariantName = "";
                                    

                                    invoiceItem.InvoiceLines.Add(ProductLine);
                                }
                                else
                                {
                                    InvoiceLine serviceLine = new InvoiceLine();
                                    serviceLine.Type = (int)InvoiceLineTypeEnum.ServiceLine;
                                    serviceLine.Quantity = InvoiceLines[i].Quantity.GetValueOrDefault(0);
                                    double price = Convert.ToDouble((InvoiceLines[i].Price.GetValueOrDefault(0) - InvoiceLines[i].TotalDiscount)) / InvoiceLines[i].Quantity.GetValueOrDefault(1);
                                    serviceLine.Price = price * (1 + (InvoiceLines[i].ProductTax.GetValueOrDefault(0) / 100.00));
                                    serviceLine.MasterCode = accessDb.GetLogoCode(2, InvoiceLines[i].ProductHierarchyLevel04, out returnValue);
                                    serviceLine.UnitCode = accessDb.GetLogoCode(1, InvoiceLines[i].UnitSet, out returnValue);
                                    serviceLine.UnitConv1 = 1;
                                    serviceLine.UnitConv2 = 1;
                                    serviceLine.VatIncluded = true;
                                    serviceLine.VatRate = InvoiceLines[i].ProductTax.GetValueOrDefault(0);
                                    serviceLine.VariantCode = "";
                                    serviceLine.VariantName = "";
                                    invoiceItem.InvoiceLines.Add(serviceLine);
                                }
                            }

                            InvoiceServiceResult res=null;
                            switch (masteritem.InvoiceType.Trim())
                            {
                                case "R":
                                    {
                                        if (masteritem.IsReturn)
                                            invoiceItem.TrCode = InvoiceTypeEnum.PerakendeSatisIade;
                                        else
                                            invoiceItem.TrCode = InvoiceTypeEnum.PerakendeSatis;
                                        res = await client.CreateSaleInvoiceForV3ProjectAsync(sessionId, invoiceItem);
                                    }
                                    break;
                                case "BP":
                                    {
                                        if (masteritem.IsReturn)
                                            invoiceItem.TrCode = InvoiceTypeEnum.SatinalmaIade;
                                        else
                                            invoiceItem.TrCode = InvoiceTypeEnum.Satinalma;
                                        res = await client.CreatePurchInvoiceForV3ProjectAsync(sessionId, invoiceItem);

                                    }
                                    break;
                                case "WS":
                                    {
                                        if (masteritem.IsReturn)
                                            invoiceItem.TrCode = InvoiceTypeEnum.ToptanSatisIade;
                                        else
                                            invoiceItem.TrCode = InvoiceTypeEnum.ToptanSatis;
                                        res = await client.CreateSaleInvoiceForV3ProjectAsync(sessionId, invoiceItem);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            if (res.ReturnCode == 100)
                            {
                                var param1 = new SqlParameter("invoiceNr", masteritem.InvoiceRef);
                                var param2 = new SqlParameter("invoiceType", masteritem.InvoiceType);

                                int affectedRows = entity.Database.ExecuteSqlCommand("sp_InvoiceTransferred @invoiceNr, @invoiceType", param1, param2);
                                TransferCount_Success += 1;
                            }
                            else
                            {
                                InsertInvoiceErrorLog(masteritem.InvoiceRef, res.Description);
                                TransferCount_Error += 1;
                                returnValue = res.Description;
                            }

                        }
                        catch (Exception ex)
                        {
                            returnValue = ex.Message;
                            InsertInvoiceErrorLog(log_invoiceRef, returnValue);
                            TransferCount_Error += 1;
                        }
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
                InsertInvoiceErrorLog(log_invoiceRef, returnValue);
                TransferCount_Error += 1;
            }
            return returnValue;
        }
    }
}
