using LP.PRManagement.OAuth.Interfaces;
using System;

namespace LP.PRManagement.OAuth.Default
{
    public class DefaultRefreshToken : IRefreshToken
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public string ProtectedTicket { get; set; }
    }
}
