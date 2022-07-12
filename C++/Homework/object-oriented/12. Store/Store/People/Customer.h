#ifndef CUSTOMER_H
#define CUSTOMER_H


#include "Person.h"


// Класс "Покупатель";
class Customer : public Person
{


public:


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Customer();

	// Конструктор с именем;
	// "sName" — имя;
	Customer(std::string sName);

	// Конструктор с именем и возрастом;
	// "sName" — имя;
	// "nAge" — возраст;
	Customer(std::string sName, size_t nAge);

	// Деструктор;
	virtual ~Customer();

	// Конструктор копиирования;
	// "anotherPerson" — объект для копировани;
	Customer(const Customer& anotherPerson);

	// Перегрузка оператора "=";
	// "anotherPerson" — объект для копирования;
	// returns — ссылку на левый экемпляр;
	Customer& operator=(const Customer& anotherPerson);


#pragma endregion CONSTRUCTION




};


#endif