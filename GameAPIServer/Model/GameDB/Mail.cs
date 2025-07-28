using System;


namespace GameAPIServer.Models.GameDB
{
    // 메일
    public class Mail
    {
        public Int64 mail_seq { get; set; }
        public Int64 account_uid { get; set; } = 0;

        // public Int64 mail_id { get; set; } = 0;
        // public Int64 account_id { get; set; } = 0;
        public string mail_type { get; set; } = "normal"; // e.g., "normal", "important"
        public string sender { get; set; } = "System";
        public string receive_condition { get; set; } = "none"; // e.g., "none", "advertise" 메일을 받기 위한 조건. advertise는 광고 시청 후 메일 받기 가능
        public string subject { get; set; } = "No Subject"; //제목
        public string content { get; set; } = "No Content"; // 내용
        public DateTime create_dt { get; set; } = DateTime.UtcNow;
        public DateTime? expire_dt { get; set; }
        public DateTime? receive_dt { get; set; }
        public bool is_read { get; set; } = false;
        public bool is_claimed { get; set; } = false;

        // Rewards (up to 8)
        public int? reward_id { get; set; }
        public string? reward_type { get; set; } // 보상 타입 (예: "gold", "item")
        public int? reward_qty { get; set; }
    }
}