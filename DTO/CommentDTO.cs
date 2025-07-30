namespace CommentAPI.DTO
{
    public class CommentDTO
    {
            public int ID { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public Guid UserID { get; set; }
            public int EventID { get; set; }
        }

        public class CreateCommentDto
        {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserID { get; set; }
        public int EventID { get; set; }
    }

        public class UpdateCommentDto
        {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    }