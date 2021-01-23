using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;
using Yuebon.DataProcess.Core.common.dbTools.baseDb.Extension;

namespace Yuebon.DataProcess.Core.common.dbTools.baseDb
{
    public class DatabaseCommon
    {
        #region 对象参数转换DbParameter
        /// <summary>
        /// 对象参数转换DbParameter
        /// </summary>
        /// <returns></returns>
        public static DbParameter[] GetParameter<T>(T entity)
        {
            IList<DbParameter> parameter = new List<DbParameter>();
            DbType dbtype = new DbType();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo pi in props)
            {
                if (pi.GetValue(entity, null) != null)
                {
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.Nullable`1[System.Int32]":
                            dbtype = DbType.Int32;
                            break;
                        case "System.Nullable`1[System.Decimal]":
                            dbtype = DbType.Decimal;
                            break;
                        case "System.Nullable`1[System.DateTime]":
                            dbtype = DbType.DateTime;
                            break;
                        default:
                            dbtype = DbType.String;
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter(DbParameters.CreateDbParmCharacter() + pi.Name, pi.GetValue(entity, null), dbtype));
                }
            }
            return parameter.ToArray();
        }
        /// <summary>
        /// 对象参数转换DbParameter
        /// </summary>
        /// <returns></returns>
        public static DbParameter[] GetParameter(Hashtable ht)
        {
            IList<DbParameter> parameter = new List<DbParameter>();
            DbType dbtype = new DbType();
            foreach (string key in ht.Keys)
            {
                if (ht[key] is DateTime)
                    dbtype = DbType.DateTime;
                else
                    dbtype = DbType.String;
                parameter.Add(DbParameters.CreateDbParameter(DbParameters.CreateDbParmCharacter() + key, ht[key], dbtype));
            }
            return parameter.ToArray();
        }
        #endregion

        #region 获取实体类自定义信息
        
        /// <summary>
        /// 获取实体类 字段中文名称
        /// </summary>
        /// <param name="pi">字段属性信息</param>
        /// <returns></returns>
        public static string GetFieldText(PropertyInfo pi)
        {
            DisplayNameAttribute descAttr;
            string txt = "";
            var descAttrs = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (descAttrs.Any())
            {
                descAttr = descAttrs[0] as DisplayNameAttribute;
                txt = descAttr.DisplayName;
            }
            else
            {
                txt = pi.Name;
            }
            return txt;
        }
        /// <summary>
        /// 获取实体类中文名称
        /// </summary>
        /// <returns></returns>
        public static string GetClassName<T>()
        {
            Type objTye = typeof(T);
            string entityName = "";
            var busingessNames = objTye.GetCustomAttributes(true).OfType<DisplayNameAttribute>();
            var descriptionAttributes = busingessNames as DisplayNameAttribute[] ?? busingessNames.ToArray();
            if (descriptionAttributes.Any())
                entityName = descriptionAttributes.ToList()[0].DisplayName;
            else
            {
                entityName = objTye.Name;
            }
            return entityName;
        }
        #endregion

        #region 拼接 Insert SQL语句
        /// <summary>
        /// 哈希表生成Insert语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <returns>int</returns>
        public static StringBuilder InsertSql(string tableName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                {
                    sb_prame.Append("," + key);
                    sp.Append("," + DbParameters.CreateDbParmCharacter() + "" + key);
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb;
        }
        #endregion

        #region 拼接 Update SQL语句
        /// <summary>
        /// 哈希表生成UpdateSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="pkName">主键</param>
        /// <returns></returns>
        public static StringBuilder UpdateSql(string tableName, Hashtable ht, string pkName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null && pkName != key)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(DbParameters.CreateDbParmCharacter() + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append(DbParameters.CreateDbParmCharacter() + key);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append(DbParameters.CreateDbParmCharacter() + pkName);
            return sb;
        }
        /// <summary>
        /// 哈希表生成UpdateSql语句,无主键
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="pkNames">唯一判定项</param>
        /// <returns></returns>
        public static StringBuilder UpdateSqlNoPk(string tableName, Hashtable ht, string[] pkNames,string keyColumn)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null && !pkNames.Contains(key)&&key!=keyColumn)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(DbParameters.CreateDbParmCharacter() + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append(DbParameters.CreateDbParmCharacter() + key);
                    }
                }
            }
            sb.Append(" Where ");
            for (int i = 0; i < pkNames.Length; i++)
            {
                sb.Append(pkNames[i]).Append("=").Append(DbParameters.CreateDbParmCharacter() + pkNames[i]);
                if(i!=pkNames.Length-1)
                {
                    sb.Append(" and ");
                }
            }

            return sb;
        }
        #endregion

        #region 拼接 Delete SQL语句
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <returns></returns>
        public static StringBuilder DeleteSql(string tableName)
        {
            return new StringBuilder("Delete From " + tableName);
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <returns></returns>
        public static StringBuilder DeleteSql(string tableName, string pkName)
        {
            return new StringBuilder("Delete From " + tableName + " Where " + pkName + " = " + DbParameters.CreateDbParmCharacter() + pkName + "");
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">多参数</param>
        /// <returns></returns>
        public static StringBuilder DeleteSql(string tableName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where 1=1");
            foreach (string key in ht.Keys)
            {
                sb.Append(" AND " + key + " = " + DbParameters.CreateDbParmCharacter() + "" + key + "");
            }
            return sb;
        }
       
        #endregion

        #region 拼接 Select SQL语句
        /// <summary>
        /// 拼接 查询 SQL语句
        /// </summary>
        /// <returns></returns>
        public static StringBuilder SelectSql<T>() where T : new()
        {
            string tableName = typeof(T).Name;
            PropertyInfo[] props = GetProperties(new T().GetType());
            StringBuilder sbColumns = new StringBuilder();
            foreach (PropertyInfo prop in props)
            {
                string propertytype = prop.PropertyType.ToString();
                sbColumns.Append(prop.Name + ",");
            }
            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);
            string strSql = "SELECT {0} FROM {1} WHERE 1=1 ";
            strSql = string.Format(strSql, sbColumns.ToString(), tableName + " ");
            return new StringBuilder(strSql);
        }
        
        /// <summary>
        /// 拼接 查询 SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static StringBuilder SelectSql(string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1 ");
            return strSql;
        }
        /// <summary>
        /// 拼接 查询 SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static StringBuilder SelectSql(string tableName,Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1 ");
            return strSql;
        }
        /// <summary>
        /// 拼接 查询 SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static StringBuilder SelectSql(string tableName,string colList,Hashtable ht)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1 ");
            return strSql;
        }
        /// <summary>
        /// 拼接 查询 SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        public static StringBuilder SelectSql(string tableName, int Top)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top " + Top + " * FROM " + tableName + " WHERE 1=1 ");
            return strSql;
        }
        #endregion

        #region 扩展
        /// <summary>
        /// 获取访问元素
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        #endregion
    }
}
