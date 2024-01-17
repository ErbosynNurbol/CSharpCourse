using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Musicplaylog
    {
        public uint Id { get; set; }
        public string Ip { get; set; }
        public byte IsComplate { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public uint MusicId { get; set; }
        public int PersonId { get; set; }
        public uint PlayTime { get; set; }
        public byte QStatus { get; set; }
        public string Sessionid { get; set; }
    }
}
