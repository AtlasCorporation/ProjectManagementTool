using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Octokit;
using System.Threading.Tasks;

namespace GithubAPIDemo
{
    public partial class Demo : System.Web.UI.Page
    {
        private string user = "";
        private string repo = "";

        protected void Page_Load(object sender, EventArgs e)
        {          
        }

        public async Task GetDataFromGithub()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("project-management-tool"));
                        
                //programming languages
                var languages = await client.Repository.GetAllLanguages(user, repo);
                lblLanguages.Text = "<ul>";
                for (int i = 0; i < languages.Count; i++)
                    lblLanguages.Text += "<li>" + languages[i].Name + "</li>";
                lblLanguages.Text += "</ul>";

                //commits
                var commits = await client.Repository.Commit.GetAll(user, repo);
                lblHeader.Text = "<h3>" + commits.Count + " commits:</h3>";
                for (int i = 0; i < commits.Count; i++)
                {
                    lblCommitFeed.Text += commits[i].Commit.Message + "<br/>";
                    lblCommitFeed.Text += commits[i].Commit.Committer.Name + " committed on " + commits[i].Commit.Committer.Date.ToString("MM/dd/yyyy HH:mm") + "<hr/>";
                }
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        protected void btnHae_Click(object sender, EventArgs e)
        {
            user = txtUser.Text;
            repo = txtRepo.Text;
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(repo))
                RegisterAsyncTask(new PageAsyncTask(GetDataFromGithub));
            else
                lblMessages.Text = "Enter username and repository name.";
        }
    }
}