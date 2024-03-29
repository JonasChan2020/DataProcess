using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Dtos
{
    public class DataProcessProfile : Profile
    {
        public DataProcessProfile()
        {
           CreateMap<Conf_conf, Conf_confOutputDto>();
           CreateMap<Conf_confInputDto, Conf_conf>();
           CreateMap<Conf_detail, Conf_detailOutputDto>();
           CreateMap<Conf_detailInputDto, Conf_detail>();
            CreateMap<Plug_ConfDetail, Plug_ConfDetailOutputDto>();
            CreateMap<Plug_ConfDetailInputDto, Plug_ConfDetail>();
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
            CreateMap<Sd_detail, Sd_detailOutputDto>();
            CreateMap<Sd_detailInputDto, Sd_detail>();
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
            CreateMap<Sys_conf_finalconf, Sys_conf_finalconfOutputDto>();
            CreateMap<Sys_conf_finalconfInputDto, Sys_conf_finalconf>();
            CreateMap<Sys_outmodel, Sys_outmodelOutputDto>();
            CreateMap<Sys_outmodelInputDto, Sys_outmodel>();
            CreateMap<Sys_outmodel_classify, Sys_outmodel_classifyOutputDto>();
            CreateMap<Sys_outmodel_classifyInputDto, Sys_outmodel_classify>();
            CreateMap<Sys_outmodel_details, Sys_outmodel_detailsOutputDto>();
            CreateMap<Sys_outmodel_detailsInputDto, Sys_outmodel_details>();
            CreateMap<Sys_outmodel_obj, Sys_outmodel_objOutputDto>();
            CreateMap<Sys_outmodel_objInputDto, Sys_outmodel_obj>();
            CreateMap<Sys_outmodel_sql, Sys_outmodel_sqlOutputDto>();
            CreateMap<Sys_outmodel_sqlInputDto, Sys_outmodel_sql>();

            CreateMap<Sys_sys, Sys_sysOutputDto>();
           CreateMap<Sys_sysInputDto, Sys_sys>();
           CreateMap<Work_work, Work_workOutputDto>();
           CreateMap<Work_workInputDto, Work_work>();
           CreateMap<Work_workdetail, Work_workdetailOutputDto>();
           CreateMap<Work_workdetailInputDto, Work_workdetail>();

        }
    }
}
