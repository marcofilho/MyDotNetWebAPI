﻿using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.Dtos
{
    public class AddressDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(50, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 1)]
        public string Number { get; set; }

        public string Complement { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 2)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(8, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 8)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 2)]
        public string State { get; set; }

        public Guid SupplierId { get; set; }

    }
}
