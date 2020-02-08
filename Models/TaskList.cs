using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace enjaz.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Employee Employee { get; set; }
    }
}
