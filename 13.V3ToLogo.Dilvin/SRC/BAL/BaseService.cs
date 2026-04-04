using System.Collections.Generic;
using V3ToLogo.DAL.ACCESSDB;
using V3ToLogo.MODEL.GENERAL;

namespace V3ToLogo.BAL
{
    public class BaseService
    {
        public int RecordCount = 0;
        public int TransferCount_Success = 0;
        public int TransferCount_Error = 0;
        public List<TransferErrorItem> ErrorList = new List<TransferErrorItem>();
        protected DataHandler accessDb = null;
        protected BaseService()
        {
            this.accessDb = new DataHandler();
        }
        protected string GetApiBaseUrl()
        {
            return SettingManager.GetInstance().ApiBaseUrl;
        }
    }
}
