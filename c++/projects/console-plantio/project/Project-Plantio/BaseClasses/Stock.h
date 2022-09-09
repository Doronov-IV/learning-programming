#ifndef STOCK_H
#define STOCK_H


#include "../Wrappers/PlantBuilder.h"

#ifndef SOIL_H
#include "Soil.h"
#endif


// Класс "Склад";
class Stock
{

public:

#pragma region ACCESS

	// Сеттер указателя на показатель почвенной смеси;
	// "ptrSoil" – указатель на показатель почвенной смеси;
	void setSoil(Soil* ptrSoil);

	// Геттер указателя на показатель почвенной смеси;
	// returns – указатель на показатель почвенной смеси;
	Soil* getSoil();

	// Геттер имени;
	// returns – имя;
	string getName();

	// Сеттер имени;
	// "sName" – имя;
	void setName(string sName);


#pragma endregion ACCESS


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Stock();

	// Конструктор со значением;
	// "ptrSoil" — указатель на показатель почвенной смеси;
	Stock(Soil* ptrSoil);

	// Деструктор;
	virtual ~Stock();

	// Конструктор копирования;
	// "anotherStock" — копируемый объект;
	Stock(Stock& anotherStock);

	// Перегрузка оператора "=";
	// "anotherStock" — копируемый объект;
	// returns — левый операнд;
	Stock& operator=(Stock& anotherStock);


#pragma endregion CONSTRUCTION


private:

	// Указатель на показатель почвенной смеси;
	Soil* ptrSoil;

	// Имя или название склада;
	string sName;

};


#endif