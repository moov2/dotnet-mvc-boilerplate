using System;
namespace DotnetMvcBoilerplate.ViewModels.Login
{
    public class LoginViewModel
    {
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Username { get; set; }
    }
}