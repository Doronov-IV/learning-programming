#include "Subject.h"




#pragma region CONSTRUCT


Subject::Subject()
{
	this->sName = "None";
}


Subject::Subject(std::string sName)
{
	this->sName = sName;
}


#pragma endregion CONSTRUCT




#pragma region ACCESS


void Subject::setName(std::string sName)
{
	this->sName = sName;
}

std::string Subject::getName()
{
	return this->sName;
}


#pragma endregion ACCESS