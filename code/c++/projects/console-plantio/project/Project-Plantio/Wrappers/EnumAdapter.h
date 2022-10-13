#ifndef ENUM_ADAPTER_H
#define ENUM_ADAPTER_H


#include <iostream>
#include <string>
#include <map>


#pragma region ENUMS_INCLUDES


#ifndef SPECIES_H
#include "../BaseClasses/Enums/Species.h"
#endif

#ifndef COUNTRY_H
#include "../BaseClasses/Enums/Country.h"
#endif

#ifndef PLANTING_H
#include "../BaseClasses/Enums/Planting.h"
#endif

#ifndef PACKAGING_TYPE_H
#include "../BaseClasses/Enums/PackagingType.h"
#endif


#pragma endregion ENUMS_INCLUDES



#ifndef using_adapter
#define using_adapter EnumAdapter* adapter = EnumAdapter::getInstance()
#endif


using namespace SpeciesEnum;


using std::string;
using std::map;


// Класс для стыковки enum'ов и всех остальных типов;
class EnumAdapter
{


public:

	//Запрашивает экземпляр адаптера;
	static EnumAdapter* getInstance();

	// Парсит вид в строку;
	// "spcName" — вид растения;
	// returns — вид растения, но уже в строковом типе;
	string parseSpeciesEnum(Species spcName);

	// Парсит строку в вид;
	// "sName" — вид в строковом типе;
	// returns — вид растения, в типе enum'а;
	Species parseSpeciesString(string sName);

	// Парсит страну в строку;
	// "cName" — страна;
	// returns — страна, но уже в строковом типе;
	string parseCountryEnum(Country cName);

	// Парсит строку в страну;
	// "sName" — страна в строковом типе;
	// returns — страна, в типе enum'а;
	Country parseCountryString(string sName);

	// Парсит материал в строку;
	// "pName" — материал;
	// returns — материал, но уже в строковом типе;
	string parsePlantingEnum(Planting pName);

	// Парсит строку в материал;
	// "sName" — материал в строковом типе;
	// returns — материал, в типе enum'а;
	Planting parsePlantingString(string sName);


private:

	// Экземпляр нашего адаптера;
	static EnumAdapter* instance;

	// Конструктор по умолчанию;
	EnumAdapter();

	// Деструктор;
	virtual ~EnumAdapter();

	// Приводит объект типа "вид" к строковому формату;
	map<Species, string> mSpecies;

	// Приводит объект типа "страна" к строковому формату;
	map<Country, string> mCountries;

	// Приводит объект типа "посадочный материал" к строковому формату;
	map<Planting, string> mPlanting;

	// Приводит объект типа "тип упаковки" к строковому формату;
	map<PackagingType, string> mPackagingTypes;

};


#endif

