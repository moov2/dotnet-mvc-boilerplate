using System;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace DotnetMvcBoilerplate.ViewModels.Install
{
    public class InstallViewModel
    {
        [EqualTo("Password", ErrorMessage = "Password confirmation doesn't match Password.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Password { get; set; }
    }
}