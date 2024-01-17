using System;
using System.Collections.Generic;
namespace Lesson_13.Models
{
    public partial class Admin
    {
        public int AddTime { get; set; }
        public uint AdminTypeId { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string HiddenColumnJson { get; set; }
        public uint Id { get; set; }
        public byte IsSuper { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public byte QStatus { get; set; }
        public byte ReLogin { get; set; }
        public string SkinName { get; set; }
        public int UpdateTime { get; set; }
    }
}
