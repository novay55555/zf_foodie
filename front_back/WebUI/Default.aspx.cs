using System;

public partial class _Default : System.Web.UI.Page
{
    static int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
    public String JavaScriptString = "alert(1)";
    protected void Button1_Click(object sender, EventArgs e)
    {
        JavaScriptString = "alert(" + i++ + ")";
    }
}
