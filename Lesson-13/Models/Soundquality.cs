using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Soundquality
    {
        public int AddTime { get; set; }
        public uint DisplayOrder { get; set; }
        public int Id { get; set; }
        public byte QStatus { get; set; }
        public string SoundQualityValue { get; set; }
        public int UpdateTime { get; set; }
    }
}
