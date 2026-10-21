using System;

namespace Functions
{
    public class WebMetric
    {
        public Guid Id { get; set; }
        public Guid WebUserId { get; set; }
        public string Cookies { get; set; }
        public string BrowserLanguage { get; set; }
        public string Language { get; set; }
        public string Platform { get; set; }
        public string Connection { get; set; }
        public string UserAgent { get; set; }
        public string Webdriver { get; set; }
        public string CurrentUrl { get; set; }
        public string BotId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Ip { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Loc { get; set; }
        public string Postal { get; set; }
        public string Referrer { get; set; }
    }
}
