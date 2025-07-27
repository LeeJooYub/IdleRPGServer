using System;

namespace GameAPIServer.Models.MasterDB
{
    public class Currency
    {
        public int currency_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public bool is_premium { get; set; }
        public DateTime create_dt { get; set; }
    }
}