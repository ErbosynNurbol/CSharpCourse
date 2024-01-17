using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Artistimage
    {
        public uint AddTime { get; set; }
        public int ArtistId { get; set; }
        public uint DisplayOrder { get; set; }
        public int Id { get; set; }
        public string ImageSize { get; set; }
        public string ImageTitle { get; set; }
        public string ImageUrl { get; set; }
        public byte QStatus { get; set; }
        public int UpdateTime { get; set; }
    }
}
