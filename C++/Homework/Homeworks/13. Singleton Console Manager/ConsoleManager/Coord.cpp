#include "Coord.h"




#pragma region CONSTRUCTION



Coord::Coord()
{
	this->x = 0;
	this->y = 0;
}


Coord::Coord(int x, int y)
{
	this->x = x;
	this->y = y;
}


Coord::Coord(Coord& anotherCoord)
{
	this->x = anotherCoord.x;
	this->y = anotherCoord.y;
}

Coord& Coord::operator=(Coord& anotherCoord)
{
	this->x = anotherCoord.x;
	this->y = anotherCoord.y;
	return *this;
}


Coord::~Coord()
{
	this->x = 0;
	this->y = 0;
}


#pragma endregion CONSTRUCTION




#pragma region ACCESS


void Coord::set(int x, int y)
{
	this->x = x;
	this->y = y;
}


int Coord::getX()
{
	return this->x;
}

int Coord::getY()
{
	return this->y;
}


#pragma endregion ACCESS