using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

public class Database
{
    static atlasEntities ctx = new atlasEntities();
    public static task previouslyCreatedTask;
    public Database(){}
    #region PROJECTS
    /// <summary>
    /// Gets project object from DB with the given project ID.
    /// </summary>
    public static project GetProjectFromDb(int id)
    {
        try
        {
            using (var db = new atlasEntities())
            {
                var ret = from p in db.projects
                          where p.id == id
                          select p;
                var project = ret.FirstOrDefault();
                if (project != null)
                    return project;
            }
        }
        catch (Exception)
        {
            throw;
        }
        return null;
    }

    /// <summary>
    /// Gets all projects for given user from DB.
    /// </summary>
    public static List<project> GetAllProjectsForUser(string username)
    {
        try
        {
            using (var db = new atlasEntities())
            {
                var ret = from u in db.users
                          where u.username == username
                          select u;
                var user = ret.FirstOrDefault();
                if (user != null)
                {
                    return user.projects.ToList();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        return null;
    }

    /// <summary>
    /// Adds given project to database. Returns false if project with the same name already exists.
    /// </summary>
    public static bool AddProject(project projectToAdd)
    {
        try
        {
            using (var db = new atlasEntities())
            {
                // Check if project with the same name already exists
                foreach (project p in db.projects)
                {
                    if (p.name == projectToAdd.name)
                        return false;
                }
                db.projects.Add(projectToAdd);
                db.SaveChanges();
                return true;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Adds given project to given user.
    /// </summary>
    public static void AddProjectToUser(string username, int projectID)
    {        
        try
        {
            using (var db = new atlasEntities())
            {
                // Get user from DB
                user user = null;
                foreach (var u in db.users)
                {
                    if (u.username == username)
                    {
                        user = u;
                        break;
                    }
                }

                // Get project from DB
                project project = null;
                foreach (var p in db.projects)
                {
                    if (p.id == projectID)
                    {
                        project = p;
                        break;
                    }
                }

                // Add project to user (if user doesn't have it already)
                if (user != null && project != null)
                {
                    if (!user.projects.Contains(project))
                        user.projects.Add(project);
                }
                db.SaveChanges(); 
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region USERS

    /// <summary>
    /// Gets all users from DB.
    /// </summary>
    public static List<user> GetAllUsers()
    {
        try
        {
            using (var db = new atlasEntities())
            {
                return db.users.ToList();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Adds given role to given user for given project.
    /// </summary>
    public static bool AddRoleForUserToProject(string role, int userID, int projectID)
    {
        string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
        string query = string.Format("UPDATE user_project SET role=@role WHERE user_id=@userID AND project_id=@projectID");
        if (Authorizer.isValidRole(role))
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnString);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@projectID", projectID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();
                if (rows > 0)
                    return true; // Success
            }
            catch (Exception)
            {
                throw;
            }
        }
        return false;
    }

    #endregion

    #region GANTT
    #region Backups
    /*
    public static IEnumerable<task> GetChildren(int taskID, int tier)
    {
        IEnumerable<task> tasks = Enumerable.Empty<task>();
        var query = (from c in ctx.tasks
                     where c.task_id == taskID
                     select c).ToList();

        for (int i = 0; i < query.Count(); i++)
        {
            query.ElementAt(i).tier = tier + 1;
            IEnumerable<task> temp = new task[] { query.ElementAt(i) };

            tasks = tasks.Union(temp).Union(GetChildren(query.ElementAt(i).id, tier + 1));
        }
        return tasks;
    }

    public static IEnumerable<task> GetChildren(int inputID)
    {
        // haetaan vanhempi
        var query = ctx.tasks.Where(x => x.id == inputID).ToList();
        // loopissa haetaan queryn seuraava rivi ja etsitään kaikki sen lapset, jotka lisätään unionilla queryn loppuun.
        // queryn koko kasvaa loopin aikana kunnes kaikki lapset on haettu.
        for (int i = 0; i < query.Count(); i++)
        {
            int currentID = query.ElementAt(i).id;
            var children = ctx.tasks.Where(x => x.task_id == currentID).ToList();
            query = query.Union(children).ToList();
        }

        return query;
    }
  

    public static IEnumerable<int> GetChildrenIds(int inputID)
    {
        using (var db = new atlasEntities())
        {
            // haetaan vanhempi
            var query = db.tasks.Where(x => x.id == inputID).Select(z => z.id).ToList();

        // loopissa haetaan queryn seuraava rivi ja etsitään kaikki sen lapset, jotka lisätään unionilla queryn loppuun. 
        // queryn koko kasvaa loopin aikana kunnes kaikki lapset on haettu. 
        for (int i = 0; i < query.Count(); i++)
        {
            int currentID = query.ElementAt(i);
            var children = db.tasks.Where(x => x.task_id == currentID).Select(z => z.id).ToList();
            query = query.Union(children).ToList();
        }

        return query;
        }
    }
    
         
    public static List<Task> GetTasks(int projectID)
    {
        // hae kaikki projektiin liittyvien donetaskien ja taskien olennainen data
        var donetasks = (from dtask in ctx.donetasks
                         join t in (from c in ctx.tasks where c.project_id == projectID select c) on dtask.task_id equals t.id
                         where dtask.task_id == t.id
                         select new { TaskID = t.id, dTaskID = dtask.id, Date = dtask.date, Worker = dtask.user_id, WorkTime = dtask.worktime, Name = t.name, Parent = t.task_id });

        Task tempTask;

        List<Task> Tasks = new List<Task>();

        // luo datasta uusia Task-olioita. 
        // Huom, Task != task
        // Task on oma luokkansa joka sisältää molempien taulujen dataa 
        int i = 1;
        foreach (var dtask in donetasks)
        {
            tempTask = new Task(dtask.TaskID, dtask.Name, dtask.Date.Value.Day + "-" + dtask.Date.Value.Month + "-" + dtask.Date.Value.Year, dtask.WorkTime, dtask.Parent, i);
            Tasks.Add(tempTask);
            i++;
        }

        // asetetaan Taskeille parentit ganttia varten
        foreach (Task t in Tasks)
        {
            if (t.Parent != null)
            {
                t.GanttParentId = (from d in Tasks where d.ID == t.Parent select d.GanttId).FirstOrDefault();
            }
        }

        return Tasks;
    }

         
         */
    #endregion
    public static IEnumerable<task> GetProjectTasks(int projectID)
    {
        IEnumerable<task> tasks = Enumerable.Empty<task>();
        // hae ylimmät taskit projektista
        var majorTasksQuery = (from c in ctx.tasks
                               where c.project_id == projectID && c.task_id == null
                               select c).ToList();

        // hae loput
        foreach (var item in majorTasksQuery)
        {            
            IEnumerable<task> temp = new task[] { item };
            tasks = tasks.Union(temp).Union(GetChildren(item.id));
        }

        return tasks;
    }

    public static IEnumerable<task> GetChildren(int taskID)
    {
        IEnumerable<task> tasks = Enumerable.Empty<task>();
        var query = (from c in ctx.tasks
                     where c.task_id == taskID
                     select c).ToList();

        for (int i = 0; i < query.Count(); i++)
        {
            IEnumerable<task> temp = new task[] { query.ElementAt(i) };

            tasks = tasks.Union(temp).Union(GetChildren(query.ElementAt(i).id));
        }
        return tasks;
    }

    public static IEnumerable<int> GetChildrenIds(int taskID)
    {
        IEnumerable<int> tasks = Enumerable.Empty<int>();
        var query = (from c in ctx.tasks
                     where c.task_id == taskID
                     select c.id).ToList();

        for (int i = 0; i < query.Count(); i++)
        {
            IEnumerable<int> temp = new int[] { query.ElementAt(i) };
            tasks = tasks.Union(temp).Union(GetChildrenIds(query.ElementAt(i)));
        }
        return tasks;
    }

    // HUOM EI HAE ITSE ROOT-TASKIIN TALLENNETTUJA TYÖTUNTEJA. Tunteja ei tule tallentaa root-taskeihin.
    public static List<Task> GetProjectWorkingHours(int projectID)
    {
        using (var db = new atlasEntities())
        {
            List<Task> TopTasks = new List<Task>();
            Task tempTask;
            IEnumerable<int> tasks;
            int tempHours;

            // haetaan ensimmäisen polven taskit, joilla task_id == null
            var majorTasksQuery = from c in db.tasks
                                  where c.project_id == projectID && c.task_id == null
                                  select c;

            // iteroidaan löydettyjen taskien läpi
            foreach (var item in majorTasksQuery)
            {
                // luo tilapäisolio taskin tiedoilla
                tempTask = new Task(item.id, item.name);
                // hae taskin kaikki lapset listaan
                tasks = GetChildrenIds(item.id);
                // hae työtunnit jokaisesta listan taskista tilapäisolion tietoihin
                tempHours = GetWorkingHours(tasks);
                // varmistetaan että työtunteja on olemassa
                if (tempHours > 0)
                {
                    tempTask.Duration = tempHours;
                    // tallennetaan tilapäisolio pysyvään listaan
                    TopTasks.Add(tempTask);
                }
            }
            // palauta lista

            return TopTasks;
        }
    }

    // HUOM EI HAE ITSE ROOT-TASKIIN TALLENNETTUJA TYÖTUNTEJA. Tunteja ei tule tallentaa root-taskeihin.
    public static List<Task> GetProjectWorkingHoursForUser(int projectID, int userID)
    {
        try
        {
            using (var db = new atlasEntities())
            {
                List<Task> TopTasks = new List<Task>();
                Task tempTask;
                IEnumerable<int> tasks;
                int tempHours;

                // haetaan ensimmäisen polven taskit, joilla task_id == null
                var majorTasksQuery = from c in db.tasks
                                      where c.project_id == projectID && c.task_id == null
                                      select c;

                // iteroidaan löydettyjen taskien läpi
                foreach (var item in majorTasksQuery)
                {
                    // luo tilapäisolio taskin tiedoilla
                    tempTask = new Task(item.id, item.name);
                    // hae taskin kaikki lapset listaan
                    tasks = GetChildrenIds(item.id);
                    // hae työtunnit jokaisesta listan taskista tilapäisolion tietoihin
                    tempHours = GetWorkingHours(tasks, userID);
                    // varmistetaan että työtunteja on olemassa
                    if (tempHours > 0)
                    {
                        tempTask.Duration = tempHours;
                        // tallennetaan tilapäisolio pysyvään listaan
                        TopTasks.Add(tempTask);
                    }
                }
                return TopTasks;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // hakee taskien id-listan perusteella yhteistuntimäärän taskeista
    protected static int GetWorkingHours(IEnumerable<int> tasks)
    {
        using (var db = new atlasEntities())
        {
            try
            {
                var query = (from dtask in db.donetasks
                             join t in tasks on dtask.task_id equals t
                             where dtask.task_id == t
                             select dtask.worktime);

                if (query.Count() > 0)
                {
                    return query.Sum();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    // hakee taskien id-listan ja userID:n perusteella yhteistuntimäärän taskeista tietylle käyttäjälle
    protected static int GetWorkingHours(IEnumerable<int> tasks, int userID)
    {
        using (var db = new atlasEntities())
        {
            try
            {
                var query = (from dtask in db.donetasks
                             join t in tasks on dtask.task_id equals t
                             where dtask.task_id == t && dtask.user_id == userID
                             select dtask.worktime);

                if (query.Count() > 0)
                {
                    return query.Sum();
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public static IEnumerable<donetask> GetDonetasks(int taskId)
    {
        using (atlasEntities db = new atlasEntities())
        {
            var donetasks = (from c in db.donetasks
                            where c.task_id == taskId
                            select c).ToList();
            return donetasks;
        }
    }

    public static IEnumerable<donetask> GetDonetasks(int taskId, int userId)
    {
        using (atlasEntities db = new atlasEntities())
        {
            var donetasks = from c in db.donetasks
                            where c.task_id == taskId && c.user_id == userId
                            select c;

            return donetasks.ToList();
        }
    }

    public static IEnumerable<task> GetMajorTasks(int projectId)
    {
        using (atlasEntities db = new atlasEntities())
        {
            var rootTasks = from c in db.tasks
                            where c.task_id == null && c.project_id == projectId
                            select c;

            return rootTasks;
        }
    }

    #endregion

    #region WorkLog



    public static int AddDonetask(int taskId, int userId, int workTime, DateTime time)
    {
        try
        {
            using (atlasEntities db = new atlasEntities())
            {
                donetask dt = new donetask();
                dt.task_id = taskId;
                dt.user_id = userId;
                dt.worktime = workTime;
                dt.date = time;
                db.donetasks.Add(dt);
                int result = db.SaveChanges();
                return result;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }

    public static int AddTask(int? parentId, int projectId, string taskName)
    {
        try
        {
            using (atlasEntities db = new atlasEntities())
            {
                task t = new task();
                t.task_id = parentId;
                t.project_id = projectId;
                t.name = taskName;
                db.tasks.Add(t);
                previouslyCreatedTask = t;
                int result = db.SaveChanges();
                return result;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static int RemoveTask(int id)
    {
        using (atlasEntities db = new atlasEntities())
        {
            // poistetaan taskiin kiinnitetyt donetaskit
            var donetasks = from c in db.donetasks where c.task_id == id select c;
            if(donetasks.Count() > 0)
            {
                foreach(donetask item in donetasks)
                {
                    db.donetasks.Remove(item);
                }
            }

            // poistetaan task
            var t = (from c in db.tasks where c.id == id select c).FirstOrDefault();
            db.tasks.Remove(t);
            // tallenna muutokset
            int result = db.SaveChanges();
            return result;
        }
    }

    #endregion
}