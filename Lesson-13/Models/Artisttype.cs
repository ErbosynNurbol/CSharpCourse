using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Artisttype
    {
        public uint AddTime { get; set; }
        public uint DisplayOrder { get; set; }
        public int Id { get; set; }
        public byte QStatus { get; set; }
        public string SearchName { get; set; }
        public string TypeName { get; set; }
        public uint UpdateTime { get; set; }
    }
}
