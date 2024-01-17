using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Person
    {
        public string AvatarUrl { get; set; }
        public uint CourseCount { get; set; }
        public string Email { get; set; }
        public byte EmailIsVerify { get; set; }
        public int Id { get; set; }
        public int OldMiniProgramId { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public byte PhoneIsVerify { get; set; }
        public byte QStatus { get; set; }
        public string RealName { get; set; }
        public string RegisterIp { get; set; }
        public int RegisterTime { get; set; }
        public uint TotalBonus { get; set; }
        public uint TotalIncome { get; set; }
        public uint TotalWithdraw { get; set; }
        public string WechatId { get; set; }
        public byte WechatIsVerify { get; set; }
        public uint WithdrawBonus { get; set; }
        public uint WithdrawIncome { get; set; }
    }
}
