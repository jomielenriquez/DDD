using AutoMapper;
using BookStore.Data.Context;
using BookStoreApi.Model;

namespace BookStoreApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, CreateBookDto>();
            CreateMap<CreateBookDto, Book>();
        }
    }
}
