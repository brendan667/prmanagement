using System;

namespace LP.PRManagement.OAuth.Interfaces
{
    public interface IRefreshToken
    {
        string Id { get; set; }

        string Subject { get; set; }

        DateTime IssuedUtc { get; set; }

        DateTime ExpiresUtc { get; set; }

        string ProtectedTicket { get; set; }
    }
}
