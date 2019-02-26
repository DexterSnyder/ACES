//Visual Studio flags ctime as unsafe. Need to suppress the warnings
#define _CRT_SECURE_NO_WARNINGS 0

#include <ctime> 
#include <string>

void gitCommit()
{
	//get the time in string form - Credit : http://www.cplusplus.com/reference/ctime/localtime/
	time_t rawtime;
	struct tm * timeinfo;

	time(&rawtime);
	timeinfo = localtime(&rawtime);
	std::string tempTime = asctime(timeinfo);

	//now we have to trim the /n off the end of the string
	tempTime = tempTime.substr(0, tempTime.size() - 1);

	std::string compiler = " - ";

	//define compiler
	/*CLion uses other compilers in their tool chain
	see: https://www.jetbrains.com/help/clion/how-to-switch-compilers-in-clion.html */

#ifdef _MSC_VER //Visual Studio
	compiler += "Microsoft_Visual_Studio";
#elif __GNUC__ //gcc
	compiler += "GCC";
#elif __clang__ //clang
	compiler += "Clang";
#elif __MINGW32__ //minGW 32 bit
	compiler += "MinGW_32_bit";
#elif __MINGW64__ //minGW 64 bit
	compiler += "MinGW_64_bit";
#elif __BORLANDC__ //Borland
	compiler += "Borland";
#elif __TINYC__ //Tiny C
	compiler += "Tiny_C";
#else
	compiler += "Unknown";
#endif
	//More listed here: https://sourceforge.net/p/predef/wiki/Compilers/

	//build the command to commit to the local branch
	std::string commitCommand = "git commit -m \"" + tempTime + compiler;
	commitCommand += "\"";

	//run commmands on the cmd line
	system("git add -A");
	system(commitCommand.c_str());

	//clean up - throws an exception when timeinfo is deleted
	//delete timeinfo;
}


int main()
{
	gitCommit();
}