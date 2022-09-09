#include "Repairman.h"




Repair Repairman::makeOrder(Customer* ptrCustomer)
{
	Customer* newPtrCustomer = new Customer(*ptrCustomer);

	Repairman* newPtrWorker = new Repairman(*this);

	std::string check = "Employee: ";
	check.append(sName);
	check.append(", Client: ");
	check.append(newPtrCustomer->getName());
	check.append(", ETA: ");
	check.append("14 days, ");
	check.append(Toolbox::getCurrentTime());

	Repair* order = new Repair(check, newPtrWorker, newPtrCustomer);

	return *order;
}




#pragma region CONSTRUCTION


Repairman::Repairman()
{
	this->sSchedule = "Null";
}


Repairman::Repairman(std::string sSchedule)
{
	this->sSchedule = sSchedule;
}


Repairman::~Repairman()
{
	this->sSchedule = "Null";
}


Repairman::Repairman(const Repairman& anotherRepairman)
{
	this->sSchedule = anotherRepairman.sSchedule;
	this->sName = anotherRepairman.sName;
	this->nAge = anotherRepairman.nAge;
}


Repairman& Repairman::operator=(const Repairman& anotherRepairman)
{
	this->sSchedule = anotherRepairman.sSchedule;
	this->sName = anotherRepairman.sName;
	this->nAge = anotherRepairman.nAge;
	return *this;
}


#pragma endregion CONSTRUCTION