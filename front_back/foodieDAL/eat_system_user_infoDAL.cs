using Foodie.Entity;
using Foodie.Parameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Foodie.DataBase;

namespace Foodie.Service
{
    public class eat_system_user_infoDAL
    {

        XmlHelper xml = new DataBase.XmlHelper();
        object obj = new object();
        Type type = null;
        public eat_system_user_infoDAL()
        {
            string ClassName = "";
            string DataBaseType = xml.GetXmlConfigValue("ComponentDbType");
            if (DataBaseType == "SQLServer_Foodie")
            {
                ClassName = "foodieConnection";
            }
            else if (DataBaseType == "MySQl_Foodie")
            {
                ClassName = "MySqlCs";
            }
            Assembly assembly = Assembly.Load("FoodieConnection");
            obj = assembly.CreateInstance("Foodie.DataBase." + ClassName);
            type = obj.GetType();
        }


        #region Method
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public int Update(eat_system_user_info entity)
        {
            Type entityType = entity.GetType();
            PropertyInfo[] props = entityType.GetProperties();
            StringBuilder sql = new StringBuilder();
            sql.Append("update eat_system_user_info set ");
            List<SqlParam> param = new List<SqlParam>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    sql.Append(prop.Name + "=@" + prop.Name + ",");

                    param.Add(new SqlParam("@" + prop.Name, prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]" ? DbType.DateTime : DbType.AnsiString, prop.GetValue(entity, null)));
                }
            }
            if (sql[sql.Length - 1] == ',')
            {
                sql.Remove(sql.Length - 1, 1);
            }
            sql.Append(" where pk_id=@pk_id");

            object[] methodParams = new object[2];
            methodParams[0] = sql;
            methodParams[1] = param.ToArray();
            return (int)type.GetMethod("ExecuteBySqlNotTran").Invoke(obj, methodParams); ;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public int Delete(string KeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from eat_system_user_info where pk_id=@pk_id");
            SqlParam[] param = { new SqlParam("@pk_id", KeyValue) };
            object[] methodParams = new object[2];
            methodParams[0] = sql;
            methodParams[1] = param;
            return (int)type.GetMethod("ExecuteBySqlNotTran").Invoke(obj, methodParams); ;
        }
        /// <summary>
        /// 获得DataTable数据列表(带条件)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public DataTable GetDataTableWhere(StringBuilder where, SqlParam[] param)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from eat_system_user_info where 1=1");
            sql.Append(where);
            object[] methodParams = new object[2];
            methodParams[0] = sql;
            methodParams[1] = param;
            return (DataTable)type.GetMethod("GetDataTableBySql").Invoke(obj, methodParams); ;
        }

        public int SaveList(List<eat_system_user_info> inList, List<eat_system_user_info> upList)
        {
            List<object> sqls = new List<object>();
            List<object> _params = new List<object>();
            for (int i = 0; i < inList.Count; i++)
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlValue = new StringBuilder();
                List<SqlParam> param = new List<SqlParam>();
                sql.Append("insert into eat_system_user_info (");
                PropertyInfo[] props = typeof(eat_system_user_info).GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetValue(inList[i], null) != null)
                    {
                        sql.Append(prop.Name + ",");
                        sqlValue.Append("@" + prop.Name + ",");
                        param.Add(new SqlParam("@" + prop.Name, prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]" ? DbType.DateTime : DbType.AnsiString, prop.GetValue(inList[i], null)));
                    }
                }
                if (sqlValue.Length > 0)
                {
                    sqlValue.Remove(sqlValue.Length - 1, 1);
                    sql.Remove(sql.Length - 1, 1);
                }
                sql.Append(") values (" + sqlValue + ")");
                sqls.Add(sql);
                _params.Add(param.ToArray());
            }
            for (int i = 0; i < upList.Count; i++)
            {
                ;
                PropertyInfo[] props = typeof(eat_system_user_info).GetProperties();
                StringBuilder sql = new StringBuilder();
                sql.Append("update eat_system_user_info set ");
                List<SqlParam> param = new List<SqlParam>();
                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetValue(upList[i], null) != null)
                    {
                        sql.Append(prop.Name + "=@" + prop.Name + ",");

                        param.Add(new SqlParam("@" + prop.Name, prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]" ? DbType.DateTime : DbType.AnsiString, prop.GetValue(upList[i], null)));
                    }
                }
                if (sql[sql.Length - 1] == ',')
                {
                    sql.Remove(sql.Length - 1, 1);
                }
                sql.Append(" where pk_id=@pk_id");
                sqls.Add(sql);
                _params.Add(param.ToArray());
            }
            object[] methodParams = new object[2];
            methodParams[0] = sqls.ToArray();
            methodParams[1] = _params.ToArray();
            return (int)type.GetMethod("ExecuteByTransaction").Invoke(obj, methodParams); ;
        }
        #endregion
    }
}
