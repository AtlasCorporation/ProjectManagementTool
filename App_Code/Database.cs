using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Database
/// </summary>
/// 
namespace Atlas
{
    public class Database
    {      
        atlasEntities ctx = new atlasEntities();

        #region TESTFUNCTIONS

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

        #endregion
        #region METHODS
        public IEnumerable<task> GetChildren(int inputID)
        {
            // haetaan vanhempi
            var query = ctx.tasks.Where(x => x.id == inputID).ToList();
            // loopissa haetaan queryn seuraava rivi ja etsitään kaikki sen lapset, jotka lisätään unionilla queryn loppuun. 
            // queryn koko kasvaa loopin aikana kunnes kaikki lapset on haettu. 
            for(int i = 0;i<query.Count();i++)
            {
                int currentID = query.ElementAt(i).id;
                var children = ctx.tasks.Where(x => x.task_id == currentID).ToList();
                query = query.Union(children).ToList();
            }
            
            return query;
        }

        public IEnumerable<int> GetChildrenIds(int inputID)
        {
            // haetaan vanhempi
            var query = ctx.tasks.Where(x => x.id == inputID).Select(z => z.id).ToList();

            // loopissa haetaan queryn seuraava rivi ja etsitään kaikki sen lapset, jotka lisätään unionilla queryn loppuun. 
            // queryn koko kasvaa loopin aikana kunnes kaikki lapset on haettu. 
            for (int i = 0; i < query.Count(); i++)
            {
                int currentID = query.ElementAt(i);
                var children = ctx.tasks.Where(x => x.task_id == currentID).Select(z => z.id).ToList();
                query = query.Union(children).ToList();
            }

            return query;
        }

        public List<Task> GetWorkingHours(int projectID)
        {
            List<Task> TopTasks = new List<Task>();
            Task tempTask;
            IEnumerable<int> tasks;

            // haetaan ensimmäisen polven taskit, joilla task_id == null
            var majorTasksQuery = from c in ctx.tasks
                             where c.project_id == projectID && c.task_id == null
                             select c;

            // iteroidaan löydettyjen taskien läpi
            foreach(var item in majorTasksQuery)
            {
                // luo tilapäisolio taskin tiedoilla
                tempTask = new Task(item.id, item.name);
                // hae taskin kaikki lapset listaan
                tasks = GetChildrenIds(item.id);
                // hae työtunnit jokaisesta listan taskista tilapäisolion tietoihin
                tempTask.duration = GetHours(tasks);
                // tallennetaan tilapäisolio pysyvään listaan
                TopTasks.Add(tempTask);
            }

            // palauta lista
            return TopTasks;
        }

        // hakee taskien id-listan perusteella yhteistuntimäärän taskeista
        protected int GetHours(IEnumerable<int> tasks)
        {
            var query = (from dtask in ctx.donetasks
                         join t in tasks on dtask.task_id equals t
                         where dtask.task_id == t
                         select dtask.worktime).Sum();

            return query;
        }


        public List<Task> GetTasks(int projectID)
        {
            // hae kaikki projektiin liittyvien donetaskien ja taskien olennainen data
            var donetasks =  (from dtask in ctx.donetasks
                                join t in (from c in ctx.tasks where c.project_id == projectID select c) on dtask.task_id equals t.id
                                where dtask.task_id == t.id
                                select new { ID = t.id, Date = dtask.date, Worker = dtask.whodid, WorkTime = dtask.worktime, Name = t.name, Parent = t.task_id});

            Task tempTask;

            List<Task> Tasks = new List<Task>();

            // luo datasta uusia Task-olioita. 
            // Huom, Task != task
            // Task on oma luokkansa joka sisältää molempien taulujen dataa 
            foreach(var dtask in donetasks)
            {
                tempTask = new Task(dtask.ID, dtask.Name, dtask.Date.Value.Day + "-" + dtask.Date.Value.Month + "-" + dtask.Date.Value.Year, dtask.WorkTime, dtask.Parent);
                Tasks.Add(tempTask);
            }

            return Tasks;
        }

        public string GetTasksInJson(int projectID)
        {
            string tasksJson = "{data:[";
            List<Task> tasks = GetTasks(projectID);
            for (int i = 0;i<tasks.Count;i++)
            {
                tasksJson += JsonConvert.SerializeObject(tasks.ElementAt(i));
                if (i != tasks.Count-1)
                {
                    tasksJson += ",";
                }
                else
                {
                    tasksJson += "]}";
                }
            }

            return tasksJson;
        }

        #endregion
    }
}