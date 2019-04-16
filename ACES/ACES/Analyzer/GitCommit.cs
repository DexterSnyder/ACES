using System;
using System.Globalization;
using System.Reflection;

namespace ACES
{
    /// <summary>
    /// Represents a Git Commit object
    /// </summary>
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

        /// <summary>
        /// default constructor
        /// </summary>
        public GitCommit()
        {
            CommitDateTime = new DateTime();
            CommitMessageDateTime = new DateTime();
            Author = "";
            FilesChanged = 0;
            LinesDeleted = 0;
            LinesInserted = 0;
        }

        /// <summary>
        /// Populates the data fields in the class
        /// </summary>
        /// <param name="date">The data of the commit</param>
        /// <param name="message">The commit message</param>
        /// <param name="author">Commit author</param>
        /// <param name="linechanges">Which lines were changed</param>
        public void PopulateDataFields(string date, string message, string author, string linechanges)
        {
            try
            {
                // " 1 file changed, 3 insertions(+), 1 deletion(-)"
                CultureInfo provider = CultureInfo.InvariantCulture;
                if (date.Substring(5).Trim().Split()[2].Length == 1)
                {
                    CommitDateTime = DateTime.ParseExact(date.Substring(5).Trim(), "ddd MMM d HH:mm:ss yyyy zzz", provider);
                }
                else
                {
                    CommitDateTime = DateTime.ParseExact(date.Substring(5).Trim(), "ddd MMM dd HH:mm:ss yyyy zzz", provider);
                }


                string[] splitAuthor = author.Split();

                Author = splitAuthor[1];

                string[] splitmessage = message.Split('-');

                string[] splitLineChanges = linechanges.Split(',');

                if (splitLineChanges[0] != "")
                {
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
                }

                try
                {
                    splitmessage[0].TrimEnd();
                    CommitMessageDateTime = DateTime.ParseExact(splitmessage[0].Trim(), "ddd MMM dd HH:mm:ss yyyy", provider);

                    Compiler = splitmessage[1];
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    Compiler = "not automatic message";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
