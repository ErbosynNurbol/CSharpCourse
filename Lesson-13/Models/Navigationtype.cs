using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Navigationtype
    {
        public string Description { get; set; }
        public uint DisplayOrder { get; set; }
        public uint Id { get; set; }
        public byte QStatus { get; set; }
        public string TypeTitle { get; set; }
    }
}
