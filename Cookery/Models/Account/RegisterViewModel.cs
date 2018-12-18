using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cookery.Models.Enums;

namespace Cookery.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required, MinLength(3)]
        [RegularExpression(@"^[1-9a-zA-Z.~*_-]+$")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "The Password must be at least 5 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [StringLength(100, ErrorMessage = "The password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender{ get; set; }
    }
}
