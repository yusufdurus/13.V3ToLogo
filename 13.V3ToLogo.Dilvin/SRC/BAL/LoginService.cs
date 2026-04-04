using V3ToLogo.MODEL.ACCESSDB;
using V3ToLogo.MODEL.GENERAL;
using V3ToLogo.MODEL.Response;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace V3ToLogo.BAL
{
    public class LoginService
    {
        private static LoginService instance= null;
        private const string accessKey = "1903CFA015FB4B84B569BJK1E40686688AF1FE115DFS4FAE8M5E4A0E18A3E27C";
        public static ConnectionSettings SettingsObject;
        public Session lastSession { get; set; }
        private LoginService() 
        {
            lastSession = null;
        }
        public static LoginService GetInstance()
        {
            if (instance == null)
                instance = new LoginService();
            return instance;
        }
        public bool HasSession() 
        { 
            return lastSession != null; 
        }    
        private string GetApiBaseUrl()
        {
            return SettingManager.GetInstance().ApiBaseUrl;
        }
        public async Task<LoginServiceResult> DoLoginAsync()
        {
            string err = "";
            ConnectionSettings connectionSettings = SettingManager.GetInstance().GetConnectionSettings(out err);
            string username = connectionSettings.LogoUserName;
            string password = connectionSettings.LogoUserPass;
            int firmNr = Convert.ToInt32(connectionSettings.LogoFirmNr);
            int periodNr = Convert.ToInt32(connectionSettings.LogoPeriodNr);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Query string oluşturma
                    var queryString = HttpUtility.ParseQueryString(string.Empty);
                    queryString["accessKey"] = accessKey;
                    queryString["username"] = username;
                    queryString["password"] = password;
                    queryString["firmNr"] = firmNr.ToString();
                    queryString["periodNr"] = periodNr.ToString();

                    // Base URL ve query string birleştirme
                    string apiUrl = GetApiBaseUrl() + "/api/Login/DoLogin?" + queryString.ToString();

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        LoginServiceResult result = JsonConvert.DeserializeObject<LoginServiceResult>(responseData);
                        if (lastSession== null) 
                            lastSession = new Session();
                        
                        lastSession.SessionId = result.SessionId;   
                        lastSession.CreatedDate = DateTime.Now;

                        return result;
                    }
                    else
                    {
                        throw new Exception("GET request failed: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda bir hata döndürme
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

    }
}
