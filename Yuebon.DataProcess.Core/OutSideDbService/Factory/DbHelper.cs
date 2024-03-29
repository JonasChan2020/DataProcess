﻿using System;
using System.Data;
using System.Data.Common;
using Yuebon.DataProcess.Core.OutSideDbService.Extension;

namespace Yuebon.DataProcess.Core.OutSideDbService.Factory
{
    /// <summary>
    /// 描 述：数据库访问扩展
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DatabaseType DbType { get; set; }

        #region 构造函数
        /// <summary>
        /// 构造方法
        /// </summary>
        public DbHelper(DbConnection _dbConnection)
        {
            dbConnection = _dbConnection;
            dbCommand = dbConnection.CreateCommand();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private DbConnection dbConnection { get; set; }
        /// <summary>
        /// 执行命令对象
        /// </summary>
        private IDbCommand dbCommand { get; set; }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// 执行SQL返回 DataReader
        /// </summary>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandType cmdType, string strSql)
        {
            return ExecuteReader(cmdType, strSql, null);
        }
        /// <summary>
        /// 执行SQL返回 DataReader
        /// </summary>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="strSql">Sql语句</param>
        /// <param name="dbParameter">Sql参数</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandType cmdType, string strSql, params DbParameter[] dbParameter)
        {
            try
            {
                PrepareCommand(dbConnection, dbCommand, null, cmdType, strSql, dbParameter);
                IDataReader rdr = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception ex)
            {
                Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL返回 DataReader
        /// </summary>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="strSql">Sql语句</param>
        /// <param name="dbParameter">Sql参数</param>
        /// <returns></returns>
        //public List<SqlBatchEntity> ExecuteReaderBatch(CommandType cmdType, List<SqlBatchEntity> sbcEntity)
        //{
        //    try
        //    {
        //        for (int i = 0; i < sbcEntity.Count; i++)
        //        {
        //            SqlBatchEntity tmp = sbcEntity.Find(x => x.rowNum < i && x.sqlStr == sbcEntity[i].sqlStr && x.dpList == sbcEntity[i].dpList);
        //            if (tmp != null)
        //            {
        //                sbcEntity[i].finalValue = tmp.finalValue;
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    using (IDbCommand cmd = dbConnection.CreateCommand())
        //                    {
        //                        if (dbConnection.State != ConnectionState.Open)
        //                            dbConnection.Open();
        //                        cmd.Connection = dbConnection;
        //                        cmd.CommandText = sbcEntity[i].sqlStr;
        //                        cmd.CommandType = cmdType;
        //                        if (sbcEntity[i].dpList != null)
        //                        {
        //                            sbcEntity[i].dpList = DbParameters.ToDbParameter(sbcEntity[i].dpList);
        //                            foreach (var parameter in sbcEntity[i].dpList)
        //                            {
        //                                cmd.Parameters.Add(parameter);
        //                            }
        //                        }

        //                        IDataReader rdr = cmd.ExecuteReader();
        //                        sbcEntity[i].finalValue = ConvertExtension.IDataReaderToDataTable((IDataReader)rdr);
        //                        sbcEntity[i].IsSuccess = true;
        //                    }
                           
        //                }
        //                catch (Exception ex)
        //                {
        //                    sbcEntity[i].IsSuccess = false;
        //                    sbcEntity[i].ErrorMsg = ex.Message.ToString() + "具体位置：" + ex.StackTrace.ToString();
        //                }
                        
        //            }
        //        }
                
        //        Close();
        //        return sbcEntity;
        //    }
        //    catch (Exception)
        //    {
        //        Close();
        //        throw;
        //    }
        //}
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集
        /// </summary>
        /// <param name="cmdType">命令的类型</param>
        /// <param name="strSql">Sql语句</param>
        /// <param name="dbParameter">Sql参数</param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            try
            {
                PrepareCommand(dbConnection, dbCommand, null, cmdType, cmdText, parameters);
                object val = dbCommand.ExecuteScalar();
                dbCommand.Parameters.Clear();
                return val;
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }
        /// <summary>
        /// 为即将执行准备一个命令
        /// </summary>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="cmd">SqlCommand对象</param>
        /// <param name="isOpenTrans">DbTransaction对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行, e.g. Select * from Products</param>
        /// <param name="dbParameter">执行命令所需的sql语句对应参数</param>
        private void PrepareCommand(DbConnection conn, IDbCommand cmd, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, params DbParameter[] dbParameter)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (dbParameter != null)
            {
                foreach (var parameter in dbParameter)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }
    }
}
