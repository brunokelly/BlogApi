﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public IList<Post> Posts { get; set; }
    }
}
