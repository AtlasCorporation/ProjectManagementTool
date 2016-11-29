using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Octokit;

public class Github
{
    protected const int MAX_COMMITS = 10;

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

    /// <summary>
    /// Gets commits from Github for given user/repo.
    /// </summary>
    public static async Task<List<GitHubCommit>> GetCommits(string user, string repo)
    {
        try
        {
            var client = new GitHubClient(new ProductHeaderValue("atlas"));
            var commits = await client.Repository.Commit.GetAll(user, repo);
            List<GitHubCommit> commitList = new List<GitHubCommit>();

            if (commits != null && commits.Count > 0)
            {
                for (int i = 0; i < commits.Count; i++)
                {
                    if (i == 6) // Take only latest (6) commits
                        break;
                    commitList.Add(commits[i]);
                }
                return commitList;
            }
        }
        catch (Exception)
        {
            throw;
        }
        return null;
    }

    /// <summary>
    /// Gets programming languages used in the project from Github.
    /// </summary>
    public static async Task<List<RepositoryLanguage>> GetLanguages(string user, string repo)
    {
        try
        {
            var client = new GitHubClient(new ProductHeaderValue("atlas"));
            var languages = await client.Repository.GetAllLanguages(user, repo);
            List<RepositoryLanguage> languageList = new List<RepositoryLanguage>();

            if (languages != null && languages.Count > 0)
            {
                foreach (RepositoryLanguage l in languages)
                {
                    languageList.Add(l);
                }
                return languageList;
            }
        }
        catch (Exception)
        {
            throw;
        }
        return null;
    }
}