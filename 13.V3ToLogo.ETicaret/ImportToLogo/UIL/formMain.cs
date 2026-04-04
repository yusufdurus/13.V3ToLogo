using ImportToLogo.BAL;
using System;
using System.Windows.Forms;

namespace ImportToLogo.UIL
{
    public partial class formMain : Form
    {
        private bool _timerenabled = false;
        private LoginService loginService = null;
        private ProductService productService = null;
        private CustomerService customerService = null;
        private InvoiceService invoiceService = null;
        public formMain()
        {
            InitializeComponent();
            loginService = LoginService.GetInstance();
            productService = ProductService.GetInstance();  
            customerService = CustomerService.GetInstance();    
            invoiceService = InvoiceService.GetInstance();
        }
        private void formMain_Load(object sender, EventArgs e)
        {
           buttonStop.Enabled = false;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartService();
        }
        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopService();
        }
        private void buttonTimerList_Click(object sender, EventArgs e)
        {
            formTimerList frm = new formTimerList();
            frm.ShowDialog();
            frm.Dispose();
        }
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            formSettings frm = new formSettings();
            frm.ShowDialog();
            frm.Dispose();
        }
        private void StartService()
        {
            string errMsg = string.Empty;
            BAL.TimerBussiness.PrepareTimeList(out errMsg);
            BAL.GeneralBussines.Initialize();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            timerMain.Enabled = true;
            _timerenabled = true;
        }
        private void StopService()
        {
            timerMain.Enabled = false;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            _timerenabled = false;
        }
        private async void timerMain_TickAsync(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            timerMain.Enabled = false;
            GeneralBussines.ExecTransfer();
            timerMain.Enabled = _timerenabled;
            Application.DoEvents();
        }
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void timerCount_Tick(object sender, EventArgs e)
        {
            label_cari_error.Text = CustomerService.GetInstance().TransferCount_Error.ToString();
            label_cari_success.Text = CustomerService.GetInstance().TransferCount_Success.ToString();
            label_fatura_error.Text = InvoiceService.GetInstance().TransferCount_Error.ToString();
            label_fatura_success.Text = InvoiceService.GetInstance().RecordCount.ToString() + " / "
                +InvoiceService.GetInstance().TransferCount_Success.ToString();
            label_malzeme_error.Text = ProductService.GetInstance().TransferCount_Error.ToString();
            label_malzeme_success.Text = ProductService.GetInstance().TransferCount_Success.ToString();
            Application.DoEvents();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string errMsg = GeneralBussines.SUCCESS_MSG; // ok ise hata yoktur.
            BAL.GeneralBussines.Initialize();
            try
            {
                var loginResult = await loginService.DoLoginAsync();
                if (loginResult.ReturnCode == 100)
                {
                    //errMsg = await productService.ProductTransfer();
                    //if (errMsg == GeneralBussines.SUCCESS_MSG)
                    //{
                        //errMsg = await customerService.CustomerTransfer();
                        //if (errMsg == string.Empty)
                        //{
                            await invoiceService.InvoiceTransfer();
                        //}
                    //}
                }
                else MessageBox.Show("Login olunamadı => " + loginResult.Description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            formSettings frm = new formSettings();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
