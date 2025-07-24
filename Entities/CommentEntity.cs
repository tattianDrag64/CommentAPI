﻿
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CommentAPI.Entities
{
    public class CommentEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int UserID { get; set; }
        [Required]
        public int EventID { get; set; }
    }
}
