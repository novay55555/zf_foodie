using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foodie.DataBase;
using System.Data;
using Foodie.DataBase;
using System.Reflection;
using Foodie.Parameters;

namespace Foodie.Service
{
    public class foodieDAL
    {
        XmlHelper xml = new DataBase.XmlHelper();
        object obj = new object();
        Type type = null;
        public foodieDAL()
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


        private object GetDataBaseType()
        {
            string DataBaseType = xml.GetXmlConfigValue("ComponentDbType");
            Assembly assembly = Assembly.Load("FoodieConnection");
            object obj = assembly.CreateInstance("Foodie.DataBase.foodieConnection");
            Type type = obj.GetType();
            object[] param = new object[2];
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from eat_system_user_info");
            param[0] = sql;
            param[1] = null;
            type.GetMethod("ExecuteBySqlNotTran").Invoke(obj, param); ;
            return obj;
        }


        /// <summary>
        /// 获取账号密码对应的角色信息
        /// </summary>
        /// <param name="LoginName">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public DataTable GetLogInfo(string LoginName, string Password)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from eat_system_user_info where pk_id=@pk_id and flag_status=0");
            List<SqlParam> param = new List<SqlParam>();
            param.Add(new SqlParam("@pk_id", LoginName));

            if (Password != null)
            {
                sql.Append(" and user_password=@user_password");
                param.Add(new SqlParam("@user_password", Password));
            }

            object[] funParam = new object[2];
            funParam[0] = sql;
            funParam[1] = param.ToArray();
            return (DataTable)type.GetMethod("GetDataTableBySql").Invoke(obj, funParam);
        }

        /// <summary>
        /// 登陆成功时将登陆状态改为手机uuid
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int ChangeLoginState(SqlParam[] param)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update eat_system_user_info set login_state=@login_state where pk_id=@pk_id");

            object[] funParam = new object[2];
            funParam[0] = sql;
            funParam[1] = param;
            return (int)type.GetMethod("ExecuteBySqlNotTran").Invoke(obj, funParam);
        }

        /// <summary>
        /// 获取食品类别和食品信息（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public DataTable GetFoodTypeAndInfoDataTable(StringBuilder where, SqlParam[] param)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.pk_id as category_pkid,a.category_name,b.* from eat_food_category a left join eat_food_list b on a.pk_id=b.food_category and b.flag_status=0 where 1=1");
            sql.Append(where);

            object[] funParam = new object[2];
            funParam[0] = sql;
            funParam[1] = param;

            return (DataTable)type.GetMethod("GetDataTableBySql").Invoke(obj, funParam);
        }
    }
}
