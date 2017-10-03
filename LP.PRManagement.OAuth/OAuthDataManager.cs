using LP.PRManagement.OAuth.Default;
using LP.PRManagement.OAuth.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LP.PRManagement.OAuth
{
    public class OAuthDataManager : IOAuthDataManager
    {
        private readonly List<IRefreshToken> _refreshTokens;

        public OAuthDataManager()
        {
            _refreshTokens = new List<IRefreshToken>();
        }

        public IRefreshToken CreateRefresherToken() => new DefaultRefreshToken();

        public Task DeleteRefreshToken(string hashedTokenId) => Task.FromResult(_refreshTokens.RemoveAll(x => x.Id == hashedTokenId));

        public Task<IRefreshToken> GetRefreshToken(string hashedTokenId) => Task.FromResult(_refreshTokens.FirstOrDefault(x => x.Id == hashedTokenId));

        public Task SaveRefreshToken(IRefreshToken token)
        {
            _refreshTokens.Add(token);
            return Task.FromResult(token);
        }
    }
}
