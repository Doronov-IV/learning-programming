#include "OrderBuilder.h"



#pragma region LOGIC


void OrderBuilder::addMeal()
{
	if (nullptr == currentOrder) currentOrder = new Order();
	currentOrder->setDaysCounter(nDaysQuantity); // !

	if (PRESET_ONE == nPresetNumber) currentOrder->setCourseCounter(THREE_COURSES);
	else if (PRESET_TWO == nPresetNumber) currentOrder->setCourseCounter(FOUR_COURSES);
	else currentOrder->setCourseCounter(FIVE_COURSES);
	
	int random;

	// adding N courses;
	for (size_t i = 0ULL; i < currentOrder->getCourseCounter(); i++)
	{
		do
		{
			random = rand() % nCoursesMenuSize;
		} while (0 != random % 3);

		Course newCourse;
		newCourse.setName(ptrCoursesMenu[random]);
		newCourse.setCalories(ptrCoursesMenu[random + CALORIES]);
		newCourse.setPrice(ptrCoursesMenu[random + PRICE]);

		currentOrder->addCourse(newCourse);
	}

	random = 0;


	// adding 1 drink;
	do
	{
		random = rand() % nDrinksMenuSize;
	} while (0 != random % 3);

	Drink newDrink;
	newDrink.setName(ptrDrinksMenu[random]);
	newDrink.setCalories(ptrDrinksMenu[random + CALORIES]);
	newDrink.setPrice(ptrDrinksMenu[random + PRICE]);

	currentOrder->addDrink(newDrink);
}


#pragma endregion LOGIC



#pragma region ACCESS


Order* OrderBuilder::getOrderPointer()
{
	return this->currentOrder;
}


size_t OrderBuilder::getPresetNumber()
{
	return this->nPresetNumber;
}


size_t OrderBuilder::getDaysQuantity()
{
	return this->nDaysQuantity;
}


void OrderBuilder::setPresetNumber(size_t nPresetNumber)
{
	this->nPresetNumber = nPresetNumber;
}


void OrderBuilder::setDaysQuantity(size_t nDaysQuantity)
{
	this->nDaysQuantity = nDaysQuantity;
}


std::string* OrderBuilder::getCoursesMenuPointer()
{
	return this->ptrCoursesMenu;
}


std::string* OrderBuilder::getDrinksMenuPointer()
{
	return this->ptrDrinksMenu;
}


#pragma endregion ACCESS



#pragma region CONSTRUCTION


void OrderBuilder::initializeMenu()
{
	FileBufferedReader reader;

	// reading courses menu;
	reader.read("DeliveryService/Misc/CoursesMenu.txt");
	ptrCoursesMenu = new std::string[reader.getBufferSize()];
	for (size_t i = 0ULL; i < reader.getBufferSize(); i++)
	{
		for (size_t j = 0ULL; j < strlen(reader.getBuffer()[i]); j++)
		{
			ptrCoursesMenu[i].push_back(reader.getBuffer()[i][j]);
		}
	}
	nCoursesMenuSize = reader.getBufferSize();
	
	// reading drinks menu;
	reader.read("DeliveryService/Misc/DrinksMenu.txt");
	ptrDrinksMenu = new std::string[reader.getBufferSize()];
	for (size_t i = 0ULL; i < reader.getBufferSize(); i++)
	{
		for (size_t j = 0ULL; j < strlen(reader.getBuffer()[i]); j++)
		{
			ptrDrinksMenu[i].push_back(reader.getBuffer()[i][j]);
		}
	}
	nDrinksMenuSize = reader.getBufferSize();
}


OrderBuilder::OrderBuilder(Order* ptrOrder, size_t nPresetNumber, size_t nDaysQuantity)
{
	this->currentOrder = ptrOrder;
	this->nPresetNumber = nPresetNumber;
	this->nDaysQuantity = nDaysQuantity;
	initializeMenu();
}


OrderBuilder::OrderBuilder(Order* ptrOrder)
{
	this->currentOrder = ptrOrder;
	this->nPresetNumber = 0;
	this->nDaysQuantity = 0;
	initializeMenu();
}


OrderBuilder::OrderBuilder()
{
	this->currentOrder = new Order();
	this->nPresetNumber = 0;
	this->nDaysQuantity = 0;
	initializeMenu();
}

OrderBuilder::~OrderBuilder()
{
	if (nullptr != this->currentOrder)
	{
		delete this->currentOrder;
		this->currentOrder = nullptr;
	}

	if (nullptr != this->ptrCoursesMenu)
	{
		delete[] this->ptrCoursesMenu;
		this->ptrCoursesMenu = nullptr;
	}

	this->nPresetNumber = 0;
	this->nDaysQuantity = 0;
}


OrderBuilder::OrderBuilder(OrderBuilder& anotherOrderBuilder)
{
	this->currentOrder = anotherOrderBuilder.currentOrder;
	this->nPresetNumber = anotherOrderBuilder.nPresetNumber;
	this->nDaysQuantity = anotherOrderBuilder.nDaysQuantity;
	this->ptrCoursesMenu = anotherOrderBuilder.ptrCoursesMenu;
	this->ptrCoursesMenu = anotherOrderBuilder.ptrDrinksMenu;
}


OrderBuilder& OrderBuilder::operator=(OrderBuilder& anotherOrderBuilder)
{
	this->currentOrder = anotherOrderBuilder.currentOrder;
	this->nPresetNumber = anotherOrderBuilder.nPresetNumber;
	this->nDaysQuantity = anotherOrderBuilder.nDaysQuantity;
	this->ptrCoursesMenu = anotherOrderBuilder.ptrCoursesMenu;
	this->ptrCoursesMenu = anotherOrderBuilder.ptrDrinksMenu;
	return *this;
}



#pragma endregion CONSTRUCTION