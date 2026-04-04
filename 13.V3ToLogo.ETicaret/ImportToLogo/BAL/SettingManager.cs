using ImportToLogo.DAL.ACCESSDB;
using ImportToLogo.MODEL.ACCESSDB;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace ImportToLogo.BAL
{
    public class SettingManager
    {
        private static SettingManager instance = null;
        private ConnectionSettings connectionSettings = null;
        private DataHandler dataHandler = null;
        private string apiBaseUrl;
        public string ApiBaseUrl
        {
            get 
            { 
                if (this.apiBaseUrl == "")
                    this.apiBaseUrl = ConfigurationManager.AppSettings["WebApiBaseUrl"];

                return this.apiBaseUrl; 
            }
            set 
            { 
                this.apiBaseUrl = value; 
            }
        }
        private SettingManager()
        {
            this.dataHandler = new DataHandler();
            this.apiBaseUrl = "";
        }
        public static SettingManager GetInstance()
        {
            if (instance == null) 
                instance= new SettingManager(); 
            return instance;
        }
        public void Reset()
        {
            this.connectionSettings= null; 
        }
        public ConnectionSettings GetConnectionSettings(out string errMsg)
        {
            errMsg = "";
            if (this.connectionSettings == null)
            {
                this.connectionSettings = dataHandler.GetConnectionSettings(out errMsg);
            }
            return this.connectionSettings;
        }

        public bool SaveConnectionSettings(string _sqlServerName, string _sqlServerDBName, string _sqlServerUserName, string _sqlServerUserPass, string _logoUserName, string _logoUserPass, string _logoFirmNr, string _logoPeriodNr, string _logoAccountCode, out string errMsg)
        {
            if (connectionSettings == null)
                connectionSettings = new ConnectionSettings();

            connectionSettings.SQLServerName = _sqlServerName;
            connectionSettings.SQLServerDBName = _sqlServerDBName;
            connectionSettings.SQLServerUserName = _sqlServerUserName;
            connectionSettings.SQLServerUserPass = _sqlServerUserPass;
            connectionSettings.LogoUserName = _logoUserName;
            connectionSettings.LogoUserPass = _logoUserPass;
            connectionSettings.LogoFirmNr = _logoFirmNr;
            connectionSettings.LogoPeriodNr = _logoPeriodNr;
            connectionSettings.LogoAccountCode = _logoAccountCode;

            return dataHandler.SaveConnectionSettings(connectionSettings, out errMsg);
        }

        public List<Mapping> GetUnitSetMapList(out string errMsg)
        {
            errMsg = "";
            List<Mapping> MapList = dataHandler.GetMappingList(out errMsg, 1);
            return MapList;
        }
        public List<Mapping> GetProductMapList(out string errMsg)
        {
            errMsg = "";
            return dataHandler.GetMappingList(out errMsg, 2);
        }

        public List<Mapping> GetCustomerMapList(out string errMsg)
        {
            errMsg = "";
            return dataHandler.GetMappingList(out errMsg, 3);
        }

        public bool DeleteLogoMap(int ID, out string errMsg)
        {
            errMsg = "";
            return dataHandler.DeleteLogoMap(ID, out errMsg);
        }

        public bool SaveUnitSetMapItem(string _v3Code, string _logoCode, out string errMsg)
        {
            bool returnValue = true;
            errMsg = "";
            List<Mapping> unitSetList = dataHandler.GetMappingList(out errMsg, 1);
            if (errMsg == "")
            {
                if (unitSetList.Exists(x => x.ModuleNr == 1 && x.V3Code == _v3Code))
                {
                    errMsg = _v3Code + " koduna ait kayıt bulunmaktadır.";
                }
                else
                {
                    dataHandler.InsertMapItem(1, _v3Code, _logoCode, out errMsg);
                }
            }
            else
                returnValue = false;
            return returnValue;
        }
        public bool SaveProductMapItem(string _v3Code, string _logoCode, out string errMsg)
        {
            bool returnValue = true;
            errMsg = "";
            List<Mapping>  productList = dataHandler.GetMappingList(out errMsg, 2);
            if (errMsg == "")
            {
                if (productList.Exists(x => x.ModuleNr == 2 && x.V3Code == _v3Code))
                {
                    errMsg = _v3Code + " koduna ait kayıt bulunmaktadır.";
                }
                else
                {
                    dataHandler.InsertMapItem(2, _v3Code, _logoCode, out errMsg);
                }
            }
            else returnValue = false;
            
            return returnValue;
        }
        public bool SaveCustomerMapItem(string _v3Code, string _logoCode, out string errMsg)
        {
            bool returnValue = true;
            errMsg = "";
            List<Mapping> customerList = dataHandler.GetMappingList(out errMsg, 3);
            if (errMsg == "")
            {
                if (customerList.Exists(x => x.ModuleNr == 3 && x.V3Code == _v3Code))
                {
                    errMsg = _v3Code + " koduna ait kayıt bulunmaktadır.";
                }
                else
                {
                    dataHandler.InsertMapItem(3, _v3Code, _logoCode, out errMsg);
                }
            }
            else
                returnValue = false;
            return returnValue;
        }
    }
}
