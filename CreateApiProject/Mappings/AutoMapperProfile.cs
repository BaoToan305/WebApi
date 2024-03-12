using AutoMapper;
using CreateApiProject.ModelDto;
using CreateApiProject.Models;

namespace CreateApiProject.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
