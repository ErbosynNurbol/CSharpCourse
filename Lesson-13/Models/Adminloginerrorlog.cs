using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Adminloginerrorlog
    {
        public int AdminId { get; set; }
        public int ErrorCount { get; set; }
        public uint Id { get; set; }
        public string LastErrorIP { get; set; }
        public uint LastErrorTime { get; set; }
    }
}
