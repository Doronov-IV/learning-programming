#include "Store.h"




#pragma region LOGIC


void Store::addSalesman(Salesman* newSalesman)
{
	ptrSalesmen.push_back(*newSalesman);
}

void Store::addRepairman(Repairman* newRepairman)
{
	ptrRepairmen.push_back(*newRepairman);
}

void Store::addToHistory(std::string sCheck)
{
	std::string* sCheckPtr = new std::string(sCheck);
	ptrHistory->enqueue(sCheckPtr, 1);
	
}

void Store::addCustomer(Customer* newCustomer)
{
	ptrCustomers.push_back(*newCustomer);
}


void Store::addRandomWorker()
{
	int nCoin = rand() % 2;
	(1 == nCoin) ? addRandomSalesman() : addRandomRepairman();
}


void Store::addRandomCustomer()
{
	int customerName = rand() % Toolbox::getNamePoolSize();

	Customer* randomCustomer = new Customer();

	std::string specificName = Toolbox::getPoolNameArray()[customerName];

	randomCustomer->setName(specificName);

	addCustomer(randomCustomer);
}

void Store::addRandomSalesman()
{
	int workerName = rand() % Toolbox::getNamePoolSize();

	std::string specificName = Toolbox::getPoolNameArray()[workerName];

	Salesman* randomWorker = new Salesman();
	randomWorker->setName(specificName);
	addSalesman(randomWorker);
}


void Store::addRandomRepairman()
{
	int workerName = rand() % Toolbox::getNamePoolSize();

	std::string specificName = Toolbox::getPoolNameArray()[workerName];

	Repairman* randomWorker = new Repairman();
	randomWorker->setName(specificName);
	addRepairman(randomWorker);
}

int Store::makeRandomContract()
{
	int res = -1;

	if (0 != ptrCustomers.size())
	{
		if (0 == ptrRepairmen.size() && 0 != ptrSalesmen.size())
		{
			if (0 == addRandomSaleContract()) res = 0;
		}
		else if (0 != ptrRepairmen.size() && 0 == ptrSalesmen.size())
		{
			if (0 == addRandomRepairContract()) res = 0;
		}
		else if (0 != ptrRepairmen.size() && 0 != ptrSalesmen.size())
		{
			int nCoin = rand() % 2;
			(1 == nCoin) ? addRandomSaleContract() : addRandomRepairContract();
			res = 0;
		}
	}
	return res;
}



int Store::addRandomSaleContract()
{
	int res = 0;

	int rSalesmanNumber;
	int rCustomerNumber;

	if (0 != ptrSalesmen.size()) rSalesmanNumber = rand() % ptrSalesmen.size();
	else
	{
		res = -1;
		return res;
	}

	if (0 != ptrCustomers.size()) rCustomerNumber = rand() % ptrCustomers.size();
	else
	{
		res = -1;
		return res;
	}

	Salesman* i_workers = new Salesman(ptrSalesmen[rSalesmanNumber]);

	Customer* i_customers = new Customer(ptrCustomers[rCustomerNumber]);

	addToHistory(i_workers->makeOrder(i_customers).getCheck());

	delete i_customers;
	delete i_workers;

	return res;
}


int Store::addRandomRepairContract()
{
	int res = 0;

	int rRepairmanNumber;
	int rCustomerNumber;

	if (0 != ptrRepairmen.size()) rRepairmanNumber = rand() % ptrRepairmen.size();
	else
	{
		res = -1;
		return res;
	}

	if (0 != ptrCustomers.size()) rCustomerNumber = rand() % ptrCustomers.size();
	else
	{
		res = -1;
		return res;
	}

	Repairman* i_workers = new Repairman(ptrRepairmen[rRepairmanNumber]);

	Customer* i_customers = new Customer(ptrCustomers[rCustomerNumber]);

	addToHistory(i_workers->makeOrder(i_customers).getCheck());

	delete i_customers;
	delete i_workers;

	return res;
}


void Store::clearHistory()
{
	if (!ptrHistory->isEmpty())
	{
		for (size_t i = 0ULL; i < ptrHistory->size() + ITERATION_ISSUE + ITERATION_ISSUE; i++)	// я хз как это работает, но
		{																						// i < ptrHistory->size() и
			ptrHistory->dequeue();																// i < ptrHistory->size() + 1
		}																					// выдают один и тот же результат.
	}																						// скорее всего, проблема именно в методе
	else return;																			// dequeue(). Если этот коммент не удалён,
}																							// значит я не смог определить причину :\

void Store::clearAllLists()
{
	if (0 != ptrSalesmen.size())
	{
		ptrSalesmen.clear();
	}

	if (0 != ptrRepairmen.size())
	{
		ptrRepairmen.clear();
	}

	if (0 != ptrCustomers.size())
	{
		ptrCustomers.clear();
	}

	if (!ptrHistory->isEmpty())
	{
		for (size_t i = 0ULL; i < ptrHistory->size(); i++)
		{
			ptrHistory->dequeue();
		}
	}
}


#pragma endregion LOGIC




#pragma region ACCESS


std::string Store::getName()
{
	return sName;
}


void Store::setName(std::string sName)
{
	this->sName = sName;
}


PriorityQueueX<std::string>* Store::getHistoryPointer()
{
	return this->ptrHistory;
}


std::vector<Salesman> Store::getSalesmenPointer()
{
	return this->ptrSalesmen;
}


std::vector<Repairman> Store::getRepairmenPointer()
{
	return this->ptrRepairmen;
}


std::vector<Customer> Store::getCustomersPointer()
{
	return this->ptrCustomers;
}


#pragma endregion ACCESS




#pragma region CONSTRUCTION


Store::Store()
{
	this->ptrHistory = new PriorityQueueX<std::string>();
	this->sName = "Null";
}


Store::Store(std::string sName)
{
	this->ptrHistory = new PriorityQueueX<std::string>();
	this->sName = sName;
}


Store::~Store()
{
	this->sName = "Null";
	if (nullptr != ptrHistory)
	{
		delete ptrHistory;
		ptrHistory = nullptr;
	}
}


Store::Store(Store& anotherStore)
{
	this->ptrCustomers = anotherStore.ptrCustomers;
	this->ptrSalesmen = anotherStore.ptrSalesmen;
	this->ptrRepairmen = anotherStore.ptrRepairmen;
	this->ptrHistory = anotherStore.ptrHistory;
	this->sName = anotherStore.sName;
}


Store& Store::operator=(Store& anotherStore)
{
	this->ptrCustomers = anotherStore.ptrCustomers;
	this->ptrSalesmen = anotherStore.ptrSalesmen;
	this->ptrRepairmen = anotherStore.ptrRepairmen;
	this->ptrHistory = anotherStore.ptrHistory;
	this->sName = anotherStore.sName;
	return *this;
}


#pragma endregion CONSTRUCTION