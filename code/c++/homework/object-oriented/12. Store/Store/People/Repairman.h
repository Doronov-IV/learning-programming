#ifndef REPAIRMAN_H
#define REPAIRMAN_H


#include "../People/Worker.h"
#include "../People/Customer.h"
#include "../Contracts/Repair.h"


// Класс "Ремонтник";
class Repairman : public Worker
{

public:



	// Оформить заказ;
	// "customer" — указатель на клиента;
	// returns — объект типа "заказ";
	Repair makeOrder(Customer* ptrCustomer);


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Repairman();

	// Конструктор с расписанием;
	// "sSchedule" — расписание;
	Repairman(std::string sSchedule);

	// Деструктор;
	virtual ~Repairman();

	// Конструктор копирования;
	// "anotherWorker" — копируемый экземпляр;
	Repairman(const Repairman& anotherRepairman);

	// Перугрузка оператора "=";
	// "anotherWorker" — копируемый экземпляр;
	// returns — ссылка левого операнда;
	Repairman& operator=(const Repairman& anotherRepairman);


#pragma endregion CONSTRUCTION


protected:



};

#endif 