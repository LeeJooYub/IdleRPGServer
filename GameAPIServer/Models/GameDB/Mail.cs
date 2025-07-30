using System;
using GameAPIServer.Models.MasterDB;


namespace GameAPIServer.Models.GameDB
{
    // 메일
    public class Mail : RewardData
    {
        public Int64 mail_id { get; set; }
        public Int64 account_uid { get; set; } = 0;
        public string mail_type_cd { get; set; } = "N"; // e.g., "normal", "important"
        public string title { get; set; } = "No Subject"; //제목
        public string content { get; set; } = "No Content"; // 내용
        public DateTime create_dt { get; set; } = DateTime.UtcNow;
        public DateTime? expire_dt { get; set; }
        public DateTime? receive_dt { get; set; }

        // Rewards 
        public char reward_receive_yn { get; set; } = 'N';
    }
}