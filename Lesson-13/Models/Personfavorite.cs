using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Personfavorite
    {
        public uint AddTime { get; set; }
        public uint ArtistId { get; set; }
        public uint GenreId { get; set; }
        public uint Id { get; set; }
        public uint MusicId { get; set; }
        public uint PersonId { get; set; }
        public byte QStatus { get; set; }
        public uint UpdateTime { get; set; }
    }
}
