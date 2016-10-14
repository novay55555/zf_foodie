//=====================================================================================
// All Rights Reserved , Copyright © Tony 2016
//=====================================================================================

<%@ WebHandler Language="C#" Class="EatSystemUserInfo" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Foodie.Parameters;
using Foodie.Business;

using Foodie.Entity;
public class EatSystemUserInfo : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string action = context.Request["action"];
        if (action == "GetEatSystemUserInfoDatagrid")
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParam> param = new List<SqlParam>();
            eat_system_user_infoBLL mybll = new eat_system_user_infoBLL();
            DataTable dt = mybll.GetDataTableWhere(sql, param.ToArray());
            context.Response.Write(JsonHelper.DataTableToJson(dt));
        }
        else if (action == "StopEatSystemUserInfo")
        {
            string pk_id = context.Request["pk_id"];
            string flag_status = context.Request["flag_status"];
            eat_system_user_infoBLL mybll = new eat_system_user_infoBLL();
            eat_system_user_info myEntity = new eat_system_user_info();
            myEntity.pk_id = pk_id;
            myEntity.modify_date = DateTime.Now;
            if (flag_status == "0")
            {
                myEntity.flag_status = -1;
            }
            else if (flag_status == "-1")
            {
                myEntity.flag_status = 0;
            }
            if (mybll.Update(myEntity))
            {
                context.Response.Write("[{\"Message\": \"操作成功\",\"Success\":\"1\" }]");
            }
            else
            {
                context.Response.Write("[{\"Message\": \"操作失败\",\"Success\":\"-1\" }]");
            }
        }
        else if (action == "DeleteEatSystemUserInfoBase")
        {
            string pk_id = context.Request["pk_id"];
            eat_system_user_infoBLL mybll = new eat_system_user_infoBLL();
            //物理删除
            if (mybll.Delete(pk_id))
            //逻辑删除
            //eat_system_user_info myEntity = new eat_system_user_info();
            //myEntity.pk_id = pk_id;
            //myEntity.modify_date = DateTime.Now;
            //myEntity.flag_status = -1;
            //if (mybll.Update(myEntity))
            {
                context.Response.Write("[{\"Message\": \"删除成功\",\"Success\":\"1\" }]");
            }
            else
            {
                context.Response.Write("[{\"Message\": \"删除失败\",\"Success\":\"-1\" }]");
            }
        }
        else if (action == "SaveEatSystemUserInfoTable")
        {
            string insertRows = context.Request["insertRows"];
            string updateRows = context.Request["updateRows"];
            List<eat_system_user_info> inList = JsonHelper.DataRowFromJSON<eat_system_user_info>(insertRows);
            List<eat_system_user_info> upList = JsonHelper.DataRowFromJSON<eat_system_user_info>(updateRows);
            for (int i = 0; i < inList.Count; i++)
            {
                inList[i].modify_date = inList[i].make_date = DateTime.Now;
            }
            for (int i = 0; i < upList.Count; i++)
            {
                upList[i].modify_date = DateTime.Now;
            }
            eat_system_user_infoBLL mybll = new eat_system_user_infoBLL();
            if (mybll.SaveList(inList, upList))
            {
                context.Response.Write("[{\"Message\": \"保存成功\",\"Success\":\"1\"}]");
            }
            else
            {
                context.Response.Write("[{\"Message\": \"保存失败\",\"Success\":\"-1\"}]");
            }
        }
    }
    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }
}