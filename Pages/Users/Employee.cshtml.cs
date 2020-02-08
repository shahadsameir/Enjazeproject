using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enjaz.Data;
using enjaz.Models;
using enjaz.Uitility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace enjaz.Pages.Users
{
    public class EmployeeModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        public EmployeeModel(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }
        public IList<Employee> Employee { get; set; }
        public void OnGet()
        {
            var item = _roleManager.FindByNameAsync(SD.EmployeeUser).GetAwaiter().GetResult();


            List<string> userids = _db.UserRoles.Where(a => a.RoleId == item.Id).Select(b => b.UserId).Distinct().ToList();
            IQueryable<Employee> ListAdmin = from s in _db.Employee
                                             where (userids.Contains(s.Id))
                                             select s;
            Employee = ListAdmin.ToList();

        }
    }
}