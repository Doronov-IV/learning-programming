#ifndef STORE_H
#define STORE_H

#include <vector>
#include <ctime>

#include "People/Salesman.h"
#include "People/Repairman.h"
#include "../PriorityQueue/PriorityQueueX.h"
#include "../FileBufferedReader/FileBufferedReader.h"


// Класс "Магазин";
class Store
{

public:




#pragma region LOGIC

	// Добавление продавца;
	// "newSalesman" — новый работник;
	void addSalesman(Salesman* newSalesman);

	// Добавление ремонтника;
	// "newRepairman" — новый рабочий;
	void addRepairman(Repairman* newRepairman);

	// Добавление чека в историю;
	// "sCheck" — чек;
	void addToHistory(std::string sCheck);

	// Добавление клиента;
	// "newCustomer" — новый клиент;
	void addCustomer(Customer* newCustomer);

	// Добавление случайного клиента;
	void addRandomCustomer();

	// Добавление случайного сотрудника;
	void addRandomWorker();

	// Добавление случайного заказа; 
	int makeRandomContract();

	// Очистка истории;
	void clearHistory();

	// Очистка всех списков;
	void clearAllLists();


#pragma endregion LOGIC




#pragma region ACCESS

	// Геттер имени;
	// returns — имя магазина;
	std::string getName();

	// Сеттер имени;
	// "sName" – новое имя;
	void setName(std::string sName);

	// Геттер истории заказов;
	PriorityQueueX<std::string>* getHistoryPointer();

	// Геттер списка продавцов;
	std::vector<Salesman> getSalesmenPointer();

	// Геттер списка ремонтников;
	std::vector<Repairman> getRepairmenPointer();

	// Геттер списка клиентов;
	std::vector<Customer> getCustomersPointer();


#pragma endregion ACCESS




#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Store();

	// Конструктор с именем (названием);
	// "sName" — имя магазина;
	Store(std::string sName);

	// Деструктор;
	virtual ~Store();

	// Конструктор копирования;
	// "anotherStore" — копируемый объект;
	Store(Store& anotherStore);

	// Перегрузка оператора "=";
	// "anotherStore" — копируемый объект;
	Store& operator=(Store& anotherStore);


#pragma endregion CONSTRUCTION




protected:

	// Добавление случайного продавца (для addRandomWorker);
	void addRandomSalesman();

	// Добавление случайного ремонтника (для addRandomWorker);
	void addRandomRepairman();

	// Добавление случайного заказа на товар;
	int addRandomSaleContract();

	// Добавление случайного заказа на ремонт;
	int addRandomRepairContract();

	// Имя магазина;
	std::string sName;

	// Указатель на историю заказов;
	PriorityQueueX<std::string>* ptrHistory;

	// Указатель на список продавцов;
	std::vector<Salesman> ptrSalesmen;

	// Указатель на список ремонтников;
	std::vector<Repairman> ptrRepairmen;

	// Указатель на список клинтов;
	std::vector<Customer> ptrCustomers;

};


#endif