using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACES
{
    public class UserInfo
    {
        public string userName;

        public string password;

        public UserInfo(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        async public Task login()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("ACES"));

            client.Credentials = new Credentials(userName, password);
            try
            {
                User user = await client.User.Get("adamvans");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
