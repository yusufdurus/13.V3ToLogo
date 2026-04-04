using System;
using System.Windows.Forms;
using V3ToLogo.BAL;

namespace V3ToLogo.UIL
{
    public partial class formMain : Form
    {
        private bool _timerenabled = false;
        private LoginService loginService = null;
        private ProductService productService = null;
        private CustomerService customerService = null;
        private InvoiceService invoiceService = null;
        private BankService bankService = null;
        public formMain()
        {
            InitializeComponent();
            loginService = LoginService.GetInstance();
            productService = ProductService.GetInstance();  
            customerService = CustomerService.GetInstance();    
            invoiceService = InvoiceService.GetInstance();
            bankService = BankService.GetInstance();
        }
        private void formMain_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'iVMEDBDataSet.sp_IvmeProductList' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            //this.sp_IvmeProductListTableAdapter.Fill(this.iVMEDBDataSet.sp_IvmeProductList);
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

        private string GetStartDate()
        {
            if (checkBox1.Checked)
                return GeneralBussines.FormatDate(dtStart.Value);
            else return "";
        }
        private string GetEndDate()
        {
            if (checkBox2.Checked)
                return GeneralBussines.FormatDate(dtEnd.Value);
            else return "";
        }

        private void timerCount_Tick(object sender, EventArgs e)
        {
            switch (GeneralBussines.activeTransfer)
            {
                case ActiveTransferType.Cari:
                    {
                        label_cari_error.Text = CustomerService.GetInstance().TransferCount_Error.ToString();
                        label_cari_success.Text = CustomerService.GetInstance().RecordCount.ToString() + " / "
                            + CustomerService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                case ActiveTransferType.Malzeme:
                    {
                        label_malzeme_error.Text = ProductService.GetInstance().TransferCount_Error.ToString();
                        label_malzeme_success.Text = ProductService.GetInstance().RecordCount.ToString() + " / "
                            + ProductService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                case ActiveTransferType.Gonderilenhavale:
                    {
                        label_gonderilenhavale_error.Text = BankService.GetInstance().TransferCount_Error.ToString();
                        label_gonderilenhavale_success.Text = BankService.GetInstance().RecordCount.ToString() + " / "
                            + BankService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                case ActiveTransferType.Gelenhavale:
                    {
                        label_gelenhavale_error.Text = BankService.GetInstance().TransferCount_Error.ToString();
                        label_gelenhavale_success.Text = BankService.GetInstance().RecordCount.ToString() + " / "
                            + BankService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                case ActiveTransferType.FaturaWS:
                    {
                        label_faturaWS_error.Text = InvoiceService.GetInstance().TransferCount_Error.ToString();
                        label_faturaWS_success.Text = InvoiceService.GetInstance().RecordCount.ToString() + " / "
                            + InvoiceService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                case ActiveTransferType.FaturaBP:
                    {
                        label_faturaBP_error.Text = InvoiceService.GetInstance().TransferCount_Error.ToString();
                        label_faturaBP_success.Text = InvoiceService.GetInstance().RecordCount.ToString() + " / "
                            + InvoiceService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                case ActiveTransferType.FaturaR:
                    {
                        label_faturaR_error.Text = InvoiceService.GetInstance().TransferCount_Error.ToString();
                        label_faturaR_success.Text = InvoiceService.GetInstance().RecordCount.ToString() + " / "
                            + InvoiceService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                case ActiveTransferType.Kredikarti:
                    {
                        label_kredikarti_error.Text = CustomerService.GetInstance().TransferCount_Error.ToString();
                        label_kredikarti_success.Text = CustomerService.GetInstance().RecordCount.ToString() + " / "
                            + CustomerService.GetInstance().TransferCount_Success.ToString();
                        break;
                    }
                // Diğer durumları burada ekleyebilirsiniz.
                default:
                    break;
            }

            Application.DoEvents();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string formattedStartDate = GetStartDate();
            string formattedEndDate = GetEndDate();

            string errMsg = GeneralBussines.SUCCESS_MSG; // ok ise hata yoktur.
            BAL.GeneralBussines.Initialize();
            try
            {
                var loginResult = await loginService.DoLoginAsync();
                if (loginResult.ReturnCode == 100)
                {
                    if (cbMalzeme.Checked) 
                        errMsg = await productService.ProductTransfer();
                    
                    if (cbCari.Checked)
                        errMsg = await customerService.CustomerTransfer();

                    if (cbFaturaBP.Checked)
                        errMsg = await invoiceService.InvoiceTransfer("BP", formattedStartDate, formattedEndDate, eFilterFaturaBP.Text);

                    if (cbFaturaWS.Checked)
                        errMsg = await invoiceService.InvoiceTransfer("WS", formattedStartDate, formattedEndDate, eFilterFaturaWS.Text);

                    if (cbFaturaR.Checked)
                        errMsg = await invoiceService.InvoiceTransfer("R", formattedStartDate, formattedEndDate, eFilterFaturaR.Text);
                   
                    if (cbKrediKartiFisi.Checked)
                        errMsg = await customerService.CHFKrediKartTransfer(formattedStartDate, formattedEndDate, eFilterKrediKartiFisi.Text);

                    if (cbGelenHavale.Checked)
                        errMsg = await bankService.BankFicheTransfer("gelenHavale", formattedStartDate, formattedEndDate, eFilterBankaGelenHavale.Text);

                    if (cbGonderilenHavale.Checked)
                        errMsg = await bankService.BankFicheTransfer("gonderilenHavale", formattedStartDate, formattedEndDate, eFilterBankaGonderilenHavale.Text);

                    MessageBox.Show("Tamamlandı!");
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            dtEnd.Enabled = checkBox2.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dtStart.Enabled = checkBox1.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BAL.GeneralBussines.Initialize();
            if (eFilterFaturaWS.Text != "")
            {
                Application.EnableVisualStyles();
                DialogResult result = MessageBox.Show("Kayıt aktarım logu silinecek. Emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int affected = InvoiceService.InvoiceNotTransferred("WS", eFilterFaturaWS.Text);
                    if (affected > 0) MessageBox.Show("Aktarım Logu Silindi");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BAL.GeneralBussines.Initialize();
            if (eFilterCariHesap.Text != "")
            {
                Application.EnableVisualStyles();
                DialogResult result = MessageBox.Show("Kayıt aktarım logu silinecek. Emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int affected = CustomerService.CustomerNotTransferred(eFilterCariHesap.Text);
                    if (affected > 0) MessageBox.Show("Aktarım Logu Silindi");
                }
            }
        }
    }
}
