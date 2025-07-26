using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.Dtos
{
    public class SupplierDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(14, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 11)]
        public string Document { get; set; }

        public int Type { get; set; }

        public bool Active { get; set; }

        public AddressDto Address { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
