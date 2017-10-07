namespace LP.PRManagement.Dal.Persistance
{
    public interface IGeneralUnitOfWorkFactory
    {
        IGeneralUnitOfWork GetConnection();
    }
}
