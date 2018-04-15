using System;
using System.Linq;

namespace TestReportServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting report server............");

                var reportServer = new XptReportServer.ReportServer("https://reports.xpert-tech.com/api/", "C:/ReportsDownload/");

                var formats = reportServer.GetAvailableFormats();

                Console.WriteLine($"Available formats - {string.Join(", ", formats.Select(s => s.name).ToList())}");

                var testModel = new XptReportServer.ReportRequest("AssetDetailedActivityReport.trdx", 1, "en");


                testModel.ParameterValues.SetDefaultReportParameters(1, "Test Company", 5, DateTime.UtcNow.Date, DateTime.UtcNow, "", "");

                testModel.ParameterValues.SetAssetReportParameters(new int[] { 1, 2, 3});

                var fileNames = reportServer.GenerateReport(testModel, new string[] { "pdf" });

                foreach (var file in fileNames)
                {
                    Console.WriteLine($"Report download - {file}");
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            Console.WriteLine("Execution completed..........");

            Console.ReadKey();
        }
    }
}
