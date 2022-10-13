#include "ScheduleSorter.h"




void ScheduleSorter::sort(std::string sOptionName, vector<ScheduleDecorator*>& vSchedulesArray)
{
    if ("Species" == sOptionName) sortBySpeciesName(vSchedulesArray);
    else if ("Revenue" == sOptionName) sortByRevenue(vSchedulesArray);
    else if ("F" == sOptionName) sortByFertilizer(vSchedulesArray);
    else if ("C" == sOptionName) sortByChemicals(vSchedulesArray);
    else if ("P" == sOptionName) sortByPlantingPrice(vSchedulesArray);
    else if ("S" == sOptionName) sortBySoilNeed(vSchedulesArray);
    else if ("H" == sOptionName) sortByHardwork(vSchedulesArray);
    else if ("Q" == sOptionName) sortByQuantity(vSchedulesArray);
    else if ("L" == sOptionName) sortByVerminExposure(vSchedulesArray);
    else if ("Days" == sOptionName) sortByTime(vSchedulesArray);
}

void ScheduleSorter::sortBySpeciesName(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if ((int)adapter->parseSpeciesEnum(vSchedulesArray[j + 1]->getSchedule()->getPlant()->getSpecies())[0] <
                (int)adapter->parseSpeciesEnum(vSchedulesArray[j]->getSchedule()->getPlant()->getSpecies())[0])
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByRevenue(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getRevenue() > vSchedulesArray[j]->getSchedule()->getRevenue())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByFertilizer(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlant()->getFertilizerNeed() >
                vSchedulesArray[j]->getSchedule()->getPlant()->getFertilizerNeed())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByChemicals(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlant()->getChemicalsNeed() >
                vSchedulesArray[j]->getSchedule()->getPlant()->getChemicalsNeed())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByPlantingPrice(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlant()->getPlantingPrice() >
                vSchedulesArray[j]->getSchedule()->getPlant()->getPlantingPrice())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortBySoilNeed(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlant()->getSoilNeedRatio() >
                vSchedulesArray[j]->getSchedule()->getPlant()->getSoilNeedRatio())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByHardwork(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlant()->getSelfloadRequired() >
                vSchedulesArray[j]->getSchedule()->getPlant()->getSelfloadRequired())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByQuantity(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize - 1; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlantsQuantity() <
                vSchedulesArray[j]->getSchedule()->getPlantsQuantity())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByVerminExposure(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlant()->getVerminExposure() <
                vSchedulesArray[j]->getSchedule()->getPlant()->getVerminExposure())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}

void ScheduleSorter::sortByTime(vector<ScheduleDecorator*>& vSchedulesArray)
{
    using_adapter;
    ScheduleDecorator* ptrTempSchedule;

    for (size_t i = 0ULL, nSize = vSchedulesArray.size(); i < nSize; i++)
    {
        for (size_t j = 0ULL; j < nSize - 1; j++)
        {
            if (vSchedulesArray[j + 1]->getSchedule()->getPlant()->getGrowingTime() <
                vSchedulesArray[j]->getSchedule()->getPlant()->getGrowingTime())
            {
                ptrTempSchedule = vSchedulesArray[j + 1];
                vSchedulesArray[j + 1] = vSchedulesArray[j];
                vSchedulesArray[j] = ptrTempSchedule;
            }
        }
    }
}




ScheduleSorter::ScheduleSorter()
{

}


ScheduleSorter::~ScheduleSorter()
{

}



static ScheduleSorter* instance;

ScheduleSorter* ScheduleSorter::instance = nullptr;



ScheduleSorter* ScheduleSorter::getInstance()
{
    if (nullptr == instance)
    {
        instance = new ScheduleSorter();
    }
    return instance;
}