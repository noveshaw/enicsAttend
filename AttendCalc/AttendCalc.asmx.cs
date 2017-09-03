using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Collections.Specialized;

namespace AttendCalc
{
    /// <summary>
    /// AttendCalc 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。
    [System.Web.Script.Services.ScriptService]
    public class AttendCalc : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetAttendHours(string beginDate, string endDate, string empCode)
        {
            string retVal = string.Empty;
            JavaScriptSerializer js = new JavaScriptSerializer();

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@beginDate", beginDate);
            param.Add("@endDate", endDate);
            param.Add("@empCode", empCode);

            retVal = DBHelper.GetResult(param);
            AttendData data = new AttendData();
            data.result = retVal;

            Context.Response.Clear();
            Context.Response.Headers["Content-Type"] = "application/json";
            Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            Context.Response.Write(js.Serialize(data));
        }

        //IE8,IE9请求方法
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetAttendHoursByXDR()
        {
            string retVal = string.Empty;
            NameValueCollection queryCol = Context.Request.QueryString;
            JavaScriptSerializer js = new JavaScriptSerializer();

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@beginDate", HttpUtility.UrlDecode(queryCol["beginDate"]));
            param.Add("@endDate", HttpUtility.UrlDecode(queryCol["endDate"]));
            param.Add("@empCode", HttpUtility.UrlDecode(queryCol["empCode"]));

            retVal = DBHelper.GetResult(param);
            AttendData data = new AttendData();
            data.result = retVal;

            Context.Response.Clear();
            Context.Response.Headers["Content-Type"] = "application/json";
            Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            Context.Response.Write(js.Serialize(data));
        }
    }

    public class AttendData
    {
        public string result;
    }
}