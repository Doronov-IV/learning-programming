#ifndef STORE_SIMULATOR_H
#define STORE_SIMULATOR_H


#include "Store.h"
#include "../Misc/Toolbox.h"

#include <thread>


using namespace std;


// Класс, который будет осуществлять визуализацию магазина;
class StoreSimulator
{


public:

	// Запрашивает экземпляр симулятора;
	static StoreSimulator* getInstance();

	// Основной метод, запускающий симулятор;
	void run();

	// Через sleep разделяет действия пользователя;
	void drawFeedback();

	// Рисует меню;
	void drawMenu();

	// Рисует предупреждение, если не хватает клиентов и/или сотрудников;
	void drawWarning();

	// Рисует линии;
	void drawField();

	// Рисует список клиентов;
	void drawCustomersList();

	// Очищает поле клиентов;
	void clearCustomersField();

	// Рисует список сотрудников;
	void drawEmployeeList();

	// Очищает поле сотрудников;
	void clearEmployeeField();

	// Рисует список истории заказов;
	void drawHistoryList();

	// Очищает поле истории заказов;
	void clearHistoryField();

private:

	// Экземпляр магазина;
	Store* example;



#pragma region SINGLETON_STUFF


	// Экземпляр нашего симулятора;
	static StoreSimulator* instance;

	// Конструктор по умолчанию;
	StoreSimulator();

	// Деструктор;
	virtual ~StoreSimulator();


#pragma endregion SINGLETON_STUFF


};


#endif