#ifndef ORDER_DIRECTOR_H
#define ORDER_DIRECTOR_H


#include "OrderBuilder.h"



class OrderDirector
{

public:


#pragma region LOGIC

	// Строит заказ;
	void buildOrder();


#pragma endregion LOGIC




#pragma region ACCESS

	// Геттер указателя на строителя;
	// returns — указатель на строителя;
	OrderBuilder* getBuilderPointer();


#pragma endregion ACCESS




#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	explicit OrderDirector();

	// Конструктор по умолчанию;
	// "nPresetNumber" — номер набора;
	// "nDaysQuantity" — кол-во дней;
	OrderDirector(size_t nPresetNumber, size_t nDaysQuantity);

	// Деструктор;
	virtual ~OrderDirector();

	// Конструктор копирования;
	// "anotherOrderDirector" — объект для копирования;
	OrderDirector(OrderDirector& anotherOrderDirector);

	// Перегрузка оператора "=";
	// "anotherOrderDirector" — объект для копирования;
	// returns — левый операнд;
	OrderDirector& operator=(OrderDirector& anotherOrderDirector);


#pragma endregion CONSTRUCTION



private:

	// Указатель на строителя;
	OrderBuilder* ptrBuilder;


};



#endif