using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Navigation
    {
        public uint AddTime { get; set; }
        public string Description { get; set; }
        public uint DisplayOrder { get; set; }
        public byte HasIcon { get; set; }
        public string Icon { get; set; }
        public uint Id { get; set; }
        public byte IsLock { get; set; }
        public uint NavigationTypeId { get; set; }
        public string NavTitle { get; set; }
        public string NavUrl { get; set; }
        public byte NoChild { get; set; }
        public uint ParentId { get; set; }
        public byte QStatus { get; set; }
        public string Target { get; set; }
        public uint UpdateTime { get; set; }
    }
}
