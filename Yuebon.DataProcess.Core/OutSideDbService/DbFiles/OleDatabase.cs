using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections;
using System.Linq;
using Yuebon.DataProcess.Core.OutSideDbService.Extension;
using Yuebon.DataProcess.Core.OutSideDbService.Entity;
using Yuebon.DataProcess.Core.PlugIServices;
using Yuebon.DataProcess.Core.OutSideDbService.Factory;
using Yuebon.DataProcess.Core.common;

namespace Yuebon.DataProcess.Core.OutSideDbService.DbFiles
{
    /// <summary>
    /// 描 述：操作数据库
    /// </summary>
    public class OleDatabase : IDatabase
    {
        #region 构造函数
        /// <summary>
        /// 构造方法
        /// <paramref name="connString">文件路径</paramref>
        /// </summary>
        public OleDatabase(string connString)
        {
            // connString格式为：“文件地址&^&0&^&1”
            string[] connStr = connString.Split(new[] { "&^&" }, StringSplitOptions.None);
            connectionString = connStr[0];
            headStartIndex = int.Parse(connStr[1]);
            headEndIndex = int.Parse(connStr[2]);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取 数据库连接串
        /// </summary>
        public string connectionString { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction dbTransaction { get; set; }
        /// <summary>
        /// 列头起始行
        /// </summary>
        public int headStartIndex { get; set; }
        /// <summary>
        /// 列头结束行
        /// </summary>
        public int headEndIndex { get; set; }


        #endregion

        #region 事物提交
        /// <summary>
        /// 无用
        /// </summary>
        /// <returns></returns>
        public IDatabase BeginTrans()
        {
            return null;
        }
        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int Commit()
        {
            return -1;
        }
        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            
        }
        /// <summary>
        /// 关闭连接 内存回收
        /// </summary>
        public void Close()
        {
            
        }
        #endregion

        #region 执行 SQL 语句
        /// <summary>
        /// 无用
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecuteBySql(string strSql)
        {
            return -1;
        }
        /// <summary>
        /// 无用
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            return -1;
        }
        /// <summary>
        /// 无用
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public int ExecuteByProc(string procName)
        {
            return -1;
        }
        /// <summary>
        /// 无用
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            return -1;
        }
        #endregion

