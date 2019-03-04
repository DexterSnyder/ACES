using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACES
{
    class GitCommit
    {
        /// <summary>
        /// The date and time of the commit per git
        /// </summary>
        public DateTime CommitDateTime {get; private set;}

        /// <summary>
        /// The date and time contained in the commit message
        /// </summary>
        public DateTime CommitMessageDateTime { get; private set; }

        /// <summary>
        /// Author of the git commit
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// compiler useed in the run command for this commit. 
        /// </summary>
        public string compiler { get; private set; }

        public GitCommit()
        {
            CommitDateTime = new DateTime();
            CommitMessageDateTime = new DateTime();
            Author = "";
        }

        public void PopulateDataFields (string date, string message, string author)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;  
            CommitDateTime = DateTime.ParseExact(date.Substring(5).Trim(), "ddd MMM dd HH:mm:ss yyyy zzz", provider);

            string[] splitAuthor = author.Split();

            Author = splitAuthor[1];

            string[] splitmassage = message.Split('-');

            try
            {
                CommitMessageDateTime = DateTime.ParseExact(splitmassage[0], "ddd MMM dd HH:mm:ss yyyy zzz", provider);

                compiler = splitmassage[1];
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                compiler = "not automatic massage";
            }
        }
    }
}
