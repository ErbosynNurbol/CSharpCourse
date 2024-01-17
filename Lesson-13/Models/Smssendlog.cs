using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Smssendlog
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string PostIP { get; set; }
        public int PostTime { get; set; }
        public byte QStatus { get; set; }
        public string SmsCode { get; set; }
    }
}
