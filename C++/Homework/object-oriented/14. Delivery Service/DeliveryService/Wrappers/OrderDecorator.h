#ifndef ORDER_DECORATOR_H
#define ORDER_DECORATOR_H
#endif


#include "../BaseClasses/Order.h"
#include "../../Misc/Toolbox.h"
#include <iostream>



// Обёртка для вывода информации о заказе в консоль;
class OrderDecorator
{

public:


#pragma region LOGIC

	// Печатает информацию о заказе на экран;
	void printDetails();


#pragma endregion LOGIC



#pragma region ACCESS

	// Геттер заказа;
	// returns — указатель на заказ;
	Order* getOrderPointer();

	// Сеттер заказа;
	// "ptrOrder" — указатель на заказ;
	void setOrderPointer(Order* ptrOrder);


#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	OrderDecorator();

	// Конструктор с заказом;
	// "ptrOrder" — указатель на заказ;
	OrderDecorator(Order* ptrOrder);

	// Деструктор;
	virtual ~OrderDecorator();

	// Конструктор копирования;
	// "anotherOrderDecorator" — копируемый объект;
	OrderDecorator(OrderDecorator& anotherOrderDecorator);

	// Перегрузка оператора "=";
	// "anotherOrderDecorator" — копируемый объект;
	// returns — левый операнд;
	OrderDecorator& operator=(OrderDecorator& anotherOrderDecorator);


#pragma endregion CONSTRUCTION


private:

	// Указатель на заказ;
	Order* ptrOrder;


};