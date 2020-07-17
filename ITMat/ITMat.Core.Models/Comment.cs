using System;

namespace ITMat.Core.Models
{
    public class Comment : AbstractModel
    {
        public string Username { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Text { get; set; }
    }
}