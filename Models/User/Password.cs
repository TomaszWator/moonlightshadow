using System;

namespace MoonlightShadow.Models
{
    public class Password
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }

        public Password(string hash, byte[] salt)
        {
            (Hash, Salt) = (hash, salt);
        }

        public Password(Tuple<string, byte[]> password)
        {
            (Hash, Salt) = password;
        }

        public Password()
        {
            
        }
    }
}