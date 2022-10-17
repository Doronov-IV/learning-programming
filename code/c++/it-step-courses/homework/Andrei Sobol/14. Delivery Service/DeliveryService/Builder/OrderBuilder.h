#ifndef ORDER_BUILDER_H
#define ORDER_BUILDER_H


#include "../Wrappers/OrderDecorator.h"
#include "../../FileBufferedReader/FileBufferedReader.h"




class OrderBuilder
{

public:


#pragma region LOGIC

	// Добавляет сразу несколько блюд, в зависимости от номера комплекта;
	void addMeal();


#pragma endregion LOGIC



#pragma region ACCESS

	// Геттер указателя на заказ;
	// returns — указатель на заказ;
	Order* getOrderPointer();

	// Геттер номера комплекта;
	// returns — номер комплекта;
	size_t getPresetNumber();

	// Геттер кол-ва дней;
	// returns — кол-во дней;
	size_t getDaysQuantity();

	// Сеттер номера набора;
	// "nPresetNumber" — новый номер набора;
	void setPresetNumber(size_t nPresetNumber);

	// Сеттер кол-ва дней;
	// "nDaysQuantity" — новый показатель кол-ва дней;
	void setDaysQuantity(size_t nDaysQuantity);

	// Геттер указателя на меню блюд;
	std::string* getCoursesMenuPointer();

	// Геттер указателя на меню напитков;
	std::string* getDrinksMenuPointer();


#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструктор со всеми параметрами;
	// "ptrOrder" — указатель на заказ;
	// "nPresetNumber" — номер набора;
	// "nDaysQuantity" — кол-во дней;
	OrderBuilder(Order* ptrOrder, size_t nPresetNumber, size_t nDaysQuantity);

	// Конструктор с указателем на заказ;
	// "ptrOrder" — указатель на заказ;
	OrderBuilder(Order* ptrOrder);

	// Конструктор по умолчанию;
	explicit OrderBuilder();
	
	// Деструктор;
	virtual ~OrderBuilder();

	// Конструктор копирования;
	// "anotherOrderBuilder" — копируемый объект;
	OrderBuilder(OrderBuilder& anotherOrderBuilder);

	// Перегрузка оператора "=";
	// "anotherOrderBuilder" — копируемый объект;
	// returns — девый операнд;
	OrderBuilder& operator=(OrderBuilder& anotherOrderBuilder);



#pragma endregion CONSTRUCTION


private:

	// Получает меню из файла;
	void initializeMenu();

	// Указатель на экземпляр заказа;
	Order* currentOrder;

	// Номер выбранного набора;
	size_t nPresetNumber;

	// Кол-во дней, выбранное пользователем;
	size_t nDaysQuantity;

	// Список блюд с их параметрами;
	std::string* ptrCoursesMenu;

	// Список напитков с их стоимостью;
	std::string* ptrDrinksMenu;

	// Размер массива меню блюд;
	size_t nCoursesMenuSize;

	// Размер массива меню напитков;
	size_t nDrinksMenuSize;

};



#endif