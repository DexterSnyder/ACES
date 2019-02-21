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

        /// <summary>
        /// public constructor
        /// </summary>
        public GitCommit()
        {
            CommitDateTime = new DateTime();
            CommitMessageDateTime = new DateTime();
            Author = "";
        }

        /// <summary>
        /// Populates the data fields
        /// </summary>
        /// <param name="date">The date of the commit</param>
        /// <param name="message">The commit message</param>
        /// <param name="author">the commit author</param>
        public void PopulateDataFields (string date, string message, string author)
        {
            //format = Author: DexterSnyder <40254136+DexterSnyder@users.noreply.github.com>
            //split on ':' first
            string[] authorSplit1 = author.Split(':');
            string authorTemp = authorSplit1[1];
            //now split on the '<'
            string[] authorSplit2 = authorTemp.Split('<');
            author = authorSplit2[0]; //Should contain just the author name
            //trim leading and trailing whitespace
            author = author.Trim();

            //format = Date:   Wed Feb 6 17:36:38 2019 -0700
            //split on ':' first
            string[] dateSplit1 = date.Split(':');
            string dateTemp = dateSplit1[1];
            //Now split on the '-'
            string[] dateSplit2 = dateTemp.Split('-');
            date = dateSplit2[0];
            //trim the leading and trailing whitespace
            date = date.Trim();

            //format:     Wed Feb  6 17:36:24 2019
            //Just trim the white spaces for now
            message = message.Trim();

            //now convert message and date to a date time object
        }
    }
}
