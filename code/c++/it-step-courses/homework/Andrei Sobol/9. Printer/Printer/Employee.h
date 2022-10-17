#ifndef EMPLOYEE_H
#define EMPLOYEE_H

#include <iostream>
#include <ctime>


// Класс "Сотрудник";
class Employee
{


public:


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Employee();
	
	// Конструктор с должностью;
	// "sRank" — должность сотрудника;
	Employee(std::string sRank);

	// Деструктор;
	virtual ~Employee();

#pragma endregion CONSTRUCTION




#pragma region ACCESS

	// Геттер должности;
	// returns — Должность сотрудника;
	std::string getRank();

	// Геттер приоритета
	// returns — приоритет в очереди на печать;
	size_t getPriority();

	// Сеттер должности;
	// "sRank" — новая должность сотрудника;
	void setRank(std::string sRank);

	// Геттер кол-ва страниц для печати;
	// returns — кол-во страниц на печать;
	size_t getPagesToPrint();

	// Сеттер кол-ва страниц для печати;
	// "nPagesToPrint" — кол-во страниц на печать;
	void setPagesToPrint(size_t nPagesToPrint);


	// Геттер кол-ва напечатанных страниц;
	// returns — кол-во напечатанных страниц;
	size_t getPagesPrinted();

	// Сеттер кол-ва напечатанных страниц;
	// "nPagesToPrint" — кол-во напечатанных страниц;
	void setPagesPrinted(size_t nHasPagesPrinted);


#pragma region ACCESS

	
private:


	// Приоритет в очереди на печать;
	size_t nPriority;

	// Должность сотрудника;
	std::string sRank;

	// Кол-во страниц для печати;
	size_t nPagesToPrint;

	// Кол-во напечатанных страниц. Необходимо для очереди на принтер, 
	// чтобы узнать сколько страниц было напечатано, если печать была прервана;
	size_t nHasPagesPrinted;

};



#endif