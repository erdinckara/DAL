using System.Collections.Concurrent;
using System.Threading.Tasks;
using AutoMapper;
using Dal.Domain.Contracts;

namespace Dal.Repository.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _context;
        private readonly IMapper _mapper;
        private readonly ConcurrentDictionary<string, object> _instances;

        public UnitOfWork(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _instances = new ConcurrentDictionary<string, object>();
        }

        public IProductRepository Product => _instances.GetOrAdd(nameof(Product), new ProductRepository(_context, _mapper)) as ProductRepository;



        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}