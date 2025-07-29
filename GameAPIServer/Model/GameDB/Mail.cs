using System;


namespace GameAPIServer.Models.GameDB
{
    // 메일
    public class Mail
    {
        public Int64 mail_id { get; set; }
        public Int64 account_uid { get; set; } = 0;
        public string mail_type_cd { get; set; } = "N"; // e.g., "normal", "important"
        public string sender_cd { get; set; } = "01"; // e.g. 01: System, 02: Admin, 03: Event
        public string title { get; set; } = "No Subject"; //제목
        public string content { get; set; } = "No Content"; // 내용
        public DateTime create_dt { get; set; } = DateTime.UtcNow;
        public DateTime? expire_dt { get; set; }
        public DateTime? receive_dt { get; set; }
        public char receive_yn { get; set; } = 'N';

        // Rewards (up to 8)
        public int? reward_id { get; set; }
        public string? reward_type_cd { get; set; } //  e.g., 01: "gold", 02: "item"
        public int? reward_qty { get; set; }
    }
}