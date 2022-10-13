#ifndef PACKAGING_H
#define PACKAGING_H


#ifndef PACKAGING_TYPE_H
#include "Enums/PackagingType.h"
#endif


// Класс "тара" или "упаковка";
class Packaging
{

public:


#pragma region ACCESS

	// Сеттер значения;
	// "dPrice" — стоимость;
	void setPrice(double dPrice);

	// Геттер значения;
	// returns — стоимость;
	double getPrice();

	// Сеттер упаковки;
	// "ePackagingType" — упаковка;
	void setPackagingType(PackagingType ePackagingType);

	// Геттер упаковки;
	// returns — упаковка;
	PackagingType getPackagingType();


#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Packaging();

	// Конструктор со значениями;
	// "dPrice" — стоимость;
	// "ePackagingType" — тип упаковки;
	Packaging(double dPrice, PackagingType ePackagingType);

	// Деструктор;
	virtual ~Packaging();

	// Конструктор копирования;
	// "anotherPackaging" — копируемый объект;
	Packaging(Packaging& anotherPackaging);

	// Перегрузка оператора "=";
	// "anotherPackaging" — копируемый объект;
	// returns — левый операнд;
	Packaging& operator=(Packaging& anotherPackaging);


#pragma endregion CONSTRUCTION


protected:

	// Показатель стоимости;
	double dPrice;

	// Тип упаковки;
	PackagingType eType;

};



#endif

