#ifndef COURSE_H
#define COURSE_H


#include <string>


// Класс "Блюдо";
class Course
{

public:


#pragma region CONSTRUCTION

	// Конструктор;
	Course();

	// Конструктор с параметрами;
	Course(std::string sMealName, std::string nMealCalories, std::string dMealPrice);

	// Деструктор (деконструктор);
	virtual ~Course();

	// Конструктор копирования;
	// "anotherMeal" — копируемый объект;
	Course(const Course& anotherMeal);

	// Перегрузка оператора "=";
	// "anotherMeal" — копируемый объект;
	// returns — левый операнд;
	Course& operator=(const Course& anotherMeal);


#pragma endregion CONSTRUCTION




#pragma region ACCESS

	// Геттер имени блюда;
	// returns — имя блюда;
	std::string getName() const;

	// Геттер показателя каллорий блюда;
	// returns — кол-во каллорий блюда;
	std::string getCalories() const;

	// Геттер стоимости блюда;
	// returns — стоимость блюда;
	std::string getPrice() const;

	// Сеттер имени блюда;
	// "sName" — имя блюда;
	void setName(std::string sName);

	// Сеттер показателя каллорий блюда;
	// "sCalories" — кол-во каллорий блюда;
	void setCalories(std::string sCalories);

	// Сеттер стоимости блюда;
	// "sPrice" — стоимость блюда;
	void setPrice(std::string sPrice);


#pragma endregion ACCESS


protected:

	// Название блюда;
	std::string sName;

	// Показатель каллорий;
	std::string sCalories;

	// Стоимость блюда;
	std::string sPrice;

};


#endif
