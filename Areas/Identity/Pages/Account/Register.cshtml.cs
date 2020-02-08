using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using enjaz.Models;
using enjaz.Uitility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace enjaz.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _host;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IHostingEnvironment host)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _host = host;
        }

        [BindProperty]
        public IFormFile img { get; set; }

        [BindProperty]

        public InputModel Input { get; set; }
        public string[] Genders = new []{ "Male", "Female" };

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public string Address { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
            public DateTime Birthday { get; set; }
        
            public double Salary { get; set; }
           
            public string Gender { get; set; }
            [Required]
            public string UserName { get; set; }
            
            public bool IsAdmin { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
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
                else
                {
                    return Page();
                }
            }
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new Employee { UserName = Input.UserName, 
                    Email = Input.Email,
                    Gender=Input.Gender,
                    Birthday=Input.Birthday,
                    Address=Input.Address,
                    ImgFile= newfileName,
                    PhoneNumber=Input.PhoneNumber,
                    Salary=Input.Salary
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (Input.IsAdmin == true)
                    {
                        await _userManager.AddToRoleAsync(user, SD.AdminUser);
                      
                        return RedirectToPage("/Employee");

                    }
                    else
                    {

                        await _userManager.AddToRoleAsync(user, SD.EmployeeUser);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToPage("/Users/AllEmployee");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
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
