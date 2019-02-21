using System;
using System.Collections.Generic;
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

        public GitCommit()
        {
            CommitDateTime = new DateTime();
            CommitMessageDateTime = new DateTime();
            Author = "";
        }

        public void PopulateDataFields(string[] input)
        {

        }

        public void PopulateDataFields (string date, string message, string author)
        {

        }
    }
}
