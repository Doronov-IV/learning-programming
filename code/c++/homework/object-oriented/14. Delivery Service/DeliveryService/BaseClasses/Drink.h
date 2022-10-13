#ifndef DRINK_H
#define DRINK_H


#include "Course.h"
#include <string>


// Класс "Напиток";
// Наследуется от Meal, потому что это в целом идентичные сущности;
class Drink : public Course
{

public:


#pragma region CONSTRUCTION

	// Конструктор;
	Drink();

	// Конструктор с параметрами;
	Drink(std::string sMealName, std::string nMealCalories, std::string dMealPrice);

	// Деструктор (деконструктор);
	virtual ~Drink();

	// Конструктор копирования;
	// "anotherDrink" — копируемый объект;
	Drink(const Drink& anotherDrink);

	// Перегрузка оператора "=";
	// "anotherDrink" — копируемый объект;
	// returns — левый операнд;
	Drink& operator=(const Drink& anotherDrink);


#pragma endregion CONSTRUCTION


};


#endif
