#include "Order.h"




Salesman* Order::getWorkerPointer()
{
	return this->ptrWorker;
}


void Order::setWorkerPointer(Salesman* worker)
{
	this->ptrWorker = worker;
}


std::string Order::getCheck()
{
	return this->sCheck;
}


void Order::setCheck(std::string sCheck)
{
	this->sCheck = sCheck;
}




Order::Order()
{
	this->ptrWorker = nullptr;
	this->ptrCustomer = nullptr;
	this->sCheck = "Null";
}


Order::Order(std::string sCheck)
{
	ptrWorker = nullptr;
	ptrCustomer = nullptr;
	this->sCheck = sCheck;
}


Order::Order(std::string sCheck, Salesman* ptrWorker, Customer* ptrCustomer)
{
	this->ptrCustomer = ptrCustomer;
	this->ptrWorker = ptrWorker;
	this->sCheck = sCheck;
}


Order::~Order()
{
	if (nullptr != ptrCustomer) delete ptrCustomer;
	if (nullptr != ptrWorker) delete ptrWorker;
	sCheck = "Null";
}


Order::Order(Order& anotherOrder)
{
	this->ptrCustomer = anotherOrder.ptrCustomer;
	this->ptrWorker = anotherOrder.ptrWorker;
	this->sCheck = anotherOrder.sCheck;
}


Order& Order::operator=(Order& anotherOrder)
{
	this->ptrCustomer = anotherOrder.ptrCustomer;
	this->ptrWorker = anotherOrder.ptrWorker;
	this->sCheck = anotherOrder.sCheck;
	return *this;
}