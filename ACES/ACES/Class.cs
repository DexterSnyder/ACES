using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace ACES
{
    class Class
    {
        List<Student> Students = new List<Student>();

        string NameOfOrgansation;

        public Class(string nameOfOrgansation)
        {
            NameOfOrgansation = nameOfOrgansation;
        }

        public void GetStudentsFromRoster(string fileLocation)
        {
            StreamReader sr = new StreamReader(fileLocation);
            // read the fiirst linw which is just a key. 
            string input = sr.ReadLine();
            // read the first data line. 
            input = sr.ReadLine();

            while (input != null)
            {
                // split the line into a list of items 
                string[] line = input.Split(',');
                // get the student username and id set by the teacher. 
                Students.Add(new Student(line[0], line[1]));
                //Read the next line
                input = sr.ReadLine();
            }
        }

        public void CloneStudentRepositorys(string assignmentName, string targetFolder, string userkey)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.Start();

            cmd.Start();
            foreach (Student current in Students)
            {
   //example of clone command:     git clone https://Adamvans:8my8w5PdYt92@github.com/DexterSnyderTestOrg/assignment1-xar83.git C:\Users\Ethgar\Documents\School\C#\test\test@testing.com
                string gitClone = "git clone https://"+ userkey +"@github.com/"+ NameOfOrgansation+"/"+assignmentName+"-"+current.GetUserName()+".get "+targetFolder+"\\"+current.GetName();


                cmd.StandardInput.WriteLine(gitClone);

                string repofolder = "cd " + targetFolder + "\\" + current.GetName();

                cmd.StandardInput.WriteLine(repofolder);

                string getcommits = "git rev-list --count Master";

                cmd.StandardInput.WriteLine(getcommits);

                string output = cmd.StandardOutput.ReadToEnd();

                try
                {
                    current.setCommits(Int32.Parse(output));
                }
                catch(Exception e)
                {
                    throw e;
                }
            }

        }
    }
}
