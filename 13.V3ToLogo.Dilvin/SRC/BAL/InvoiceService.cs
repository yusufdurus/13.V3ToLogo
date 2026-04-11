using V3ToLogo.MODEL.ACCESSDB;
using V3ToLogo.MODEL.DATABASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenApi.Invoice;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.ServiceModel;

namespace V3ToLogo.BAL
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
            DBEntities entity = new DBEntities();
            IVME_TRANSFER_INVOICE_ERR invErr = new IVME_TRANSFER_INVOICE_ERR();
            invErr.InvoiceNumber = _invoiceNumber;
            invErr.ERRLOG = _errLog;
            invErr.ERRDATE = DateTime.Now;
            entity.IVME_TRANSFER_INVOICE_ERR.Add(invErr);
            entity.SaveChanges();
        }
        public static void SetVariableBasedOnInvoiceType(string invoiceType)
        {
            switch (invoiceType)
            {
                case "WS":
                    GeneralBussines.activeTransfer = ActiveTransferType.FaturaWS;
                    break;
                case "BP":
                    GeneralBussines.activeTransfer = ActiveTransferType.FaturaBP;
                    break;
                case "R":
                    GeneralBussines.activeTransfer = ActiveTransferType.FaturaR;
                    break;
                // Diğer durumları burada ekleyebilirsiniz.
                default:
                    GeneralBussines.activeTransfer = ActiveTransferType.None;
                    break;
            }
        }
        
        
        public static int InvoiceNotTransferred(string invoiceType, string invoiceNr)
        {
            // aktarım logunu sil. kayıt sonraki aktarımlarda tekrar aktarım listesine gelir.
            using (DBEntities entity = new DBEntities())
            {
                var param1 = new SqlParameter("invoiceNr", invoiceNr);
                var param2 = new SqlParameter("invoiceType", invoiceType);

                return entity.Database.ExecuteSqlCommand("sp_InvoiceNotTransferred @invoiceNr, @invoiceType", param1, param2);
            }
        }

        private int GetLogoCurrencyId(string currencyCode)
        {
            int currencyId;
            switch (currencyCode)
            {
                case "TRL":
                    {
                        currencyId = 0;
                    }
                    break;
                case "USD":
                    {
                        currencyId = 1;
                    }
                    break;
                case "EUR":
                    {
                        currencyId = 20;
                    }
                    break;
                default:
                    currencyId = 0;
                    break;
            }
            return currencyId;
        }
        public async Task<string> InvoiceTransfer(string invoiceType, string startDate, string endDate, string _invoiceNr)
        {
            string returnValue = GeneralBussines.SUCCESS_MSG;

            string log_invoiceRef = "";
            string slipNumber = "";
            string docNumber = "";
            string customerCode = "";
            bool vatIncluded; // kdv dahil mi
            bool isReturn = false; // iade faturası mı
            InvoiceTypeEnum trCode;
            string sessionId;
            if (LoginService.GetInstance().HasSession())
                sessionId = LoginService.GetInstance().lastSession.SessionId;
            else return "Login Session Yok";

            ConnectionSettings connectionSettings = SettingManager.GetInstance().GetConnectionSettings(out returnValue);
            try
            {
                SetVariableBasedOnInvoiceType(invoiceType);

                using (DBEntities entity = new DBEntities())
                {
                    string sql = "EXEC sp_IvmeInvoiceList @invoiceType, @_startDate, @_endDate, @_invoiceNr";

                    List<SqlParameter> parms = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@invoiceType", Value = invoiceType },
                        new SqlParameter { ParameterName = "@_startDate", Value = startDate },
                        new SqlParameter { ParameterName = "@_endDate", Value = endDate },
                        new SqlParameter { ParameterName = "@_invoiceNr", Value = _invoiceNr }
                    };
                    entity.Database.CommandTimeout = 660;
                    // Dönüş türünü belirtin
                    List<sp_IvmeInvoiceList_Result> spResList = entity.Database.SqlQuery<sp_IvmeInvoiceList_Result>(sql, parms.ToArray()).ToList();

                    //spResList = entity.sp_IvmeInvoiceList(invoiceType, startDate, endDate, _invoiceNr).ToList();
                    var invoiceMasterList = spResList.Select(x => new { x.InvoiceRef, x.IsReturn, x.InvoiceSeries, x.InvoiceSeriesNumber, x.InvoiceDate, x.TaxType, x.CustomerCode, 
                                x.InvoiceType, x.IsEInvoice, x.EInvoiceNumber, x.TrCurrencyRate, x.TrCurrencyCode }).Distinct();
                    RecordCount = invoiceMasterList.Count();
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());
                    foreach (var masteritem in invoiceMasterList)
                    {
                        try
                        {
                            log_invoiceRef = masteritem.InvoiceRef;
                            slipNumber = masteritem.InvoiceSeries + "" + masteritem.InvoiceSeriesNumber.ToString();
                            docNumber = slipNumber;
                            isReturn = masteritem.IsReturn;
                            customerCode = masteritem.CustomerCode;
                            vatIncluded = false;
                            Invoice invoiceItem = new Invoice();


                            switch (masteritem.InvoiceType.Trim())
                            {
                                case "R": // Perakende Satış Faturası
                                    vatIncluded = true;
                                    customerCode = connectionSettings.LogoAccountCode;
                                    if (isReturn)
                                    {
                                        trCode = InvoiceTypeEnum.PerakendeSatisIade;
                                        slipNumber = log_invoiceRef;
                                    }
                                    else
                                    {
                                        trCode = InvoiceTypeEnum.PerakendeSatis;
                                    }
                                    break;
                                case "BP": // toptan-perakende Alış Faturası
                                    vatIncluded = true;
                                    if (isReturn)
                                    {
                                        trCode = InvoiceTypeEnum.SatinalmaIade;
                                        slipNumber = log_invoiceRef;
                                    }
                                    else
                                    {
                                        trCode = InvoiceTypeEnum.Satinalma;
                                    }
                                    break;
                                case "WS": // Toptan Satış Faturası
                                    vatIncluded = false;
                                    if (isReturn)
                                    {
                                        trCode = InvoiceTypeEnum.ToptanSatisIade;
                                        slipNumber = log_invoiceRef; // iade faturalarında V3 de aynı fiş numarası 1den fazla olabiliyor. Bu durumda V3 deki uniq alan değeri fiş numarası olarak logoya aktarılacak.
                                    }
                                    else
                                    {
                                        trCode = InvoiceTypeEnum.ToptanSatis;
                                    }
                                    break;
                                case "EP": // Satınalma Hizmet Faturası
                                    vatIncluded = false;
                                    if (isReturn)
                                    {
                                        trCode = InvoiceTypeEnum.SatinalmaIade;
                                        slipNumber = log_invoiceRef; // iade faturalarında V3 de aynı fiş numarası 1den fazla olabiliyor. Bu durumda V3 deki uniq alan değeri fiş numarası olarak logoya aktarılacak.
                                    }
                                    else
                                    {
                                        trCode = InvoiceTypeEnum.AlinanHizmet;
                                    }
                                    break;
                                case "EXS": // Satış Hizmet Faturası
                                    vatIncluded = false;
                                    if (isReturn)
                                    {
                                        trCode = InvoiceTypeEnum.ToptanSatisIade;
                                        slipNumber = log_invoiceRef; // iade faturalarında V3 de aynı fiş numarası 1den fazla olabiliyor. Bu durumda V3 deki uniq alan değeri fiş numarası olarak logoya aktarılacak.
                                    }
                                    else
                                    {
                                        trCode = InvoiceTypeEnum.VerilenHizmet;
                                    }
                                    break;
                                default:
                                    vatIncluded = true;
                                    if (isReturn)
                                    {
                                        trCode = InvoiceTypeEnum.PerakendeSatisIade;
                                    }
                                    else
                                    {
                                        trCode = InvoiceTypeEnum.PerakendeSatis;
                                    }
                                    break;
                            }

                            if ((String.IsNullOrEmpty(masteritem.InvoiceSeries)) || (masteritem.InvoiceSeries == "0") || (String.IsNullOrEmpty(masteritem.InvoiceSeriesNumber.ToString())))
                            {
                                slipNumber = masteritem.InvoiceRef;
                                docNumber = slipNumber;
                            }
                            invoiceItem.TrCode = trCode;
                            invoiceItem.Date = masteritem.InvoiceDate;
                            invoiceItem.CustomerCode = customerCode;
                            invoiceItem.DivisionNr = 0;
                            invoiceItem.DocNumber = docNumber;
                            invoiceItem.SlipNumber = slipNumber;
                            invoiceItem.FactoryNr = 0;
                            invoiceItem.WareHouseNr = 0;
                            invoiceItem.SpecialCode = "V3-" + log_invoiceRef.Substring(6);
                            invoiceItem.InvoiceLines = new List<InvoiceLine>();
                            invoiceItem.PaymentCode = "";
                            invoiceItem.ProjectCode = "";
                            invoiceItem.Description_ = "";
                            invoiceItem.SalesmanCode = "";
                            invoiceItem.AuthCode = "";
                            int currencyId = GetLogoCurrencyId(masteritem.TrCurrencyCode);
                            
                            invoiceItem.TrCurrency = currencyId; //işlem dovizi
                            invoiceItem.TrRate = Convert.ToDouble(masteritem.TrCurrencyRate); // işlem dövizi kuru
                            invoiceItem.RcRate = 1; // raporlama dövizi kuru  Todo : 115 nolu firmada rapor dovizi TL olduğu için 1 gidecek. ama başka firmada usd ise yanlış olur.
                            invoiceItem.CurrselTotals = 2; // genel toplam döviz gösterim türü  1= raporlama dövizine göre, 2 = işlem dövizine göre 3= Euro
                            invoiceItem.CurrselTransactions = 1; // satır döviz gösterim türü  1=yerel para birimi, 2=raporlama 3=işlem dövizi 4= euro 5=fiyatlandırma dövizi

                            List<sp_IvmeInvoiceList_Result> InvoiceLines = spResList.Where(x => x.InvoiceRef == masteritem.InvoiceRef).ToList();
                            for (int i = 0; i < InvoiceLines.Count; i++)
                            {
                                if (InvoiceLines[i].LineType == 0)
                                {
                                    InvoiceLine ProductLine = new InvoiceLine();
                                    ProductLine.Type = (int)InvoiceLineTypeEnum.ProductLine;
                                    ProductLine.Quantity = InvoiceLines[i].Quantity;
                                    ProductLine.Price = Convert.ToDouble(InvoiceLines[i].Price);
                                    ProductLine.CurrencyId = GetLogoCurrencyId(InvoiceLines[i].CurrencyCode);
                                    ProductLine.CurrencyRate = InvoiceLines[i].CurrencyRate;
                                    ProductLine.LocalPrice = Convert.ToDouble(InvoiceLines[i].LocalPrice);
                                    ProductLine.MasterCode = accessDb.GetLogoCode(2, InvoiceLines[i].ProductHierarchyLevel, out returnValue);
                                    ProductLine.UnitCode = accessDb.GetLogoCode(1, InvoiceLines[i].UnitSet, out returnValue);
                                    ProductLine.UnitConv1 = 1;
                                    ProductLine.UnitConv2 = 1;
                                    ProductLine.VatRate = Convert.ToDouble(InvoiceLines[i].VatRate);
                                    ProductLine.VatIncluded = vatIncluded;
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
                                    serviceLine.Quantity = InvoiceLines[i].Quantity;
                                    serviceLine.Price = Convert.ToDouble(InvoiceLines[i].Price);
                                    serviceLine.CurrencyId = GetLogoCurrencyId(InvoiceLines[i].CurrencyCode);
                                    serviceLine.CurrencyRate = InvoiceLines[i].CurrencyRate;
                                    serviceLine.LocalPrice = Convert.ToDouble(InvoiceLines[i].LocalPrice);
                                    serviceLine.MasterCode = accessDb.GetLogoCode(2, InvoiceLines[i].ProductHierarchyLevel, out returnValue);
                                    serviceLine.UnitCode = accessDb.GetLogoCode(1, InvoiceLines[i].UnitSet, out returnValue);
                                    serviceLine.UnitConv1 = 1;
                                    serviceLine.UnitConv2 = 1;
                                    serviceLine.VatIncluded = vatIncluded;
                                    serviceLine.VatRate = Convert.ToDouble(InvoiceLines[i].VatRate);
                                    serviceLine.VariantCode = "";
                                    serviceLine.VariantName = "";
                                    invoiceItem.InvoiceLines.Add(serviceLine);
                                }
                            }

                            InvoiceServiceResult res=null;
                            switch (masteritem.InvoiceType.Trim())
                            {
                                case "R" : res = await client.CreateSaleInvoiceForV3ProjectAsync(sessionId, invoiceItem);
                                    break;
                                case "BP": res = await client.CreatePurchInvoiceForV3ProjectAsync(sessionId, invoiceItem);
                                    break;
                                case "WS":
                                    res = await client.CreateSaleInvoiceForV3ProjectAsync(sessionId, invoiceItem);
                                    break;
                                case "EXS":
                                    res = await client.CreateSaleInvoiceForV3ProjectAsync(sessionId, invoiceItem);
                                    break;
                                case "EP":
                                    res = await client.CreatePurchInvoiceForV3ProjectAsync(sessionId, invoiceItem);
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
                                ErrorList.Add(new MODEL.GENERAL.TransferErrorItem
                                {
                                    Id = masteritem.InvoiceRef,
                                    Kod = slipNumber,
                                    Aciklama = customerCode,
                                    Log = res.Description
                                });
                                TransferCount_Error += 1;
                                returnValue = res.Description;
                            }

                        }
                        catch (Exception ex)
                        {
                            returnValue = ex.Message;
                            InsertInvoiceErrorLog(log_invoiceRef, returnValue);
                            ErrorList.Add(new MODEL.GENERAL.TransferErrorItem
                            {
                                Id = log_invoiceRef,
                                Kod = slipNumber,
                                Aciklama = customerCode,
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
                InsertInvoiceErrorLog(log_invoiceRef, returnValue);
                TransferCount_Error += 1;
            }
            return returnValue;
        }
    }
}
