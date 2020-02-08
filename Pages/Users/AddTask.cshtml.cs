using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enjaz.Data;
using enjaz.Models;
using enjaz.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace enjaz.Pages.Users
{
    public class AddTaskModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AddTaskModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public EmployeeAndTask EmployeeTaskVM { get; set; }

        public async Task<IActionResult> OnGet(string Id)
        {
            EmployeeTaskVM = new EmployeeAndTask
            {
                Employee = await _db.Employee.FirstOrDefaultAsync(c => c.Id == Id),
                TaskList = await _db.TaskList
                .Include(c => c.Function)
                .Where(c => c.Employee.Id == Id).ToListAsync(),
        };

            List<string> listUserTask = _db.TaskList
                .Include(c => c.Function)
                .Where(c => c.Employee.Id==Id)
                .Select(c => c.Function.TaskForEmployee)
                .ToList();

            IQueryable<Function> ListServiceTypes = from s in _db.Function
                                                    where !(listUserTask.Contains(s.TaskForEmployee))
                                                       select s;


            EmployeeTaskVM.FunctionList = ListServiceTypes.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAddToCart()
        {
            TaskList ObjTaskList = new TaskList
            {
                UserId = EmployeeTaskVM.Employee.Id,
               FunctionId = EmployeeTaskVM.TaskListId,
            };
            _db.TaskList.Add(ObjTaskList);
            await _db.SaveChangesAsync();

            return RedirectToPage("AddTask", new { Id = EmployeeTaskVM.Employee.Id });

        }
        public async Task<IActionResult> OnPostRemoveFromCart(int Id)
        {
            TaskList ObjTaskList = _db.TaskList.FirstOrDefault
                (u => u.Id == Id );
            _db.TaskList.Remove(ObjTaskList);
            await _db.SaveChangesAsync();
            return RedirectToPage("AddTask", new { Id = EmployeeTaskVM.Employee.Id });
        }


        public async Task<IActionResult> OnPostCreate()
        {
            EmployeeTaskVM.TaskList = await _db.TaskList
                .Include(c => c.Function)
                .Where(c => c.Employee.Id == EmployeeTaskVM.Employee.Id).ToListAsync();



                foreach (var details in EmployeeTaskVM.TaskList)
                {
                    UserTask serviceDetails = new UserTask
                    {
                        UserId= EmployeeTaskVM.Employee.Id,
                        FunctionId= details.FunctionId


                    };
                    _db.UserTask.Add(serviceDetails);

                }

                _db.TaskList.RemoveRange(EmployeeTaskVM.TaskList);
                await _db.SaveChangesAsync();
                return RedirectToPage("AllEmployee");

          
        }
    }
}