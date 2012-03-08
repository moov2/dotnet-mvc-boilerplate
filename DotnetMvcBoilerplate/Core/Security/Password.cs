using System;

namespace DotnetMvcBoilerplate.Core.Security
{
    public class Password
    {
        public Password()
        {

        }

        public Password(byte[] key, byte[] salt)
        {
            Key = key;
            Salt = salt;
        }

        public virtual byte[] Key { get; private set; }
        public virtual byte[] Salt { get; private set; }
    }
}