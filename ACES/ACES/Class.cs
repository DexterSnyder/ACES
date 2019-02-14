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
        string RosterLocation;

        public Class(string nameOfOrgansation, string rosterLocation)
        {
            NameOfOrgansation = nameOfOrgansation;
            RosterLocation = rosterLocation;
            GetStudentsFromRoster();
        }

        public void GetStudentsFromRoster()
        {
            StreamReader sr = new StreamReader(RosterLocation);
            // read the fiirst linw which is just a key. 
            string input = sr.ReadLine();
            // read the first data line. 
            input = sr.ReadLine();

            while (input != null)
            {
                // split the line into a list of items 
                string[] line = input.Split(',');
                // get the student username and id set by the teacher. 
                Students.Add(new Student(line[0].Trim('"'), line[1].Trim('"')));
                //Read the next line
                input = sr.ReadLine();
            }
        }

        public void CloneStudentRepositorys(string assignmentName, string targetFolder, string userkey)
        {
            foreach (Student current in Students)
            {

                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.Start();
                //example of clone command:     git clone https://Adamvans:8my8w5PdYt92@github.com/DexterSnyderTestOrg/assignment1-xar83.git C:\Users\Ethgar\Documents\School\C#\test\test@testing.com
                string gitClone = "git clone https://"+ userkey +"@github.com/"+ NameOfOrgansation+"/"+assignmentName+"-"+current.GetUserName()+".git "+targetFolder+"\\"+current.GetName();


                cmd.StandardInput.WriteLine(gitClone);

                string repofolder = "cd " + targetFolder + "\\" + current.GetName();

                cmd.StandardInput.WriteLine(repofolder);

                cmd.StandardInput.WriteLine("git rev-list --count Master");

                cmd.StandardInput.WriteLine("exit");
                string output = cmd.StandardOutput.ReadLine();
                List<string> lines = new List<string>();

                // cycle though the lines of output untill it runs out and get the last line 
                while (output != null)
                {
                    // get the last line in output. 
                    lines.Add(output);
                    //
                    output = cmd.StandardOutput.ReadLine();
                }

                string error = cmd.StandardError.ReadLine();

                // cycle though the lines of output untill it runs out and get the last line 
                while (error != null)
                {
                    if (error == "remote: Invalid username or password.")
                    {
                        throw new Exception("Invalid username or password.");
                    }
                    else if (error.Contains("fatal: repository") && error.Contains("not found"))
                    {
                        Console.Error.Write("repository not found for user " + current.GetName());
                    }
                    // get the last line in output. 
                    lines.Add(error);
                    // get error info. 
                    error = cmd.StandardError.ReadLine();
                }

                
                
                try
                {
                    current.setCommits(Int32.Parse(lines[8]));
                }
                catch (Exception e)
                {
                    throw e;
                }
                
                
            }

        }

        internal string getRosterLocation()
        {
            return RosterLocation;
        }

        internal string getOrgName()
        {
            return NameOfOrgansation;
        }
    }
}
