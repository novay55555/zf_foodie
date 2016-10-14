using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Foodie.Service;
using Foodie.DataBase;
using Foodie.Parameters;

namespace Foodie.Business
{
    public class foodieBLL
    {
        foodieDAL dal = new foodieDAL();
        //public DataTable GetFoodieType()
        //{

        //    return dal.GetFoodieType();
        //}

        /// <summary>
        /// 获取账号密码对应的角色信息
        /// </summary>
        /// <param name="LoginName">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public DataTable GetLogInfo(string LoginName, string Password = null)
        {
            return dal.GetLogInfo(LoginName, Password);
        }

        /// <summary>
        /// 登陆成功时将登陆状态改为手机uuid，登陆时间改为现在
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public bool ChangeLoginState(SqlParam[] param)
        {
            return dal.ChangeLoginState(param) >= 0 ? true : false;
        }

        /// <summary>
        /// 获取食品类别和食品信息（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public DataTable GetFoodTypeAndInfoDataTable(StringBuilder where, SqlParam[] param)
        {
            return dal.GetFoodTypeAndInfoDataTable(where, param);
        }
    }
}
