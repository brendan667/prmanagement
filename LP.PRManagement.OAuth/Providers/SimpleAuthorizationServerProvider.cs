using Autofac;
using LP.PRManagement.Common;
using LP.PRManagement.Dal.Persistance;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LP.PRManagement.OAuth.Providers
{
    /// <summary>
    /// Simple Authorization Server provider
    /// </summary>
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public IGeneralUnitOfWork GeneralUnitOfWork { get; }

        public SimpleAuthorizationServerProvider(IContainer container)
        {
            GeneralUnitOfWork = container.Resolve<IGeneralUnitOfWork>();
        }

        /// <summary>
        /// Validate client authentication override
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult(true);
        }

        /// <summary>
        /// Grant Resource Owner Credentials override
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            var user = await GeneralUnitOfWork.User.FindOne(x => x.Email.ToLower() == context.UserName.ToLower());
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (!PasswordHash.ValidatePassword(context.Password, user.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {"userName", context.UserName},
                    {"fullname", user.Name}
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        /// <summary>
        /// Token Endpoint override
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Grant refresh token override
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);

            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}