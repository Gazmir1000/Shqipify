using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shqipify.Models.DBEnteties
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<Comments>? Comments { get; set; }
        public string? university { get; set; }
    }
}
