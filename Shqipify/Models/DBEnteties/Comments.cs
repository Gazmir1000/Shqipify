using Shqipify.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shqipify.Models.DBEnteties
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public string? Text { get; set; }
        public AppUser? User { get; set; }
    }
}
