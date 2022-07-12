#ifndef MONEY_H
#define MONEY_H
#endif

#include <iostream>
#include <math.h>


class Money
{
public:

#pragma region OPERATIONS

	// Сложение
	static void add(Money& ourCurrency, Money anotherCurrency);

	// Вычитание
	static void diff(Money& ourCurrency, Money anotherCurrency);

	// Умножение
	static void mul(Money& ourCurrency, Money anotherCurrency);

	// Деление
	static void div(Money& ourCurrency, Money anotherCurrency);

	// Сравнение
	static int compare(Money& ourCurrency, Money anotherCurrency);


#pragma endregion OPERATIONS

	// Складвает целую и дробную части, возвращая double
	double getAbs();

	// Конвертирует
	void convert(Money anotherCurrency);

	// Выводит в консоль информацию о текущем объекте
	void printInfo();


#pragma region SETGET

	double getCourse();

	double getInteger();

	double getDecimal();

	void setCourse(double course);

	void setInteger(double integer);

	void setDecimal(double decimal);

#pragma endregion SETGET

#pragma region CONSTRUCT

	Money(double course);

	Money(double integer, double decimal);

	Money(double course, double integer, double decimal);

#pragma endregion CONSTRUCT

private:

	double course; // курс
	
	double integer; // целая часть

	double decimal; // дробная часть

};

