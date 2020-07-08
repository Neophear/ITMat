using System;

namespace ITMat.Data.Repositories.Entities
{
    internal class Comment : AbstractEntity
    {
        public string Username { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Text { get; set; }
    }
}