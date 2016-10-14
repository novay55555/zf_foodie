<%@ WebHandler Language="C#" Class="Handler_Log" %>

using System;
using System.Web;
using Foodie.Business;
using System.Data;
using System.Text;
using Foodie.Parameters;

public class Handler_Log : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string action = context.Request["action"];
        foodieBLL mybll = new foodieBLL();

        if (action == "login")
        {
            string LoginName = context.Request["LoginName"];
            string Password = context.Request["Password"];
            string MobieCode = context.Request["MobieCode"];

            DataTable dtLogInfo = mybll.GetLogInfo(LoginName, Password);
            if (dtLogInfo == null)
            {
                context.Response.Write("[{\"Success\":\"-1\",\"Message\":\"登录异常，服务器无法连接\"}]");
                return;
            }
            else if (dtLogInfo.Rows.Count == 0)
            {
                context.Response.Write("[{\"Success\":\"-1\",\"Message\":\"用户名或密码错误\"}]");
                return;
            }
            SqlParam[] param = { 
                               new SqlParam("@login_state", MobieCode),
                               new SqlParam("@pk_id",LoginName)
                           };
            if (mybll.ChangeLoginState(param))
            {
                context.Response.Write("[{\"Success\":\"1\"}]");
            }
            else
            {
                context.Response.Write("[{\"Success\":\"-1\",\"Message\":\"登录异常，请检查网络\"}]");
            }
        }

        else if (action == "logout")
        {
            string LoginName = context.Request["LoginName"];
            SqlParam[] param = { 
                               new SqlParam("@login_state", DBNull.Value),
                               new SqlParam("@pk_id",LoginName)
                           };
            if (mybll.ChangeLoginState(param))
            {
                context.Response.Write("[{\"Success\":\"1\"}]");
            }
            else
            {
                context.Response.Write("[{\"Success\":\"-1\",\"Message\":\"注销异常，请检查网络\"}]");
            }
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}