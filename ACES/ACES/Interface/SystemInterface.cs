using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;

namespace ACES
{
    /// <summary>
    /// This class interfaces with the operating system command line
    /// </summary>
    public class SystemInterface
    {
        /// <summary>
        /// builds a students c++ assignment
        /// </summary>
        /// <param name="studentProjLocation">Folder location of the students project</param>
        /// <param name="instructorUnitTests">Directory location of the instructors unit tests</param>
        public Score BuildAssignment(string studentProjLocation, string instructorUnitTests, string securityCode)
        {
            Score tempScore = new Score();
            try
            {
                tempScore.NumberCorrect = 0;
                tempScore.NumberIncorrect = 0;

                //A process is used to run commands on the command line
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.Start();

                //Set up the cmd prompt to run the VS tools
                string batDirectory = "";
                //MODIFY THIS TO SUPPORT DIFFERENT VERSIONS OF VISUAL STUDIO
                //Only 2017 is supported
                if (Directory.Exists("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\VC\\Auxiliary\\Build"))
                {
                    batDirectory = "cd \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\VC\\Auxiliary\\Build\"";
                }
                else if (Directory.Exists("C:\\Program Files (x86)\\Microsoft Visual Studio\\2015\\Enterprise\\VC\\Auxiliary\\Build"))
                {
                    batDirectory = "cd \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2015\\Enterprise\\VC\\Auxiliary\\Build\"";
                }
                else if (Directory.Exists("C:\\Program Files (x86)\\Microsoft Visual Studio\\2013\\Enterprise\\VC\\Auxiliary\\Build"))
                {
                    batDirectory = "cd \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2013\\Enterprise\\VC\\Auxiliary\\Build\"";
                }
                else
                {
                    throw new Exception("Compiler not supported");
                }

                //This runs a special cmd prompt that allows us to build using the VS compiler
                cmd.StandardInput.WriteLine(batDirectory);
                string runBat = "vcvars32.bat";
                cmd.StandardInput.WriteLine(runBat);

                //move to the directory
                string directoryMove = "cd \"" + studentProjLocation + "\"";
                cmd.StandardInput.WriteLine(directoryMove);

                //Delete the students unit test
                string deleteCmd = "del /f UnitTests.cpp";
                cmd.StandardInput.WriteLine(deleteCmd);

                //Copy the instructors unit test
                string moveCmd = "copy \"" + instructorUnitTests + "\" \"" + studentProjLocation + "\" /Y";
                cmd.StandardInput.WriteLine(moveCmd);

                //Build the project
                string buildCmd = "cl /EHsc UnitTests_InstructorVersion.cpp";
                cmd.StandardInput.WriteLine(buildCmd);

                //Run the project
                string runCmd = "UnitTests_InstructorVersion.exe";
                cmd.StandardInput.WriteLine(runCmd);

                cmd.StandardInput.WriteLine("exit");

                List<string> lines = new List<string>();

                // cycle though the lines of output untill it runs out and get the last line 
                string output = cmd.StandardOutput.ReadLine();
                while (output != null)
                {
                    // get the last line in output. 
                    lines.Add(output);
                    output = cmd.StandardOutput.ReadLine();
                }

                // cycle though the lines of output untill it runs out and get the last line 
                string error = cmd.StandardError.ReadLine();
                while (error != null)
                {
                    // get the last line in output. 
                    lines.Add(error);
                    //
                    error = cmd.StandardError.ReadLine();
                }

                //Count correct answers. Uses the security code the ensure that students can't write "Passed" to Command line
                string correctAnswer = securityCode + " Passed";
                foreach (var line in lines)
                {
                    string temp = line.Trim();

                    if (temp == "Failed test")
                    {
                        tempScore.NumberIncorrect++;
                    }

                    if (temp == correctAnswer)
                    {
                        tempScore.NumberCorrect++;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            return tempScore;
        }//build assignment
    }
}
