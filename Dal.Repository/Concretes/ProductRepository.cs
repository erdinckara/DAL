using AutoMapper;
using Dal.Domain.Contracts;
using Dal.Domain.Entities;

namespace Dal.Repository.Concretes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}