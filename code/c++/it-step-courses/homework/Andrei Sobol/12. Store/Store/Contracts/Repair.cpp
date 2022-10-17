#include "Repair.h"






#pragma region ACCESS


Repairman* Repair::getWorkerPointer()
{
	return this->ptrWorker;
}


void Repair::setWorkerPointer(Repairman* worker)
{
	this->ptrWorker = worker;
}


std::string Repair::getDeadline()
{
	return this->sDeadline;
}


void Repair::setDeadline(std::string sDeadline)
{
	this->sDeadline = sDeadline;
}



std::string Repair::getCheck()
{
	return this->sCheck;
}


void Repair::setCheck(std::string sCheck)
{
	this->sCheck = sCheck;
}


#pragma endregion ACCESS




#pragma region CONSTRUCTION


Repair::Repair()
{
	ptrWorker = nullptr;
	ptrCustomer = nullptr;
	this->sCheck = "Null";
	this->sDeadline = "14 days";
}


Repair::Repair(std::string sCheck)
{
	ptrWorker = nullptr;
	ptrCustomer = nullptr;
	this->sCheck = sCheck;
	this->sDeadline = "14 days";
}


Repair::Repair(std::string sCheck, Repairman* ptrWorker, Customer* ptrCustomer)
{
	this->ptrCustomer = ptrCustomer;
	this->ptrWorker = ptrWorker;
	this->sCheck = sCheck;
	this->sDeadline = "14 days";
}


Repair::~Repair()
{
	if (nullptr != ptrCustomer) delete ptrCustomer;
	if (nullptr != ptrWorker) delete ptrWorker;
	sCheck = "Null";
	this->sDeadline = "Null";
}


Repair::Repair(Repair& anotherRepair)
{
	this->ptrCustomer = anotherRepair.ptrCustomer;
	this->ptrWorker = anotherRepair.ptrWorker;
	this->sCheck = anotherRepair.sCheck;
	this->sDeadline = anotherRepair.sDeadline;
}


Repair& Repair::operator=(Repair& anotherRepair)
{
	this->ptrCustomer = anotherRepair.ptrCustomer;
	this->ptrWorker = anotherRepair.ptrWorker;
	this->sCheck = anotherRepair.sCheck;
	this->sDeadline = anotherRepair.sDeadline;
	return *this;
}


#pragma endregion CONSTRUCTION