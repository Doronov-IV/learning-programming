#ifndef PRINTER_H
#define PRINTER_H

#include "../PriorityQueue/PriorityQueueX.h"
#include "Employee.h"
#include <string>

// Класс "Принтер"
class Printer
{



public:



#pragma region LOGIC


	// Добавляет сотрудника в очередь печати со случайным кол-вом страниц;
	void addEmployee(std::string sRank);


#pragma endregion LOGIC




#pragma region CONSTRUCTION


	// Конструктор по умолчанию;
	Printer();


	// Виртуальный деструктор;
	virtual ~Printer();


	// Конструктор с очередью печати;
	// "queue" — Очередь печати;
	Printer(PriorityQueueX<Employee>* queue);


#pragma endregion CONSTRUCTION




#pragma region ACCESS

	// Геттер очереди печати;
	// returns — указатель на очередь печати;
	PriorityQueueX<Employee>* getPrintingQueuePointer();


	// Геттер очереди печати;
	// returns — указатель на очередь печати;
	PriorityQueueX<std::string>* getStatisticsPointer();


	// Геттер счётчика напечатанных страниц;
	// returns — Показатель напечатанных страниц;
	size_t getPagesPrinted();


	// Геттер счётчика принятых клиентов;
	// returns — Показатель обслуженных сотрудников;
	size_t getClientsServed();


	// Геттер скорости печати;
	// returns — Показатель скорости печати;
	size_t getPrintingSpeed();


	// Геттер полной статистики
	// returns — Указатель на массив из трёх элементов соответственно по порядку
	PriorityQueueX<std::string>* getFullStatistics();


	// Сеттер счётчика напечатанных страниц;
	// "nValue" — Показатель напечатанных страниц;
	void setPagesPrinted(int nValue);


	// Сеттер счётчика принятых клиентов;
	// "nValue" — Показатель обслуженных сотрудников;
	void setClientsServed(int nValue);


	// Сеттер скорости печати;
	// "nValue" — Показатель скорости печати;
	void setPrintingSpeed(int nValue);


#pragma endregion ACCESS


private:


	// Кол-во напечатанных страниц (size_t);
	size_t nPagesPrinted;


	// Кол-во авторизованных пользователей (size_t);
	size_t nClientsServed;


	// Скорость печати в процентах (20 — default value);
	size_t nPrintingSpeed;

	// Очередь печати;
	PriorityQueueX<Employee>* ptrPrintingQueue;

	// Статистика печати;
	PriorityQueueX<std::string>* ptrStatistics;


};


#endif

