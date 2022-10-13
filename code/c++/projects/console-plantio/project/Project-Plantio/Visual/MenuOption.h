#ifndef MENU_OPTION_H
#define MENU_OPTION_H


// Подключает тулбокс, если он ещё не подключён;
#ifndef TOOLBOX_H
#include "../Misc/Toolbox.h"
#endif


#include <vector>


// Класс "Пункт меню";
class MenuOption
{


public:


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	MenuOption();

	// Деструктор;
	virtual ~MenuOption();

	// Конструктор с именем;
	// "sName" — имя;
	MenuOption(std::string sName);

	// Конструктор копирования;
	// "anotherMenuOption" — копируемый объект;
	MenuOption(const MenuOption& anotherMenuOption);

	// Перегрузка оператора "=";
	// "anotherMenuOption" — копируемый объект;
	// returns — левый операнд;
	MenuOption& operator=(const MenuOption& anotherMenuOption);


#pragma endregion CONSTRUCTION




#pragma region ACCESS

	// Геттер имени пункта;
	// returns — имя пункта;
	std::string getName();

	// Сеттер имени пункта;
	// "sName" — имя пункта;
	void setName(std::string sName);

	// Геттер массива пунктов выпадающего меню;
	// returns — массив пунктов выпадающего меню;
	std::vector<MenuOption*> getDropDownMenu();

	// Сеттер массива пунктов выпадающего меню;
	// "ptrDropDownMenuActions" — массив пунктов выпадающего меню;
	void setDropDownMenu(std::vector<MenuOption*> ptrDropDownMenuActions);

	// Показывает, не выбран ли пункт;
	bool isSelected;

	// Показывает, не "упало" ли меню;
	bool isMenuDown;


#pragma endregion ACCESS


private:

	// Имя пункта;
	std::string sName;

	// Массив пунктов выпадающего меню;
	std::vector<MenuOption*> ptrDropDownMenuActions;


};


#endif

