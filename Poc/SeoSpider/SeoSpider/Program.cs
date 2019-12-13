using System;
using System.Windows.Forms;
using SeoSpider.AgiltyTest;
using SeoSpider.Test2;

namespace SeoSpider
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new SpiderConfigForm());
			Application.Run(new Form2());
			//Application.Run(new AgilityForm());
		}
	}
}
