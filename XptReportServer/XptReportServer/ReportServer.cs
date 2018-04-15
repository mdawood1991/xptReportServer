using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace XptReportServer
{
    public class ReportServer
    {
        private readonly RestClient _client;

        public ReportServer(string reportServerUrl)
        {
            _client = new RestClient(reportServerUrl);
        }

        public List<string> GenerateReports(ReportRequest reportRequest)
        {
            var fileNames = new List<string>();

            return fileNames;
        }


        public List<ReportFormat> GetAvailableFormats()
        {
            var request = new RestRequest("/reports/formats", Method.GET);

            var response = _client.Execute(request);

            return JsonConvert.DeserializeObject<List<ReportFormat>>(response.Content);
        }
    }
}
