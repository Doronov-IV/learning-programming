#include "OrderDirector.h"


#pragma region LOGIC


void OrderDirector::buildOrder()
{
	for (size_t i = 0ULL; i < ptrBuilder->getDaysQuantity(); i++)
	{
		for (size_t j = 0ULL; j < MEAL_QUANTITY; j++)
		{
			ptrBuilder->addMeal();
		}
	}
}


#pragma endregion LOGIC




#pragma region ACCESS


OrderBuilder* OrderDirector::getBuilderPointer()
{
	return ptrBuilder;
}


#pragma endregion ACCESS




#pragma region CONSTRUCTION


OrderDirector::OrderDirector()
{
	this->ptrBuilder = new OrderBuilder();
	ptrBuilder->setDaysQuantity(0);
	ptrBuilder->setPresetNumber(0);
}

OrderDirector::OrderDirector(size_t nPresetNumber, size_t nDaysQuantity)
{
	this->ptrBuilder = new OrderBuilder();
	ptrBuilder->setDaysQuantity(nDaysQuantity);
	ptrBuilder->setPresetNumber(nPresetNumber);
}


OrderDirector::~OrderDirector()
{
	if (nullptr != ptrBuilder)
	{
		delete ptrBuilder;
		ptrBuilder = nullptr;
	}
}


OrderDirector::OrderDirector(OrderDirector& anotherOrderDirector)
{
	this->ptrBuilder = anotherOrderDirector.ptrBuilder;
}


OrderDirector& OrderDirector::operator=(OrderDirector& anotherOrderDirector)
{
	this->ptrBuilder = anotherOrderDirector.ptrBuilder;
	return *this;
}


#pragma endregion CONSTRUCTION