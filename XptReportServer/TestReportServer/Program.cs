using System;
using System.Linq;

namespace TestReportServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting report server............");

            var reportServer = new XptReportServer.ReportServer("https://reports.xpert-tech.com/api/");


            var formats = reportServer.GetAvailableFormats();

            Console.WriteLine($"Available formats - {string.Join(", ", formats.Select(s => s.name).ToList())}");




            Console.WriteLine("Execution completed..........");

            Console.ReadKey();
        }
    }
}
