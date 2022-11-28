using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintReceiptDemo
{
    public partial class frmImpr : Form
    {
        List<Receipt> _list;
        string _total, _cash, _change, _date, _client, _tva;

        public frmImpr(List<Receipt> datasource, string total, string cash, string change, string date, string client, string tva)
        {
            InitializeComponent();
            _list = datasource;
            _total = total;
            _cash = cash;
            _change = change;
            _date = date;
            _client = client;
            _tva = tva;
        }

        private void frmImpr_Load(object sender, EventArgs e)
        {
            ReceiptBindingSource.DataSource = _list;
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pTotal", _total),
                new Microsoft.Reporting.WinForms.ReportParameter("pCash", _cash),
                new Microsoft.Reporting.WinForms.ReportParameter("pChange", _change),
                new Microsoft.Reporting.WinForms.ReportParameter("pDate", _date),
                new Microsoft.Reporting.WinForms.ReportParameter("pClient", _client),
                new Microsoft.Reporting.WinForms.ReportParameter("pTva", _tva)
            };
            this.reportViewer.LocalReport.SetParameters(para);
            this.reportViewer.RefreshReport();
        }
    }
}
