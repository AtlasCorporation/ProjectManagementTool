using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Octokit;

public class Github
{
    /// <summary>
    /// Gets Github repositories for given user.
    /// </summary>
    public static async Task<List<string>> GetReposForUser(string username)
    {
        try
        {
            var client = new GitHubClient(new ProductHeaderValue("atlas"));
            var repos = await client.Repository.GetAllForUser(username);
            List<string> repoNames = new List<string>();

            if (repos != null && repos.Count > 0)
            {
                foreach (var r in repos)
                {
                    repoNames.Add(r.Name);
                }
                return repoNames;
            }
        }
        catch (Exception)
        {
            throw;
        }
        return null;
    }
}