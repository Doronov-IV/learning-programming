#include "Plant.h"



#pragma region GET


Planting Plant::getPlantingMaterial()
{
	return this->ePlantingMaterial;
}


double Plant::getPackagingPrice()
{
	return this->dPackagingPrice;
}


Species Plant::getSpecies()
{
	return this->eSpecies;
}


Country Plant::getMaterialCountry()
{
	return this->ePlantingMaterialCountry;
}


double Plant::getSoilNeedRatio()
{
	return this->dSoilNeedRatio;
}


size_t Plant::getFertilizerNeed()
{
	return this->nFertilizerNeed;
}


size_t Plant::getChemicalsNeed()
{
	return this->nChemicalsNeed;
}


double Plant::getPlantingPrice()
{
	return this->dPlantingPrice;
}


size_t Plant::getGrowingTime()
{
	return this->nGrowingTime;
}


double Plant::getRevenue()
{
	return this->dRevenuePerUnit;
}


size_t Plant::getSelfloadRequired()
{
	return this->nSelfloadRequired;
}


double Plant::getHiredWorkload()
{
	return this->dHiredWorkload;
}


size_t Plant::getVerminExposure()
{
	return this->nVerminExposure;
}


#pragma endregion GET



#pragma region SET


void Plant::setPlantingMaterial(Planting ePlantingMaterial)
{
	this->ePlantingMaterial = ePlantingMaterial;
}


void Plant::setSpecies(Species eSpecies)
{
	this->eSpecies = eSpecies;
}


void Plant::setMaterialCountry(Country ePlantingMaterialCountry)
{
	this->ePlantingMaterialCountry = ePlantingMaterialCountry;
}


void Plant::setSoilNeedRatio(double dSoilNeedRatio)
{
	this->dSoilNeedRatio = dSoilNeedRatio;
}


void Plant::setFertilizerNeed(size_t nFertilizerNeed)
{
	this->nFertilizerNeed = nFertilizerNeed;
}


void Plant::setChemicalsNeed(size_t nChemicalsNeed)
{
	this->nChemicalsNeed = nChemicalsNeed;
}


void Plant::setPlantingPrice(double dPlantingPrice)
{
	this->dPlantingPrice = dPlantingPrice;
}


void Plant::setPackagingPrice(double dPackagingPrice)
{
	this->dPackagingPrice = dPackagingPrice;
}


void Plant::setGrowingTime(size_t nGrowingTime)
{
	this->nGrowingTime = nGrowingTime;
}


void Plant::setRevenue(double dRevenuePerUnit)
{
	this->dRevenuePerUnit = dRevenuePerUnit;
}


void Plant::setSelfloadRequired(size_t nSelfloadRequired)
{
	this->nSelfloadRequired = nSelfloadRequired;
}


void Plant::setHiredWorkload(double dHiredWorkload)
{
	this->dHiredWorkload = dHiredWorkload;
}


void Plant::setVerminExposure(size_t nVerminExposure)
{
	this->nVerminExposure = nVerminExposure;
}


#pragma endregion SET



#pragma region CONSTRUCTION


Plant::Plant()
{
	this->dHiredWorkload = 0;
	this->dPackagingPrice = 0;
	this->dPlantingPrice = 0;
	this->dRevenuePerUnit = 0;
	this->dSoilNeedRatio = 0;
	this->nChemicalsNeed = 0;
	this->nFertilizerNeed = 0;
	this->nGrowingTime = 0;
	this->nSelfloadRequired = 0;
	this->nVerminExposure = 0;
}


Plant::~Plant()
{

}


Plant::Plant(Plant& anotherPlant)
{
	this->ePlantingMaterial  = anotherPlant.ePlantingMaterial;
	this->eSpecies  = anotherPlant.eSpecies;
	this->ePlantingMaterialCountry = anotherPlant.ePlantingMaterialCountry;
	this->dSoilNeedRatio = anotherPlant.dSoilNeedRatio;
	this->nFertilizerNeed = anotherPlant.nFertilizerNeed;
	this->nChemicalsNeed = anotherPlant.nChemicalsNeed;
	this->dPlantingPrice = anotherPlant.dPlantingPrice;
	this->nGrowingTime = anotherPlant.nGrowingTime;
	this->dRevenuePerUnit = anotherPlant.dRevenuePerUnit;
	this->nSelfloadRequired = anotherPlant.nSelfloadRequired;
	this->dHiredWorkload = anotherPlant.dHiredWorkload;
	this->nVerminExposure = anotherPlant.nVerminExposure;
}


Plant& Plant::operator=(Plant& anotherPlant)
{
	this->ePlantingMaterial = anotherPlant.ePlantingMaterial;
	this->eSpecies = anotherPlant.eSpecies;
	this->ePlantingMaterialCountry = anotherPlant.ePlantingMaterialCountry;
	this->dSoilNeedRatio = anotherPlant.dSoilNeedRatio;
	this->nFertilizerNeed = anotherPlant.nFertilizerNeed;
	this->nChemicalsNeed = anotherPlant.nChemicalsNeed;
	this->dPlantingPrice = anotherPlant.dPlantingPrice;
	this->nGrowingTime = anotherPlant.nGrowingTime;
	this->dRevenuePerUnit = anotherPlant.dRevenuePerUnit;
	this->nSelfloadRequired = anotherPlant.nSelfloadRequired;
	this->dHiredWorkload = anotherPlant.dHiredWorkload;
	this->nVerminExposure = anotherPlant.nVerminExposure;
	return *this;
}


#pragma endregion CONSTRUCTION



