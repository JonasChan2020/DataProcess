using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuebon.AspNetCore.Controllers;

namespace Yuebon.WebApi.Areas.DataProcess.Controllers
{
    /// <summary>
    /// 数据导出接口
    /// </summary>
    [Route("api/DataProcess/[controller]")]
    [ApiController]
    public class DataExpController : ApiController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
