using System;


namespace GameAPIServer.Models.GameDB
{
    // 메일
    public class Mail
    {
        public Int64 mail_id { get; set; } = 0;
        public Int64 account_id { get; set; } = 0;
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
        public int? reward1_id { get; set; }
        public string? reward1_type { get; set; }
        public int? reward1_qty { get; set; }

        public int? reward2_id { get; set; }
        public string? reward2_type { get; set; }
        public int? reward2_qty { get; set; }

        public int? reward3_id { get; set; }
        public string? reward3_type { get; set; }
        public int? reward3_qty { get; set; }

        public int? reward4_id { get; set; }
        public string? reward4_type { get; set; }
        public int? reward4_qty { get; set; }

        public int? reward5_id { get; set; }
        public string? reward5_type { get; set; }
        public int? reward5_qty { get; set; }

        public int? reward6_id { get; set; }
        public string? reward6_type { get; set; }
        public int? reward6_qty { get; set; }

        public int? reward7_id { get; set; }
        public string? reward7_type { get; set; }
        public int? reward7_qty { get; set; }

        public int? reward8_id { get; set; }
        public string? reward8_type { get; set; }
        public int? reward8_qty { get; set; }
    }
}