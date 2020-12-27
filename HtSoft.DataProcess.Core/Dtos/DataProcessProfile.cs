using AutoMapper;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Dtos
{
    public class DataProcessProfile : Profile
    {
        public DataProcessProfile()
        {
           CreateMap<Sys_classify, Sys_classifyOutputDto>();
           CreateMap<Sys_classifyInputDto, Sys_classify>();
           CreateMap<Sys_sys, Sys_sysOutputDto>();
           CreateMap<Sys_sysInputDto, Sys_sys>();

        }
    }
}
