using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Database
/// </summary>
/// 
namespace Atlas
{
    public static class Database
    {
        public static DataTable GetGanttData(int projectId)
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myDb"].ConnectionString))
                {
                    conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(String.Format("SELECT (SELECT SUM(worktime) FROM donetask WHERE task_id = (SELECT id FROM task WHERE project_id = {0} AND id = 1)) AS dokumentaatiotunnit, (SELECT SUM(worktime) FROM donetask WHERE task_id = (SELECT id FROM task WHERE project_id = {0} AND id = 3)) as ohjelmointitunnit", projectId), conn))
                    {
                        using (MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(command))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}