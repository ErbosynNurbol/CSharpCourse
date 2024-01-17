using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Admintype
    {
        public uint AddTime { get; set; }
        public string Description { get; set; }
        public uint Id { get; set; }
        public byte IsSuper { get; set; }
        public byte QStatus { get; set; }
        public string TypeName { get; set; }
        public uint UpdateTime { get; set; }
    }
}
