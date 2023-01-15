using System.ComponentModel.DataAnnotations;

namespace Shqipify.Models.DBEnteties
{
    public class University
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

    }
}
