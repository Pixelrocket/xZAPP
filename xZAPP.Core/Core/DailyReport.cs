using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public class DailyReport: IDailyReport
    {

        public long DossierId {get; set;}
        [JsonProperty(PropertyName="cdo_date_added")]
        public string Date {get; set;}
        [JsonProperty(PropertyName="cdo_time_added")]
        public string Time {get; set;}
        [JsonProperty(PropertyName="cdo_subject")]
        public string Subject {get; set;}
        [JsonProperty(PropertyName="cdo_dossier")]
        public string Content {get; set;}
        [JsonProperty(PropertyName="dossiermapid")]
        public long FolderId {get; set;}
        [JsonProperty(PropertyName="employeefirstname")]
        public string EmployeeFirstname {get; set;}
        [JsonProperty(PropertyName="employeeinfix")]
        public string EmployeeInfix {get; set;}
        [JsonProperty(PropertyName="employeelastname")]
        public string EmployeeLastname {get; set;}



        public DailyReport()
        {
        }

        public List<DailyReport> GetDailyReports(string token, long clientId)
        {
            ZillizProxy clProxy = new ZillizProxy();
            List<DailyReport> reports =  JsonConvert.DeserializeObject<List<DailyReport>>(clProxy.GetJSON(ZillizProxy.ServiceURL.Reports, token:token, clientId: clientId, wsAction: "GET"));

            //return reports.FindAll(rep => rep.FolderId == 744);
            return reports;
        }

        // Async version of GetDailyReports
        public async Task<List<DailyReport>> GetDailyReportsAsync(string token, long clientId)
        {
            List<DailyReport> reports = null;
            ZillizProxy clProxy = new ZillizProxy();          

            // Call async proxy and use returned JSON string
            string jsonClients = await clProxy.GetJSONAsync(ZillizProxy.ServiceURL.Reports, token:token, clientId: clientId, wsAction: "GET");
            reports = JsonConvert.DeserializeObject<List<DailyReport>>(jsonClients);

            //return reports.FindAll(rep => rep.FolderId == 744);
            return reports;
        }
    }
}

