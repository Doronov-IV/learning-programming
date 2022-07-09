#ifndef REPAIR_H
#define REPAIR_H


class Repairman;


#include "Contract.h"


// Класс представляет собой заказ на ремонт;
class Repair : public Contract
{


public:




#pragma region ACCESS


	// Геттер времени выполнения заказа;
	// returns – ожидаемое время выполнения заказа (ETA);
	std::string getDeadline();

	// Сеттер времени выполнения заказа;
	// "sDeadline" – ожидаемое время выполнения заказа (ETA);
	void setDeadline(std::string sDeadline);


	// Геттер указателя на рабочего;
	// returns — указатель на рабочего;
	Repairman* getWorkerPointer();

	// Сеттер указателя на рабочего;
	// "worker" — указатель на рабочего;
	void setWorkerPointer(Repairman* worker);

	// Геттер чека;
	// returns — чек заказа;
	std::string getCheck();

	// Составляет чек;
	void setCheck(std::string sCheck);


#pragma endregion ACCESS




#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Repair();

	// Конструктор с чеком;
	// "sCheck" — Текст заказа;
	Repair(std::string sCheck);

	// Конструктор со всеми аргументами;
	// "sCheck" — Текст заказа;
	// "ptrWorker" — Указатель на рабочего;
	// "ptrCustomer" — Указатель на клиента;
	Repair(std::string sCheck, Repairman* ptrWorker, Customer* ptrCustomer);

	// Деструктор;
	virtual ~Repair();

	// Конструктор копирования;
	// "anotherContract" — объект для копирования;
	Repair(Repair& anotherRepair);

	// Перегрузка оператора "=";
	// "anotherContract" — объект для копирования;
	// returns — ссылку на левый экемпляр;
	Repair& operator=(Repair& anotherRepair);


#pragma endregion CONSTRUCTION

	
private:

	// Срок выполнения работы;
	std::string sDeadline;

	// Указатель на работника;
	Repairman* ptrWorker;

};



#endif