#include "Person.h"


#pragma region ACCESS


void Person::setName(std::string sName)
{
	this->sName = sName;
}


std::string Person::getName()
{
	return this->sName;
}


void Person::setAge(size_t nAge)
{
	this->nAge = nAge;
}


size_t Person::getAge()
{
	return this->nAge;
}


#pragma endregion ACCESS




#pragma region CONSTRUCTION


Person::Person()
{
	this->sName = "Null";
	this->nAge = 0;
}


Person::Person(std::string sName) : Person()
{
	this->sName = sName;
}


Person::Person(std::string sName, size_t nAge) : Person(sName)
{
	this->nAge = nAge;
}


Person::~Person()
{
	this->sName = "Null";
	this->nAge = 0;
}


Person::Person(Person& anotherPerson)
{
	this->sName = anotherPerson.sName;
	this->nAge = anotherPerson.nAge;
}


Person& Person::operator=(Person& anotherPerson)
{
	this->sName = anotherPerson.sName;
	this->nAge = anotherPerson.nAge;
	return *this;
}


#pragma endregion CONSTRUCTION