using V3ToLogo.DAL.ACCESSDB;

namespace V3ToLogo.BAL
{
    public class BaseService
    {
        public int RecordCount = 0;
        public int TransferCount_Success = 0;
        public int TransferCount_Error = 0;
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
