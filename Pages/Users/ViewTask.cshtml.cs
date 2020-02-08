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
    public class ViewTaskModel : PageModel
    {
        private readonly ApplicationDbContext _db;
       public ViewTaskModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IList<UserTask> UserTask { get; set; }
        public IActionResult OnGet(string Id)
        {
            UserTask = _db.UserTask.Include(c=>c.Function).Where(c => c.UserId == Id).ToList();
            return Page();
        }
    }
}