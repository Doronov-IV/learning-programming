#include "Stock.h"



#pragma region ACCESS


void Stock::setSoil(Soil* ptrSoil)
{
	this->ptrSoil = ptrSoil;
}


Soil* Stock::getSoil()
{
	return this->ptrSoil;
}


string Stock::getName()
{
	return this->sName;
}


void Stock::setName(string sName)
{
	this->sName = sName;
}


#pragma endregion ACCESS


#pragma region CONSTRUCTION


Stock::Stock()
{
	ptrSoil = nullptr;
}


Stock::Stock(Soil* ptrSoil)
{
	this->ptrSoil = ptrSoil;
}


Stock::~Stock()
{
	if (nullptr != ptrSoil)
	{
		delete ptrSoil;
		ptrSoil = nullptr;
	}
}


Stock::Stock(Stock& anotherStock)
{
	this->ptrSoil = anotherStock.ptrSoil;
	this->sName = anotherStock.sName;
}


Stock& Stock::operator=(Stock& anotherStock)
{
	this->ptrSoil = anotherStock.ptrSoil;
	this->sName = anotherStock.sName;
	return *this;
}


#pragma endregion CONSTRUCTION