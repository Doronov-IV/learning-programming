#ifndef ORDER_H
#define ORDER_H


#include "Drink.h"
#include <vector>





#pragma region PREPROCESSING



// Константа, необходимая для трансформации порядковых показателей в количественные.
// Пример: третий день будет вторым по счёту в массивах и тд.
#ifndef NUMERATION_ISSUE
#define NUMERATION_ISSUE 1
#endif


// Константа, представляет собой фиксированное кол-во приёмов пищи в день.
// Пример: завтрак, обед, второй обед, ужин, второй ужин – всего 5.
#ifndef SITTINGS_COUNTER
#define SITTINGS_COUNTER 5
#endif


// Запасная константа, которая появилась после переименования класса 
// "Meal" в "Course". Всё из-за того, что в английском языке слово "Meal"
// имеет несколько значений.
#ifndef MEALS_COUNTER
#define MEALS_COUNTER 5
#endif


// Запасной, аналог MEALS_COUNTER;
#ifndef MEAL_QUANTITY 
#define MEAL_QUANTITY 5
#endif


// Кол-во позиций для одного объекта в текстовом файле;
#ifndef PROPERTIES_QUANTITY
#define PROPERTIES_QUANTITY 3
#endif


/* Order builder & decorator */


#ifndef PRESET_ONE
#define PRESET_ONE 1
#endif

#ifndef PRESET_TWO
#define PRESET_TWO 2
#endif

#ifndef PRESET_THREE
#define PRESET_THREE 3
#endif

#ifndef PRESET_MANUAL
#define PRESET_MANUAL 5
#endif

#ifndef THREE_COURSES
#define THREE_COURSES 3
#endif

#ifndef FOUR_COURSES
#define FOUR_COURSES 4
#endif

#ifndef FIVE_COURSES
#define FIVE_COURSES 5
#endif

#ifndef CALORIES
#define CALORIES 1
#endif

#ifndef PRICE
#define PRICE 2
#endif



#pragma endregion PREPROCESSING



// Класс "Заказ";
class Order
{


public:



#pragma region LOGIC

	// Добавляет блюдо;
	// "newCourse" — новое блюдо;
	void addCourse(Course newCourse);

	// Добавляет напиток;
	// "newDrink" — новый напиток;
	void addDrink(Drink newDrink);

	// Очищает все поля;
	void clear();


#pragma endregion LOGIC




#pragma region ACCESS

	// Геттер общего кол-ва блюд;
	// returns — общее кол-во блюд;
	size_t getCourseCounter() const;

	// Сеттер общего кол-ва блюд;
	// "nCourseCounter" — общее кол-во блюд;
	void setCourseCounter(size_t nCourseCounter);

	// Геттер общего кол-ва дней;
	// returns — общее кол-во дней;
	size_t getDaysCounter() const;

	// Сеттер общего кол-ва дней;
	// "nDaysCounter" — общее кол-во дней;
	void setDaysCounter(size_t nDaysCounter);

	// Геттер указателя на массив блюд;
	// returns — указатель на массив блюд;
	std::vector<Course>* getCoursesPointer();

	// Геттер указателя на массив напитков;
	// returns — указатель на массив напитков;
	std::vector<Drink>* getDrinksPointer();

	// Геттер времени доставки. [!] парсит числовые значения в символьный тип данных;
	// returns — время доставки в виде строки;
	std::string getDeliveryTime();

	// Геттер общей калорийности заказа;
	// returns — счётчик общего показателя калорийности;
	double getOverallCalories();

	// Геттер общей стоимости заказа;
	// returns — общая стоимость;
	double getOverallPrice();


#pragma endregion ACCESS




#pragma region CONSTRUCTION

	// Конструктор по умолчанию; 
	Order();

	// Конструктор копирования;
	// "anotherOrder" — объект для копирования;
	Order(Order& anotherOrder);

	// Перегрузка оператора "=";
	// "anotherOrder" — правый операнд;
	// returns — левый операнд;
	Order& operator=(Order& anotherOrder);

	// Деструктор;
	virtual ~Order();


#pragma endregion CONSTRUCTION



private:

	// Счётчик кол-ва блюд для каждого(!) приёма пищи;
	// Раньше их было несколько;
	size_t nCourseCounter;

	// Показатель кол-ва дней, в течение которых клиенту будут доставляться блюда;
	size_t nDaysCounter;

	// Общий список блюд;
	std::vector<Course>* ptrCourses;

	// Общий список напитков;
	std::vector<Drink>* ptrDrinks;

	// Час доставки;
	size_t nDeliveryHour;

	// Минута доставки;
	size_t nDeliveryMinute;

	// Общая стоимость заказа;
	double nGlobalPriceCounter;

	// Общая калорийность блюд и напитков;
	double nGlobalCaloriesCounter;




};


#endif