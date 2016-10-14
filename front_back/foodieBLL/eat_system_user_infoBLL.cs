using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foodie.Service;
using Foodie.Entity;
using System.Data;
using Foodie.Parameters;

namespace Foodie.Business
{
    public class eat_system_user_infoBLL
    {

        private readonly eat_system_user_infoDAL dal = new eat_system_user_infoDAL();

        #region Method
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(eat_system_user_info entity)
        {
            return dal.Update(entity) >= 0 ? true : false;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public bool Delete(string KeyValue)
        {
            return dal.Delete(KeyValue) >= 0 ? true : false;
        }
        /// <summary>
        /// 获得DataTable数据列表(带条件)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public DataTable GetDataTableWhere(StringBuilder where, SqlParam[] param)
        {
            return dal.GetDataTableWhere(where, param);
        }

        public bool SaveList(List<eat_system_user_info> inList, List<eat_system_user_info> upList)
        {
            return dal.SaveList(inList, upList) >= 0 ? true : false;
        }
        #endregion
    }
}

