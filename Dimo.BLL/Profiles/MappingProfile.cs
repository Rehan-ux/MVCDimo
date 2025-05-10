using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Dimo.BLL.DTO.EmployeesDto;
using Dimo.DAL.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.BLL.Profiles
{
	public class MappingProfile :Profile
	{
        //profileانا ورثته من auto mapper 
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));

			CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)));

            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdatedEmployeeDto, Employee>() 
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

		}
	}
}
