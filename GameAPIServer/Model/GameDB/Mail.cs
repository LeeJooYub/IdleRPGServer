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

    }

    // 메일 보상
    // 메일에 포함된 보상 정보를 나타내는 클래스입니다.
    public class RewardInMail
    {
        public Int64 mail_id { get; set; }
        public Int64 account_id { get; set; }
        public int reward_id { get; set; }
        public int reward_qty { get; set; } = 0;
        public string reward_type { get; set; } = "normal"; // gold, item, etc.
    }

}