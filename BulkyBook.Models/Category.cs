using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Display order")]
        [Range(1,100, ErrorMessage = "Length is not valid")]
        public int DisplayOrder { get; set; }

        public DateTime MyProperty { get; set; } = DateTime.Now;
    }
}
