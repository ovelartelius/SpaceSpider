using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Util;
using SeoSpider.Test2.models;
using SeoSpider.Test2.models.data;

namespace SeoSpider.Test2
{
	public partial class SpiderConfigForm : Form
	{
		private readonly ISpiderDataProvider _data;

		public SpiderConfigForm()
		{
			InitializeComponent();

			_data = new SpiderDataProvider();
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			Console.WriteLine("buttonStart_Clicked");

			var spiderConfigSettingsJson = CreateConfigSettings();

			//TODO: Need to validate the startUrl.

			// Create the spiderrun
			var spiderRun = new SpiderRun();
			spiderRun.CreatedAt = DateTime.Now;
			spiderRun.StartUrl = textBoxStartUrl.Text;
			spiderRun.SettingsJson = spiderConfigSettingsJson;

			// Save it to the database
			using (var db = new EtDataContext())
			{
				_data.SaveSpiderRun(db, spiderRun);
				//db.SpiderRuns.Add(spiderRun);
				//db.SaveChanges();
			}

			StartSpiderRun(spiderRun);

			using (var db = new EtDataContext())
			{
				// Create the first page
				var spiderPage = new SeoSpider.Test2.models.data.SpiderPage
				{
					Url = spiderRun.StartUrl,
					SpiderRunId = spiderRun.SpiderRunId,
					SpiderRunKey = spiderRun.SpiderRunKey,
				};
				_data.SaveSpiderPage(db, spiderPage);

				while (db.SpiderPages.Any(x => x.SpiderRunId == spiderRun.SpiderRunId && (!x.CheckedOut || !x.Handled) ))
				{
					StartSpiderRun(spiderRun);
					//Thread.Sleep(500);
				}

				spiderRun.IsCompleted = true;
				spiderRun.CompletedAt = DateTime.Now;
				_data.SaveSpiderRun(db, spiderRun);
			}



			MessageBox.Show("Done");
		}

		/// <summary>
		/// Create SpiderSettings and serialize to JSON.
		/// </summary>
		/// <returns>JSON</returns>
		private string CreateConfigSettings()
		{
			// Create/Setup the settings
			var spiderConfigSettings = new SpiderSetting();
			spiderConfigSettings.AnchorRegexPattern = "<a[^>]*href=\"([^\"]*)\"[^>]*>";
			spiderConfigSettings.ScriptRegexPattern = "<script.*?src=\"(.*?)\".*?>";
			spiderConfigSettings.LinkRegexPattern = "<link.*?href=\"(.*?)\".*?>";
			spiderConfigSettings.ImageRegexPattern = "<[img|IMG].*?src=\"(.*?)\".*?>";
			spiderConfigSettings.HttpRegexPattern = "\"(http.*?)\"|\"(\\/.*?)\"|'(http.*?)'|'(\\/.*?)";
			spiderConfigSettings.MaxSiteDepth = 0;
			spiderConfigSettings.MaxNumberOfPages = 0;
			spiderConfigSettings.ImageSizeLimit = 50000;
			spiderConfigSettings.PageSizeLimit = 100000;
			spiderConfigSettings.PageSpeedHighLimit = 1000;
			spiderConfigSettings.PageSpeedWarningLimit = 500;

			// Serialize the settings
			var spiderConfigSettingsJson = JsonConvert.SerializeObject(spiderConfigSettings);

			return spiderConfigSettingsJson;
		}


		private void StartSpiderRun(SpiderRun spiderRun)
		{
			// Start the spider
			try
			{
				ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

				// get a scheduler
				var scheduler = schedulerFactory.GetScheduler();
				scheduler.Start();

				IJobDetail job = JobBuilder.Create<SpiderRunner>()
					.WithIdentity("RunSpider_{0}".FormatInvariant(spiderRun.SpiderRunKey.ToString()), "Spider jobs")
					.UsingJobData("SpiderRunKey", spiderRun.SpiderRunKey.ToString())
					.Build();

				// Trigger the job to run now.
				var trigger = TriggerBuilder.Create()
					.WithIdentity("RunSpiderTrigger_{0}".FormatInvariant(spiderRun.SpiderRunKey.ToString()), "Spider jobs")
					.StartNow()
					.Build();

				// Tell quartz to schedule the job using our trigger
				scheduler.ScheduleJob(job, trigger);


				Console.WriteLine("SpiderConfigForm finished");
			}
			catch (SchedulerException ex)
			{
				//TODO: Vi måste se till att användare och db får reda på att denna inte kommer att startas. Man skulle kunna ha en funktion som kan start en i efterhand.
				//_logger.LogError(ex);
			}

		}
	}
}
