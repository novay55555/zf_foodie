<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using Foodie.Business;
using System.Data;
using System.Text;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string action = context.Request["action"];
        if (action == "GetFoodType")
        {
            foodieBLL mybll = new foodieBLL();
            //DataTable dt = mybll.GetFoodieType();
            //context.Response.Write(DataTableToJson(dt));
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