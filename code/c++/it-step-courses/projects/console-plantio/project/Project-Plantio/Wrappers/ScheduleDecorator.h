#ifndef SCHEDULE_DECORATOR_H
#define SCHEDULE_DECORATOR_H


#include "../BaseClasses/Schedule.h"


// Подключает тулбокс, если он ещё не подключён;
#ifndef TOOLBOX_H
#include "../Misc/Toolbox.h"
#endif


#ifndef CURRYNCY_H
#include "../BaseClasses/Currency.h"
#endif


#include <iomanip>


#ifndef SCREEN_HEIGHT
#define SCREEN_HEIGHT 36
#endif

#ifndef SCREEN_WIDTH
#define SCREEN_WIDTH 135
#endif


// Класс преднозначен для вывода данных Schedule в консоль;
class ScheduleDecorator
{


public:

	//;
	bool isSelected;

	void drawSchedule(size_t start_x, size_t start_y, vector<string*> sColorScheme);


#pragma region ACCESS

	// Сеттер указателя на программу;
	// "ptrSchedule" — указатель на программу;
	void setSchedule(Schedule* ptrSchedule);

	// Геттер указателя на программу;
	// returns — указатель на программу;
	Schedule* getSchedule();


#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	ScheduleDecorator();

	// Конструктор с программой;
	// "ptrSchedule" — программа, схема, whatever;
	ScheduleDecorator(Schedule* ptrSchedule);

	// Деструктор;
	virtual ~ScheduleDecorator();

	// Конструктор копирования;
	// "anotherDecorator" — копируемый объект;
	ScheduleDecorator(ScheduleDecorator& anotherDecorator);

	// Перегрузка оператора "=";
	// "anotherDecorator" — копируемый объект;
	// returns — левый операнд;
	ScheduleDecorator& operator=(ScheduleDecorator& anotherDecorator);


#pragma endregion CONSTRUCTION


protected:

	// Указатель на программу; 
	Schedule* ptrSchedule;


};


#endif

