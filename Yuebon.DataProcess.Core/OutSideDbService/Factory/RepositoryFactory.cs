namespace Yuebon.DataProcess.Core.OutSideDbService.Factory
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
        public IRepository BaseRepository(string connString, string dataType)
        {
            return new Repository(DbFactory.Base(connString, dataType));
        }
        
    }
}
