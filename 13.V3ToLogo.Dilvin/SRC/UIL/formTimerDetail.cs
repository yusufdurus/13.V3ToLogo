using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace V3ToLogo.UIL
{
    public partial class formTimerDetail : Form
    {
        public int Id;
        public formTimerDetail()
        {
            InitializeComponent();
        }

        private void formTimerDetail_Load(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                string errMsg = string.Empty;
                MODEL.ACCESSDB.Timer maindb = BAL.TimerBussiness.GetTimerById(Id, out errMsg);
                List<MODEL.ACCESSDB.TimerDetail> dtldb = new List<MODEL.ACCESSDB.TimerDetail>();
                dtldb = BAL.TimerBussiness.GetTimerDetailList(Id, out errMsg);
                comboBox1.SelectedIndex = maindb.Type_;
                dateTimePicker1.Value = maindb.StartTime;
                switch (maindb.Type_)
                {
                    case 2:
                        {
                            foreach (var item in dtldb)
                            {
                                CheckBox myCheckBox = this.Controls.Find("checkBoxhf" + item.Value2, true).FirstOrDefault() as CheckBox;
                                myCheckBox.Checked = true;
                            }
                        } break;
                    case 3:
                        {
                            foreach (var item in dtldb.Where(x => x.Value1 == "a"))
                            {
                                CheckBox myCheckBox = this.Controls.Find("checkBoxa" + item.Value2, true).FirstOrDefault() as CheckBox;
                                myCheckBox.Checked = true;
                            }

                            foreach (var item in dtldb.Where(x => x.Value1 == "g"))
                            {
                                CheckBox myCheckBox = this.Controls.Find("checkBoxg" + item.Value2, true).FirstOrDefault() as CheckBox;
                                myCheckBox.Checked = true;
                            }
                        } break;
                    default:
                        break;
                }
            }
            else
            {
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.SelectedIndex = 0;
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int comboValue = Convert.ToInt32(comboBox1.SelectedIndex);
            switch (comboValue)
            {
                case 0:
                case 1: { groupBox1.Visible = false; groupBox2.Visible = false; } break;
                case 2: { groupBox1.Visible = false; groupBox2.Visible = true; } break;
                case 3: { groupBox1.Visible = true; groupBox2.Visible = false; } break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<MODEL.GENERAL.StringArray2D> valueList = new List<MODEL.GENERAL.StringArray2D>();
            int _value = comboBox1.SelectedIndex;
            switch (_value)
            {
                case 2:
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            CheckBox myCheckBox = this.Controls.Find("checkBoxhf" + i.ToString(), true).FirstOrDefault() as CheckBox;
                            if (myCheckBox.Checked)
                            {
                                MODEL.GENERAL.StringArray2D valueItem = new MODEL.GENERAL.StringArray2D();
                                valueItem.value1 = "h";
                                valueItem.value2 = i.ToString();
                                valueList.Add(valueItem);
                            }
                        }
                    } break;
                case 3:
                    {
                        for (int i = 1; i < 13; i++)
                        {
                            CheckBox myCheckBox = this.Controls.Find("checkBoxa" + i.ToString(), true).FirstOrDefault() as CheckBox;
                            if (myCheckBox.Checked)
                            {
                                MODEL.GENERAL.StringArray2D valueItem = new MODEL.GENERAL.StringArray2D();
                                valueItem.value1 = "a";
                                valueItem.value2 = i.ToString();
                                valueList.Add(valueItem);
                            }
                        }

                        for (int i = 1; i < 32; i++)
                        {
                            CheckBox myCheckBox = this.Controls.Find("checkBoxg" + i.ToString(), true).FirstOrDefault() as CheckBox;
                            if (myCheckBox.Checked)
                            {
                                MODEL.GENERAL.StringArray2D valueItem = new MODEL.GENERAL.StringArray2D();
                                valueItem.value1 = "g";
                                valueItem.value2 = i.ToString();
                                valueList.Add(valueItem);
                            }
                        }
                    } break;
                default:
                    break;
            }
            DateTime myDate = Convert.ToDateTime(dateTimePicker1.Value.ToString("dd.MM.yyyy HH:mm:00"));
            string errMsg = string.Empty;
            BAL.TimerBussiness.SaveTimer(Id, _value, myDate, valueList, out errMsg);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
