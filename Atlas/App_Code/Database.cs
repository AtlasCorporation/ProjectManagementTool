using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Database
{
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
}