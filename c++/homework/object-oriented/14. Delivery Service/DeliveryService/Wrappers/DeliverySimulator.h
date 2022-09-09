#ifndef DELIVERY_SIMULATOR_H
#define DELIVERY_SIMULATOR_H


#include "../Builder/OrderDirector.h"
#include <thread>


using namespace std;

using Toolbox::setCursor;
using Toolbox::clearScreenInRange;
using Toolbox::cinSelector;
using Toolbox::getchSelector;



#pragma region PREPROCESSING

// Кол-во пунктов в главном меню доставки;
#ifndef LOOP_MENU_OPTIONS
#define LOOP_MENU_OPTIONS 5
#endif

// Кол-во пунктов в меню ручных пресетов;
#ifndef PRESET_MENU_OPTIONS
#define PRESET_MENU_OPTIONS 6
#endif

#ifndef MENU_LEFT_UP_X
#define MENU_LEFT_UP_X 5
#endif

#ifndef MENU_LEFT_UP_Y
#define MENU_LEFT_UP_Y 5
#endif

#ifndef COURSES_MENU_SIZE
#define COURSES_MENU_SIZE 13
#endif

#ifndef DRINKS_MENU_SIZE
#define DRINKS_MENU_SIZE 7
#endif


#pragma endregion PREPROCESSING



class DeliverySimulator
{


public:

	//Запрашивает экземпляр симулятора;
	static DeliverySimulator* getInstance();

	// Основной метод, запускающий симулятор;
	void run(size_t nDaysCounter);

	// Деструктор;
	virtual ~DeliverySimulator();

	// ;
	void release();


private:

	// Процесс выбора блюд и напитков из соответствующих меню;
	void commonSelectingProcedure(size_t nUserMealsInput, size_t nCurrentDaysCounter);

	// Инициализация Директора и Декоратора;
	// "nPresetNumber" — номер пресета;
	// "nDaysCounter" — кол-во дней;
	void initializeWrappers(size_t nPresetNumber, size_t nDaysCounter);

	// Указатель на текущий заказ;
	Order* ptrCurrentOrder;

	// Экземпляр декоратора;
	OrderDecorator* currentDecorator;

	// Экземпляр директора;
	OrderDirector* currentDirector;

	// Рисует меню блюд;
	void drawCoursesMenu();

	// Рисует меню блюд;
	void drawDrinksMenu();

	// Рисует пользовательское меню;
	void drawLoopMenu();

	// Рисует меню выбора количества блюд;
	void drawPresetMenu();

	// Рисует уведомление об успешном действии;
	void drawFeedBack(size_t x_coord, size_t y_coord);

	// Рисует предупреждение;
	// "sWarningMessage" — текст сообщения;
	void drawWarning(std::string sWarningMessage);

	// Экземпляр нашего симулятора;
	static DeliverySimulator* instance;

	// Конструктор по умолчанию;
	DeliverySimulator();

};


#endif