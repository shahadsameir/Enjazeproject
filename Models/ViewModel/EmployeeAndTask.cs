using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace enjaz.Models.ViewModel
{
    public class EmployeeAndTask
    {
        public Employee Employee { get; set; }   
        public IList<Function> FunctionList { get; set; }
        public int TaskListId { get; set; }
        public IList<TaskList>TaskList { get; set; }
        public string UserTaskId { get; set; }
        [ForeignKey("UserTaskId ")]
        public virtual UserTask UserTasks { get; set; }
    }
}
