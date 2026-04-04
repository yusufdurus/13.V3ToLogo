using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace ImportToLogo.DAL.ACCESSDB
{
    public class DataHandler
    {
        private OleDbConnection GetConnection(out string errMsg)
        {
            errMsg = string.Empty;
            System.IO.DirectoryInfo baseDir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            OleDbConnection AccessConnection = new OleDbConnection();
            AccessConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                @"Data source= " + baseDir.Parent.FullName + "\\DATA\\SettingsDB.mdb";
            try
            {
                AccessConnection.Open();
            }
            catch (Exception ex)
            {
                AccessConnection = null;
                errMsg = ex.Message;
            }
            return AccessConnection;
        }

        public List<MODEL.ACCESSDB.VV_Timer> GetTimeViewList(out string errMsg)
        {
            errMsg = string.Empty;
            List<MODEL.ACCESSDB.VV_Timer> TimeList = new List<MODEL.ACCESSDB.VV_Timer>();
            string cmdStr = "SELECT ID, TYPE_, StartTime, Switch( TYPE_ = 0 , \"Sadece Bir Kez\" ," + Environment.NewLine +
                            "           TYPE_ = 1 , \"Günlük\"," + Environment.NewLine +
                            "           TYPE_ = 2 , \"Haftalık\" ," + Environment.NewLine +
                            "           TYPE_ = 3 , \"Aylık\") AS TYPETEXT" + Environment.NewLine +
                            "FROM Timer;";

            OleDbDataAdapter oleAdapter = new OleDbDataAdapter(cmdStr, GetConnection(out errMsg));
            if (errMsg == string.Empty)
            {
                try
                {
                    DataTable dt = new DataTable();
                    oleAdapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MODEL.ACCESSDB.VV_Timer timeItem = new MODEL.ACCESSDB.VV_Timer();
                        timeItem.ID = Convert.ToInt32(dt.Rows[i][0]);
                        timeItem.Type_ = Convert.ToInt32(dt.Rows[i][1]);
                        timeItem.TypeText = Convert.ToString(dt.Rows[i][3]);
                        timeItem.StartTime = Convert.ToDateTime(dt.Rows[i][2]);
                        TimeList.Add(timeItem);
                        timeItem = null;
                    }
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            oleAdapter.SelectCommand.Connection.Close();
            oleAdapter.Dispose();
            return TimeList;
        }

        public List<MODEL.ACCESSDB.Timer> GetTimeList(out string errMsg)
        {
            errMsg = string.Empty;
            List<MODEL.ACCESSDB.Timer> TimeList = new List<MODEL.ACCESSDB.Timer>();
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter("SELECT * FROM Timer", GetConnection(out errMsg));
            if (errMsg == string.Empty)
            {
                try
                {
                    DataTable dt = new DataTable();
                    oleAdapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MODEL.ACCESSDB.Timer timeItem = new MODEL.ACCESSDB.Timer();
                        timeItem.ID = Convert.ToInt32(dt.Rows[i][0]);
                        timeItem.Type_ = Convert.ToInt32(dt.Rows[i][1]);
                        timeItem.StartTime = Convert.ToDateTime(dt.Rows[i][2]);
                        TimeList.Add(timeItem);
                        timeItem = null;
                    }
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            oleAdapter.SelectCommand.Connection.Close();
            oleAdapter.Dispose();
            return TimeList;
        }

        public MODEL.ACCESSDB.Timer GetTimerById(int _mainId, out string errMsg)
        {
            errMsg = string.Empty;
            MODEL.ACCESSDB.Timer Timer = new MODEL.ACCESSDB.Timer();
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter("SELECT * FROM Timer Where ID = " + _mainId.ToString(), GetConnection(out errMsg));
            if (errMsg == string.Empty)
            {
                try
                {
                    DataTable dt = new DataTable();
                    oleAdapter.Fill(dt);
                    Timer.ID = Convert.ToInt32(dt.Rows[0][0]);
                    Timer.Type_ = Convert.ToInt32(dt.Rows[0][1]);
                    Timer.StartTime = Convert.ToDateTime(dt.Rows[0][2]);

                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            oleAdapter.SelectCommand.Connection.Close();
            oleAdapter.Dispose();
            return Timer;
        }

        public List<MODEL.ACCESSDB.TimerDetail> GetTimerDetailList(int _mainId, out string errMsg)
        {
            errMsg = string.Empty;
            List<MODEL.ACCESSDB.TimerDetail> TimerDetailList = new List<MODEL.ACCESSDB.TimerDetail>();
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter("SELECT * FROM TimerDetail Where TimerID = " + _mainId.ToString(), GetConnection(out errMsg));
            if (errMsg == string.Empty)
            {
                try
                {
                    DataTable dt = new DataTable();
                    oleAdapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MODEL.ACCESSDB.TimerDetail timedtlItem = new MODEL.ACCESSDB.TimerDetail();
                        timedtlItem.ID = Convert.ToInt32(dt.Rows[i][0]);
                        timedtlItem.TimerId = Convert.ToInt32(dt.Rows[i][1]);
                        timedtlItem.Value1 = dt.Rows[i][2].ToString();
                        timedtlItem.Value2 = dt.Rows[i][3].ToString();
                        TimerDetailList.Add(timedtlItem);
                        timedtlItem = null;
                    }
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            oleAdapter.SelectCommand.Connection.Close();
            oleAdapter.Dispose();
            return TimerDetailList;
        }

        public int InsertTimer(MODEL.ACCESSDB.Timer _timerItem, out string errMsg)
        {
            errMsg = string.Empty;
            int returnValue = 0;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = string.Format("INSERT INTO Timer (TYPE_, StartTime) VALUES ({0},CDATE(\"{1}\"))", _timerItem.Type_.ToString(), _timerItem.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbCmd.ExecuteNonQuery();
                    dbCmd.CommandText = string.Format("Select @@Identity");
                    returnValue = (int)dbCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();
            return returnValue;
        }

        public int InsertTimerDetail(MODEL.ACCESSDB.TimerDetail _timerDtlItem, out string errMsg)
        {
            errMsg = string.Empty;
            int returnValue = 0;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = string.Format("INSERT INTO TimerDetail (TimerId, Value1, Value2) VALUES ({0},\"{1}\",\"{2}\");", _timerDtlItem.TimerId.ToString(), _timerDtlItem.Value1, _timerDtlItem.Value2);
                    dbCmd.ExecuteNonQuery();
                    dbCmd.CommandText = string.Format("Select @@Identity");
                    returnValue = (int)dbCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();

            return returnValue;
        }

        public bool UpdateTimer(MODEL.ACCESSDB.Timer _timerItem, out string errMsg)
        {
            errMsg = string.Empty;
            bool returnValue = false;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = string.Format("UPDATE Timer SET Type_ = {1}, StartTime = CDATE(\"{2}\") WHERE ID = {0}", _timerItem.ID.ToString(), _timerItem.Type_.ToString(), _timerItem.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbCmd.ExecuteNonQuery();
                    returnValue = DeleteTimerDetail(_timerItem.ID, out errMsg);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();

            return returnValue;
        }

        public bool DeleteTimer(int _mainId, out string errMsg)
        {
            errMsg = string.Empty;
            bool returnValue = false;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = "DELETE FROM Timer Where ID = " + _mainId.ToString();
                    dbCmd.ExecuteNonQuery();
                    returnValue = DeleteTimerDetail(_mainId, out errMsg);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();

            return returnValue;
        }

        public bool DeleteTimerDetail(int _mainId, out string errMsg)
        {
            errMsg = string.Empty;
            bool returnValue = false;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = "DELETE FROM TimerDetail Where TimerID = " + _mainId.ToString();
                    dbCmd.ExecuteNonQuery();
                    returnValue = true;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();

            return returnValue;
        }

        public MODEL.ACCESSDB.ConnectionSettings GetConnectionSettings(out string errMsg)
        {
            errMsg = string.Empty;
            MODEL.ACCESSDB.ConnectionSettings cSettings = new MODEL.ACCESSDB.ConnectionSettings();
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter("SELECT * FROM ConnectionSettings", GetConnection(out errMsg));
            if (errMsg == string.Empty)
            {
                try
                {
                    DataTable dt = new DataTable();
                    oleAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cSettings.SQLServerName = Convert.ToString(dt.Rows[0][0]);
                        cSettings.SQLServerDBName = Convert.ToString(dt.Rows[0][1]);
                        cSettings.SQLServerUserName = Convert.ToString(dt.Rows[0][2]);
                        cSettings.SQLServerUserPass = Convert.ToString(dt.Rows[0][3]);
                        cSettings.LogoUserName = Convert.ToString(dt.Rows[0][4]);
                        cSettings.LogoUserPass = Convert.ToString(dt.Rows[0][5]);
                        cSettings.LogoFirmNr = Convert.ToString(dt.Rows[0][6]);
                        cSettings.LogoPeriodNr = Convert.ToString(dt.Rows[0][7]);
                        cSettings.LogoAccountCode = Convert.ToString(dt.Rows[0][8]);
                    }
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            oleAdapter.SelectCommand.Connection.Close();
            oleAdapter.Dispose();
            return cSettings;
        }


        public bool SaveConnectionSettings(MODEL.ACCESSDB.ConnectionSettings _settings, out string errMsg)
        {
            errMsg = string.Empty;
            bool returnValue = false;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = string.Format("DELETE FROM ConnectionSettings;");
                    dbCmd.ExecuteNonQuery();
                    dbCmd.CommandText = string.Format("INSERT INTO ConnectionSettings (SQLServerName, SQLServerDBName, SQLServerUserName, SQLServerUserPass, LogoUserName, LogoUserPass, LogoFirmNr, LogoPeriodNr, LogoAccountCode) VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\");", _settings.SQLServerName, _settings.SQLServerDBName, _settings.SQLServerUserName, _settings.SQLServerUserPass, _settings.LogoUserName, _settings.LogoUserPass, _settings.LogoFirmNr, _settings.LogoPeriodNr, _settings.LogoAccountCode);
                    dbCmd.ExecuteNonQuery();
                    returnValue = true;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();

            return returnValue;
        }

        public bool DeleteLogoMap(int _id, out string errMsg)
        {
            errMsg = string.Empty;
            bool returnValue = false;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = string.Format("DELETE FROM Mapping Where ID = "+_id.ToString());
                    dbCmd.ExecuteNonQuery();
                    returnValue = true;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();

            return returnValue;
        }


        public string GetLogoCode(int _moduleNr, string _V3Code, out string errMsg)
        {
            errMsg = string.Empty;
            string returnValue = _V3Code;
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter(string.Format("SELECT LogoCode FROM Mapping Where ModuleNr = {0} and V3Code = '{1}'", _moduleNr.ToString(), _V3Code), GetConnection(out errMsg));
            if (errMsg == string.Empty)
            {
                try
                {
                    DataTable dt = new DataTable();
                    oleAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        returnValue = Convert.ToString(dt.Rows[0][0]);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            oleAdapter.SelectCommand.Connection.Close();
            oleAdapter.Dispose();
            return returnValue;
        }

        public List<MODEL.ACCESSDB.Mapping> GetMappingList(out string errMsg, int _moduleNr)
        {
            List<MODEL.ACCESSDB.Mapping> MappingList = new List<MODEL.ACCESSDB.Mapping>();
            errMsg = string.Empty;
            
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM Mapping where Modulenr = "+_moduleNr.ToString() ), GetConnection(out errMsg));
            if (errMsg == string.Empty)
            {
                try
                {
                    DataTable dt = new DataTable();
                    oleAdapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MODEL.ACCESSDB.Mapping mapItem = new MODEL.ACCESSDB.Mapping();
                        mapItem.ID = Convert.ToInt32(dt.Rows[i][0]);
                        mapItem.ModuleNr = Convert.ToInt32(dt.Rows[i][1]);
                        mapItem.V3Code = dt.Rows[i][2].ToString();
                        mapItem.LogoCode = dt.Rows[i][3].ToString();
                        MappingList.Add(mapItem);
                        mapItem = null;
                    }
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            oleAdapter.SelectCommand.Connection.Close();
            oleAdapter.Dispose();
            return MappingList;
        }

        public int InsertMapItem(int _moduleNr, string _v3Code, string _logoCode, out string errMsg)
        {
            errMsg = string.Empty;
            int returnValue = 0;
            OleDbCommand dbCmd = new OleDbCommand();
            dbCmd.Connection = GetConnection(out errMsg);
            if (errMsg == string.Empty)
            {
                try
                {
                    dbCmd.CommandText = string.Format("INSERT INTO Mapping (ModuleNr, V3Code, LogoCode) VALUES ({0},\"{1}\",\"{2}\");", _moduleNr.ToString(), _v3Code, _logoCode);
                    dbCmd.ExecuteNonQuery();
                    dbCmd.CommandText = string.Format("Select @@Identity");
                    returnValue = (int)dbCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            dbCmd.Connection.Close();
            dbCmd.Dispose();

            return returnValue;
        }
    }
}
