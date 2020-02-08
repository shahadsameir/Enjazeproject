using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace enjaz.Models
{
    public class Employee : IdentityUser
    {
        [Required]
        public string Address { get; set; }
     
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Birthday { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public string Gender { get; set; }

        public string ImgFile { get; set; }
    }
}
