#include "Schedule.h"



#pragma region ACCESS


Plant* Schedule::getPlant()
{
	return this->ptrPlant;
}


void Schedule::setPlant(Plant* ptrPlant)
{
	this->ptrPlant = ptrPlant;
}


size_t Schedule::getPlantsQuantity()
{
	return this->nPlantsQuantity;
}


void Schedule::setPlantsQuantity(size_t nPlantsQuantity)
{
	this->nPlantsQuantity = nPlantsQuantity;
}


map<string, string> Schedule::getDetails()
{
	if (0 == this->mDetails.size())
	{
		mDetails.emplace("F",to_string(ptrPlant->getFertilizerNeed()));
		mDetails.emplace("C", to_string(ptrPlant->getChemicalsNeed()));
		mDetails.emplace("S", to_string(ptrPlant->getSoilNeedRatio()));
		mDetails.emplace("W", to_string(ptrPlant->getSelfloadRequired()));
		mDetails.emplace("H", to_string(ptrPlant->getHiredWorkload()));
		mDetails.emplace("Q", to_string(nPlantsQuantity));
		mDetails.emplace("L", to_string(ptrPlant->getVerminExposure()));

	}

	return this->mDetails;
}

double Schedule::getRevenue()
{
	if (0 == nPlantsQuantity)
	{
		pvtGetPlantsQuantity();
		return -1;
	}
	else
	{
		if (0 == dOverallRevenue && 0 != nPlantsQuantity)
		{
			dOverallRevenue = nPlantsQuantity * ptrPlant->getRevenue();

			return dOverallRevenue;
		}
	}
	return dOverallRevenue;
}

/*
AbstractType* Schedule::getData(string sCharacteristicsName)
{
	using_adapter;
	using_reader;

	reader->read("Misc/Characteristics.list");

	StringType* stStringTypePointer;
	DoubleType* dtDoubleTypePointer;
	IntegerType* itIntegerTypePointer;

	for (size_t i = 0ULL, nSize = reader->getBuffer().size(); i < nSize; i++)
	{
		switch (i)
		{
		case 1:
			itIntegerTypePointer = new IntegerType();
			itIntegerTypePointer->value = ptrPlant->getChemicalsNeed();
			return (AbstractType*)itIntegerTypePointer;
			break;
		case 2:
			stStringTypePointer = new StringType();
			stStringTypePointer->value = adapter->parseCountryEnum(ptrPlant->getMaterialCountry());
			return (AbstractType*)stStringTypePointer;
			break;
		case 3:
			itIntegerTypePointer = new IntegerType();
			itIntegerTypePointer->value = ptrPlant->getFertilizerNeed();
			return (AbstractType*)itIntegerTypePointer;
			break;
		case 4:
			itIntegerTypePointer = new IntegerType();
			itIntegerTypePointer->value = ptrPlant->getGrowingTime();
			return (AbstractType*)itIntegerTypePointer;
			break;
		case 5: // hired load
			itIntegerTypePointer = new IntegerType();
			itIntegerTypePointer->value = ptrPlant->getHiredWorkload();
			return (AbstractType*)itIntegerTypePointer;
			break;
		case 6: // packaging
			dtDoubleTypePointer = new DoubleType();
			dtDoubleTypePointer->value = ptrPlant->getPackagingPrice();
			return (AbstractType*)dtDoubleTypePointer;
			break;
		case 7: // planting
			stStringTypePointer = new StringType();
			stStringTypePointer->value = adapter->parsePlantingEnum(ptrPlant->getPlantingMaterial());
			return (AbstractType*)stStringTypePointer;
			break;
		case 8: // planting price
			dtDoubleTypePointer = new DoubleType();
			dtDoubleTypePointer->value = ptrPlant->getPlantingPrice();
			return (AbstractType*)dtDoubleTypePointer;
			break;
		case 9: // revenue
			itIntegerTypePointer = new IntegerType();
			itIntegerTypePointer->value = ptrPlant->getRevenue();
			return (AbstractType*)itIntegerTypePointer;
			break;
		case 10: // selfload
			itIntegerTypePointer = new IntegerType();
			itIntegerTypePointer->value = ptrPlant->getSelfloadRequired();
			return (AbstractType*)itIntegerTypePointer;
			break;
		case 11: // soil need
			dtDoubleTypePointer = new DoubleType();
			dtDoubleTypePointer->value = ptrPlant->getSoilNeedRatio();
			return (AbstractType*)dtDoubleTypePointer;
			break;
		case 12: // species
			stStringTypePointer = new StringType();
			stStringTypePointer->value = adapter->parseSpeciesEnum(ptrPlant->getSpecies());
			return (AbstractType*)stStringTypePointer;
			break;
		case 13: // vermin exposure
			itIntegerTypePointer = new IntegerType();
			itIntegerTypePointer->value = ptrPlant->getVerminExposure();
			return (AbstractType*)itIntegerTypePointer;
			break;
		default:
			return nullptr;
			break;
		}
	}


	delete reader;
	reader = nullptr;
	return nullptr;
}
*/


#pragma endregion ACCESS



#pragma region CONSTRUCTION


Schedule::Schedule()
{
	this->ptrPlant = nullptr;
	this->nPlantsQuantity = 0ULL;
	this->dOverallRevenue = 0;
}


Schedule::Schedule(Plant* ptrPlant)
{
	this->ptrPlant = ptrPlant;
	this->nPlantsQuantity = 0ULL;
	this->dOverallRevenue = 0;
}


Schedule::Schedule(Plant* ptrPlant, size_t nPlantsQuantity)
{
	this->ptrPlant = ptrPlant;
	this->nPlantsQuantity = nPlantsQuantity;
	this->dOverallRevenue = 0;
}


Schedule::~Schedule()
{
	if (nullptr != ptrPlant)
	{
		delete ptrPlant;
		ptrPlant = nullptr;
	}
}


Schedule::Schedule(Schedule& anotherSchedule)
{
	this->ptrPlant = anotherSchedule.ptrPlant;
	this->nPlantsQuantity = anotherSchedule.nPlantsQuantity;
	this->dOverallRevenue = anotherSchedule.dOverallRevenue;
}


Schedule& Schedule::operator=(Schedule& anotherSchedule)
{
	this->ptrPlant = anotherSchedule.ptrPlant;
	this->nPlantsQuantity = anotherSchedule.nPlantsQuantity;
	this->dOverallRevenue = anotherSchedule.dOverallRevenue;
	return *this;
}


void Schedule::pvtGetPlantsQuantity()
{

}


#pragma endregion CONSTRUCTION