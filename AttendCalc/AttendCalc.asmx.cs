using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

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
        public string HelloWorld(string value1)
        {
            return value1;
        }

        [WebMethod]
        public string GetAttendHours(string beginDate, string endDate, string empCode)
        {
            string retVal = string.Empty;

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@beginDate", beginDate);
            param.Add("@endDate", endDate);
            param.Add("@empCode", empCode);

            retVal = DBHelper.GetResult(param);
            return retVal;
        }
    }
}