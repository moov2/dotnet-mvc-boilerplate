using System;
using DataAnnotationsExtensions;

namespace DotnetMvcBoilerplate.ViewModels.Install
{
    public class InstallViewModel
    {
        [EqualTo("Password", ErrorMessage = "Password confirmation doesn't match Password.")]
        public string ConfirmPassword { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}