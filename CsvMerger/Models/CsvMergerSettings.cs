using Spider.Models;

namespace CsvMerger.Models
{
	public class CsvMergerSettings : ISettings
	{
		public string SettingsType => this.ToString();
		
		public string CsvFilePath1 { get; set; }
		public bool FirstRowContainsTitle1 { get; set; }
		public string CsvFileSeperator1 { get; set; }

		public string CsvFilePath2 { get; set; }
		public bool FirstRowContainsTitle2 { get; set; }
		public string CsvFileSeperator2 { get; set; }

		public string IgnorePatterns { get; set; }
		public string NewSiteDomain { get; set; }
		public string OutputDirectory { get; set; }
	}
}
