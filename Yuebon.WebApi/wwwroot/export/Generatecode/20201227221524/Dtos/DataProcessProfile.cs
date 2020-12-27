using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using HtSoft.DataProcess.Models;

namespace HtSoft.DataProcess.Dtos
{
    public class DataProcessProfile : Profile
    {
        public DataProcessProfile()
        {
           CreateMap<Dp_sys_classify, Dp_sys_classifyOutputDto>();
           CreateMap<Dp_sys_classifyInputDto, Dp_sys_classify>();
           CreateMap<Dp_sys_sys, Dp_sys_sysOutputDto>();
           CreateMap<Dp_sys_sysInputDto, Dp_sys_sys>();

        }
    }
}
