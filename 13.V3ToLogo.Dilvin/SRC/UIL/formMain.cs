using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V3ToLogo.BAL;
using V3ToLogo.MODEL.DATABASE;
using V3ToLogo.MODEL.GENERAL;

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
        private bool _stokDataLoaded = false;
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
            buttonStop.Enabled = false;
            SetupErrorGrids();
        }
        private void SetupErrorGrids()
        {
            DataGridView[] grids = new DataGridView[]
            {
                dgvCariHesap, dgvFaturaBP, dgvFaturaR, dgvFaturaWS,
                dgvFaturaEP, dgvFaturaEXS,
                dgvGelenHavale, dgvGonderilenHavale, dgvKrediKarti
            };
            foreach (var dgv in grids)
            {
                dgv.AutoGenerateColumns = false;
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Id", DataPropertyName = "Id", Width = 120 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colKod", HeaderText = "Kod", DataPropertyName = "Kod", Width = 150 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAciklama", HeaderText = "Açıklama", DataPropertyName = "Aciklama", Width = 200 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLog", HeaderText = "Aktarım Log", DataPropertyName = "Log", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            }
        }
        private void BindErrorGrid(DataGridView dgv, List<TransferErrorItem> errorList)
        {
            dgv.DataSource = null;
            dgv.DataSource = errorList;
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

        private string GetFormattedDate(CheckBox checkBox, DateTimePicker dateTimePicker)
        {
            if (checkBox.Checked)
                return GeneralBussines.FormatDate(dateTimePicker.Value);

            return "";
        }

        private bool IsActiveTab(TabPage tabPage)
        {
            return tabControl.SelectedTab == tabPage;
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
        private async void ButtonRunClick(object sender, EventArgs e)
        {
            string errMsg = GeneralBussines.SUCCESS_MSG; // ok ise hata yoktur.
            BAL.GeneralBussines.Initialize();
            try
            {
                var loginResult = await loginService.DoLoginAsync();
                if (loginResult.ReturnCode == 100)
                {
                    if (IsActiveTab(tabPageStok))
                    {
                        productService.ErrorList.Clear();
                        errMsg = await productService.ProductTransfer();
                    }

                    if (IsActiveTab(tabPageCariHesap))
                    {
                        customerService.ErrorList.Clear();
                        errMsg = await customerService.CustomerTransfer();
                        BindErrorGrid(dgvCariHesap, customerService.ErrorList);
                    }

                    if (IsActiveTab(tabPageFaturaBP))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox4, dt1_1);
                        string formattedEndDate = GetFormattedDate(checkBox3, dt1_2);
                        invoiceService.ErrorList.Clear();
                        errMsg = await invoiceService.InvoiceTransfer("BP", formattedStartDate, formattedEndDate, eFilterFaturaBP.Text);
                        BindErrorGrid(dgvFaturaBP, invoiceService.ErrorList);
                    }

                    if (IsActiveTab(tabPageFaturaR))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox6, dt2_1);
                        string formattedEndDate = GetFormattedDate(checkBox5, dt2_2);
                        invoiceService.ErrorList.Clear();
                        errMsg = await invoiceService.InvoiceTransfer("R", formattedStartDate, formattedEndDate, eFilterFaturaR.Text);
                        BindErrorGrid(dgvFaturaR, invoiceService.ErrorList);
                    }

                    if (IsActiveTab(tabPageFaturaWS))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox8, dt3_1);
                        string formattedEndDate = GetFormattedDate(checkBox7, dt3_2);
                        invoiceService.ErrorList.Clear();
                        errMsg = await invoiceService.InvoiceTransfer("WS", formattedStartDate, formattedEndDate, eFilterFaturaWS.Text);
                        BindErrorGrid(dgvFaturaWS, invoiceService.ErrorList);
                    }
                    if (IsActiveTab(tabPageFaturaEP))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox10, dt4_1);
                        string formattedEndDate = GetFormattedDate(checkBox9, dt4_2);
                        invoiceService.ErrorList.Clear();
                        errMsg = await invoiceService.InvoiceTransfer("EP", formattedStartDate, formattedEndDate, eFilterFaturaEP.Text);
                        BindErrorGrid(dgvFaturaEP, invoiceService.ErrorList);
                    }
                    if (IsActiveTab(tabPageFaturaEXS))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox12, dt5_1);
                        string formattedEndDate = GetFormattedDate(checkBox11, dt5_2);
                        invoiceService.ErrorList.Clear();
                        errMsg = await invoiceService.InvoiceTransfer("EXS", formattedStartDate, formattedEndDate, eFilterFaturaEXS.Text);
                        BindErrorGrid(dgvFaturaEXS, invoiceService.ErrorList);
                    }
                    if (IsActiveTab(tabPageGelenHavale))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox14, dt6_1);
                        string formattedEndDate = GetFormattedDate(checkBox13, dt6_2);
                        bankService.ErrorList.Clear();
                        errMsg = await bankService.BankFicheTransfer("gelenHavale", formattedStartDate, formattedEndDate, eFilterBankaGelenHavale.Text);
                        BindErrorGrid(dgvGelenHavale, bankService.ErrorList);
                    }

                    if (IsActiveTab(tabPageGonderilenHavale))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox16, dt7_1);
                        string formattedEndDate = GetFormattedDate(checkBox15, dt7_2);
                        bankService.ErrorList.Clear();
                        errMsg = await bankService.BankFicheTransfer("gonderilenHavale", formattedStartDate, formattedEndDate, eFilterBankaGonderilenHavale.Text);
                        BindErrorGrid(dgvGonderilenHavale, bankService.ErrorList);
                    }

                    if (IsActiveTab(tabPageKrediKarti))
                    {
                        string formattedStartDate = GetFormattedDate(checkBox18, dt8_1);
                        string formattedEndDate = GetFormattedDate(checkBox17, dt8_2);
                        customerService.ErrorList.Clear();
                        errMsg = await customerService.CHFKrediKartTransfer(formattedStartDate, formattedEndDate, eFilterKrediKartiFisi.Text);
                        BindErrorGrid(dgvKrediKarti, customerService.ErrorList);
                    }

                    MessageBox.Show("Tamamlandı!");
                }
                else MessageBox.Show("Login olunamadı => " + loginResult.Description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            if (eFilterFaturaWSlog.Text != "")
            {
                Application.EnableVisualStyles();
                DialogResult result = MessageBox.Show("Kayıt aktarım logu silinecek. Emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int affected = InvoiceService.InvoiceNotTransferred("WS", eFilterFaturaWSlog.Text);
                    if (affected > 0) MessageBox.Show("Aktarım Logu Silindi");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BAL.GeneralBussines.Initialize();
            if (eFilterCariHesaplog.Text != "")
            {
                Application.EnableVisualStyles();
                DialogResult result = MessageBox.Show("Kayıt aktarım logu silinecek. Emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int affected = CustomerService.CustomerNotTransferred(eFilterCariHesaplog.Text);
                    if (affected > 0) MessageBox.Show("Aktarım Logu Silindi");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }
    }
}
