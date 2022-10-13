#include "Plantio.h"



void Plantio::init()
{
    PlantBuilder* builder = new PlantBuilder();
    EnumAdapter* adapter = EnumAdapter::getInstance();
    Plant* currentPlant;

    for (auto spc : SpeciesEnum::All)
    {
        builder->build(spc);
        currentPlant = new Plant(*builder->getPlantPointer());
        vPlantsList.push_back(currentPlant);
    }

    createDefaultSchedules();
}


void Plantio::createDefaultSchedules()
{
    using_reader;
    using_adapter;

    reader->read("JSON/Data/Numbers/StandartProgramsPlantsQuantity.json");
    JsonParser* parser = new JsonParser();
    parser->parse(reader->toString());

    Schedule* currentSchedule;
    for (Plant* pl : vPlantsList)
    {
        size_t nSize = 0ULL;
        for (auto el : parser->ptrRoot->strings)
            if (adapter->parseSpeciesEnum(pl->getSpecies()) == el.first)
            {
                try
                {
                    nSize = stoi(el.second);
                }
                catch (...)
                {

                }
            }

        currentSchedule = new Schedule(pl, nSize);
        vDefaultSchedules.push_back(currentSchedule);
    }

    delete reader;
    delete parser;
}


vector<Plant*> Plantio::getPlantList()
{
    return this->vPlantsList;
}


vector<Schedule*> Plantio::getDefaultSchedulesList()
{
    return this->vDefaultSchedules;
}


bool Plantio::createNewStock(string sNtockName, Soil* ptrSoil)
{
    bool res = false;

    if ("" != sNtockName && 0 != ptrSoil->getValue())
    {
        res = true;

        Stock* newStock = new Stock(ptrSoil);
        newStock->setName(sNtockName);

        vStocks.push_back(newStock);
    }

    if (res) runStocksSync();

    return res;

}


bool Plantio::loadStock(string sStockName)
{
    using_parser;
    using_reader;

    bool res = false;
    bool bIsItAlreadyLoaded = false;

    for (Stock* unit : vStocks)
    {
        if (sStockName == unit->getName())
        {
            bIsItAlreadyLoaded = true;
            setActiveStock(unit);
        }
    }

    if (!bIsItAlreadyLoaded)
    {
        if (reader->read("../../JSON/Data/Stocks/" + sStockName))
        {
            parser->parse(reader->toString());

            Soil* ptrSoil = new Soil();
            Stock* newStock = new Stock();

            for (auto unit : parser->ptrRoot->numbers)
            {
                if ("Soil" == unit.first) ptrSoil->setValue(unit.second);
                if ("Name" == unit.first) newStock->setName(unit.first);
            }

            vStocks.push_back(newStock);

            setActiveStock(newStock);

            setCurrentStockSchedules();

            res = true;
        }


    }

    delete parser;
    delete reader;

    return res;
}


vector<Schedule*> Plantio::getCurrentStockSchedules()
{
    return this->vCurrentStockSchedules;
}


void Plantio::runStocksSync()
{

}


void Plantio::setActiveStock(Stock* ptrStock)
{
    bool bPresentFlag = false;
    for (Stock* unit : vStocks)
    {
        if (ptrStock->getName() == unit->getName())
        {
            this->ptrCurrentStock = ptrStock;
            bPresentFlag = true;
        }
    }
    if (!bPresentFlag)
    {
        vStocks.push_back(ptrStock);
    }
    this->ptrCurrentStock = ptrStock;
    setCurrentStockSchedules();
}


void Plantio::setCurrentStockSchedules()
{
    using_reader;

    if (ptrCurrentStock->getName() != "Default")
    {
        cleansePreviousStockSchedules();
        Schedule* currentSchedule;
        for (Plant* pl : vPlantsList)
        {
            size_t nSize = 0ULL;
            nSize = getCurrentStock()->getSoil()->getValue() * pl->getSoilNeedRatio();
            currentSchedule = new Schedule(pl, nSize);
            vCurrentStockSchedules.push_back(currentSchedule);
        }
    }

    delete reader;
}


vector<Stock*> Plantio::getStocksList()
{
    return this->vStocks;
}


void Plantio::cleansePreviousStockSchedules()
{
    vCurrentStockSchedules.clear();
}


Stock* Plantio::getCurrentStock()
{
    return this->ptrCurrentStock;
}



#pragma region CONSTRUCT


static Plantio* instance;

Plantio* Plantio::instance = nullptr;


Plantio::Plantio()
{
    init();
}


Plantio::~Plantio()
{

}


void Plantio::release()
{
    // write your code here .....
}


#pragma endregion CONSTRUCT




Plantio* Plantio::getInstance()
{
    if (nullptr == instance)
    {
        instance = new Plantio();
    }
    return instance;
}