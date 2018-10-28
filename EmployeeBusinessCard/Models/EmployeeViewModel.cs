using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBusinessCard.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 20")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 20")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(maximumLength: 19, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 19")]
        public string Profession { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid Phone No")]
        [StringLength(maximumLength: 15, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 15")]
        public string TelNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string Website { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        public string PhotoName { get; set; }

        public IFormFile Photo { get; set; }
    }
}
