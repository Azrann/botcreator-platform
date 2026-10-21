using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Functions.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly string _connectionString;

        public DashboardService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public WidgetDashboard GetWidgetDashboard(string botId,int days)
        {
            WidgetDashboard result = new WidgetDashboard();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var results = conn.QueryMultiple($@"
                    select Count(*) from webmetrics as w
                    where w.BotId = '{botId}' and CreatedDate > DATEADD(Day,-{days} ,getDate())

                    select Count(Distinct(Auth))  from ChatLiveusers as c
                    where c.BotId = '{botId}' and CreatedOn > DATEADD(Day,-{days} ,getDate())

                    select Count(*)  from EventItems as c
                    where c.BotId = '{botId}' and (DataJson like '%fa-facebook%' OR DataJson like '%fa-whatsapp%'  OR DataJson like '%fa-phone%') and CreatedOn > DATEADD(Day,-{days} ,getDate())

                    SELECT CreatedDate as LastVisit,WebUserId,NameList,EmailList, Interactions, IsNull(Conversions, 0) as Conversions, Country FROM 
                    (select ClientId, Count(*) as Conversions from EventItems as c
                    where c.BotId = '{botId}' and (DataJson like '%fa-facebook%' OR DataJson like '%fa-whatsapp%'  OR DataJson like '%fa-phone%') and CreatedOn > DATEADD(Day,-{days} ,getDate())
                    Group By ClientId) as a
                    RIGHT JOIN (select Max(CreatedDate) as CreatedDate, WebUserId, STRING_AGG  (CAST(ISNULL(name,'N/A') AS nvarchar(max)), ',') as NameList, STRING_AGG ( CAST(ISNULL(email,'N/A') AS nvarchar(max)), ',')  as EmailList , Count(*) as Interactions, Country  from webmetrics as w
                    INNER JOIN ChatLiveusers as c ON ((w.WebUserId = c.ClientId) AND (w.BotId = c.BotId))
                    --LEFT JOIN EventItems as e ON (w.WebUserId = e.ClientId)
                    where w.BotId = '{botId}' and CreatedDate > DATEADD(Day,-{days} ,getDate())
                    Group by WebUserId, Country) as b 
                    ON (a.ClientId = b.WebUserId)
                ");

                result.Visits = results.Read<ulong>().First();
                result.BotOpenings = results.Read<ulong>().First();
                result.Conversions = results.Read<ulong>().First();
                result.Conversations = results.Read<Conversation>().ToList();
            }

            result.Conversations = result.Conversations.Select(x => x.RemoveDuplicates()).ToList();
            return result;
        }
    }

    public interface IDashboardService
    {
        WidgetDashboard GetWidgetDashboard(string botId, int days);
    }

    public class WidgetDashboard
    {
        public ulong Visits { get; set; }
        public ulong BotOpenings { get; set; }
        public ulong Conversions { get; set; }
        public List<Conversation> Conversations { get; set; }
    }

    public class Conversation
    {
        public DateTime LastVisit { get; set; }
        public Guid WebUserId { get; set; }
        public uint Interactions { get; set; }
        public uint Conversions { get; set; }
        public string Country { get; set; }
        public string EmailList { get; set; }
        public string NameList { get; set; }

        public Conversation RemoveDuplicates()
        {
            List<string> uniqueEmails = EmailList.Split(',').Distinct().ToList();
            EmailList = string.Join(",", uniqueEmails);
            List<string> uniqueNames = NameList.Split(',').Distinct().ToList();
            NameList = string.Join(",", uniqueNames);
            return this;
        }
    }
}
