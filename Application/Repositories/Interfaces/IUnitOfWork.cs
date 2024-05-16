namespace Application.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<int> CompleteAsync();
    }
}