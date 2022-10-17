#include "Drink.h"




#pragma region CONSTRUCTION


Drink::Drink()
{
	this->sName = "Null";
	this->sCalories = "Null";
	this->sPrice = "Null";
}


Drink::Drink(std::string sMealName, std::string nMealCalories, std::string dMealPrice)
{
	this->sName = sMealName;
	this->sCalories = nMealCalories;
	this->sPrice = dMealPrice;
}


Drink::~Drink()
{
	this->sName = "Null";
	this->sCalories = "Null";
	this->sPrice = "Null";
}


Drink::Drink(const Drink& anotherDrink)
{
	this->sName = anotherDrink.sName;
	this->sCalories = anotherDrink.sCalories;
	this->sPrice = anotherDrink.sPrice;
}


Drink& Drink::operator=(const Drink& anotherDrink)
{
	this->sName = anotherDrink.sName;
	this->sCalories = anotherDrink.sCalories;
	this->sPrice = anotherDrink.sPrice;
	return *this;
}


#pragma endregion CONSTRUCTION

