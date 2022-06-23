using System.ComponentModel.DataAnnotations;

namespace webapi.Data
{
    internal sealed class Post
    {
        [Required]
        [MaxLength(10000)]
        public string Content { get; set; } = string.Empty;
        [Key]
        public int PostId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
    }
}