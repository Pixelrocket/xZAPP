using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xZAPP.Core
{
    public interface IDailyReport
    {
        List<DailyReport> GetDailyReports(string token, long clientId);
        Task<List<DailyReport>> GetDailyReportsAsync(string token, long clientId);
    }
}

