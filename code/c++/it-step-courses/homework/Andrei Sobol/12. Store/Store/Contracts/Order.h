#ifndef ORDER_H
#define ORDER_H


#include "Contract.h"


class Salesman;


// Класс представляет собой заказ товара;
class Order : public Contract
{

public:


	// Геттер указателя на рабочего;
	// returns — указатель на рабочего;
	Salesman* getWorkerPointer();

	// Сеттер указателя на рабочего;
	// "worker" — указатель на рабочего;
	void setWorkerPointer(Salesman* worker);

	// Геттер чека;
	// returns — чек заказа;
	std::string getCheck();

	// Составляет чек;
	void setCheck(std::string sCheck);


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Order();

	// Конструктор с чеком;
	// "sCheck" — Текст заказа;
	Order(std::string sCheck);

	// Конструктор со всеми аргументами;
	// "sCheck" — Текст заказа;
	// "ptrWorker" — Указатель на рабочего;
	// "ptrCustomer" — Указатель на клиента;
	Order(std::string sCheck, Salesman* ptrWorker, Customer* ptrCustomer);

	// Деструктор;
	virtual ~Order();

	// Конструктор копирования;
	// "anotherContract" — объект для копирования;
	Order(Order& anotherOrder);

	// Перегрузка оператора "=";
	// "anotherContract" — объект для копирования;
	// returns — ссылку на левый экемпляр;
	Order& operator=(Order& anotherOrder);


#pragma endregion CONSTRUCTION


protected:

	// Указатель на работника;
	Salesman* ptrWorker;

};



#endif