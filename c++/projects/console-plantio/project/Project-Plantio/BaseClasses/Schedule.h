#ifndef SCHEDULE_H
#define SCHEDULE_H


#ifndef PLANT_H
#include "Plant.h"
#endif

#ifndef ENUM_ADAPTER_H
#include "../Wrappers/EnumAdapter.h"
#endif

#ifndef FILE_BUFFERED_READER_H
#include "../Misc/FileBufferedReader.h"
#endif

#ifndef JSON_PARSER_H
#include "..//JSON/Parsing/JsonParser.h"
#endif


#include "../Misc/PlantsQuantities.h"


#include <map>
#include <iostream>
#include <string>


#ifdef JSON_PARSER_H
using namespace JsonParsing;
#endif


using std::string;
using std::to_string;
using std::map;


// Класс "План", ранее "Программа";
class Schedule
{

public:


#pragma region ACCESS

	// Геттер указателя на растение;
	// returns — указатель на растение;
	Plant* getPlant();

	// Сеттер указателя на растение;
	// "ptrPlant" —  ;
	void setPlant(Plant* ptrPlant);

	// Геттер кол-ва выращиваемых за сезон растений;
	// returns — кол-во выращиваемых растений;
	size_t getPlantsQuantity();

	// сеттер кол-ва выращиваемых за сезон растений;
	// "nPlantsQuantity" — кол-во выращиваемых растений;
	void setPlantsQuantity(size_t nPlantsQuantity);

	// Геттер детальной информации по программе;
	// returns – характеристики программы;
	map<string, string> getDetails();

	// Геттер выручки;
	// returns – выручка всей программы;
	double getRevenue();



#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструкто по умолчанию;
	Schedule();

	// Конструктор с растением;
	// "ptrPlant" — растение;
	Schedule(Plant* ptrPlant);

	// Конструктор со всеми аргументами;
	// "ptrPlant" — растение;
	// "nPlantsQuantity" — срок на выращивание;
	Schedule(Plant* ptrPlant, size_t nPlantsQuantity);

	// Деструктор;
	virtual ~Schedule();

	// Конструктор копирования;
	// "anotherSchedule" — копируемый объект;
	Schedule(Schedule& anotherSchedule);

	// Перегрузка оператора "=";
	// "anotherSchedule" — копируемый объект;
	// returns — левый операнд;
	Schedule& operator=(Schedule& anotherSchedule);

	// Берёт кол-во растений из набора стандартных схем (программ);
	void pvtGetPlantsQuantity();


#pragma endregion CONSTRUCTION


protected:

	Plant* ptrPlant;

	size_t nPlantsQuantity;

	map<string, string> mDetails;

	double dOverallRevenue;

};


#endif

