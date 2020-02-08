using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using enjaz.Data;
using enjaz.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using enjaz.Uitility;

namespace enjaz.Pages.Users
{
    [Authorize(Roles = SD.AdminUser)]
    public class EditModel : PageModel
    {
       
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _host;
        public EditModel(ApplicationDbContext db, IHostingEnvironment host)
        {
            _db = db;
            _host = host;
        }

        [BindProperty]
        public IFormFile img { get; set; }
        [BindProperty]
        public Employee ApplicationUser { get; set; }


        public async Task<IActionResult> OnGet(String Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            ApplicationUser = await _db.Employee.FirstOrDefaultAsync(m => m.Id == Id);
            if (ApplicationUser == null)
            {

                return NotFound();
            }
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            string newfileName = string.Empty;
            if (img != null && img.Length > 0)
            {

                string fn = img.FileName;
                //to uploate just photo not other type of file
                if (IsImagValidate(fn))
                {
                    string extension = Path.GetExtension(fn);
                    newfileName = Guid.NewGuid().ToString() + extension;
                    string filename = Path.Combine(_host.WebRootPath + "/Images/", newfileName);
                   
                        await img.CopyToAsync(new FileStream(filename, FileMode.Create));
                    
                }
            }

                var ServiceTypeFromDB = await _db.Employee.FirstOrDefaultAsync(m => m.Id == ApplicationUser.Id);
            if (ServiceTypeFromDB == null)
            {
                return NotFound();
            }
            ServiceTypeFromDB.UserName = ApplicationUser.UserName;
            ServiceTypeFromDB.Email = ApplicationUser.Email;
            ServiceTypeFromDB.PhoneNumber = ApplicationUser.PhoneNumber;
            ServiceTypeFromDB.Address = ApplicationUser.Address;
            ServiceTypeFromDB.Birthday = ApplicationUser.Birthday;
            ServiceTypeFromDB.Salary = ApplicationUser.Salary;
            if (img != null && img.Length > 0)
            {
                ServiceTypeFromDB.ImgFile = newfileName;
            }
            await _db.SaveChangesAsync();

            return RedirectToPage("AllEmployee");



        }
        private bool IsImagValidate(string filename)
        {
            string extension = Path.GetExtension(filename);
            if (extension.Contains(".png"))
                return true;
            if (extension.Contains(".PNG"))
                return true;
            if (extension.Contains(".jpeg"))
                return true;
            if (extension.Contains(".jpg"))
                return true;
            if (extension.Contains(".gif"))
                return true;
            return false;
        }
    }
}
