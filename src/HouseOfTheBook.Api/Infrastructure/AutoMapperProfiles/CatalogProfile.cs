using AutoMapper;
using HouseOfTheBook.Catalog.Application.Books;
using HouseOfTheBook.Catalog.Model;

namespace HouseOfTheBook.Api.Infrastructure.AutoMapperProfiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Add.Request, Book>();
            CreateMap<Update.Request, Book>();
        }
    }
}
