#include "Soil.h"




#pragma region ACCESS


void Soil::setValue(double dValue)
{
	this->dValue = dValue;
}


double Soil::getValue()
{
	return this->dValue;
}


#pragma endregion ACCESS



#pragma region CONSTRUCTION


Soil::Soil()
{
	dValue = 0.0;
}


Soil::Soil(double dValue)
{
	this->dValue = dValue;
}


Soil::~Soil()
{
	dValue = 0.0;
}


Soil::Soil(Soil& anotherSoil)
{
	this->dValue = anotherSoil.dValue;
}


Soil& Soil::operator=(Soil& anotherSoil)
{
	this->dValue = anotherSoil.dValue;
	return *this;
}


#pragma endregion CONSTRUCTION
