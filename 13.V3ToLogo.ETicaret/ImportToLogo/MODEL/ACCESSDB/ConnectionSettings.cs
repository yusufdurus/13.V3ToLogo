using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportToLogo.MODEL.ACCESSDB
{
    public class ConnectionSettings
    {
        public string SQLServerName { get; set; }
        public string SQLServerDBName { get; set; }
        public string SQLServerUserName { get; set; }
        public string SQLServerUserPass { get; set; }
        public string LogoUserName { get; set; }
        public string LogoUserPass { get; set; }
        public string LogoFirmNr { get; set; }
        public string LogoPeriodNr { get; set; }
        public string LogoAccountCode { get; set; }
    }
}
