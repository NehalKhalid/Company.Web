using AutoMapper;
using Company.Data.Models;
using Company.Service.Services.Department.Dto;

namespace Company.Service.Mapping
{ 
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentDto>().ReverseMap();
        }
    }
}
