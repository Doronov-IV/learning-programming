#ifndef WORKER_H
#define WORKER_H


#include "Person.h"


// Класс "Рабочий";
class Worker : public Person
{


public:


#pragma region ACCESS

	// Геттер расписания;
	// returns — расписание;
	std::string getSchedule();

	// Сеттер расписания;
	// "sSchedule" — расписание;
	void setScedule(std::string sSchedule);


#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Worker();

	// Конструктор с расписанием;
	// "sSchedule" — расписание;
	Worker(std::string sSchedule);

	// Деструктор;
	virtual ~Worker() = 0;


#pragma endregion CONSTRUCTION


protected:

	// Расписание;
	std::string sSchedule;


};


#endif