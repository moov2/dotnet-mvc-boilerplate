using System;

namespace DotnetMvcBoilerplate.Core.Security
{
    public class Password
    {
        public Password(byte[] key, byte[] salt)
        {
            Key = key;
            Salt = salt;
        }

        public byte[] Key { get; private set; }
        public byte[] Salt { get; private set; }
    }
}