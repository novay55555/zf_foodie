<%@ WebHandler Language="C#" Class="Handler_Foodie" %>

using System;
using System.Web;
using Foodie.Business;
using System.Data;
using System.Text;
using Foodie.Parameters;

public class Handler_Foodie : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string MobieCode = context.Request["MobieCode"];
        string LoginName = context.Request["LoginName"];

        foodieBLL mybll = new foodieBLL();
        DataTable dtLogInfo = mybll.GetLogInfo(LoginName);
        if (dtLogInfo == null || dtLogInfo.Rows[0]["login_state"].ToString() != MobieCode)
        {
            context.Response.Write("[{\"Success\":\"0\",\"Message\":\"该账号在其他地点登陆，请重新登录\"}]");
            context.Response.End();
        }

        string action = context.Request["action"];

        if (action == "GetFoodTypeAndInfo")
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" and (a.user_pkid is null or a.user_pkid in('',@user_pkid)) and (b.user_pkid is null or b.user_pkid in('',@user_pkid)) and a.flag_status=0 order by a.flag_sort,b.flag_sort");
            SqlParam[] param = { new SqlParam("@user_pkid", LoginName) };
            DataTable dt = mybll.GetFoodTypeAndInfoDataTable(sql, param);
            if (dt == null)
            {
                context.Response.Write("[{\"Success\":\"-1\",\"Message\":\"网络异常，请重试\"}]");
                return;
            }
            string food_category = null;
            StringBuilder Json = new StringBuilder();
            bool flag_first = true;
            Json.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["category_pkid"].ToString() != food_category)
                {
                    if (food_category != null)
                    {
                        Json.Append("]},");
                    }
                    food_category = dt.Rows[i]["category_pkid"].ToString();
                    Json.Append("{\"id\":\"" + food_category + "\",\"type\":\"" + dt.Rows[i]["category_name"].ToString() + "\",\"data\":[");
                    flag_first = true;
                }
                if (string.IsNullOrEmpty(dt.Rows[i]["pk_id"].ToString()))
                {
                    continue;
                }
                if (flag_first)
                {
                    flag_first = false;
                }
                else
                {
                    Json.Append(",");
                }
                Json.Append("{\"id\":\"" + dt.Rows[i]["pk_id"].ToString() + "\",\"data\":\"" + dt.Rows[i]["food_name"].ToString() + "\"}");
            }
            Json.Append("]}]");
            context.Response.Write(Json);
        }
    }

    private StringBuilder DataTableToJson(DataTable dt)
    {
        StringBuilder Json = new StringBuilder();
        Json.Append("[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Json.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                if (dt.Rows[i][j] != DBNull.Value)
                {
                    Json.Append("\"" + dt.Columns[j].ColumnName + "\":\"" + dt.Rows[i][j].ToString() + "\",");
                }

            }
            Json.Remove(Json.Length - 1, 1);
            Json.Append("}");
            if (i != dt.Rows.Count - 1)
            {
                Json.Append(",");
            }
        }
        Json.Append("]");
        return Json;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}