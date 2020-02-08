using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enjaz.Data;
using enjaz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace enjaz.Pages.Users
{
    public class AllEmployeeModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        public AllEmployeeModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IList<Employee> Employee { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Employee = await _db.Employee.ToListAsync();
            return Page();

        }
    }
}