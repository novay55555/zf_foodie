using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.Types;
using MySql.Data.MySqlClient;
using System.Data;
using Foodie.Parameters;

namespace Foodie.DataBase
{
    public class MySqlCs
    {
        private string ConStr()
        {
            XmlHelper xml = new XmlHelper();
            return xml.GetXmlConfigValue("MySQl_Foodie");
        }

        /// <summary>
        /// 查询，根据sql语句获取DataTable数据表（带参数）
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTableBySql(StringBuilder strSql, SqlParam[] param)
        {
            MySqlParameter[] cmdParms = new MySqlParameter[param.Length];
            for (int i = 0; i < param.Length; i++)
            {
                cmdParms[i] = new MySqlParameter(param[i].FieldName.Replace("@", "?"), param[i].FiledValue);
            }
            DataSet ds = MySqlHelper.ExecuteDataSet(ConStr(), (CommandType)Enum.Parse(typeof(CommandType), "1"), strSql.Replace("@", "?").ToString(), cmdParms);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0];
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
            int num = -1;
            using (MySqlConnection conn = new MySqlConnection(ConStr()))
            {
                conn.Open();
                //启动一个事务
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        try
                        {
                            cmd.Transaction = transaction;  //为命令指定事务

                            for (int i = 0; i < sqls.Length; i++)
                            {
                                StringBuilder sql = new StringBuilder();
                                sql = (StringBuilder)sqls[i];
                                cmd.CommandText = sql.Replace("@", "?").ToString();
                                SqlParam[] param = (SqlParam[])_params[i];
                                cmd.Parameters.Clear();
                                for (int j = 0; j < param.Length; j++)
                                {
                                    cmd.Parameters.Add(new MySqlParameter(param[j].FieldName.Replace("@", "?"), param[j].FiledValue));
                                }
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();   //事务提交
                            num = 1;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); //事务回滚
                            num = -1;
                        }

                    }
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
        public int ExecuteBySqlNotTran(StringBuilder strSql, SqlParam[] param)
        {
            MySqlParameter[] cmdParms = new MySqlParameter[param.Length];
            for (int i = 0; i < param.Length; i++)
            {
                cmdParms[i] = new MySqlParameter(param[i].FieldName.Replace("@", "?"), param[i].FiledValue);
            }
            return MySqlHelper.ExecuteNonQuery(ConStr(), (CommandType)Enum.Parse(typeof(CommandType), "1"), strSql.Replace("@", "?").ToString(), cmdParms);
        }

    }
}