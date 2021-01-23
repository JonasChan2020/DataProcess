using System.Configuration;
using Yuebon.DataProcess.Core.common.dbTools.baseDb;
using Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension;
using Yuebon.DataProcess.Core.common.dbTools.dbs;

namespace Yuebon.DataProcess.Core.common.dbTools.Factory
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
        public static IDatabase Base(string connString, DatabaseType DbType)
        {
            switch (DbType)
            {
                case DatabaseType.MySql:
                    return new MySqlDatabase(connString);
                case DatabaseType.SqlServer:
                    return new SqlDatabase(connString);
                case DatabaseType.Oracle:
                    return new OracleDatabase(connString);
                default:
                    return null;
                    break;
            }
        }
    }
}
