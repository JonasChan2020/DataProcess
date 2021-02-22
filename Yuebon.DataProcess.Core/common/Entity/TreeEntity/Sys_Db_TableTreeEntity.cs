using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuebon.DataProcess.Core.common.Entity.TreeEntity
{
    /// <summary>
    /// 系统或库与表的树（用于配置页面）
    /// </summary>
    public class Sys_Db_TableTreeEntity
    {

        /// <summary>
        /// 树节点元素ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 树节点元素名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 树节点元素描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 树节点元素类型
        /// </summary>
        public string NodeType { get; set; }
        /// <summary>
        /// 树节点元素类型
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 树节点元素子节点集合
        /// </summary>
        public List<Sys_Db_TableTreeEntity> Children { get; set; }
    }
}
