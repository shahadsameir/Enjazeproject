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
    public class DeleteTaskModel : PageModel
    {
        private readonly ApplicationDbContext _db;


        public DeleteTaskModel(ApplicationDbContext db)
        {

            _db = db;
        }

        [BindProperty]
        public Function Task { get; set; }

        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
               Task = await _db.Function.FirstOrDefaultAsync(m => m.Id == Id);
                if (Task == null)
                {

                    return NotFound();
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {

            var DTask = await _db.Function.FirstOrDefaultAsync(m => m.Id == Task.Id);
            _db.Function.Remove(DTask);
            await _db.SaveChangesAsync();
            return RedirectToPage("TaskPage");
        }




    }
}