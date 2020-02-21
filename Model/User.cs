using System;

namespace DI.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public bool Status { get; set; }
        public DateTime Create_time { get; set; }
        public DateTime Update_time { get; set; }
    }
}
