using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Mediainfo
    {
        public uint AddTime { get; set; }
        public int AdminId { get; set; }
        public uint Id { get; set; }
        public string MediaFormat { get; set; }
        public string MediaSize { get; set; }
        public string MediaTitle { get; set; }
        public string MediaType { get; set; }
        public string MediaUrl { get; set; }
        public byte QStatus { get; set; }
        public uint UpdateTime { get; set; }
    }
}
