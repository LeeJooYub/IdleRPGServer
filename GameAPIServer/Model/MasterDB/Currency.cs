using System;

namespace GameAPIServer.Models.MasterDB
{
    public class Currency
    {
        public int currency_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public char premium_yn { get; set; } = 'N'; // 'Y' for premium currency, 'N' for regular currency
        public DateTime create_dt { get; set; }
    }
}