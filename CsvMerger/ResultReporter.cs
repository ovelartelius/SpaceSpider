using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvMerger.Models;

namespace CsvMerger
{
    public class ResultReporter
    {
        private List<string> _resultLog = new List<string>();

        public List<string> CreateResult(WorkLoad workLoad)
        {
            var dateTimeString = DateTime.Now.ToString("yyyy-MM-ddTHHmm");

            workLoad.OutputDirectory = workLoad.OutputDirectory + "\\" + dateTimeString;
            if (!Directory.Exists(workLoad.OutputDirectory))
            {
                Directory.CreateDirectory(workLoad.OutputDirectory);
            }

            var sb = new StringBuilder();

            foreach (var item in workLoad.Urls)
            {
                sb.AppendLine(item);
            }

            SaveToExcel(dateTimeString, "merge", sb, workLoad.OutputDirectory);

            return _resultLog;
        }

        private void SaveToExcel(string dateTimeString, string extraFileName, StringBuilder sb, string outputDirectory)
        {
            string filePath = $"{outputDirectory}\\Result_{dateTimeString}_{extraFileName}.csv";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Log($"Deleted old file {filePath}");
            }

            //this code section write stringbuilder content to physical text file.
            using (var writer = File.CreateText(filePath))
            {
                writer.Write(sb.ToString());
            }
            Log($"Created result file {filePath}");
        }

        private void Log(string message)
        {
            _resultLog.Add(message);
        }
    }
}
