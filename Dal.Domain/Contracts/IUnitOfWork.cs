using System;
using System.Threading.Tasks;

namespace Dal.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        Task CommitAsync();
    }
}