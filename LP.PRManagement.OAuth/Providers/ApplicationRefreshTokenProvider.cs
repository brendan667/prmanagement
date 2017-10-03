using LP.PRManagement.OAuth.Interfaces;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace LP.PRManagement.OAuth.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IOAuthSecurity _authSecurity;
        private readonly IOAuthDataManager _ioAuthDataManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioAuthDataManager"></param>
        /// <param name="authSecurity"></param>
        public ApplicationRefreshTokenProvider(IOAuthDataManager ioAuthDataManager, IOAuthSecurity authSecurity)
        {
            _ioAuthDataManager = ioAuthDataManager;
            _authSecurity = authSecurity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            string refreshTokenId = Guid.NewGuid().ToString("n");
            var token = _ioAuthDataManager.CreateRefresherToken();
            token.Id = _authSecurity.GetHash(refreshTokenId);
            token.Subject = context.Ticket.Identity.Name;
            token.IssuedUtc = DateTime.UtcNow;
            token.ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(TimeSpan.FromDays(1).TotalMinutes));
            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            await _ioAuthDataManager.SaveRefreshToken(token);
            context.SetToken(refreshTokenId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var hashedTokenId = _authSecurity.GetHash(context.Token);
            var refreshToken = await _ioAuthDataManager.GetRefreshToken(hashedTokenId);
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await _ioAuthDataManager.DeleteRefreshToken(hashedTokenId);
            }
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            ReceiveAsync(context).Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Create(AuthenticationTokenCreateContext context)
        {
            CreateAsync(context).Wait();
        }
    }
}