using Yuebon.DataProcess.Core.OutSideDbService.DbFiles;
using Yuebon.DataProcess.Core.PlugIServices;

namespace Yuebon.DataProcess.Core.OutSideDbService.Factory
{
    /// <summary>
    /// 描 述：数据库建立工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        public static IDatabase Base(string connString, string DbType)
        {
            switch (DbType)
            {
                case "MySql":
                    return new MySqlDatabase(connString);
                case "SqlServer":
                    return new SqlDatabase(connString);
                case "Oracle":
                    return new OracleDatabase(connString);
                default:
                    return null;
                    break;
            }
        }
    }
}
