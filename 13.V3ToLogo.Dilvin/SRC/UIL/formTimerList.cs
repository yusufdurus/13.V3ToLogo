using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V3ToLogo.UIL
{
    public partial class formTimerList : Form
    {
        public formTimerList()
        {
            InitializeComponent();
        }

        private void PopulateData()
        {

            string errMsg = string.Empty;

            List<MODEL.ACCESSDB.VV_Timer> vvTimeList = new List<MODEL.ACCESSDB.VV_Timer>();
            vvTimeList = BAL.TimerBussiness.GetTimeViewList(out errMsg);
            if (errMsg == string.Empty)
            {
                dataGridView1.DataSource = vvTimeList;
                dataGridView1.Enabled = true;
                btnEdit.Enabled = vvTimeList.Count > 0;
                btnDelete.Enabled = vvTimeList.Count > 0;
            }
            else
            {
                MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }


        private void formTimerList_Load(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void ShowCheckDetailForm(int Id)
        {
            formTimerDetail frm = new formTimerDetail();
            frm.Id = Id;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // System.Threading.Thread.Sleep(500);
                string errMsg = string.Empty;
                BAL.TimerBussiness.PrepareTimeList(out errMsg);

            }
            frm.Dispose();
            PopulateData();

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowCheckDetailForm(0);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            if (Id > 0)
            {
                ShowCheckDetailForm(Id);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            if (Id > 0)
            {
                if (MessageBox.Show("Kayıt Silinecektir.", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string errMsg = string.Empty;
                    BAL.TimerBussiness.DeleteTimer(Id, out errMsg);
                    //      System.Threading.Thread.Sleep(500);
                    if (errMsg != string.Empty)
                        MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BAL.TimerBussiness.PrepareTimeList(out errMsg);
                    if (errMsg != string.Empty)
                        MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PopulateData();
                }

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

    }
}
