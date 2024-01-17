using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Personloginerrorlog
    {
        public uint ErrorCount { get; set; }
        public int Id { get; set; }
        public string LastErrorIP { get; set; }
        public uint LastErrorTime { get; set; }
        public uint PersonId { get; set; }
    }
}
