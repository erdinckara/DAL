using AutoMapper;
using Dal.Domain.Entities;
using Dal.Domain.Models;

namespace Dal.Domain
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}