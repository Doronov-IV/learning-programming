#include "Customer.h"



#pragma region CONSTRUCTION


Customer::Customer()
{
	this->sName = "Null";
	this->nAge = 0;
}


Customer::Customer(std::string sName) : Person()
{
	this->sName = sName;
}


Customer::Customer(std::string sName, size_t nAge) : Person(sName)
{
	this->nAge = nAge;
}


Customer::~Customer()
{
	this->sName = "Null";
	this->nAge = 0;
}


Customer::Customer(const Customer& anotherPerson)
{
	this->sName = anotherPerson.sName;
	this->nAge = anotherPerson.nAge;
}


Customer& Customer::operator=(const Customer& anotherPerson)
{
	this->sName = anotherPerson.sName;
	this->nAge = anotherPerson.nAge;
	return *this;
}


#pragma endregion CONSTRUCTION