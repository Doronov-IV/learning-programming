#ifndef SOIL_H
#define SOIL_H


// Класс "Почва". Может быть расширен и дополнен;
class Soil
{


public:


#pragma region ACCESS

	// Сеттер значения;
	// "dValue" — показатель в литрах;
	void setValue(double dValue);

	// Геттер значения;
	// returns — показатель в литрах;
	double getValue();


#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Soil();

	// Конструктор со значением;
	// "dValue" — показатель в литрах;
	Soil(double dValue);

	// Деструктор;
	virtual ~Soil();

	// Конструктор копирования;
	// "anotherSoil" — копируемый объект;
	Soil(Soil& anotherSoil);

	// Перегрузка оператора "=";
	// "anotherSoil" — копируемый объект;
	// returns — левый операнд;
	Soil& operator=(Soil& anotherSoil);


#pragma endregion CONSTRUCTION


protected:

	// Показатель объёма в литрах;
	double dValue;


};



#endif

