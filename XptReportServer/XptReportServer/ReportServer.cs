using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace XptReportServer
{
    public class ReportServer
    {
        private readonly string _reportOutputDirectory;
        private readonly RestClient _restClient;

        public ReportServer(string reportServerUrl, string reportOutputDirectory)
        {
            _restClient = new RestClient(reportServerUrl);
            _reportOutputDirectory = reportOutputDirectory;
        }

        public List<string> GenerateReport(ReportRequest request, string[] formats)
        {
            var fileNames = new List<string>();

            var clientId = GetClientId();

            var instanceId = GetReportInstanceId(clientId, request);

            //TODO: Generate the report documents async
            foreach (var format in formats)
            {
                var customFileName = $"{request.XptReport.ReportName}_{DateTime.UtcNow.ToString("dd_MM_yy_HH_mm")}.{format}";

                if (format == "image")
                    customFileName = $"{request.XptReport.ReportName}_{DateTime.UtcNow.ToString("dd_MM_yy_HH_mm")}.tiff";

                var documentId = GetReportDocumentId(clientId, instanceId, format);
                var filename = DownloadDocument(customFileName, clientId, instanceId, documentId, format);

                if (!string.IsNullOrEmpty(filename))
                    fileNames.Add(filename);
            }

            return fileNames;
        }

        private string GetClientId()
        {
            var request = new RestRequest("/reports/clients", Method.POST);

            var response = _restClient.Execute(request);

            Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

            return result["clientId"];
        }

        private string GetReportInstanceId(string clientId, ReportRequest requestBody)
        {
            try
            {
                var request = new RestRequest($"/reports/clients/{clientId}/instances", Method.POST);

                request.AddJsonBody(requestBody);

                var response = _restClient.Execute(request);

                Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

                return result["instanceId"];

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create report instance, please check parameters", ex);
            }
        }

        private string GetReportDocumentId(string clientId, string instanceId, string format)
        {
            try
            {
                var request = new RestRequest($"/reports/clients/{clientId}/instances/{instanceId}/documents", Method.POST);

                var bdy = new { format };

                request.AddJsonBody(bdy);

                var response = _restClient.Execute(request);

                Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

                return result["documentId"];
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to create document for report", ex);
            }
        }

        private string DownloadDocument(string fileName, string clientId, string instanceId, string documentId, string format)
        {
            // https://docs.telerik.com/reporting/telerik-reporting-rest-documents-api-get-document

            var request = new RestRequest($"/reports/clients/{clientId}/instances/{instanceId}/documents/{documentId}", Method.GET);

            var isReady = CheckDocumentIsReady(clientId, instanceId, documentId);

            var tryCount = 0;

            while (!isReady && tryCount < 15)
            {
                isReady = CheckDocumentIsReady(clientId, instanceId, documentId);

                // sleep for one second before trying again
                Thread.Sleep(1000);

                tryCount++;

                if (tryCount == 15)
                {
                    return null;
                }
            }

            if (!Directory.Exists(_reportOutputDirectory))
                Directory.CreateDirectory(_reportOutputDirectory);

            _restClient.DownloadData(request).SaveAs($"{_reportOutputDirectory}{fileName}");

            return fileName;
        }

        private bool CheckDocumentIsReady(string clientId, string instanceId, string documentId)
        {
            //https://docs.telerik.com/reporting/telerik-reporting-rest-documents-api-get-document-info
            var request = new RestRequest($"/reports/clients/{clientId}/instances/{instanceId}/documents/{documentId}/info", Method.GET);

            var response = _restClient.Execute(request);


            if (response.IsSuccessful)
            {
                Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
                return bool.Parse(result["documentReady"]);
            }

            return false;
        }

        public List<ReportFormat> GetAvailableFormats()
        {
            var request = new RestRequest("/reports/formats", Method.GET);

            var response = _restClient.Execute(request);

            return JsonConvert.DeserializeObject<List<ReportFormat>>(response.Content);
        }
    }
}
