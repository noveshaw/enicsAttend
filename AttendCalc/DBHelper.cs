using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;

namespace AttendCalc
{
    public class DBHelper
    {
        private static string _CONNECT_STRING;
        private static SqlDataReader reader;
        private static SqlConnection _sqlConnection;

        public static string ProcedureName = "sp_attend_calc";
        public static Dictionary<string, string> ParamDict = new Dictionary<string, string>();

        //public string ConnString
        //{
        //    get { return _CONNECT_STRING; }
        //    set { _CONNECT_STRING = value; }
        //}

        private static void Init()
        {
            if (string.IsNullOrEmpty(_CONNECT_STRING))
            {
                _CONNECT_STRING = ConfigurationManager.ConnectionStrings["AttendCalc.Properties.Settings.connString"].ConnectionString;
            }
        }

        public static string GetResult(Dictionary<string, string> paramDict)
        {
            Init();
            string retVal = "";
            using (_sqlConnection = new SqlConnection(_CONNECT_STRING))
            {
                _sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _sqlConnection;
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var key in paramDict.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, paramDict[key]);
                    }

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        retVal = reader[0].ToString();
                    }
                }
                _sqlConnection.Close();
            }
            return retVal;
        }
    }
}