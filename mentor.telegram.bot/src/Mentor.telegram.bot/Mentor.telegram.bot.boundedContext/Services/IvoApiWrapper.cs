using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Core.Common.Extensions;
using log4net;
using Newtonsoft.Json;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class IvoApiWrapper : IIvoApi
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(IvoApiWrapper));

        public IvoApiWrapper()
        {
        }

        public async Task<List<MembershipResponse>> GetMembershipsForUser(string username,Guid correlationId)
        {
            var memberships = new List<MembershipResponse>();
            var result = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/member-area/member/", username,
                "REPLACE_WITH_YOUR_VALUE");

            if (!result.IsSuccessStatusCode)
            {
                Log.LogErr($"Could not find membership for {username}", correlationId);
                return memberships;
            }

            var stringResponse = await result.Content.ReadAsStringAsync();
            GetMembershipsResponse response =
                JsonConvert.DeserializeObject<GetMembershipsResponse>(stringResponse);
            if (response.Success == true && response.Payload != null && response.Payload.Count > 0)
            {
                return response.Payload;
            }
            Log.LogErr($"Could not find membership for {username}", correlationId);
            return memberships;
        }

        public async Task<List<string>> GetUsersThatExpireInDays(int days, string membership, Guid correlationId)
        {
            List<string> users = new List<string>();
            var result = await HttpClientHelper.GetAsync("https://verifiedcryptotraders.com/member-area/users/", membership,
                "REPLACE_WITH_YOUR_VALUE");

            if (!result.IsSuccessStatusCode)
            {
                Log.LogErr($"Could not find GetUsersThatExpireInDays", correlationId);
                return users;
            }

            var stringResponse = await result.Content.ReadAsStringAsync();
            GetExpiredUsersResponse response =
                JsonConvert.DeserializeObject<GetExpiredUsersResponse>(stringResponse);
            if (response.Success == true && response.Payload != null && response.Payload.Count > 0)
            {
                var expiryDate = DateTime.Now.AddDays(days).Date;
                foreach (var user in response.Payload.Where(x => x.ExpiryDate.Date == expiryDate))
                {
                    users.Add(user.username);
                }
            }

            return users;
        }
    }


    public class GetMembershipsResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public List<MembershipResponse> Payload { get; set; }
    }

    public class MembershipResponse
    {
        public string membership_name { get; set; }
        public string status { get; set; }
        public string start_date { get; set; }
        public string expiry_date { get; set; }
        public DateTime StartDate
        {
            get
            {
                return ParseDate(start_date);
            }
        }

        public DateTime ExpiryDate
        {
            get
            {
                return ParseDate(expiry_date);
            }
        }

        public bool Active
        {
            get
            {
                return status == "active";
            }
        }

        private DateTime ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.MinValue;
            }
            try
            {
                return DateTime.Parse(date);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public override string ToString()
        {
            return $"Name: <b>{membership_name}</b> \n" +
                   $"Start date: {StartDate.ToShortDateString()} \n" +
                   $"Expiry date: {ExpiryDate.ToShortDateString()} \n" +
                   $"Active: {Active} \n\n";
        }
    }

    public class ExpirationData
    {
        public int Days { get; set; }
        public string Message { get; set; }
        public string MembershipName { get; set; }
    }
    public class GetExpiredUsersResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public List<ExpiredUserResponse> Payload { get; set; }
    }

    public class ExpiredUserResponse
    {
        public string username { get; set; }
        public string expiry_date { get; set; }
        public DateTime ExpiryDate
        {
            get
            {
                return ParseDate(expiry_date);
            }
        }

        private DateTime ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return DateTime.MinValue;
            }
            try
            {
                return DateTime.Parse(date);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
    }

    public interface IIvoApi
    {
        Task<List<MembershipResponse>> GetMembershipsForUser(string username, Guid correlationId);
        Task<List<string>> GetUsersThatExpireInDays(int days, string membership, Guid correlationId);
    }
}
