using System;
using System.Windows.Forms;

namespace SeoSpider
{
	public partial class SeoReport : Form
	{
		private SpiderReport _report = null;
		public SpiderReport Report {
			get { return _report; }
			set
			{
				_report = value;
				PaintReport();
			} 
		}

		public SeoReport()
		{
			InitializeComponent();
		}

		private void SeoReport_Load(object sender, EventArgs e)
		{
			PaintReport();
		}

		private void PaintReport()
		{
			if (Report != null)
			{
				labNumberOfPages.Text = Report.NumberOfPages.ToString();
				txtReportText.Text = Report.ReportText;
			}
		}
	}
}
