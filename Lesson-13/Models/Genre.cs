using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Genre
    {
        public uint AddTime { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string GenreName { get; set; }
        public int Id { get; set; }
        public string IdName { get; set; }
        public uint MusicCount { get; set; }
        public byte QStatus { get; set; }
        public string SearchName { get; set; }
        public string ThumbnailUrl { get; set; }
        public uint UpdateTime { get; set; }
    }
}
