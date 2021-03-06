using Yuebon.Commons.Services;
using Yuebon.DataProcess.IRepositories;
using Yuebon.DataProcess.IServices;
using Yuebon.DataProcess.Dtos;
using Yuebon.DataProcess.Models;

namespace Yuebon.DataProcess.Services
{
    /// <summary>
    /// 目标库详情服务接口实现
    /// </summary>
    public class Sd_detailService: BaseService<Sd_detail,Sd_detailOutputDto, string>, ISd_detailService
    {
		private readonly ISd_detailRepository _repository;
        public Sd_detailService(ISd_detailRepository repository) : base(repository)
        {
			_repository=repository;
        }
    }
}