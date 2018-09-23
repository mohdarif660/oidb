using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OIDBMVCWEBSITE.CustomCode
{
    public class Connection
    {
        private string objConnect;
        private SqlConnection sqlConnection;
        public SqlConnection dbConnect()
        {
            try
            {
                objConnect = ConfigurationManager.ConnectionStrings["OIDBConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(objConnect);
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
            return sqlConnection;
        }

        public void dbClose()
        {
            try
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
                sqlConnection.Dispose();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }

        }


        //public long insertSPGenral(string TableName, string TableColumnFields, string TableColumnValue, string MaxId, string ModuleID)
        //{
        //    try
        //    {
                
        //        Hashtable hashTable = new Hashtable();
        //        hashTable.Add("@tablename", TableName);
        //        hashTable.Add("@Tabfields", TableColumnFields);
        //        hashTable.Add("@values", TableColumnValue);
        //        hashTable.Add("@pid", MaxId);
        //        DataSet ds_insert = sp_call("insert_tab", hashTable);
        //        if (ds_insert != null && ds_insert.Tables.Count > 0 && ds_insert.Tables[0].Rows.Count > 0)
        //        {
        //            if (Convert.ToInt64(ds_insert.Tables[0].Rows[0][0].ToString()) > 0)
        //            {
        //                usertrail(TableName, ds_insert.Tables[0].Rows[0][0].ToString(), "Add", ModuleID);
        //                return Convert.ToInt64(ds_insert.Tables[0].Rows[0][0]);
        //            }
        //            else
        //            {
        //                return 0;
        //            }
        //        }
        //        else
        //            return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.Current.Response.Write(ex.ToString());
        //        return -1;
        //    }
        //}

        //public void usertrail(string TableName, string link_id, string operation, string module)
        //{
        //    try
        //    {
        //        sqlConnection = dbConnect();

        //        long entry_by;
        //        entry_by = Convert.ToInt64(Session["UserId"]);
        //        simplefetch_dr("insert into UserTrail (UserID,TableName,LID,ModuleID,ActionTaken,IpAddr) values ('" + entry_by + "','" + TableName + "','" + link_id + "','" + module + "','" + operation + "','" + ipAddr() + "')");
        //        dbClose();
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.Current.Response.Write("");
        //    }

        //}
    }
}