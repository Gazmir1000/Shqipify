using Shqipify.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shqipify.Models
{
    public class CommentViewModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        [Required]
        public string? Text { get; set; }
        public string? Author { get; set; }
    }
}
