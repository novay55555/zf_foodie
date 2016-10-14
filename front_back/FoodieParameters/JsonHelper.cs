using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Collections;
namespace Foodie.Parameters
{
    /// <summary>
    /// 转换Json格式帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string json = "";
                if (t != null)
                {
                    sb.Append("{");
                    PropertyInfo[] properties = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in properties)
                    {
                        sb.Append("\"" + pi.Name.ToString() + "\"");
                        sb.Append(":");
                        if (pi.GetValue(t, null) != null && pi.GetValue(t, null) != DBNull.Value && pi.GetValue(t, null).ToString() != "")
                        {
                            string aa = pi.GetValue(t, null).ToString();
                            aa = aa.Replace("\n", "");
                            aa = aa.Replace("\r", "");
                            aa = aa.Replace("\t", "");
                            aa = aa.Replace("			", "");
                            sb.Append("\"" + aa + "\"");
                        }
                        else
                        {
                            sb.Append("\"" + pi.GetValue(t, null) + "\"");
                        }
                        sb.Append(",");
                    }
                    json = sb.ToString().TrimEnd(',');
                    json += "}";
                }
                return json;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// DataTable转Json
        /// </summary>
        /// <param name="dt">table数据集</param>
        /// <param name="dtName">json名</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt, string dtName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"");
                sb.Append(dtName);
                sb.Append("\":[");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("{");
                        foreach (DataColumn dc in dr.Table.Columns)
                        {
                            sb.Append("\"");
                            sb.Append(dc.ColumnName);
                            sb.Append("\":\"");
                            if (dr[dc] != null && dr[dc] != DBNull.Value && dr[dc].ToString() != "")
                            {
                                string aa = dr[dc].ToString().Replace("\"", "");
                                aa = aa.Replace("\n", "");
                                aa = aa.Replace("\r", "");
                                aa = aa.Replace("\t", "");
                                aa = aa.Replace("			", "");
                                sb.Append(aa).Replace("\\", "/");
                            }
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]}");
                return JsonCharFilter(sb.ToString());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// DataTable转Json
        /// </summary>
        /// <param name="dt">table数据集</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("{");
                        foreach (DataColumn dc in dr.Table.Columns)
                        {
                            sb.Append("\"");
                            sb.Append(dc.ColumnName);
                            sb.Append("\":\"");
                            if (dr[dc] != null && dr[dc] != DBNull.Value && dr[dc].ToString() != "")
                            {
                                string aa = dr[dc].ToString().Replace("\"", "");
                                aa = aa.Replace("\n", "");
                                aa = aa.Replace("\r", "");
                                aa = aa.Replace("\t", "");
                                aa = aa.Replace("			", "");
                                sb.Append(aa).Replace("\\", "/");
                            }
                            else
                                sb.Append("");
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                return JsonCharFilter(sb.ToString());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// IList转Json
        /// </summary>
        /// <param name="dt">IList</param>
        /// <param name="dtName">json名</param>
        /// <returns></returns>
        public static string ListToJson<T>(IList list, string dtName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"");
                sb.Append(dtName);
                sb.Append("\":[");
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] pi = obj.GetType().GetProperties();
                        sb.Append("{");
                        for (int j = 0; j < pi.Length; j++)
                        {
                            sb.Append("\"");
                            sb.Append(pi[j].Name.ToString());
                            sb.Append("\":\"");
                            if (pi[j].GetValue(list[i], null) != null && pi[j].GetValue(list[i], null) != DBNull.Value && pi[j].GetValue(list[i], null).ToString() != "")
                            {
                                string aa = pi[j].GetValue(list[i], null).ToString().Replace("\"", "");
                                aa = aa.Replace("\n", "");
                                aa = aa.Replace("\r", "");
                                aa = aa.Replace("\t", "");
                                aa = aa.Replace("			", "");
                                sb.Append(aa).Replace("\\", "/");
                            }
                            else
                            {
                                sb.Append("");
                            }
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]}");
                return JsonCharFilter(sb.ToString());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// IList转Json
        /// </summary>
        /// <param name="dt">IList</param>
        /// <returns></returns>
        public static string ListToJson<T>(IList list)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        T obj = Activator.CreateInstance<T>();
                        PropertyInfo[] pi = obj.GetType().GetProperties();
                        sb.Append("{");
                        for (int j = 0; j < pi.Length; j++)
                        {
                            sb.Append("\"");
                            sb.Append(pi[j].Name.ToString());
                            sb.Append("\":\"");
                            if (pi[j].GetValue(list[i], null) != null && pi[j].GetValue(list[i], null) != DBNull.Value && pi[j].GetValue(list[i], null).ToString() != "")
                            {
                                string aa = pi[j].GetValue(list[i], null).ToString().Replace("\"", "");
                                aa = aa.Replace("\n", "");
                                aa = aa.Replace("\r", "");
                                aa = aa.Replace("\t", "");
                                aa = aa.Replace("			", "");
                                sb.Append(aa).Replace("\\", "/");
                            }
                            else
                            {
                                sb.Append("");
                            }
                            sb.Append("\",");
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                return JsonCharFilter(sb.ToString());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>  
        /// Json特符字符过滤
        /// </summary>  
        /// <param name="sourceStr">要过滤的源字符串</param>  
        /// <returns>返回过滤的字符串</returns>  
        private static string JsonCharFilter(string sourceStr)
        {
            return sourceStr;
        }

        /// <summary> 
        /// JSON文本转对象集合
        /// </summary> 
        /// <typeparam name="T">类型</typeparam> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>数据行的字典</returns> 

        public static List<T> DataRowFromJSON<T>(string jsonText)
        {
            return JSONToObject<List<T>>(jsonText);
        }
        /// <summary> 
        /// JSON文本转对象,泛型方法
        /// </summary> 
        /// <typeparam name="T">类型</typeparam> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>指定类型的对象</returns> 
        public static T JSONToObject<T>(string jsonText)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                jss.MaxJsonLength = 50000000;
                return jss.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

    }
}

