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
            foreach (Student current in students)
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.Start();

                
                string gitClone = "git clone https://" + userkey + "@github.com/" + nameOfOrganization + "/" 
                    + assignmentName + "-" + current.GitHubUserName + ".get " + targetFolder + "\\" + current.Name;


                cmd.StandardInput.WriteLine(gitClone);

                string repofolder = "cd " + targetFolder + "\\" + current.Name;

                cmd.StandardInput.WriteLine(repofolder);

                cmd.StandardInput.WriteLine("git rev-list --count Master");

                cmd.StandardInput.WriteLine("git Log");

                cmd.StandardInput.WriteLine("exit");
                string output = cmd.StandardOutput.ReadLine();

                // cycle though the lines of output untill it runs out and get the last line 
                while (output != null)
                {
                    if (output.Contains("Author:"))
                    {
                        lines.Add(output);
                        output = cmd.StandardOutput.ReadLine();
                        lines.Add(output);
                        output = cmd.StandardOutput.ReadLine();
                        lines.Add(output);
                    }
                    else if (output.All(Char.IsDigit))
                    {
                        current.Commits = Int32.Parse(output);
                    }
                    //
                    output = cmd.StandardOutput.ReadLine();
                }

                string error = cmd.StandardError.ReadLine();

                // cycle though the lines of output until it runs out and get the last line 
                while (error != null)
                {
                    if (error == "remote: Invalid username or password.")
                    {
                        throw new Exception("Invalid username or password.");
                    }
                    else if (error.Contains("fatal: repository") && error.Contains("not found"))
                    {
                      
                        Console.Error.Write("repository not found for user " + current.Name);
                    }
                    else
                    {
                        current.ProjectLocation = targetFolder + "\\" + current.Name;
                    }
                    // get the last line in output.
                    error = cmd.StandardError.ReadLine();
                }
              
            }// for loop

        }

        

    }//class
}//namespace
