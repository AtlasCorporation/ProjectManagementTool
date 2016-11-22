﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Octokit;

public partial class Home : System.Web.UI.Page
{
    protected project activeProject;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if some project is currently active from Session value
        if (Session["ActiveProject"] != null)
        {
            // Get the active project's data from DB
            try
            {
                activeProject = Database.GetProjectFromDb(Convert.ToInt32(Session["ActiveProject"]));
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        if (!IsPostBack && activeProject != null)
        {
            InitProjectHomePage();
        }
    }

    /// <summary>
    /// Initializes home page.
    /// </summary>
    protected void InitProjectHomePage()
    {
        lblProjectName.Text = activeProject.name;
        lblProjectDesc.Text = activeProject.description;
        InitGithub();
    }

    /// <summary>
    /// Initializes stuff from Github (commits etc).
    /// </summary>
    protected async void InitGithub()
    {
        lblCommitFeed.Text = "";
        List<GitHubCommit> commits = await Github.GetCommits(activeProject.github_username, activeProject.github_reponame);
        foreach (GitHubCommit c in commits)
        {
            if (c.Committer.Login != null && c.Commit.Message != null)
                lblCommitFeed.Text += "- " + c.Committer.Login + " -- " + c.Commit.Message + "<br/>";
        }
    }
}