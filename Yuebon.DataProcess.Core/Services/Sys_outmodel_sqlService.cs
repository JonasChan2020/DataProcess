using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuebon.Commons.Dtos;
using Yuebon.Commons.Mapping;
using Yuebon.Commons.Pages;
using Yuebon.Commons.Services;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 数据输出模型最终查询语句服务接口实现
    /// </summary>
    public class Sys_outmodel_sqlService: BaseService<Sys_outmodel_sql,Sys_outmodel_sqlOutputDto, string>, ISys_outmodel_sqlService
    {
		private readonly ISys_outmodel_sqlRepository _repository;
        public Sys_outmodel_sqlService(ISys_outmodel_sqlRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}