#ifndef PRINTER_SIM_H
#define PRINTER_SIM_H


#include "../Misc/Toolbox.h"
#include "Printer.h"
#include <conio.h>
#include <thread>


using namespace std;


// Класс-симулятор работы принтера;
// Супер-класс, имеет экземпляр некого принтера, для отображения его характеристик на экран;
class PrinterSimulator
{

public :


	// Запрашивает экземпляр симулятора;
	static PrinterSimulator* getInstance();

	// Основной метод, запускающий симулятора;
	void run();


private:

	// Дефолтный принтер, необходимый для демонстрации;
	Printer* machine;

	// Рисует очередь, лоадбар, статистику;
	void startPrinting();

	// Рисует меню действий;
	void drawMenu();

	// Рисует рамки;
	void drawField();

	// Рисует поле в левом нижнем углу;
	void drawActiveEmployeeList();

	// Рисует надпись "Done", затем удаляет;
	void drawFeedback();

	// Подсказывает, в случае если очередь окажется пустой;
	void drawWarning();

	// Рисует очередь статистики;
	void drawStatistics();

	// Очищает область меню;
	void clearMenuField();

	// Очищает область списка сотрудников;
	void clearActiveEmployeeList();

	// Чистит очередь статистики;
	void clearStatistics();





#pragma region SINGLETON_STUFF


	// Экземпляр нашего симулятора;
	static PrinterSimulator* instance;

	// Конструктор по умолчанию;
	PrinterSimulator();

	// Деструктор;
	virtual ~PrinterSimulator();


#pragma endregion SINGLETON_STUFF

};


#endif
