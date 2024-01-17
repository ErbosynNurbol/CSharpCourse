using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Artist
    {
        public uint AddTime { get; set; }
        public string ArtistIds { get; set; }
        public string ArtistIntroduction { get; set; }
        public string ArtistName { get; set; }
        public uint FavoriteCount { get; set; }
        public byte Gender { get; set; }
        public byte GroupType { get; set; }
        public int Id { get; set; }
        public string IdName { get; set; }
        public uint KlipCount { get; set; }
        public uint LikeCount { get; set; }
        public uint MusicCount { get; set; }
        public byte QStatus { get; set; }
        public string SearchName { get; set; }
        public string ThumbnailUrl { get; set; }
        public uint TopTime { get; set; }
        public uint UpdateTime { get; set; }
    }
}
