using AutoMapper;
using ExpensesTracker.API.Contracts.Requests;
using ExpensesTracker.API.Contracts.Responses;
using ExpensesTracker.API.Data.Models;

namespace ExpensesTracker.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Budget, BudgetResponse>().ReverseMap();
            CreateMap<CreateBudgetRequest, Budget>().ReverseMap();
            CreateMap<UpdateBudgetRequest, Budget>().ReverseMap();
        }
    }
}
