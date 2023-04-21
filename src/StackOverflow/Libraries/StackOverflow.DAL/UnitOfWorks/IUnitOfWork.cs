namespace StackOverflow.DAL.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    void SaveChanges();
    void BeginTransaction();
    void CommitTransaction();
    void RollBackTransaction();
}
