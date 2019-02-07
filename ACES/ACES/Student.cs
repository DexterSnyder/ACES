using System;

namespace ACES
{
    /// <summary>
    /// class to old the students after being pulled from the site.
    /// </summary>
    class Student
    {
        // username of student. 
        String Name;
        // numnber of commits. 
        int Commits;
        // rate wether the student cheeted or not. 
        string Rateing;
        // getHuber username to pull students assigment. 
        string getHubUserName;
        // score of the unit tests 
        int Score;

        public Student(string name, string userName)
        {
            Name = name;
            Commits = 0;
            Rateing = "Green";
            getHubUserName = userName;
            Score = 0;
        }

        internal string GetUserName()
        {
            return getHubUserName;
        }

        internal string GetName()
        {
            return Name;
        }

        internal void setCommits(int num)
        {
            this.Commits = num;
        }
    }
}
