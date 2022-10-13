#include "Packaging.h"




#pragma region ACCESS


void Packaging::setPrice(double dPrice)
{
	this->dPrice = dPrice;
}


double Packaging::getPrice()
{
	return this->dPrice;
}


void Packaging::setPackagingType(PackagingType ePackagingType)
{
	this->eType = ePackagingType;
}


PackagingType Packaging::getPackagingType()
{
	return this->eType;
}


#pragma endregion ACCESS



#pragma region CONSTRUCTION


Packaging::Packaging()
{
	this->dPrice = 0.0;
	this->eType = Pot;
}


Packaging::Packaging(double dPrice, PackagingType ePackagingType)
{
	this->dPrice = dPrice;
	this->eType = ePackagingType;
}


Packaging::~Packaging()
{
	this->dPrice = 0.0;
}


Packaging::Packaging(Packaging& anotherPackaging)
{
	this->dPrice = anotherPackaging.dPrice;
	this->eType = anotherPackaging.eType;
}


Packaging& Packaging::operator=(Packaging& anotherPackaging)
{
	this->dPrice = anotherPackaging.dPrice;
	this->eType = anotherPackaging.eType;
	return *this;
}


#pragma endregion CONSTRUCTION
