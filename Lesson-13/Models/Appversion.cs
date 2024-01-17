using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Appversion
    {
        public uint AddTime { get; set; }
        public string ChangeLog { get; set; }
        public uint DownloadCount { get; set; }
        public string DownloadUrl { get; set; }
        public int Id { get; set; }
        public byte Mandatory { get; set; }
        public string Platform { get; set; }
        public byte QStatus { get; set; }
        public uint UpdateTime { get; set; }
        public uint VersionCode { get; set; }
        public string VersionName { get; set; }
    }
}
