
using AutoMapper;
using DotNetsTask.Data.Models;
using DotNetTask.Dto;
using DotNetTask.Dto.ViewModel;


namespace DotNetsTasks.Web.MappingProfile
{
	public class MappingProfile : Profile

    {
        public MappingProfile() {


			
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, FilterBookDto>().ReverseMap();
            CreateMap<IssuedBook, IssuedBookDto>().ReverseMap();
            CreateMap<IssuedBook, ReturnDto>().ReverseMap();
           


          

        }
       
        
    }
}

