#include "ScheduleDecorator.h"




void ScheduleDecorator::drawSchedule(size_t start_x, size_t start_y, vector<string*> sColorScheme)
{
	using_adapter;
	using_currency;
	currency->setFileName("Misc/Currency.list");


	size_t max_x = start_x + SCREEN_WIDTH - 70;

	std::cout << BACKWHT << FOREBLK << "[";
	
	for (size_t x = start_x; x < max_x; x++)
	{
		std::cout << " ";
	}

	std::cout << BACKWHT << FOREBLK << "]";

	Toolbox::setCursor(start_x, start_y);
		
	

	Toolbox::setCursor(start_x + 2, start_y);
	std::cout << BACKWHT << FOREBLK;

	std::string sName = adapter->parseSpeciesEnum(ptrSchedule->getPlant()->getSpecies());
	std::cout << sName;

	Toolbox::setCursor(start_x + 28, start_y);

	for (size_t i = 0ULL; i < 7; i++)
	{
		std::cout << *sColorScheme[i] << BACKWHT << SQR;
		std::cout << BACKWHT << FOREBLK << " ";
	}

	std::cout << BACKWHT << FOREBLK;

	Toolbox::setCursor(max_x - 21, start_y);
	

	double dMultiplyer = 1.0;

	if (currency->getActiveCurrency() == L"RUB") dMultiplyer = 1.0;
	else if (currency->getActiveCurrency() == L"EUR") dMultiplyer = 0.011;
	else if (currency->getActiveCurrency() == L"USD") dMultiplyer = 0.012;


	std::wcout << std::setw(9) << ptrSchedule->getRevenue() * dMultiplyer << " " << currency->getActiveCurrency();

	Toolbox::setCursor(max_x - 3, start_y);

	std::cout << BACKWHT << FOREBLK;

	std::cout << std::setw(3) << ptrSchedule->getPlant()->getGrowingTime();

	std::cout << DEFAULT_TEXT;

}



#pragma region ACCESS


void ScheduleDecorator::setSchedule(Schedule* ptrSchedule)
{
	this->ptrSchedule = ptrSchedule;
}


Schedule* ScheduleDecorator::getSchedule()
{
	return this->ptrSchedule;
}


#pragma endregion ACCESS



#pragma region CONSTRUCTION


ScheduleDecorator::ScheduleDecorator()
{
	this->ptrSchedule = nullptr;
	this->isSelected = false;
}


ScheduleDecorator::ScheduleDecorator(Schedule* ptrSchedule)
{
	this->ptrSchedule = ptrSchedule;
	this->isSelected = false;
}


ScheduleDecorator::~ScheduleDecorator()
{
	if (nullptr != ptrSchedule)
	{
		delete ptrSchedule;
		ptrSchedule = nullptr;
	}
}


ScheduleDecorator::ScheduleDecorator(ScheduleDecorator& anotherDecorator)
{
	this->ptrSchedule = anotherDecorator.ptrSchedule;
	this->isSelected = anotherDecorator.isSelected;
}


ScheduleDecorator& ScheduleDecorator::operator=(ScheduleDecorator& anotherDecorator)
{
	this->ptrSchedule = anotherDecorator.ptrSchedule;
	this->isSelected = anotherDecorator.isSelected;
	return *this;
}


#pragma endregion CONSTRUCTION