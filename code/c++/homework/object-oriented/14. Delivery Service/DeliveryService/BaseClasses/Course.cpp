#include "Course.h"



#pragma region CONSTRUCTION


Course::Course()
{
	this->sName = "Null";
	this->sCalories = "Null";
	this->sPrice = "Null";
}


Course::Course(std::string sMealName, std::string nMealCalories, std::string dMealPrice)
{
	this->sName = sMealName;
	this->sCalories = nMealCalories;
	this->sPrice = dMealPrice;
}


Course::~Course()
{
	this->sName = "Null";
	this->sCalories = "Null";
	this->sPrice = "Null";
}


Course::Course(const Course& anotherCourse)
{
	this->sName = anotherCourse.sName;
	this->sCalories = anotherCourse.sCalories;
	this->sPrice = anotherCourse.sPrice;
}


Course& Course::operator=(const Course& anotherCourse)
{
	this->sName = anotherCourse.sName;
	this->sCalories = anotherCourse.sCalories;
	this->sPrice = anotherCourse.sPrice;
	return *this;
}


#pragma endregion CONSTRUCTION




#pragma region ACCESS


std::string Course::getName() const
{
	return sName;
}


std::string Course::getCalories() const
{
	return sCalories;
}


std::string Course::getPrice() const
{
	return sPrice;
}


void Course::setName(std::string sName)
{
	this->sName = sName;
}


void Course::setCalories(std::string sCalories)
{
	this->sCalories = sCalories;
}


void Course::setPrice(std::string sPrice)
{
	this->sPrice = sPrice;
}


#pragma endregion ACCESS