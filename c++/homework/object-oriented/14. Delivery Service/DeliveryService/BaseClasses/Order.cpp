#include "Order.h"



#pragma region LOGIC


void Order::addCourse(Course newCourse)
{
	if (nullptr == ptrCourses) ptrCourses = new std::vector<Course>();
	ptrCourses->push_back(newCourse);

	nGlobalCaloriesCounter += std::stod(newCourse.getCalories());
	nGlobalPriceCounter += std::stod(newCourse.getPrice());
}


void Order::addDrink(Drink newDrink)
{
	if (nullptr == ptrDrinks) ptrDrinks = new std::vector<Drink>();
	ptrDrinks->push_back(newDrink);

	nGlobalCaloriesCounter += std::stod(newDrink.getCalories());
	nGlobalPriceCounter += std::stod(newDrink.getPrice());
}


void Order::clear()
{
	nCourseCounter = 0;

	nDaysCounter = 0;

	ptrCourses = nullptr;

	ptrDrinks = nullptr;

	nDeliveryHour = 0;

	nDeliveryMinute = 0;

	nGlobalPriceCounter = 0;

	nGlobalCaloriesCounter = 0;
}


#pragma endregion LOGIC




#pragma region ACCESS


size_t Order::getCourseCounter() const
{
	return nCourseCounter;
}


void Order::setCourseCounter(size_t nCourseCounter)
{
	this->nCourseCounter = nCourseCounter;
}


size_t Order::getDaysCounter() const
{
	return nDaysCounter;
}


void Order::setDaysCounter(size_t nDaysCounter)
{
	this->nDaysCounter = nDaysCounter;
}




std::vector<Course>* Order::getCoursesPointer()
{
	return ptrCourses;
}


std::vector<Drink>* Order::getDrinksPointer()
{
	return ptrDrinks;
}


std::string Order::getDeliveryTime()
{
	std::string result = "";

	result.append(std::to_string(nDeliveryHour) + ':' + std::to_string(nDeliveryMinute));

	return result;
}


double Order::getOverallCalories()
{
	return this->nGlobalCaloriesCounter;
}


double Order::getOverallPrice()
{
	return this->nGlobalPriceCounter;
}


#pragma endregion ACCESS




#pragma region CONSTRUCTION


Order::Order()
{
	nCourseCounter = 0;

	nDaysCounter = 0;

	ptrCourses = nullptr;

	ptrDrinks = nullptr;

	nDeliveryHour = 0;

	nDeliveryMinute = 0;

	nGlobalPriceCounter = 0;

	nGlobalCaloriesCounter = 0;
}


Order::Order(Order& anotherOrder)
{
	this->nCourseCounter = anotherOrder.nCourseCounter;

	this->nDaysCounter = anotherOrder.nDaysCounter;

	this->ptrCourses = anotherOrder.ptrCourses;

	this->ptrDrinks = anotherOrder.ptrDrinks;

	this->nDeliveryHour = anotherOrder.nDeliveryHour;

	this->nDeliveryMinute = anotherOrder.nDeliveryMinute;

	this->nGlobalPriceCounter = anotherOrder.nGlobalPriceCounter;

	this->nGlobalCaloriesCounter = anotherOrder.nGlobalCaloriesCounter;
}


Order& Order::operator=(Order& anotherOrder)
{
	this->nCourseCounter = anotherOrder.nCourseCounter;

	this->nDaysCounter = anotherOrder.nDaysCounter;

	this->ptrCourses = anotherOrder.ptrCourses;

	this->ptrDrinks = anotherOrder.ptrDrinks;

	this->nDeliveryHour = anotherOrder.nDeliveryHour;

	this->nDeliveryMinute = anotherOrder.nDeliveryMinute;

	this->nGlobalPriceCounter = anotherOrder.nGlobalPriceCounter;

	this->nGlobalCaloriesCounter = anotherOrder.nGlobalCaloriesCounter;

	return *this;
}


Order::~Order()
{
	nCourseCounter = 0;

	nDaysCounter = 0;

	ptrCourses = nullptr;

	ptrDrinks = nullptr;

	nDeliveryHour = 0;

	nDeliveryMinute = 0;
}


#pragma endregion CONSTRUCTION