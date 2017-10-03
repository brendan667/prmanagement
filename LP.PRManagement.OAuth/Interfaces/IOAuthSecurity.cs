namespace LP.PRManagement.OAuth.Interfaces
{
    public interface IOAuthSecurity
    {
        string GetHash(string clientSecret);
    }
}
