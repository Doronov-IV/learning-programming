#include "Money.h"



#pragma region OPERATIONS

/// <summary>
/// Сложение
/// </summary>
void Money::add(Money& ourCurrency, Money anotherCurrency)
{
	if (ourCurrency.course != anotherCurrency.course) anotherCurrency.convert(ourCurrency);
	double temp = ourCurrency.getAbs() + anotherCurrency.getAbs();
	ourCurrency.decimal = modf(temp, &ourCurrency.integer);	//	отделяет целую часть от дробной <math.h>	https://www.cplusplus.com/reference/cmath/modf/
}

/// <summary>
/// Вычитание
/// </summary>
void Money::diff(Money& ourCurrency, Money anotherCurrency)
{
	if (ourCurrency.course != anotherCurrency.course) anotherCurrency.convert(ourCurrency);
	if (ourCurrency.getAbs() > anotherCurrency.getAbs())
	{
		if (ourCurrency.course != anotherCurrency.course) anotherCurrency.convert(ourCurrency);
		double temp = ourCurrency.getAbs() - anotherCurrency.getAbs();
		ourCurrency.decimal = modf(temp, &ourCurrency.integer);	//	отделяет целую часть от дробной <math.h>	https://www.cplusplus.com/reference/cmath/modf/
	}
	else
	{
		ourCurrency.integer = 0;
		ourCurrency.decimal = 0;
	}
}

/// <summary>
/// Умножение
/// </summary>
void Money::mul(Money& ourCurrency, Money anotherCurrency)
{
	if (ourCurrency.course != anotherCurrency.course) anotherCurrency.convert(ourCurrency);
	double temp = ourCurrency.getAbs() * anotherCurrency.getAbs();
	ourCurrency.decimal = modf(temp, &ourCurrency.integer);	//	отделяет целую часть от дробной <math.h>	https://www.cplusplus.com/reference/cmath/modf/
}

/// <summary>
/// Деление
/// </summary>
void Money::div(Money& ourCurrency, Money anotherCurrency)
{
	if (ourCurrency.course != anotherCurrency.course) anotherCurrency.convert(ourCurrency);
	if (anotherCurrency.integer != 0 && anotherCurrency.decimal != 0)
	{
		if (ourCurrency.course != anotherCurrency.course) anotherCurrency.convert(ourCurrency);
		double temp = ourCurrency.getAbs() / anotherCurrency.getAbs();
		ourCurrency.decimal = modf(temp, &ourCurrency.integer);	//	отделяет целую часть от дробной <math.h>	https://www.cplusplus.com/reference/cmath/modf/
	}
	else return;
}

/// <summary>
/// Сравнение [!] Важно, чтобы вторая валюта не передавалась по ссылке, иначе её придётся конвертировать обратно 
/// </summary>
int Money::compare(Money& ourCurrency, Money anotherCurrency)
{
	if (ourCurrency.course != anotherCurrency.course) anotherCurrency.convert(ourCurrency);
	
	if (ourCurrency.getAbs() > anotherCurrency.getAbs()) return 1;
	else if (ourCurrency.getAbs() < anotherCurrency.getAbs()) return 2;
	else if (ourCurrency.getAbs() == anotherCurrency.getAbs()) return 0;
	else return -1;

}

#pragma endregion OPERATIONS


/// <summary>
/// Полная сумма: целая часть + дробная часть, приведённая к типу double
/// </summary>
double Money::getAbs()
{
	return double(getInteger()) + getDecimal();
}

/// <summary>
/// Приводит текущую валюту к У.Е.
/// </summary>
void Money::convert(Money anotherCurrency)
{
	double abs = getAbs();							// считаем всю сумму без остатка 
	abs = abs / course * anotherCurrency.course;	// конвертируем
	course = anotherCurrency.course;				//
	decimal = modf(abs, &integer);					//	отделяет целую часть от дробной <math.h>	https://www.cplusplus.com/reference/cmath/modf/
}

/// <summary>
/// Печатает в консоль информацию об объекте
/// </summary>
void Money::printInfo()
{
	std::cout << "\t" << course << "\t\t" << integer << "\t\t" << decimal << "\n";
}


#pragma region CONSTRUCT

Money::Money(double course)
{
	this->course = course;
	this->integer = 0;
	this->decimal = 0;
}

Money::Money(double integer, double decimal)
{
	this->course = 1;
	this->integer = integer;
	this->decimal = decimal;
}

Money::Money(double course, double integer, double decimal) : Money(integer, decimal)
{
	this->course = course;
}

#pragma endregion CONSTRUCT

#pragma region SETGET

double Money::getCourse() 
{
	return course;
}

double Money::getInteger()
{
	return integer;
}

double Money::getDecimal()
{
	return decimal;
}

void Money::setCourse(double course)
{
	this->course = course;
}

void Money::setInteger(double integer)
{
	this->integer = integer;
}

void Money::setDecimal(double decimal)
{
	this->decimal = decimal;
}

#pragma endregion SETGET

