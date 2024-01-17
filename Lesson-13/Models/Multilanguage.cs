using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Multilanguage
    {
        public uint ColumnId { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public uint Id { get; set; }
        public string Language { get; set; }
        public byte QStatus { get; set; }
        public string TableName { get; set; }
    }
}
