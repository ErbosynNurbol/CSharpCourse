using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Transferlog
    {
        public int Id { get; set; }
        public uint NewItemId { get; set; }
        public string NewTable { get; set; }
        public uint OldItemId { get; set; }
        public string OldTable { get; set; }
        public byte QStatus { get; set; }
    }
}
