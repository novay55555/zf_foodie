using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Odbc;
using MySql.Data.Types;
using MySql.Data.MySqlClient;
using Foodie.Parameters;

namespace Foodie.DataBase
{
    public class foodieConnection
    {
        XmlHelper xml = new XmlHelper();

        private SqlDatabase db = null;
        private static readonly Object locker = new Object();
        /// <summary>
        /// 取得单身实例
        /// </summary>
        public void GetInstance()
        {
            //在并发时，使用单一对象
            if (db == null)
            {
                db = new SqlDatabase(xml.GetXmlConfigValue("SqlServer_Foodie"));
            }
            else
            {
                lock (locker)
                {
                    //return db;
                }
            }
        }

        /// <summary>
        /// 查询，根据sql语句获取DataTable数据表（带参数）
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTableBySql(StringBuilder strSql, SqlParam[] param)
        {
            string SqlServer_Foodie_xml = xml.GetXmlConfigValue("SqlServer_Foodie");

            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());

                if (param != null)
                {
                    DbType dbtype = new DbType();
                    foreach (SqlParam _param in param)
                    {
                        if (_param.FiledValue is DateTime)
                            dbtype = DbType.DateTime;
                        else
                            dbtype = DbType.AnsiString;
                        db.AddInParameter(dbCommand, _param.FieldName, dbtype, _param.FiledValue);
                    }
                }

                IDataReader reader = db.ExecuteReader(dbCommand);
                DataTable objDataTable = new DataTable("Table");
                int intFieldCount = reader.FieldCount;
                for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
                {
                    objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
                }
                objDataTable.BeginLoadData();
                object[] objValues = new object[intFieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    objDataTable.LoadDataRow(objValues, true);
                }
                reader.Close();
                objDataTable.EndLoadData();
                return objDataTable;

            }
            catch (Exception e)
            {
                return null;
            }
        }


        /// <summary>
        /// 根据sql语句和参数，执行事务
        /// </summary>
        /// <param name="sqls">sql语句</param>
        /// <param name="_params">参数</param>
        /// <returns></returns>
        public int ExecuteByTransaction(object[] sqls, object[] _params)
        {
            int num = 0;
            DbTransaction dbTransaction = null;
            GetInstance();
            DbConnection connection = db.CreateConnection();

            if (xml.GetXmlConfigValue("ComponentDbType") == "SQLServer")
            {
                connection.Open();
                dbTransaction = connection.BeginTransaction();
                try
                {
                    for (int i = 0; i < sqls.Length; i++)
                    {
                        StringBuilder builder = (StringBuilder)sqls[i];
                        if (builder != null)
                        {

                            DbCommand sqlStringCommand = db.GetSqlStringCommand(builder.ToString());

                            if (_params[i] != null)
                            {
                                DbType dbtype = new DbType();
                                foreach (SqlParam _param in (SqlParam[])_params[i])
                                {
                                    if (_param.FiledValue is DateTime)
                                    {
                                        dbtype = DbType.DateTime;
                                    }
                                    else
                                    {
                                        dbtype = DbType.AnsiString;
                                    }
                                    db.AddInParameter(sqlStringCommand, _param.FieldName, dbtype, _param.FiledValue);
                                }
                            }
                            db.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                        }
                    }
                    dbTransaction.Commit();
                    num = 1;
                }
                catch (Exception e)
                {
                    num = -1;
                    dbTransaction.Rollback();
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                    dbTransaction.Dispose();
                }
            }

            return num;
        }

        /// <summary>
        ///  根据SQL执行,带参数,不带事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns>object</returns>
        public int ExecuteBySqlNotTran(StringBuilder sql, SqlParam[] param)
        {
            int num = 0;
            GetInstance();
            try
            {
                DbCommand dbCommand = db.GetSqlStringCommand(sql.ToString());

                if (param != null)
                {
                    DbType dbtype = new DbType();
                    foreach (SqlParam _param in param)
                    {
                        if (_param.FiledValue is DateTime)
                        {
                            dbtype = DbType.DateTime;
                        }
                        else
                        {
                            dbtype = DbType.AnsiString;
                        }
                        db.AddInParameter(dbCommand, _param.FieldName, dbtype, _param.FiledValue);
                    }
                }

                num = db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception e)
            {
                num = -1;
            }
            return num;
        }

    }
}