        #region 数据源查询
        public DataTable FindTable(string sheetName)
        {
            return NPOIHelper.ReadExcelToDataTable(connectionString, sheetName);
        }
        /// <summary>
        /// 无用
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public DataTable FindTable(string sheetName, DbParameter[] dbParameter)
        {
            return null;
        }
        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            total = 0;
            return null;
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            total = 0;
            return null;
        }
        #endregion

        #region 操作方法

        #region 连接测试
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public bool ConnectionTest()
        {
            return FileHelper.FileExist(connectionString);
        }
        #endregion

        /// <summary>
        /// 无用
        /// </summary>
        /// <returns></returns>
        public List<DataBaseInfo> GetAllDataBases()
        {
            return null;
        }
        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="tableList"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName, string tableList)
        {
            ArrayList tbNameList = NPOIHelper.ReadExcelSheets(connectionString);
            List<string> cusTbNames = tableList.Split(',').ToList();
            List<DbTableInfo> dbTableList = new List<DbTableInfo>();
            foreach (string item in tbNameList)
            {
                if (cusTbNames.IndexOf(item) > 0)
                {
                    DataTable dt = NPOIHelper.ReadExcelToDataTable(connectionString, item);
                    dbTableList.Add(DataTableToDbTableInfo(dt));
                }
            }
            return dbTableList;
        }
        /// <summary>
        /// 分页获取数据库的所有表的信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="strwhere"></param>
        /// <param name="fieldNameToSort"></param>
        /// <param name="isDescending"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string dbName, string strwhere, string fieldNameToSort, bool isDescending, int PageSize, int CurrenetPageIndex,int RecordCount)
        {
            return null;
        }
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        public List<DbFieldInfo> GetAllColumns(string dbName, string tableName)
        {
            List<DbFieldInfo> list = new List<DbFieldInfo>();
            List<DbTableInfo> tbInfo = GetAllTables("", tableName);
            if (tbInfo != null && tbInfo.Count > 0)
            {
                list = tbInfo[0].Fileds;
                List<DbFieldInfo> reslist = new List<DbFieldInfo>();
                foreach (DbFieldInfo info in list)
                {
                    info.DataType = ConvertDataType(info);
                    reslist.Add(info);
                }
                return reslist;
            }
            else
            {
                return new List<DbFieldInfo>();
            }
           
        }
        #endregion

        #region 参数操作
        /// <summary>
        /// 获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public string CreateDbParmCharacter()
        {
            return "@";
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter()
        {
            return new MySqlParameter();
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter(string paramName, object value)
        {
            DbParameter param = CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            DbParameter param = CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 转换对应的数据库参数
        /// </summary>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public DbParameter[] ToDbParameter(DbParameter[] dbParameter)
        {
            int i = 0;
            int size = dbParameter.Length;
            DbParameter[] _dbParameter = new MySqlParameter[size];
            while (i < size)
            {
                _dbParameter[i] = new MySqlParameter(dbParameter[i].ParameterName, dbParameter[i].Value);
                i++;
            }
            return _dbParameter;
        }
        /// <summary>
        /// 转换对应的数据库语句中参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramSymbol">当前使用的参数符号</param>
        /// <param name="paramStr">当前使用的参数字符</param>
        /// <returns></returns>
        public string ToSqlDbParameter(string sql, string paramSymbol, string paramStr)
        {
            sql = sql.Replace(paramSymbol + paramStr, "@" + paramStr);
            return sql;
        }
        #endregion

        #region 辅助方法

        #region 字段转换
        /// <summary>
        /// 将字段信息的类型转换为C#信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string ConvertDataType(DbFieldInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            if (string.IsNullOrEmpty(info.FieldType))
                throw new ArgumentNullException(nameof(info.FieldType));
            info.DataType = SqlType2CsharpTypeStr(info.FieldType, info.IsNullable);
            return info.DataType;
        }

        /// <summary>
        /// 将数据库类型转为系统类型。
        /// </summary>
        /// <param name="sqlType">数据库字段类型</param>
        /// <param name="isNullable">字段是否可空</param>
        /// <returns></returns>
        private static string SqlType2CsharpTypeStr(string sqlType, bool isNullable = false)
        {
            if (string.IsNullOrEmpty(sqlType))
                throw new ArgumentNullException(nameof(sqlType));
            var val = string.Empty;
            var allowNull = false;
            switch (sqlType.ToLower())
            {
                case "bit":
                    val = "bool";
                    break;
                case "int":
                    val = "int";
                    break;
                case "smallint":
                    val = "short";
                    break;
                case "bigint":
                    val = "long";
                    break;
                case "tinyint":
                    val = "bool";
                    break;

                case "binary":
                case "image":
                case "varbinary":
                    val = "byte[]";
                    allowNull = true;
                    break;

                case "decimal":
                    val = "decimal";
                    break;
                case "numeric":
                case "money":
                case "smallmoney":
                    val = "decimal";
                    break;

                case "float":
                    val = "float";
                    break;
                case "real":
                    val = "Single";
                    break;

                case "datetime":
                    val = "DateTime";
                    break;
                case "smalldatetime":
                case "timestamp":
                    val = "DateTime";
                    break;

                case "uniqueidentifier":
                    val = "Guid";
                    break;
                case "Variant":
                    val = "object";
                    allowNull = true;
                    break;

                case "text":
                    val = "string";
                    allowNull = true;
                    break;
                case "ntext":
                    val = "string";
                    allowNull = true;
                    break;
                case "char":
                    val = "string";
                    allowNull = true;
                    break;
                case "nchar":
                    val = "string";
                    allowNull = true;
                    break;
                case "varchar":
                    val = "string";
                    allowNull = true;
                    break;
                case "nvarchar":
                    val = "string";
                    allowNull = true;
                    break;
                default:
                    val = "string";
                    allowNull = true;
                    break;
            }
            if (isNullable && !allowNull)
                return val + "?";
            return val;
        }
        #endregion

        /// <summary>
        /// 将数据库参数转换为Dapper专用的数据库参数类型
        /// </summary>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        private DynamicParameters ToDynamicParameters(DbParameter[] dbParameter)
        {
            DynamicParameters dynamicParameters = null;
            if (dbParameter != null && dbParameter.Length > 0)
            {
                dynamicParameters = new DynamicParameters();
                foreach (DbParameter item in dbParameter)
                {
                    dynamicParameters.Add(item.ParameterName, item.Value, item.DbType, item.Direction, item.Size);
                }
            }
            return dynamicParameters;
        }

        /// <summary>
        /// 表格转为表结构实体类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DbTableInfo DataTableToDbTableInfo(DataTable dt)
        {
            DbTableInfo info = new DbTableInfo();
            info.TableName = dt.TableName;
            foreach (DataColumn item in dt.Columns)
            {
                DbFieldInfo field = new DbFieldInfo();
                field.FieldName = item.ColumnName;
                field.DataType = ConvertDataType(field); 
                info.Fileds.Add(field);
            }
            return info;
        }
        #endregion

    }
}
