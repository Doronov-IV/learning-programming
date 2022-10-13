#include "EnumAdapter.h"






string EnumAdapter::parseSpeciesEnum(Species spcName)
{
    auto ptrRes = mSpecies.find(spcName);

    string res = ptrRes->second;

    return res;
}


Species EnumAdapter::parseSpeciesString(string sName)
{
    Species res = Osteospermum;

    for (auto iter : mSpecies) 
        if (sName == iter.second) res = iter.first;
    
    return res;
}


string EnumAdapter::parseCountryEnum(Country cName)
{
    auto ptrRes = mCountries.find(cName);

    string res = ptrRes->second;

    return res;
}


Country EnumAdapter::parseCountryString(string sName)
{
    Country res = Poland;

    for (auto iter : mCountries)  
        if (sName == iter.second) res = iter.first;
    
    return res;
}


string EnumAdapter::parsePlantingEnum(Planting pName)
{
    auto ptrRes = mPlanting.find(pName);

    string res = ptrRes->second;

    return res;
}


Planting EnumAdapter::parsePlantingString(string sName)
{
    Planting res = Tuber;

    for (auto iter : mPlanting) 
        if (sName == iter.second) res = iter.first;

    return res;
}




EnumAdapter* EnumAdapter::getInstance()
{
    if (nullptr == instance) instance = new EnumAdapter();
    
    return instance;
}


EnumAdapter* EnumAdapter::instance = nullptr;


EnumAdapter::EnumAdapter()
{
    mSpecies.emplace(Species::PetuniaPendula, "PetuniaPendula");
    mSpecies.emplace(Species::PetuniaAtkinsiana, "PetuniaAtkinsiana");
    mSpecies.emplace(Species::Geranium, "Geranium");
    mSpecies.emplace(Species::Fuchsia, "Fuchsia");
    mSpecies.emplace(Species::Cineraria, "Cineraria");
    mSpecies.emplace(Species::Hyacinthus, "Hyacinthus");
    mSpecies.emplace(Species::Viola, "Viola");
    mSpecies.emplace(Species::PetuniaPendulaCachepot, "PetuniaPendulaCachepot");
    mSpecies.emplace(Species::Verbena, "Verbena");
    mSpecies.emplace(Species::Tagetes, "Tagetes");
    mSpecies.emplace(Species::Antirrhinum, "Antirrhinum");
    mSpecies.emplace(Species::Osteospermum, "Osteospermum");
    mSpecies.emplace(Species::Dahlia, "Dahlia");
    mSpecies.emplace(Species::Calibrachoa, "Calibrachoa");
    mSpecies.emplace(Species::Salvia, "Salvia");
    mSpecies.emplace(Species::Catharanthus, "Catharanthus");


    mCountries.emplace(Country::Germany, "Germany");
    mCountries.emplace(Country::Poland, "Poland");
    mCountries.emplace(Country::Netherlands, "Netherlands");


    mPlanting.emplace(Planting::Cutting, "Cutting");
    mPlanting.emplace(Planting::Seed, "Seed");
    mPlanting.emplace(Planting::Bulb, "Bulb");
    mPlanting.emplace(Planting::Tuber, "Tuber");


    mPackagingTypes.emplace(PackagingType::Pot, "Pot");
    mPackagingTypes.emplace(PackagingType::PlasticCup, "PlasticCup");

 }


EnumAdapter::~EnumAdapter()
{

}



