#include "ConsoleManager.h"



void ConsoleManager::setCursor(size_t x_coord, size_t y_coord)
{
	this->сurrentCoords.set(x_coord, y_coord);
	std::cout << "\033[" << std::to_string(y_coord) << ";" << std::to_string(x_coord) << "f";
}



void ConsoleManager::printColoredText(std::string sColor, std::string sText)
{
	if ("Black" == sColor) std::cout << FOREBLK;
	else if ("Red" == sColor) std::cout << FORERED;
	else if ("Green" == sColor) std::cout << FOREGRN;
	else if ("Yellow" == sColor) std::cout << FOREYEL;
	else if ("Blue" == sColor) std::cout << FOREBLU;
	else if ("Magent" == sColor) std::cout << FOREMAG;
	else if ("Cyan" == sColor) std::cout << FORECYN;
	else if ("White" == sColor) std::cout << FOREWHT;
	else return;

	this->sTextColor = sColor;
	std::cout << sText;
}


void ConsoleManager::clear()
{
	std::cout << "\x1B[2J\x1B[H";
}


Coord ConsoleManager::getCursorPosition()
{
	return сurrentCoords;
}


std::string ConsoleManager::getCurrentTextColor()
{
	return this->sTextColor;
}




#pragma region CONSTRUCTION


static ConsoleManager* instance;


ConsoleManager* ConsoleManager::instance = nullptr;


ConsoleManager* ConsoleManager::getInstance()
{
	if (nullptr == instance) instance = new ConsoleManager();
	return instance;
}


ConsoleManager::ConsoleManager()
{
	this->сurrentCoords.set(0, 0);
}


void ConsoleManager::release()
{
	if (nullptr != instance)
	{
		delete instance;
		instance = nullptr;
	}
}


ConsoleManager::~ConsoleManager()
{

}


#pragma endregion CONSTRUCTION
