using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Sharelog
    {
        public uint ArtistId { get; set; }
        public uint GenreId { get; set; }
        public int Id { get; set; }
        public uint MusicId { get; set; }
        public uint OpenPersonId { get; set; }
        public sbyte QStatus { get; set; }
        public uint SharePersonId { get; set; }
        public uint ShareTime { get; set; }
    }
}
