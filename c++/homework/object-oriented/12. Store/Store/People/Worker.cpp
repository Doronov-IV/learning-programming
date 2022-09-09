#include "Worker.h"




#pragma region ACCESS


std::string Worker::getSchedule()
{
	return this->sSchedule;
}


void Worker::setScedule(std::string sSchedule)
{
	this->sSchedule = sSchedule;
}


#pragma endregion ACCESS



#pragma region CONSTRUCTION


Worker::Worker()
{
	this->sSchedule = "Null";
}


Worker::Worker(std::string sSchedule)
{
	this->sSchedule = sSchedule;
}


Worker::~Worker()
{
	this->sSchedule = "Null";
}


#pragma endregion CONSTRUCTION