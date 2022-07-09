#include "Contract.h"




void Contract::createCheck()
{
	if (nullptr != this->ptrCustomer)
	{
		sCheck.append("Customer: ");
		sCheck.append(ptrCustomer->getName());
		sCheck.append(", ");
	}
	else
	{
		sCheck.append("Customer: ");
		sCheck.append("Null, ");
	}

	sCheck.append("time: ");
	sCheck.append(Toolbox::getCurrentTime());
}




#pragma region ACCESS


std::string Contract::getCheck()
{
	if ("Null" != sCheck) return sCheck;
	else
	{
		createCheck();
		return sCheck;
	}
}


void Contract::setCheck(std::string sCheck)
{
	this->sCheck = sCheck;
}


Customer* Contract::getCustomerPointer()
{
	return this->ptrCustomer;
}


void Contract::setCustomerPointer(Customer* customer)
{
	this->ptrCustomer = customer;
}


#pragma endregion ACCESS




#pragma region CONSTRUCTION


Contract::Contract()
{
	ptrCustomer = nullptr;
	sCheck = "Null";
}


Contract::~Contract()
{
	sCheck = "Null";
}


#pragma endregion CONSTRUCTION

