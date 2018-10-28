using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace EmployeeBusinessCard.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public string Profession { get; set; }

        [MaxLength(50)]
        public string TelNumber { get; set; }

        [MaxLength(200)]
        public string Website { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string PhotoName { get; set; }
    }
}
