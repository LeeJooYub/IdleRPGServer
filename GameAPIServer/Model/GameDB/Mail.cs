using System;


namespace GameAPIServer.Models.GameDB
{
    public class Mail
    {
        public Int64 mail_id { get; set; } = 0;
        public string mail_type { get; set; } = "normal"; // e.g., "normal", "important"
        public string sender { get; set; } = "System";
        public string condition { get; set; } = "none"; // e.g., "none", "advertise" 메일을 받기 위한 조건. advertise는 광고 시청 후 메일 받기 가능
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
        public int reward_id { get; set; }
        public int reward_qty { get; set; } = 0;
        public string reward_type { get; set; } = "normal"; // gold, item, etc.
    }

}