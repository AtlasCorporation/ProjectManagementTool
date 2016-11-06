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

    public class Database
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

        public static int EFTEST(int projectID)
        {
            atlasEntities ctx = new atlasEntities();
            var worktime = from a in ctx.donetasks
                join taskname in ctx.tasks on a.task_id equals taskname.id 
                    where (from c in ctx.tasks where c.project_id == projectID select c.id).Contains(a.task_id)
                        select a.worktime;


           // var majorTasks = (from c in ctx.tasks where c.project_id == projectID && c.task_id == null select new { c.id, c.name }).ToList();
            
            


         //   int time = worktime.Count();
            //System.Diagnostics.Debug.WriteLine("Count: " +worktime.Count());
            return worktime.Sum();
        } 
    }
}