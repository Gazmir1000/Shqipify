using System.ComponentModel.DataAnnotations;

namespace Shqipify.Models
{
    public class PostViewModel
    {
      
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }
       
        public string? Author { get; set; }
        
        public DateTime CreatedTime { get; set; }

    }
}
