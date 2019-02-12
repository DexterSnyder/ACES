using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ACES
{
    

    class GitInterface
    {
        /// <summary>
        /// builds a students c++ assignment
        /// </summary>
        /// <param name="studentProjLocation">Folder location of the students project</param>
        /// <param name="instructorUnitTests">Directory location of the instructors unit tests</param>
        public void BuildAssignment (string studentProjLocation, string instructorUnitTests)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.Start();

            //Set up the cmd prompt to run the VS tools
            string batDirectory = "\"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\VC\\Auxiliary\\Build\"";
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
            string moveCmd = "move \"" + instructorUnitTests + "\" \"" + studentProjLocation + "\"";
            cmd.StandardInput.WriteLine(moveCmd);

            //Build the project
            string buildCmd = "cl /EHsc UnitTests_InstructorVersion.cpp";
            cmd.StandardInput.WriteLine(buildCmd);

            //Run the project
            string runCmd = "UnitTests_InstructorVersion.exe";
            cmd.StandardInput.WriteLine(runCmd);
        }

    }
}
