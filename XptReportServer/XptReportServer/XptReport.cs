using System;
using System.Collections.Generic;
using System.Text;

namespace XptReportServer
{
    public class XptReport
    {
        public string ReportName { get; set; } // filename of the report

        public string ReportPath { get; set; }

        public int CompanyId { get; set; }

        /// <summary>
        /// The language report is required in at the moment only supports en,ar
        /// </summary>
        public string Language { get; set; }


        public XptReport()
        {

        }

        public XptReport(string reportFileName, int companyId, string language)
        {
            ReportName = reportFileName;
            CompanyId = companyId;
            Language = language;
        }
    }
}
