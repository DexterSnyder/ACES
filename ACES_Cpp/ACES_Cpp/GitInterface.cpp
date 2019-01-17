#include <stdio.h> 
#include <stdlib.h>
//#ifndef GIT_INTERFACE_H
//#define GIT_INTERFACE_H

//class GitInterface
//{
//	void gitCommit();
//
//
//};
//
//#endif // !GIT_INTERFACE_H
//
//
////#include <io.h>
//
//static void runGit()
//{
//	
////#ifdef _WIN32
//
////#elif _WIN64
//
////#elif __unix || __unix__
//
////#elif __APPLE__ || __MACH__
//
////#elif __linux__
//
////#else
//
//#endif	
//}

void gitCommit()
{
	system("git add -A");
	system("git commit -m \"Test Commit\"");
}


int main()
{
	gitCommit();
}