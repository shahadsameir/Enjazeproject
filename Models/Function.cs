using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace enjaz.Models
{
    public class Function
    {
        public int Id{ get; set; }
        [Required]
        public string TaskForEmployee { get; set; }
        [Required]
        public int NumberOfDay { get; set; }
    }
}
