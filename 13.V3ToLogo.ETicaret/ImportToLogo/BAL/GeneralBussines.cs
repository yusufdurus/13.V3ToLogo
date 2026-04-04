using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using ImportToLogo.MODEL.ACCESSDB;
using ImportToLogo.MODEL.GENERAL;

namespace ImportToLogo.BAL
{
    public static class GeneralBussines
    {
        public static string SUCCESS_MSG = "ok";
        public static List<TimeList> OperationTimeList;
        public static async void ExecTransfer()
        {
            string errMsg = SUCCESS_MSG;
            try
            {
                DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                List<MODEL.GENERAL.TimeList> TimeList = new List<MODEL.GENERAL.TimeList>();
                TimeList = OperationTimeList.Where(x => x.CheckTime == currentDate && x.Used == false).ToList();

                foreach (var opItem in OperationTimeList.FindAll(x => x.CheckTime == currentDate && x.Used == false))
                {
                    opItem.Used = true;
                }

                foreach (var item in TimeList)
                {
                    item.Used = true;
                }

                if (TimeList.Count > 0)
                {
                    var loginServiceResult = await LoginService.GetInstance().DoLoginAsync();
                    if (loginServiceResult.ReturnCode==100)
                    {
                        errMsg = await ProductService.GetInstance().ProductTransfer();
                        if (errMsg == SUCCESS_MSG)
                        {
                            errMsg = await CustomerService.GetInstance().CustomerTransfer();
                            if (errMsg == string.Empty)
                            {
                                errMsg = await InvoiceService.GetInstance().InvoiceTransfer();
                            }
                        }
                    }
                    else
                    {
                        errMsg = loginServiceResult.Description;
                    }
                }

                if (!OperationTimeList.Exists(x => (x.CheckTime > currentDate && x.Used == false) || (x.CheckTime == currentDate && x.Used == true)))
                {
                    TimerBussiness.PrepareTimeList(out errMsg);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            
            //return errMsg;
        }
        public static void Initialize()
        {
            string err = string.Empty;
            SettingManager.GetInstance().Reset();
            ConnectionSettings connectionSettings = SettingManager.GetInstance().GetConnectionSettings(out err);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["LogoEntities"].ConnectionString = "metadata=res://*/MODEL.LOGODB.LogoModel.csdl|res://*/MODEL.LOGODB.LogoModel.ssdl|res://*/MODEL.LOGODB.LogoModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source=" + connectionSettings.SQLServerName + ";initial catalog=" + connectionSettings.SQLServerDBName + ";user id=" + connectionSettings.SQLServerUserName + ";password=" + connectionSettings.SQLServerUserPass + ";multipleactiveresultsets=True;application name=EntityFramework\"";
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
