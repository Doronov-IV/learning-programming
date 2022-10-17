#pragma once
#include <string>

using namespace std;

// Пример класса для хранения в файле
class Person
{
public:

	// Значение в 1 байт
	char group;

	// Значение в 4 байта (или 8 в 64 битной системе)
	int age;

	// Булево значение размером в 1 байт
	bool isMale;

	// Значение в 4 байта с плавающей запятой
	float height;

	// Массив символов произвольной длины
	const char* name;


	Person()
	{
		this->name = "";
		this->age = 0;
		this->height = 0.0f;
		this->group = '\0';
		this->isMale = false;
	}


	Person(const char* name, int age, float height, char group, bool isMale)
	{
		this->name = name;
		this->age = age;
		this->height = height;
		this->group = group;
		this->isMale = isMale;
	}

};