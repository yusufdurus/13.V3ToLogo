using System;
using System.Collections.Generic;
using System.Text;

namespace ImportToLogo.MODEL.ACCESSDB
{
    public class VV_Timer
    {
        [System.ComponentModel.DisplayName("No"), System.ComponentModel.ReadOnly(true)]
        public int ID { get; set; }
        [System.ComponentModel.DisplayName("Tip"), System.ComponentModel.ReadOnly(true)]
        public string TypeText { get; set; }
        [System.ComponentModel.Browsable(false)]
        public int Type_ { get; set; }

        [System.ComponentModel.DisplayName("Başlangıç Tarihi"), System.ComponentModel.ReadOnly(true)]
        public DateTime StartTime { get; set; }
    }
}
