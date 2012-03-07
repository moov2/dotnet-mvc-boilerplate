﻿using System;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Security;

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

        /// <summary>
        /// Converts the user entered data into a User object.
        /// </summary>
        /// <returns>User object made up of this data.</returns>
        public virtual User ToUser()
        {
            var user = new User();
            user.Username = Username;
            user.MakeAdmin();
            user.Password = Encryption.Encrypt(Password);
            return user;
        }
    }
}