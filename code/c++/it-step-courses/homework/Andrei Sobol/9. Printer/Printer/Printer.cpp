#include "Printer.h"




#pragma region LOGIC



void Printer::addEmployee(std::string sRank)
{
	Employee* newEmployee = new Employee(sRank);

	ptrPrintingQueue->enqueue(newEmployee, newEmployee->getPriority());
}


#pragma endregion LOGIC




#pragma region CONSTRUCTION


Printer::Printer()
{
	ptrPrintingQueue = new PriorityQueueX<Employee>();
	ptrStatistics = new PriorityQueueX<std::string>();
	nPagesPrinted = 0;
	nClientsServed = 0;
	nPrintingSpeed = 20;
}


Printer::~Printer()
{
	if (nullptr != ptrPrintingQueue) delete ptrPrintingQueue;
	if (nullptr != ptrStatistics) delete ptrStatistics;
}


Printer::Printer(PriorityQueueX<Employee>* queue)
{
	this->ptrPrintingQueue = queue;
	nPagesPrinted = 0;
	nClientsServed = 0;
	nPrintingSpeed = 20;
}


#pragma endregion CONSTRUCTION




#pragma region ACCESS


PriorityQueueX<std::string>* Printer::getStatisticsPointer()
{
	return this->ptrStatistics;
}


PriorityQueueX<Employee>* Printer::getPrintingQueuePointer()
{
	return ptrPrintingQueue;
}


size_t Printer::getPagesPrinted()
{
	return nPagesPrinted;
}


size_t Printer::getClientsServed()
{
	return nClientsServed;
}


size_t Printer::getPrintingSpeed()
{
	return nPrintingSpeed;
}


PriorityQueueX<std::string>* Printer::getFullStatistics()
{
	PriorityQueueX<std::string>* result;

	std::string* pages = new std::string(std::to_string(nPagesPrinted));

	std::string* clients = new std::string(std::to_string(nClientsServed));

	std::string* speed = new std::string(std::to_string(nPrintingSpeed));

	result->enqueue(pages,1);
	result->enqueue(clients,2);
	result->enqueue(speed,3);

	return result;
}


void Printer::setPagesPrinted(int nValue)
{
	nPagesPrinted = nValue;
}


void Printer::setClientsServed(int nValue)
{
	nClientsServed = nValue;
}


void Printer::setPrintingSpeed(int nValue)
{
	nPrintingSpeed = nValue;
}


#pragma endregion ACCESS



