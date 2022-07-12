#ifndef SALESMAN_H
#define SALESMAN_H


#include "../People/Worker.h"
#include "../People/Customer.h"
#include "../Contracts/Order.h"


// Класс "Продавец";
class Salesman : public Worker
{


public:


	// Оформить заказ;
	// "customer" — указатель на клиента;
	// returns — объект типа "заказ";
	Order makeOrder(Customer* customer);


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Salesman();

	// Конструктор с расписанием;
	// "sSchedule" — расписание;
	Salesman(std::string sSchedule);

	// Деструктор;
	virtual ~Salesman();

	// Конструктор копирования;
	// "anotherWorker" — копируемый экземпляр;
	Salesman(const Salesman& anotherSalesman);

	// Перугрузка оператора "=";
	// "anotherWorker" — копируемый экземпляр;
	// returns — ссылка левого операнда;
	Salesman& operator=(const Salesman& anotherSalesman);


#pragma endregion CONSTRUCTION



};

#endif 
