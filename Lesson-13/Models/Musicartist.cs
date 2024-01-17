using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Musicartist
    {
        public int ArtistId { get; set; }
        public int ArtistTypeId { get; set; }
        public int Id { get; set; }
        public uint MusicId { get; set; }
        public byte QStatus { get; set; }
    }
}
