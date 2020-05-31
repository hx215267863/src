using System;
using System.Security.Cryptography;
using System.Text;

namespace IFactory.Common.Utils
{
    internal class RSAEncryptor
    {
        private static string m_PrivateKey = "<RSAKeyValue><Modulus>nuu/y5DbYZNYJJONIkVSLFqfG2DleYNYKgZ1faJPe4Yx37RIeWEJkUp45XOraohsMXpMLLEg4U4LdehW8Opmu/2Dkg94dWQW7ArivDUcsbA+76JGTLvwWrV82q8ep6fFlsZlfiEbsWmPJhV87Q9+N7IjBZqtJGk0Yrnu9J3zf+U=</Modulus><Exponent>AQAB</Exponent><P>223EmRhlUjw2mmSpR452EQvIOYR3AHbVocjEZXezf73JoEAkbxJKjN3hhDFaqS6Gtf347eZBc4wuix0OIkmYww==</P><Q>uWhUJo1vRUZ4mynxdaTqJ3SF8fGBOsUJfXP5mblPlqOXb5OHpmdEQWtPtwcMHwlTh4tLVkIWSkAAQg+MueK6Nw==</Q><DP>K7NEwCi3pRUQ2tbJT9LzeJmcGrhi6ti/2ySc2IhqWzp0+VFM8EH4Tu2xiB48LA10DrKx7M86ocR9UH9M3U9C+Q==</DP><DQ>eit0x7KKj2tQLW4F3S8926G8YSBxtvf+uNuirNieyPQi5TKhP9Tr9O9xJ3lNU2Yh1D7E9aG2blad1OnKIPJ1IQ==</DQ><InverseQ>QNqeMPZutKLWMViVojB94YbJ0BtJ40tdLKRBVf7+nMAM6Mt0pANHb0sZmnBJij+Jp1YJUFPKoGDTHlNZBFZgkw==</InverseQ><D>b/SS3DzDYB0mFjZgUclWF2sL9YSwhIcUIB8GvCgRKGskTX07JU9IJzO4saRoGRfcaIrIiR+Lk02g40J3pSbskdvWgcTLFH6q26RJrX5FPyBFiogiQbWd8AasSclid/H5D6H7XmbVpN2QTetKJPKnToUrIDnsOidytEnRrNvHjg0=</D></RSAKeyValue>";
        private static string m_PublicKey = "<RSAKeyValue><Modulus>nuu/y5DbYZNYJJONIkVSLFqfG2DleYNYKgZ1faJPe4Yx37RIeWEJkUp45XOraohsMXpMLLEg4U4LdehW8Opmu/2Dkg94dWQW7ArivDUcsbA+76JGTLvwWrV82q8ep6fFlsZlfiEbsWmPJhV87Q9+N7IjBZqtJGk0Yrnu9J3zf+U=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public static string Encrypt(string token)
        {
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
            string xmlString = m_PublicKey;
            cryptoServiceProvider.FromXmlString(xmlString);
            byte[] bytes = Encoding.UTF8.GetBytes(token);
            int num = 0;
            token = Convert.ToBase64String(cryptoServiceProvider.Encrypt(bytes, num != 0));
            return token;
        }

        public static string Decrypt(string token)
        {
            try
            {
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                string xmlString = m_PrivateKey;
                cryptoServiceProvider.FromXmlString(xmlString);
                byte[] rgb = Convert.FromBase64String(token);
                int num = 0;
                token = Encoding.UTF8.GetString(cryptoServiceProvider.Decrypt(rgb, num != 0));
            }
            catch (Exception ex)
            {
                token = string.Empty;
            }
            return token;
        }
    }
}
