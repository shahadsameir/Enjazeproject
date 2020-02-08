using enjaz.Models;
using enjaz.Uitility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace enjaz.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async void Initializer()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }


            }
            catch (Exception ex)
            {

            }
            if (_roleManager.RoleExistsAsync(SD.AdminUser).GetAwaiter().GetResult())
            {
                return;
            }
            _roleManager.CreateAsync(new IdentityRole(SD.AdminUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.EmployeeUser)).GetAwaiter().GetResult();
            _userManager.CreateAsync(new Employee
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                PhoneNumber = "123456",
                EmailConfirmed = true,
                Address="Baghdad",
                Birthday=new DateTime(2017, 1, 18),
                Salary =800,
                Gender="Female",  }, "Qwe123!@").GetAwaiter().GetResult();
            IdentityUser user = _db.Employee.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, SD.AdminUser).GetAwaiter().GetResult();
    }
    }
}
