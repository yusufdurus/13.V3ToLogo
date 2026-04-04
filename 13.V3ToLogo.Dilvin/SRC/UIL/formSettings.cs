using V3ToLogo.BAL;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace V3ToLogo.UIL
{
    public partial class formSettings : Form
    {
        public formSettings()
        {
            InitializeComponent();
        }

        private void formSettings_Load(object sender, EventArgs e)
        {
            PopulateCustomerData();
            PopulateProductData();
            PopulateUnitSetData();

            V3ToLogo.MODEL.ACCESSDB.ConnectionSettings mySettings = new V3ToLogo.MODEL.ACCESSDB.ConnectionSettings();
            string errMsg = string.Empty;
            mySettings = SettingManager.GetInstance().GetConnectionSettings(out errMsg);
            if (errMsg == string.Empty)
            {
                txtSQLServerName.Text = mySettings.SQLServerName;
                txtSQLServerDBName.Text = mySettings.SQLServerDBName;
                txtSQLServerUserName.Text = mySettings.SQLServerUserName;
                txtSQLServerUserPass.Text = mySettings.SQLServerUserPass;
                txtLogoUserName.Text = mySettings.LogoUserName;
                txtLogoUserPass.Text = mySettings.LogoUserPass;
                txtLogoFirmNr.Text = mySettings.LogoFirmNr;
                txtLogoPeriodNr.Text = mySettings.LogoPeriodNr;
                txtLogoAccount.Text = mySettings.LogoAccountCode;
            }
            else
            {
                MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void PopulateProductData()
        {
            string errMsg = "";
            dataGridViewProduct.DataSource = SettingManager.GetInstance().GetProductMapList(out errMsg);
            if (errMsg != "")
                MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void PopulateUnitSetData()
        {
            string errMsg = "";
            dataGridViewUnitSet.DataSource = SettingManager.GetInstance().GetUnitSetMapList(out errMsg);
            if (errMsg != "")
                MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        private void PopulateCustomerData()
        {
            string errMsg = "";
            dataGridViewCustomer.DataSource = SettingManager.GetInstance().GetCustomerMapList(out errMsg);
            if (errMsg != "")
                MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            SettingManager.GetInstance().SaveConnectionSettings(txtSQLServerName.Text, txtSQLServerDBName.Text, txtSQLServerUserName.Text, txtSQLServerUserPass.Text, txtLogoUserName.Text, txtLogoUserPass.Text, txtLogoFirmNr.Text, txtLogoPeriodNr.Text, txtLogoAccount.Text, out errMsg);
            if (errMsg != string.Empty)
                MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            BAL.GeneralBussines.Initialize();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void contextMenuStripForUnitSetCode_Opening(object sender, CancelEventArgs e)
        {

        }

        private void birimSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewUnitSet.CurrentRow == null)
                return;
            int Id = Convert.ToInt32(dataGridViewUnitSet.CurrentRow.Cells[0].Value);
            if (Id > 0)
            {
                if (MessageBox.Show("Kayıt Silinecektir.", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string errMsg = string.Empty;
                    SettingManager.GetInstance().DeleteLogoMap(Id, out errMsg);
                    //      System.Threading.Thread.Sleep(500);
                    if (errMsg != string.Empty)
                        MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PopulateUnitSetData();
                }

            }
        }


        private void CustomerDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewCustomer.CurrentRow == null)
                return;

            int Id = Convert.ToInt32(dataGridViewCustomer.CurrentRow.Cells[0].Value);
            if (Id > 0)
            {
                if (MessageBox.Show("Kayıt Silinecektir.", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string errMsg = string.Empty;
                    SettingManager.GetInstance().DeleteLogoMap(Id, out errMsg);
                    //      System.Threading.Thread.Sleep(500);
                    if (errMsg != string.Empty)
                        MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PopulateCustomerData();
                }
            }
        }

        private void productToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridViewProduct.CurrentRow == null)
                return;
            int Id = Convert.ToInt32(dataGridViewProduct.CurrentRow.Cells[0].Value);
            if (Id > 0)
            {
                if (MessageBox.Show("Kayıt Silinecektir.", "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string errMsg = string.Empty;
                    SettingManager.GetInstance().DeleteLogoMap(Id, out errMsg);
                    //      System.Threading.Thread.Sleep(500);
                    if (errMsg != string.Empty)
                        MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PopulateProductData();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUnitSetLogoCode.Text.Trim() != "" && txtUnitSetV3Code.Text.Trim() != "")
            {
                string errMsg = "";
                SettingManager.GetInstance().SaveUnitSetMapItem(txtUnitSetV3Code.Text, txtUnitSetLogoCode.Text, out errMsg);
                if (errMsg != "")
                {
                    MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtUnitSetV3Code.Text = "";
                    txtUnitSetLogoCode.Text = "";
                    PopulateUnitSetData();
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtProductLogoCode.Text.Trim() != "" && txtProductV3Code.Text.Trim() != "")
            {
                string errMsg = "";
                SettingManager.GetInstance().SaveProductMapItem(txtProductV3Code.Text, txtProductLogoCode.Text, out errMsg);
                if (errMsg != "")
                {
                    MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtProductV3Code.Text = "";
                    txtProductLogoCode.Text = "";
                    PopulateProductData();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtCustomerLogoCode.Text.Trim() != "" && txtCustomerV3Code.Text.Trim() != "")
            {
                string errMsg = "";
                SettingManager.GetInstance().SaveCustomerMapItem(txtCustomerV3Code.Text, txtCustomerLogoCode.Text, out errMsg);
                if (errMsg != "")
                {
                    MessageBox.Show(errMsg, "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtCustomerV3Code.Text = "";
                    txtCustomerLogoCode.Text = "";
                    PopulateCustomerData();
                }
                
            }
        }

        private void dataGridViewUnitSet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
