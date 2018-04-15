using System;
using System.Collections.Generic;
using System.Text;

namespace XptReportServer
{
    public class ReportParameters
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public double GmtOffset { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string LogoImageUrl { get; set; }
        public string WebsiteUrl { get; set; }

        public double SpeedLimit { get; set; }
        public double MinJourneyDistance { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public int NonCommunicatingHours { get; set; }

        public int[] SelectedGeofences { get; set; }
        public int[] SelectedAssets { get; set; }
        public int[] SelectedCompanies { get; set; }
        public int[] SelectedDrivers { get; set; }

        public int UserId { get; set; }
        public int UserType { get; set; }

        public int ResellerId { get; set; }

        #region Admin Billing Report
        public double CostPerUnit { get; set; }
        public double TaxRate { get; set; }
        public string CurrencySymbol { get; set; }
        #endregion 
    }
}
