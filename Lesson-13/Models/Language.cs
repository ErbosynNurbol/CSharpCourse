using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Language
    {
        public byte BackendDisplay { get; set; }
        public byte DisplayOrder { get; set; }
        public byte FrontendDisplay { get; set; }
        public string FullName { get; set; }
        public uint Id { get; set; }
        public byte IsDefault { get; set; }
        public string ISOCode { get; set; }
        public byte IsRtl { get; set; }
        public uint IsSubLanguage { get; set; }
        public string LanguageCulture { get; set; }
        public string LanguageFlagImageUrl { get; set; }
        public byte QStatus { get; set; }
        public string ShortName { get; set; }
        public string UniqueSeoCode { get; set; }
    }
}
