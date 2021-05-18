using System.Collections.Generic;

namespace Yuebon.DataProcess.Core.OutSideDbService.Entity
{
    public class DbRowInfo
    {
        /// <summary>
        /// 实体类行序号（动态表合并用）
        /// </summary>
        public int listRowIndex { get; set; }
        /// <summary>
        /// 行序号
        /// </summary>
        public int rowIndex { get; set; }
        /// <summary>
        /// 行标识
        /// </summary>
        public string gpid { get; set; }
        /// <summary>
        /// s数据库内行标识
        /// </summary>
        public string dbGpid { get; set; }
        /// <summary>
        /// 列集合
        /// </summary>
        public List<DbColums> colList { get; set; }

        #region 列信息
        public class DbColums : CustomEntityBase
        {
            /// <summary>
            /// 列序号
            /// </summary>
            public int colIndex { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public string values { get; set; }
        }

        #endregion
    }
}
