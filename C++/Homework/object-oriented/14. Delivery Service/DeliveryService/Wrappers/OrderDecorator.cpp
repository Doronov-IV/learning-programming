#include "OrderDecorator.h"




#pragma region LOGIC


void OrderDecorator::printDetails()
{
	int nOverallCourseCounter = 0;
	int nOverallDrinkCounter = 0;

	int nOverallPrice = 0;
	int nOverallCalories = 0;

	// Actually info printing;
	for (size_t i = 0ULL; i < this->ptrOrder->getDaysCounter(); i++)
	{
		line;
		std::cout << FOREBLU << "Day " << i + 1;
		line;
		for (size_t j = 0ULL; j < MEAL_QUANTITY; j++)
		{
			// cout'ing courses;
			line;
			for (size_t k = 0ULL; k < this->ptrOrder->getCourseCounter(); k++)
			{
				if ("Null" != this->ptrOrder->getCoursesPointer()->operator[](nOverallCourseCounter).getName())
				{
					std::cout << FOREYEL << this->ptrOrder->getCoursesPointer()->operator[](nOverallCourseCounter).getName() << FOREWHT << " (" <<
						this->ptrOrder->getCoursesPointer()->operator[](nOverallCourseCounter).getPrice() << " EUR);";
					nOverallCourseCounter += 1;
					line;
				}
				else
				{
					std::cout << FOREBLU << "[Empty];" << FOREWHT;
					nOverallCourseCounter += 1;
					line;
				}
			}

			// cout'ing drinks;
			line;
			if ("Null" != this->ptrOrder->getDrinksPointer()->operator[](nOverallDrinkCounter).getName())
			{
				std::cout << FORECYN << this->ptrOrder->getDrinksPointer()->operator[](nOverallDrinkCounter).getName() << FOREWHT << " (" <<
					this->ptrOrder->getDrinksPointer()->operator[](nOverallDrinkCounter).getPrice() << " EUR);";
				nOverallDrinkCounter += 1;
				line;
			}
			else
			{
				std::cout << FOREBLU << "[Empty];" << FOREWHT;
				nOverallDrinkCounter += 1;
				line;
			}
		}
	}


	// Summary;
	space;
	std::cout << BACKBLU << "Summary:" << BACKBLK;
	line;
	std::cout << "Total calories: " << BACKBLU << ptrOrder->getOverallCalories() << " Cal" << FOREWHT << BACKBLK;
	line;
	std::cout << "Total price: " << BACKBLU << ptrOrder->getOverallPrice() << " EUR" << FOREWHT << BACKBLK;
	space;
}


#pragma endregion LOGIC



#pragma region ACCESS


Order* OrderDecorator::getOrderPointer()
{
	return ptrOrder;
}


void OrderDecorator::setOrderPointer(Order* ptrOrder)
{
	this->ptrOrder = ptrOrder;
}


#pragma endregion ACCESS



#pragma region CONSTRUCTION


OrderDecorator::OrderDecorator()
{
	ptrOrder = new Order();
}


OrderDecorator::OrderDecorator(Order* ptrOrder)
{
	this->ptrOrder = ptrOrder;
}


OrderDecorator::~OrderDecorator()
{
	if (nullptr != ptrOrder)
	{
		delete ptrOrder;
		ptrOrder = nullptr;
	}
}


OrderDecorator::OrderDecorator(OrderDecorator& anotherOrderDecorator)
{
	this->ptrOrder = anotherOrderDecorator.ptrOrder;
}


OrderDecorator& OrderDecorator::operator=(OrderDecorator& anotherOrderDecorator)
{
	this->ptrOrder = anotherOrderDecorator.ptrOrder;
	return *this;
}


#pragma endregion CONSTRUCTION