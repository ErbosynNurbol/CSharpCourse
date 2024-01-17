using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Advertise
    {
        public uint AddTime { get; set; }
        public int AdvertiseTypeId { get; set; }
        public uint ClickCount { get; set; }
        public uint DisplayOrder { get; set; }
        public uint Id { get; set; }
        public string MobileImageUrl { get; set; }
        public string MobileTitle { get; set; }
        public string MobileUrl { get; set; }
        public uint ParentAdvertiseId { get; set; }
        public string PcImageUrl { get; set; }
        public string PcTitle { get; set; }
        public string PcUrl { get; set; }
        public byte QStatus { get; set; }
        public uint UpdateTime { get; set; }
        public uint ViewCount { get; set; }
    }
}
