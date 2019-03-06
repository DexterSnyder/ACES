using System;
using System.Globalization;

namespace ACES
{
    public class GitCommit
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
        public string Compiler { get; private set; }

        /// <summary>
        /// the number of files changed in this commit.  
        /// </summary>
        public int FilesChanged { get; private set; }

        /// <summary>
        /// lines added in this commit.  
        /// </summary>
        public int LinesInserted { get; private set; }

        /// <summary>
        /// lines removed in this commit.  
        /// </summary>
        public int LinesDeleted { get; private set; }

        public GitCommit()
        {
            CommitDateTime = new DateTime();
            CommitMessageDateTime = new DateTime();
            Author = "";
        }

        public void PopulateDataFields(string date, string message, string author, string linechanges)
        {  // " 1 file changed, 3 insertions(+), 1 deletion(-)"
            CultureInfo provider = CultureInfo.InvariantCulture;
            CommitDateTime = DateTime.ParseExact(date.Substring(5).Trim(), "ddd MMM dd HH:mm:ss yyyy zzz", provider);

            string[] splitAuthor = author.Split();

            Author = splitAuthor[1];

            string[] splitmassage = message.Split('-');

            string[] splitLineChanges = linechanges.Split(',');

            FilesChanged = Int32.Parse(splitLineChanges[0].Split()[1]);

            if (splitLineChanges[1].Contains("insertions"))  
            {
                LinesInserted = Int32.Parse(splitLineChanges[1].Split()[1]);

                if (splitLineChanges.Length == 3)
                {
                    LinesDeleted = Int32.Parse(splitLineChanges[2].Split()[1]);
                }
            }
            else
            {
                int LinesDeleted = Int32.Parse(splitLineChanges[1].Split()[1]);
            }

            try
            {
                CommitMessageDateTime = DateTime.ParseExact(splitmassage[0], "ddd MMM dd HH:mm:ss yyyy zzz", provider);

                Compiler = splitmassage[1];
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                Compiler = "not automatic massage";
            }
        }
    }
}
