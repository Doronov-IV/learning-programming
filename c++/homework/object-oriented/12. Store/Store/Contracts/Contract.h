#ifndef CONTRACT_H
#define CONTRACT_H


#include <string>


#include "../People/Customer.h"
#include "../../Misc/Toolbox.h"
#include "../People/Worker.h"


// Класс "Заказ";
class Contract
{

public:


#pragma region ACCESS

	// Геттер чека;
	// returns — текст чека;
	std::string getCheck();

	// Сеттер чека;
	// "sCheck" — текст чека;
	void setCheck(std::string sCheck);

	// Геттер указателя на клиента;
	// returns — указатель на клиента;
	Customer* getCustomerPointer();

	// Сеттер указателя на клиента;
	// "customer" — указатель на клиента;
	void setCustomerPointer(Customer* customer);


#pragma endregion ACCESS




#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Contract();

	// Деструктор;
	virtual ~Contract() = 0;


#pragma endregion CONSTRUCTION


protected:

	// Текст с датой, временем и именем сотрудника;
	std::string sCheck;

	// Указатель на клиента;
	Customer* ptrCustomer;

	// Создаёт чек, занося туда работника, клиента, время и тд;
	void createCheck();

};



#endif