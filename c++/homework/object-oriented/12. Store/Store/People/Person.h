#ifndef PERSON_H
#define PERSON_H


#include <string>


// Класс "Человек";
class Person
{


public:


#pragma region ACCESS

	// Сеттер имени;
	// "sName" — имя;
	virtual void setName(std::string sName);

	// Геттер имени;
	// returns — имя;
	virtual std::string getName();

	// Сеттер возраста;
	// "nAge" — возраст;
	void setAge(size_t nAge);

	// Геттер возраста;
	// returns — ;
	size_t getAge();


#pragma endregion ACCESS




#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Person();
	
	// Конструктор с именем;
	// "sName" — имя;
	Person(std::string sName);

	// Конструктор с именем и возрастом;
	// "sName" — имя;
	// "nAge" — возраст;
	Person(std::string sName, size_t nAge);

	// Деструктор;
	virtual ~Person() = 0;

	// Конструктор копиирования;
	// "anotherPerson" — объект для копировани;
	Person(Person& anotherPerson);

	// Перегрузка оператора "=";
	// "anotherPerson" — объект для копирования;
	// returns — ссылку на левый экемпляр;
	Person& operator=(Person& anotherPerson);


#pragma endregion CONSTRUCTION



protected:

	// Имя человека;
	std::string sName;

	// Возраст;
	size_t nAge;

};


#endif