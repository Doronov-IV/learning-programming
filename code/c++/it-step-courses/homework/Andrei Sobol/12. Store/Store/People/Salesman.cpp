#include "Salesman.h"




Order Salesman::makeOrder(Customer* customer)
{
	Customer* newPtrCustomer = new Customer(*customer);

	Salesman* newPtrWorker = new Salesman(*this);

	std::string check = "Employee: ";
	check.append(sName);
	check.append(", Client: ");
	check.append(newPtrCustomer->getName());
	check.append(", ");
	check.append(Toolbox::getCurrentTime());

	Order* order = new Order(check, newPtrWorker, newPtrCustomer);

	return *order;
}




#pragma region CONSTRUCTION


Salesman::Salesman()
{
	this->sSchedule = "Null";
}


Salesman::Salesman(std::string sSchedule)
{
	this->sSchedule = sSchedule;
}


Salesman::~Salesman()
{
	this->sSchedule = "Null";
}


Salesman::Salesman(const Salesman& anotherSalesman)
{
	this->sSchedule = anotherSalesman.sSchedule;
	this->sName = anotherSalesman.sName;
	this->nAge = anotherSalesman.nAge;
}


Salesman& Salesman::operator=(const Salesman& anotherSalesman)
{
	this->sSchedule = anotherSalesman.sSchedule;
	this->sName = anotherSalesman.sName;
	this->nAge = anotherSalesman.nAge;
	return *this;
}


#pragma endregion CONSTRUCTION