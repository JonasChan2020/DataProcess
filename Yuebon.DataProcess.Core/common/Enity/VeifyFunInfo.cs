using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.DataProcess.Core.common.Enity
{
    /// <summary>
    /// 验证实体类
    /// </summary>
    public class VeifyFunInfo
    {
        public string colName { get; set; }
        public string NOTNULLS { get; set; }
        public List<VerifySubFun> colVerifyInfo { get; set; }

    }

    /// <summary>
    /// 验证方法实体类
    /// </summary>
    public class VerifySubFun
    {
        public string verId { get; set; }
        public string colName { get; set; }
        public string stepNum { get; set; }
        public string ISINSTEP { get; set; }
        public string ErrorMsg { get; set; }
        public string STEPVALUE { get; set; }
        public string COLVALUE { get; set; }
    }
}
