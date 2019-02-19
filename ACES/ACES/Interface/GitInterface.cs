using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace ACES
{
    class GitInterface
    {
        
        /// <summary>
        /// Clone the student repo's onto the local machine
        /// </summary>
        /// <param name="assignmentName">Name of the assignment</param>
        /// <param name="targetFolder">The high level target folder</param>
        /// <param name="userkey">Username:Password</param>
        /// <param name="students">A list of the students</param>
        /// <param name="nameOfOrganization">The name of the GitHub org</param>
        public void CloneStudentRepositorys(string assignmentName, string targetFolder, string userkey, ObservableCollection<Student> students, string nameOfOrganization)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.Start();

            cmd.Start();
            foreach (Student current in students)
            {
                //example of clone command:     git clone https://Adamvans:8my8w5PdYt92@github.com/DexterSnyderTestOrg/assignment1-xar83.git C:\Users\Ethgar\Documents\School\C#\test\test@testing.com
                string gitClone = "git clone https://" + userkey + "@github.com/" + nameOfOrganization + "/" 
                    + assignmentName + "-" + current.GetUserName() + ".get " + targetFolder + "\\" + current.GetName();


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

                // cycle though the lines of output until it runs out and get the last line 
                while (error != null)
                {
                    // get the last line in output. 
                    lines.Add(error);
                    //
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
            }// for loop

        }

        

    }//class
}//namespace
