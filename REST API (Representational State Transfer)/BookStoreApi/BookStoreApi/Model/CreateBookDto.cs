using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Model
{
    public class CreateBookDto
    {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }
    }
}
