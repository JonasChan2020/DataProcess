using System;
using Yuebon.Commons.Services;
using Yuebon.Security.Dtos;
using Yuebon.Security.IRepositories;
using Yuebon.Security.IServices;
using Yuebon.Security.Models;

namespace Yuebon.Security.Services
{
    public class UserLogOnService: BaseService<UserLogOn, UserLogOnOutputDto, string>, IUserLogOnService
    {
        private readonly IUserLogOnRepository _userLogOnRepository;
        private readonly ILogService _logService;
        public UserLogOnService(IUserLogOnRepository repository, ILogService logService) : base(repository)
        {
            _userLogOnRepository = repository;
            _logService = logService;
        }

        /// <summary>
        /// ���ݻ�ԱID��ȡ�û���¼��Ϣʵ��
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
       public UserLogOn GetByUserId(string userId)
        {
           return _userLogOnRepository.GetByUserId(userId);
        }

    }
}