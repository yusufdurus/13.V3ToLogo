using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using V3ToLogo.MODEL.DATABASE;
using OpenApi.Product;

namespace V3ToLogo.BAL
{
    public class ProductService : BaseService
    {
        private static ProductService instance = null;
        private ProductService() : base() { } 
        public static ProductService GetInstance()
        {
            if (instance == null)
                instance = new ProductService();
            return instance;
        }
        private void AddTransferProductToDb(DBEntities entity, sp_IvmeProductList_Result spResItem)
        {
            IVME_TRANSFER_PRODUCT currentProduct = new IVME_TRANSFER_PRODUCT();
            currentProduct.ItemCode = spResItem.ProductHierarchyLevel04;
            currentProduct.TransferDate = DateTime.Now;
            entity.IVME_TRANSFER_PRODUCT.Add(currentProduct);
            entity.SaveChanges();
        }
        private static void InsertProductErrorLog(string _itemCode, string _errLog)
        {
            DBEntities entity = new DBEntities();
            IVME_TRANSFER_PRODUCT_ERR productErr = new IVME_TRANSFER_PRODUCT_ERR();
            productErr.ItemCode = _itemCode;
            productErr.ERRLOG = _errLog;
            productErr.ERRDATE = DateTime.Now;
            entity.IVME_TRANSFER_PRODUCT_ERR.Add(productErr);
            entity.SaveChanges();
        }
        private Product CreateProduct(ProductTypeEnum cardType, sp_IvmeProductList_Result spResItem)
        {
            string code = accessDb.GetLogoCode(2, spResItem.ProductHierarchyLevel04, out string _errMsg);
            Product productItem = new Product
            {
                CardType = cardType,
                UseFMM = 1,
                UseFPurch = 1,
                UseFSales = 1,
                SellPrVat = Convert.ToInt32(spResItem.ItemTaxGrCode),
                ReturnPrVat = Convert.ToInt32(spResItem.ItemTaxGrCode),
                ReturnVat = Convert.ToInt32(spResItem.ItemTaxGrCode),
                SellVat = Convert.ToInt32(spResItem.ItemTaxGrCode),
                Vat = Convert.ToInt32(spResItem.ItemTaxGrCode),
                Code = code,
                Name = code,
                SpeCode = code,
                ProducerCode = "V3",
                UnitSetCode = accessDb.GetLogoCode(1, spResItem.UnitOfMeasureCode1, out _errMsg)
            };
            return productItem;
        }
        public async Task<string> ProductTransfer()
        {
            string returnValue = GeneralBussines.SUCCESS_MSG;

            string log_ProductCode = "";
            string sessionId;
            if (LoginService.GetInstance().HasSession())
                sessionId = LoginService.GetInstance().lastSession.SessionId;
            else return "Login Session Yok";

            try
            {
                GeneralBussines.activeTransfer = ActiveTransferType.Malzeme;
                using (DBEntities entity = new DBEntities())
                {
                    List<sp_IvmeProductList_Result> spResList = new List<sp_IvmeProductList_Result>();
                    spResList = entity.sp_IvmeProductList().ToList();
                    var client = new Client(GetApiBaseUrl(), new System.Net.Http.HttpClient());
                    foreach (var spResItem in spResList)
                    {
                        try
                        {
                            var product = CreateProduct(ProductTypeEnum.TicariMal, spResItem);
                            ProductServiceResult res = await client.SaveProductByControlForSpecodeAsync(sessionId, product);
                            if (res.ReturnCode == 100)
                            {
                                AddTransferProductToDb(entity, spResItem);
                                TransferCount_Success += 1;
                            }
                            else
                            {
                                InsertProductErrorLog(product.Code, res.Description);
                                TransferCount_Error += 1;
                                returnValue = res.Description;
                            }
                        }
                        catch (Exception ex)
                        {
                            TransferCount_Error += 1;
                            InsertProductErrorLog(log_ProductCode, ex.Message);
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
                InsertProductErrorLog(log_ProductCode, returnValue);
            }
            return returnValue;
        }
    }
}
