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
           CreateMap<Conf_classify, Conf_classifyOutputDto>();
           CreateMap<Conf_classifyInputDto, Conf_classify>();
           CreateMap<Conf_conf, Conf_confOutputDto>();
           CreateMap<Conf_confInputDto, Conf_conf>();
           CreateMap<Conf_detail, Conf_detailOutputDto>();
           CreateMap<Conf_detailInputDto, Conf_detail>();
           CreateMap<Conf_detail_conf, Conf_detail_confOutputDto>();
           CreateMap<Conf_detail_confInputDto, Conf_detail_conf>();
           CreateMap<Ds_classify, Ds_classifyOutputDto>();
           CreateMap<Ds_classifyInputDto, Ds_classify>();
           CreateMap<Ds_detail, Ds_detailOutputDto>();
           CreateMap<Ds_detailInputDto, Ds_detail>();
           CreateMap<Ds_ds, Ds_dsOutputDto>();
           CreateMap<Ds_dsInputDto, Ds_ds>();
           CreateMap<Ds_relation, Ds_relationOutputDto>();
           CreateMap<Ds_relationInputDto, Ds_relation>();
           CreateMap<Plug_plug, Plug_plugOutputDto>();
           CreateMap<Plug_plugInputDto, Plug_plug>();
           CreateMap<Plug_sysrelation, Plug_sysrelationOutputDto>();
           CreateMap<Plug_sysrelationInputDto, Plug_sysrelation>();
           CreateMap<Plug_type, Plug_typeOutputDto>();
           CreateMap<Plug_typeInputDto, Plug_type>();
           CreateMap<Sd_classify, Sd_classifyOutputDto>();
           CreateMap<Sd_classifyInputDto, Sd_classify>();
           CreateMap<Sd_sysdb, Sd_sysdbOutputDto>();
           CreateMap<Sd_sysdbInputDto, Sd_sysdb>();
           CreateMap<Sys_classify, Sys_classifyOutputDto>();
           CreateMap<Sys_classifyInputDto, Sys_classify>();
           CreateMap<Sys_conf, Sys_confOutputDto>();
           CreateMap<Sys_confInputDto, Sys_conf>();
           CreateMap<Sys_conf_classify, Sys_conf_classifyOutputDto>();
           CreateMap<Sys_conf_classifyInputDto, Sys_conf_classify>();
           CreateMap<Sys_conf_details, Sys_conf_detailsOutputDto>();
           CreateMap<Sys_conf_detailsInputDto, Sys_conf_details>();
           CreateMap<Sys_conf_obj, Sys_conf_objOutputDto>();
           CreateMap<Sys_conf_objInputDto, Sys_conf_obj>();
           CreateMap<Sys_sys, Sys_sysOutputDto>();
           CreateMap<Sys_sysInputDto, Sys_sys>();
           CreateMap<Work_work, Work_workOutputDto>();
           CreateMap<Work_workInputDto, Work_work>();
           CreateMap<Work_workdetail, Work_workdetailOutputDto>();
           CreateMap<Work_workdetailInputDto, Work_workdetail>();

        }
    }
}
