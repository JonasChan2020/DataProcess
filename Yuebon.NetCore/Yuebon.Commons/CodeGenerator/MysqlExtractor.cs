using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuebon.Commons.Pages;

namespace Yuebon.Commons.CodeGenerator
{
    /// <summary>
    /// MYSQL
    /// </summary>
    public class MysqlExtractor : DbExtractorAbstract
    {
        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="tablelist"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string tablelist)
        {
            DbConnection conn = OpenSharedConnection();
            string dbName = conn.Database;
            var sql = string.Format(@"SELECT tbs.TABLE_NAME as TableName ,tbs.TABLE_COMMENT as Description FROM information_schema.tables tbs
where tbs.TABLE_SCHEMA='" + dbName.ToLower() + "' and tbs.TABLE_TYPE='BASE TABLE'");
            if (!string.IsNullOrEmpty(tablelist))
            {
                sql += string.Format(@" and tbs.TABLE_NAME in('{0}')", tablelist.Replace(",", "','"));
            }
            return GetAllTablesInternal(sql);
        }


        /// <summary>
        /// 获取数据库的所有表的信息
        /// </summary>
        /// <param name="strwhere"></param>
        /// <param name="fieldNameToSort"></param>
        /// <param name="isDescending"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<DbTableInfo> GetAllTables(string strwhere, string fieldNameToSort, bool isDescending, PagerInfo info)
        {
            DbConnection conn = OpenSharedConnection();
            string dbName = conn.Database;
            var sql = string.Format(@"SELECT tbs.TABLE_NAME as TableName ,tbs.TABLE_COMMENT as Description FROM information_schema.tables tbs
where tbs.TABLE_SCHEMA='" + dbName.ToLower() + "' and tbs.TABLE_TYPE='BASE TABLE'");

            string sqlcount = string.Format(@"select count(*) as Total from({0}) AA where {1}", sql, strwhere);

            string strOrder = string.Format(" order by {0} {1}", fieldNameToSort, isDescending ? "DESC" : "ASC");
            int minRow = info.PageSize * (info.CurrenetPageIndex - 1) + 1;
            int maxRow = info.PageSize * info.CurrenetPageIndex;

            string pagesql = string.Format(@"With Paging AS
                ( SELECT ROW_NUMBER() OVER ({0}) as RowNumber, {1} FROM ({2}) AA Where {3})
                SELECT * FROM Paging WHERE RowNumber Between {4} and {5}", strOrder, "AA.*", sql, strwhere,
            minRow, maxRow);
            pagesql = sqlcount + ";" + pagesql;
            return GetAllTablesInternal(pagesql, info);
        }
        /// <summary>
        /// 获取表的所有字段名及字段类型
        /// </summary>
        /// <param name="tableName">数据表的名称</param>
        /// <returns></returns>
        public List<DbFieldInfo> GetAllColumns(string tableName)
        {
            if (tableName == null)
                throw new ArgumentNullException(nameof(tableName));

            DbConnection conn = OpenSharedConnection();
            string dbName = conn.Database;

            var sqlFields = string.Format(@"
               SELECT
	COLUMN_NAME as FieldName,
	(case when COLUMN_KEY='PRI' then '1'else '0' end) as Increment,
	(case when COLUMN_KEY='PRI' then '1'else '0' end) as IsIdentity,
	DATA_TYPE as FieldType,
	CHARACTER_OCTET_LENGTH AS FieldPrecision,
	CHARACTER_MAXIMUM_LENGTH AS FieldMaxLength,
    (case when CHARACTER_OCTET_LENGTH is null then '0'else CHARACTER_OCTET_LENGTH end) as FieldPrecision,
    (case when CHARACTER_MAXIMUM_LENGTH is null then '0'else CHARACTER_MAXIMUM_LENGTH end) as FieldScale,
    (case when NUMERIC_SCALE is null then '0'else NUMERIC_SCALE end) as FieldScale,
    (case when IS_NULLABLE='YES' then '1'else '0' end) as IsNullable,
    COLUMN_DEFAULT as FieldDefaultValue,
	COLUMN_COMMENT AS Description
	
FROM
	information_schema.COLUMNS 
WHERE
	table_schema = '{1}' 
	AND table_name = '{0}' 
ORDER BY
	ORDINAL_POSITION", tableName, dbName);
            List<DbFieldInfo> list = new List<DbFieldInfo>();
            list = GetAllColumnsInternal(sqlFields);
            List<DbFieldInfo> reslist = new List<DbFieldInfo>();
            foreach (DbFieldInfo info in list)
            {
                info.DataType = ConvertDataType(info);
                reslist.Add(info);
            }
            return reslist;
        }


        #region 字段转换
        //所有类型转换 http://www.cnblogs.com/vjine/p/3462167.html
        /// <summary>
        /// 将字段信息的类型转换为C#信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string ConvertDataType(DbFieldInfo info)
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
        /// <param name="sqlType"></param>
        /// <param name="isNullable">字段是否可空</param>
        /// <returns></returns>
        public static string SqlType2CsharpTypeStr(string sqlType, bool isNullable = false)
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
                    val = "byte";
                    break;

                case "binary":
                case "image":
                case "varbinary":
                case "mediumblob":
                    val = "byte[]";
                    allowNull = true;
                    break;

                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    val = "decimal";
                    break;

                case "float":
                case "double":
                    val = "float";
                    break;
                case "real":
                    val = "Single";
                    break;

                case "date":
                case "datetime":
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
                case "ntext":
                case "mediumtext":
                case "longtext":
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
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
    }
}
