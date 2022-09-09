#include "Employee.h"


#define NUMERATION_ISSUE 1


#pragma region CONSTRUCTION


Employee::Employee()
{
	this->nPriority = 0;
	this->sRank = "Null";
	this->nPagesToPrint = 2 + rand() % 5;
	this->nHasPagesPrinted = 0;
}


Employee::Employee(std::string sRank)
{
	this->sRank = sRank;
	if ("Admin" == sRank) this->nPriority = 1;
	else if ("Manager" == sRank) this->nPriority = 2;
	else this->nPriority = 3;
	this->nPagesToPrint = 2 + rand() % 5;
	this->nHasPagesPrinted = 0;
}


Employee::~Employee()
{
	this->nPriority = 0;
	this->sRank = "Null";
}

#pragma endregion CONSTRUCTION




#pragma region ACCESS



size_t Employee::getPagesToPrint()
{
	return this->nPagesToPrint;
}

void Employee::setPagesToPrint(size_t nPagesToPrint)
{
	this->nPagesToPrint = nPagesToPrint;
}

size_t Employee::getPagesPrinted()
{
	return this->nHasPagesPrinted;
}

void Employee::setPagesPrinted(size_t nHasPagesPrinted)
{
	this->nHasPagesPrinted = nHasPagesPrinted;
}


std::string Employee::getRank()
{
	return this->sRank;
}



size_t Employee::getPriority()
{
	return this->nPriority;
}



void Employee::setRank(std::string sRank)
{
	this->sRank = sRank;
}


#pragma region ACCESS