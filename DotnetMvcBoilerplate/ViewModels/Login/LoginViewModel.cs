using System;
namespace DotnetMvcBoilerplate.ViewModels.Login
{
    public class LoginViewModel
    {
        public virtual string Password { get; set; }
        public virtual bool RememberMe { get; set; }
        public virtual string Username { get; set; }
    }
}