using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enjaz.Data;
using enjaz.Models;
using enjaz.Uitility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace enjaz.Pages.Users
{
    [Authorize(Roles = SD.AdminUser)]
    public class TaskPageModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public TaskPageModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Function Taskadd { get; set; }
        [BindProperty]
        public IList<Function> Tasks { get; set; }



        public IActionResult OnGet()
        {
            Tasks = _db.Function.ToList();
            return Page();

        }

        public async Task<IActionResult> OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Function.Add(Taskadd);
            await _db.SaveChangesAsync();
            return RedirectToPage("TaskPage");
        }


    }
}