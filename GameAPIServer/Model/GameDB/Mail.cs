using System;


namespace GameAPIServer.Model.GameDB
{
    public class Mail
    {
        public Int64 mail_id { get; set; } = 0;
        public Int64 account_id { get; set; }
        public string subject { get; set; } = "No Subject";
        public string content { get; set; } = "No Content";
        public DateTime create_dt { get; set; } = DateTime.UtcNow;
        public DateTime? expire_dt { get; set; }
        public DateTime? receive_dt { get; set; }
        public bool is_read { get; set; } = false;
        public bool is_claimed { get; set; } = false;
    }


    public class RewardInMail
    {
        public Int64 mail_id { get; set; }
        public Int64 account_id { get; set; }
        public int item_id { get; set; } 
        public int reward_qty { get; set; } = 0;
        public string reward_type { get; set; } = "default_type";
    }

}