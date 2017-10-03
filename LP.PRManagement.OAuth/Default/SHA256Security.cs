using LP.PRManagement.OAuth.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LP.PRManagement.OAuth.Default
{
    public class SHA256Security : IOAuthSecurity
    {
        #region Implementation of IOAuthSecurity

        public string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

        #endregion
    }
}