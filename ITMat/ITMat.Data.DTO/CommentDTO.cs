using System;

namespace ITMat.Data.DTO
{
    public class CommentDTO : AbstractDTO
    {
        public string Username { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Text { get; set; }
    }
}