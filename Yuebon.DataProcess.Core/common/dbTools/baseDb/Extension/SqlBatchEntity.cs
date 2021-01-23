using System.Data.Common;

namespace Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension
{
    public class SqlBatchEntity
    {
        /// <summary>
        /// 参数语句
        /// </summary>
        public string sqlStr { get; set; }
        /// <summary>
        /// 参训所用参数
        /// </summary>
        public DbParameter[] dpList { get; set; }

        /// <summary>
        /// 行序号
        /// </summary>
        public int rowNum { get; set; }
        /// <summary>
        /// 相同查询的行序号集合字符串
        /// </summary>
        public string sameValueRowNum { get; set; }
        /// <summary>
        /// 查询结果
        /// </summary>
        public object finalValue { get; set; }

        /// <summary>
        ///  是否正确执行
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        ///  错误信息
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 条件1
        /// </summary>
        public object condition1 { get; set; }
        /// <summary>
        /// 条件2
        /// </summary>
        public object condition2 { get; set; }
        /// <summary>
        /// 条件3
        /// </summary>
        public object condition3 { get; set; }
        /// <summary>
        /// 条件4
        /// </summary>
        public object condition4 { get; set; }
        /// <summary>
        /// 条件5
        /// </summary>
        public object condition5 { get; set; }
        /// <summary>
        /// 条件6
        /// </summary>
        public object condition6 { get; set; }
        /// <summary>
        /// 条件7
        /// </summary>
        public object condition7 { get; set; }
        /// <summary>
        /// 条件8
        /// </summary>
        public object condition8 { get; set; }
        /// <summary>
        /// 条件9
        /// </summary>
        public object condition9 { get; set; }
        /// <summary>
        /// 条件10
        /// </summary>
        public object condition10 { get; set; }
    }
}
