using System.ComponentModel.DataAnnotations;

namespace Shqipify.Models
{
    public class UniversityViewModel
    {
    
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
