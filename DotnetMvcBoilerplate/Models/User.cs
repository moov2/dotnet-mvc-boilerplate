using System;
using MongoDB.Bson;
using System.Collections.Generic;
using DotnetMvcBoilerplate.Core.Security;

namespace DotnetMvcBoilerplate.Models
{
    public class User
    {
        public ObjectId Id { get; set; }

        public byte[] PasswordKey { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual string Username { get; set; }

        public User()
        {
            Id = ObjectId.GenerateNewId();
        }

        /// <summary>
        /// Sets the values of PasswordKey & PasswordSalt
        /// using the values from the Password object.
        /// </summary>
        public void SetPassword(Password password)
        {
            PasswordKey = password.Key;
            PasswordSalt = password.Salt;
        }
    }
}