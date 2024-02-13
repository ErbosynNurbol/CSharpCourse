namespace MODEL;

  public partial class Article
    {
        public uint Id { get; set; }
        public int MenuId { get; set; }
        public uint AddTime { get; set; }
        public string Author { get; set; }
        public string FullDescription { get; set; }
        public byte IsFocusNews { get; set; }
        public string LatynUrl { get; set; }
        public byte QStatus { get; set; }
        public string SearchName { get; set; }
        public string ShortDescription { get; set; }
        public string ThumbnailCopyright { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string SourceUrl { get; set; }
        public uint UpdateTime { get; set; }
        public int ViewCount { get; set; }
    }