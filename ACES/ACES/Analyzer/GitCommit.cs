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
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="message"></param>
        /// <param name="author"></param>
        /// <param name="linechanges"></param>
        public void PopulateDataFields(string date, string message, string author, string linechanges)
        {  // " 1 file changed, 3 insertions(+), 1 deletion(-)"
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

                //String[] tempDate = splitmessage[0].Split(' ');
                //String[] tempTime = tempDate[3].Split(':');

                //DateTime dateTime = new DateTime(Int32.Parse(tempDate[4]), monthToInt(tempDate[1]),
                //Int32.Parse(tempDate[2]), Int32.Parse(tempTime[0]), Int32.Parse(tempTime[1]), Int32.Parse(tempTime[2]));

                //CommitMessageDateTime = dateTime;

                Compiler = splitmessage[1];
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                Compiler = "not automatic message";
            }
        }

        private int monthToInt(String month)
        {
            int monthNum = 0;

            switch (month)
            {
                case "Jan":
                    monthNum = 1;
                    break;
                case "Feb":
                    monthNum = 2;
                    break;
                case "Mar":
                    monthNum = 3;
                    break;
                case "Apr":
                    monthNum = 4;
                    break;
                case "May":
                    monthNum = 5;
                    break;
                case "Jun":
                    monthNum = 6;
                    break;
                case "Jul":
                    monthNum = 7;
                    break;
                case "Aug":
                    monthNum = 8;
                    break;
                case "Sep":
                    monthNum = 9;
                    break;
                case "Oct":
                    monthNum = 10;
                    break;
                case "Nov":
                    monthNum = 11;
                    break;
                case "Dec":
                    monthNum = 12;
                    break;
                default:
                    Console.Write("Could not set month");
                    break;
            }

            return monthNum;
        }
    }
}
