using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Advertisetype
    {
        public string AdvertiseTypeName { get; set; }
        public uint DisplayOrder { get; set; }
        public uint Id { get; set; }
        public byte IsMultiAdvertise { get; set; }
        public string MobileAdvertiseSize { get; set; }
        public string PcAdvertiseSize { get; set; }
        public byte QStatus { get; set; }
    }
}
