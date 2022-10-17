#ifndef VISUAL_PLANTIO_H
#define VISUAL_PLANTIO_H


#include "../BaseClasses/Plantio.h"
#include "MenuOption.h"
#include "../BaseClasses/Currency.h"
#include "../Wrappers/ScheduleSorter.h"


#include <thread>


using namespace std;


#pragma region PREPROCESSING

// Подключает тулбокс, если он ещё не подключён;
#ifndef TOOLBOX_H
#include "../Misc/Toolbox.h"
#endif


#ifndef SCREEN_HEIGHT
#define SCREEN_HEIGHT 36
#endif

#ifndef SCREEN_WIDTH
#define SCREEN_WIDTH 135
#endif


#ifndef SCREEN_HEIGHT_DEFAULT
#define SCREEN_HEIGHT_DEFAULT 36
#endif

#ifndef SCREEN_WIDTH_DEFAULT
#define SCREEN_WIDTH_DEFAULT 135
#endif

// Два пробела между пунктами верхнего меню;
#ifndef TWO_SPACES
#define TWO_SPACES 2
#endif

// У-координата выпадающего меню;
#ifndef DROP_DOWN_MENU_Y
#define DROP_DOWN_MENU_Y 2
#endif

// Ширина выпадающего меню;
#ifndef DROP_DOWN_MENU_WIDTH
#define DROP_DOWN_MENU_WIDTH 30
#endif

#ifndef CURRENCY_DROP_DOWN_MENU_WIDTH
#define CURRENCY_DROP_DOWN_MENU_WIDTH 48
#endif

#ifndef CURRENCY_MENU_X
#define CURRENCY_MENU_X 44
#endif

#ifndef CURRENCY_USER_INPUT_X
#define CURRENCY_USER_INPUT_X 51
#endif

#ifndef CURRENCY_USER_INPUT_Y
#define CURRENCY_USER_INPUT_Y 7
#endif

#ifndef OPTIONS_MENU_X
#define OPTIONS_MENU_X 16
#endif

#ifndef STOCK_MENU_X
#define STOCK_MENU_X 2
#endif

#ifndef WHITE_SHEET_START_X 
#define WHITE_SHEET_START_X 5
#endif

#ifndef WHITE_SHEET_START_Y
#define WHITE_SHEET_START_Y 12
#endif

#ifndef clear_screen_view
#define clear_screen_view drawScreenInRange(0, SCREEN_WIDTH, 0, SCREEN_HEIGHT + 20)
#endif

#ifndef clear_screen
#define clear_screen drawScreenInRange(0, SCREEN_WIDTH, 0, SCREEN_HEIGHT)
#endif

#pragma endregion PREPROCESSING


// Класс, отвечающий за происходящее в консоли;
class VisualPlantio
{


public:


	//Запрашивает экземпляр класса;
	static VisualPlantio* getInstance();

	// Основной метод, запускающий интерфейс;
	void run();


private:

	// Экземпляр класса;
	static VisualPlantio* instance;

	// Конструктор по умолчанию;
	VisualPlantio();

	// Деструктор;
	virtual ~VisualPlantio();

	// Указатель на экземпляр класса Plantio; 
	Plantio* ptrPlantio;

	// Массив "кнопок" верхнего меню;
	std::vector<std::vector<MenuOption*>> vAllMajorMenuOptions;

	// Массив "кнопок" меню view;
	std::vector<MenuOption*> vViewTableOptions;

	// Массив программ по умолчанию;
	vector<ScheduleDecorator*> vDefaultSchedules;

	// Текущие схемы;
	vector<ScheduleDecorator*> vCurrentSchedules;

	// Для изменения информации на экране в разделе "Вид" при смене валюты;
	bool bisViewDown;

	// Указатель на список валют;
	// Является свойством программы, см "Options";
	Currency* ptrCurrencies;


#pragma region VISUALIZE

	// Задать размер экрана и раскрасить поле (if needed);
	void init();

	// Приготовить стандартные программы;
	void prepareDefaultSchedules();

	// Нарисовать поле в заданном диапазоне;
	void drawScreenInRange(size_t start_x, size_t end_x, size_t start_y, size_t end_y);

	// Перерисовать верхнее меню;
	void refreshTopMenu();

	// Переместить курсор влево;
	void moveLeft();

	// Переместить курсор вправо;
	void moveRight();

	// Переместить курсор вверх;
	void moveUpDropDownMenu();

	// Переместить курсор вниз;
	void moveDownDropDownMenu();

	// Переместить курсор вверх;
	void moveUpGeneralMenu();

	// Переместить курсор вниз;
	void moveDownGeneralMenu();

	// Нарисовать выпадающее меню;
	void drawDropDownMenu();

	// Нарисовать раздел "Вид";
	void drawView();

	// Нарисовать список кнопок в таблице раздела "Вид";
	void drawViewTableMenu();

	// Нарисовать пункт меню;
	void drawOption(MenuOption* option);

	// Нарисовать список программ в обёртках;
	void drawSchedulesList();

	// Нарисовать меню выбора валют;
	void drawCurrencyOptions();

	// Нарисовать пункт 'Help';
	// Draw the 'Help' option;
	void drawHelpOption();

	// Нарисовать белое полотно с заголовком;
	// "sHeaderText" - заголовок на нулевой строке полотна;
	void drawDialogeSheet(std::string& sHeaderText);

	// Нарисовать страницу создания склада;
	void drawNewStock();

	// Нарисовать страницу загрузки существующего склада;
	void drawLoadStock();


#pragma endregion VISUALIZE



#pragma region AUXILIARY

	// Вернуть активный пункт меню;
	MenuOption* getSelectedOption();

	size_t getSelectedOptionRow();

	// Вернуть активный пункт выпавшего меню;
	MenuOption* getSelectedDropDownOption();

	// Сбросить выбор подпунктов выпадающего меню;
	void resetSubmenuSelection();

	// Получить цветовую схему для схемы;
	vector<string*> getColorSchemeForSchedule(Schedule* ptrSchedule);

	// Получить входные данные по валюте;
	void getUserCurrencyInput();

	// Собрать декораторы схем текущего склада;
	void loadCurrentSchedulesDecorators();


#pragma endregion AUXILIARY



#pragma region SORTINGS


	void useSort();


#pragma endregion SORTINGS


};



#endif

