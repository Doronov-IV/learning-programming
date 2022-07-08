#include "MenuOption.h"




#pragma region ACCESS


std::string MenuOption::getName()
{
	return sName;
}


void MenuOption::setName(std::string sName)
{
	this->sName = sName;
}


std::vector<MenuOption*> MenuOption::getDropDownMenu()
{
	return ptrDropDownMenuActions;
}


void MenuOption::setDropDownMenu(std::vector<MenuOption*> ptrDropDownMenuActions)
{
	this->ptrDropDownMenuActions = ptrDropDownMenuActions;
}


#pragma endregion ACCESS




#pragma region CONSTRUCTION


MenuOption::MenuOption()
{
	sName = "Null";
	ptrDropDownMenuActions;
}


MenuOption::MenuOption(std::string sName)
{
	this->sName = sName;
	ptrDropDownMenuActions;
}



MenuOption::MenuOption(const MenuOption& anotherMenuOption)
{
	this->sName = anotherMenuOption.sName;
	this->ptrDropDownMenuActions = anotherMenuOption.ptrDropDownMenuActions;
	this->isMenuDown = anotherMenuOption.isMenuDown;
	this->isSelected = anotherMenuOption.isSelected;
}


MenuOption& MenuOption::operator=(const MenuOption& anotherMenuOption)
{
	this->sName = anotherMenuOption.sName;
	this->ptrDropDownMenuActions = anotherMenuOption.ptrDropDownMenuActions;
	this->isMenuDown = anotherMenuOption.isMenuDown;
	this->isSelected = anotherMenuOption.isSelected;
	return *this;
}


MenuOption::~MenuOption()
{
	for (auto &option : ptrDropDownMenuActions)
	{
		if (nullptr != option)
		{
			delete option;
			option = nullptr;
		}
	}

	ptrDropDownMenuActions.clear();
}


#pragma endregion CONSTRUCTION
