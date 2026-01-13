using AutoMapper;
using ExpensesTracker.API.Data.Models;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;

namespace ExpensesTracker.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
        }
    }
}
