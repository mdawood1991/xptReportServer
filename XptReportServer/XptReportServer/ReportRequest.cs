using Newtonsoft.Json;

namespace XptReportServer
{
    public class ReportRequest
    {
        public string Report => JsonConvert.SerializeObject(this.XptReport);
        public XptReport XptReport { get; set; }
        public ReportParameters ParameterValues { get; set; }

        public ReportRequest(string reportFileName, int companyId, string language)
        {
            XptReport = new XptReport(reportFileName, companyId, language);

            //ParameterValues = new ReportParameters()
            //{
            //    CompanyId = subscription.CompanyId,
            //    CompanyName = subscription.CompanyName,

            //    //StartDate = new DateTime(2018, 1, 1),
            //    //EndDate = new DateTime(2017, 2, 15, 23, 59 ,59),

            //    // the date time should be in company time, because we convert to utc time on the server
            //    StartDate = subscription.UTCLastRun.ConvertFromUtcToUserTime(subscription.GmtOffset),
            //    EndDate = DateTime.UtcNow.ConvertFromUtcToUserTime(subscription.GmtOffset),

            //    GmtOffset = subscription.GmtOffset,
            //    LogoImageUrl = subscription.LogoImageUrl,
            //    WebsiteUrl = subscription.WebsiteUrl,

            //    UserId = subscription.UserId,
            //    UserType = subscription.UserType,

            //    SelectedAssets = subscription.ParametersDTO.SelectedAssetIds,
            //    SelectedDrivers = subscription.ParametersDTO.SelectedDriverIds,
            //    SelectedGeofences = subscription.ParametersDTO.SelectedGeofenceIds,
            //    SelectedCompanies = subscription.ParametersDTO.SelectedCompanyIds,

            //    EndHour = subscription.ParametersDTO.NightEndHour,
            //    StartHour = subscription.ParametersDTO.NightStartHour,
            //    NonCommunicatingHours = subscription.ParametersDTO.NotCommHours,
            //    MinJourneyDistance = subscription.ParametersDTO.MinJourneyDistance,
            //    SpeedLimit = subscription.ParametersDTO.SpeedLimit,

            //    //admin report 

            //    CostPerUnit = subscription.ParametersDTO.CostPerUnit,
            //    TaxRate = subscription.ParametersDTO.TaxRate,
            //    CurrencySymbol = subscription.ParametersDTO.CurrencySymbol

            //};

        }
    }
}
