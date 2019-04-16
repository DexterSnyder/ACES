//Copyright 2017, Bradley Peterson, Weber State University, All rights reserved.

#ifndef STACKS_AND_QUEUES
#define STACKS_AND_QUEUES

#include <iostream>
#include <string>
#include <sstream>
#include <cstdio>

//To prevent those using g++ from trying to use a library
//they don't have
#ifndef __GNUC__
#include <conio.h>
#endif

//These two base classes help the assignment compile and run without any changes.  
//Dot not modify these.  You will instead override the methods in the derived classes below.
template <typename T>
class CourseBaseStack {
public:
	CourseBaseStack() {}
	CourseBaseStack(const unsigned int size) {}
	void push(const T& item) {}
	T pop() { T temp{}; return temp; }
	const unsigned int size() const { return 0; }
protected:
	T * arr{ nullptr };

};

template <typename T>
class CourseBaseQueue {
public:
	CourseBaseQueue() {}
	CourseBaseQueue(const unsigned int size) {}
	void push_back(const T& item) {}
	T pop_front() { T temp{}; return temp; }
	const unsigned int size() const { return 0; }
protected:
	T * arr{ nullptr };
};


//**********************************
//Write your code below here
//**********************************

template <typename T>
class CourseStack : public CourseBaseStack<T> {
public:
	CourseStack(const unsigned int size);
	~CourseStack();
	void Push(const T& value);
	T Pop();
private:
	unsigned int capacity{ 0 };
	T* arr{ nullptr };
	unsigned int index{ 0 };
};

template <typename T>
class CourseQueue : public CourseBaseQueue<T> {
public:
	CourseQueue(const unsigned int size) {}
};

//ALL TEMPLATE IMPLEMENTATIONS MUST GO IN A HEADER FILE-> Because reasons

template <typename T>
CourseStack<T>::CourseStack(const unsigned int size) {
	capacity = size;
	arr = new T[capacity];
}
//---------------------------------------------------------------------
template <typename T>
CourseStack<T>::~CourseStack()
{
	delete[] arr;
}
//--------------------------------------------------------------------
template <typename T>
void CourseStack<T>::Push(const T& value) {
	if (index >= capacity) {
		cout << "Error- Stack size exceeded." << endl;
		throw 1;
	}
	arr[index] = value;
	index++;
}
//-----------------------------------------------------------------------
template <typename T>
T CourseStack<T>::Pop() {
	//I shouldn't need an error catch here,  because the index should never
	//exceed the capacity
	index--;
	return arr[index + 1];
}

#endif // !STACKS_AND_QUEUES

//**********************************
//Write your code above here
//**********************************