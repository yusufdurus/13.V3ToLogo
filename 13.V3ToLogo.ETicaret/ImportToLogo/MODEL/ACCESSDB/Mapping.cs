using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportToLogo.MODEL.ACCESSDB
{
    public class Mapping
    {
        [System.ComponentModel.DisplayName("No"), System.ComponentModel.ReadOnly(true)]
        public int ID { get; set; }
        [System.ComponentModel.Browsable(false), System.ComponentModel.ReadOnly(true)]
        public int ModuleNr { get; set; }
        [System.ComponentModel.DisplayName("V3 Kodu"), System.ComponentModel.ReadOnly(true)]
        public string V3Code { get; set; }
        [System.ComponentModel.DisplayName("Logo Kodu"), System.ComponentModel.ReadOnly(true)]
        public string LogoCode { get; set; }
    }
}
