using Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension;

namespace Yuebon.DataProcess.Core.common.dbTools.Factory
{
    /// <summary>
    /// 描 述：定义仓储模型工厂
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public IRepository BaseRepository(string connString, DatabaseType dataType)
        {
            return new Repository(DbFactory.Base(connString, dataType));
        }
        
    }
}
