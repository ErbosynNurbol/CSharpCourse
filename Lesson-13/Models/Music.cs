using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Music
    {
        public uint AddTime { get; set; }
        public uint ClickCount { get; set; }
        public string CloudFileId { get; set; }
        public string CloudMediaUrl { get; set; }
        public string CloudTaskId { get; set; }
        public string Description { get; set; }
        public uint DownloadCount { get; set; }
        public string Duration { get; set; }
        public uint FavoriteCount { get; set; }
        public int GenreId { get; set; }
        public int Id { get; set; }
        public string IdName { get; set; }
        public uint LikeCount { get; set; }
        public string Lyric { get; set; }
        public string MusicTitle { get; set; }
        public uint MusicTypeId { get; set; }
        public int PlayCount { get; set; }
        public uint PublishYear { get; set; }
        public byte QStatus { get; set; }
        public uint RhythmId { get; set; }
        public string SearchName { get; set; }
        public uint ShareCount { get; set; }
        public uint SingTypeId { get; set; }
        public uint SoundQualityId { get; set; }
        public string ThumbnailUrl { get; set; }
        public uint TopTime { get; set; }
        public uint UpdateTime { get; set; }
        public string Url { get; set; }
        public uint ViewCount { get; set; }
    }
}
