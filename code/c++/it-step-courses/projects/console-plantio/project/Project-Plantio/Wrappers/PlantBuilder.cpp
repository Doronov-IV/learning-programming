#include "PlantBuilder.h"




void PlantBuilder::build(Species eSpecies)
{
	JsonParser* parser = new JsonParser();
	EnumAdapter* adapter = EnumAdapter::getInstance();
	string sPath = "JSON/Data/Plants/";
	sPath.append(adapter->parseSpeciesEnum(eSpecies));
	sPath.append(".json");
	FileBufferedReader* reader = new FileBufferedReader();
	reader->read(sPath);
	string sSource = reader->toString();

	parser->parse(sSource);

	for (auto elem : parser->ptrRoot->strings)
	{
		if ("chemicals" == elem.first)
		{
			try
			{
				ptrPlant->setChemicalsNeed(stoi(elem.second));
			}
			catch(...)
			{

			}
		}
		else if ("country" == elem.first)
		{
			try
			{
				ptrPlant->setMaterialCountry(adapter->parseCountryString(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("fertilizer" == elem.first)
		{
			try
			{
				ptrPlant->setFertilizerNeed(stoi(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("growing time" == elem.first)
		{
			try
			{
				ptrPlant->setGrowingTime(stoi(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("hired load" == elem.first)
		{
			try
			{
				ptrPlant->setHiredWorkload(stoi(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("packaging" == elem.first)
		{
			try
			{
				ptrPlant->setPackagingPrice(stod(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("planting" == elem.first)
		{
			try
			{
				ptrPlant->setPlantingMaterial(adapter->parsePlantingString(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("planting price" == elem.first)
		{
			try
			{
				ptrPlant->setPlantingPrice(stoi(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("revenue" == elem.first)
		{
			try
			{
				ptrPlant->setRevenue(stoi(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("selfload" == elem.first)
		{
			try
			{
				ptrPlant->setSelfloadRequired(stoi(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("soil need" == elem.first)
		{
			try
			{
				ptrPlant->setSoilNeedRatio(stod(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("species" == elem.first)
		{
			try
			{
				ptrPlant->setSpecies(adapter->parseSpeciesString(elem.second));
			}
			catch (...)
			{

			}
		}
		else if ("vermin exposure" == elem.first)
		{
			try
			{
				ptrPlant->setVerminExposure(stoi(elem.second));
			}
			catch (...)
			{

			}
		}
	}


	delete parser;
}


#pragma region ACCESS


Plant* PlantBuilder::getPlantPointer()
{
	return this->ptrPlant;
}


void PlantBuilder::setPlantPointer(Plant* ptrPlant)
{
	if (nullptr != ptrPlant)
	{
		delete ptrPlant;
	}

	this->ptrPlant = ptrPlant;
}


#pragma endregion ACCESS



#pragma region CONSTRUCTION


PlantBuilder::PlantBuilder()
{
	ptrPlant = new Plant();
}


PlantBuilder::~PlantBuilder()
{
	if (nullptr != ptrPlant)
	{
		delete ptrPlant;
		ptrPlant = nullptr;
	}
}


PlantBuilder::PlantBuilder(Plant* ptrPlant)
{
	if (nullptr != ptrPlant)
	{
		delete ptrPlant;
	}

	this->ptrPlant = ptrPlant;
}


PlantBuilder::PlantBuilder(Species eSpecies)
{
	ptrPlant = new Plant();
	ptrPlant->setSpecies(eSpecies);
}


PlantBuilder::PlantBuilder(PlantBuilder& anotherBuilder)
{
	this->ptrPlant = anotherBuilder.ptrPlant;
}


PlantBuilder& PlantBuilder::operator=(PlantBuilder& anotherBuilder)
{
	this->ptrPlant = anotherBuilder.ptrPlant;
	return *this;
}


#pragma endregion CONSTRUCTION
