#ifndef PLANT_H
#define PLANT_H


#ifndef PLANTING_H
#include "Enums/Planting.h"
#endif


#ifndef COUNTRY_H
#include "Enums/Country.h"
#endif


#ifndef SPECIES_H
#include "Enums/Species.h"
#endif

#include "../Wrappers/EnumAdapter.h"


#include <iostream>

using namespace SpeciesEnum;

// Класс "Растение", не имеет особых конструкторов, создаётся билдерами;
class Plant
{


public:



#pragma region GET

	//;
	//;
	Planting getPlantingMaterial();

	// Геттер вида;
	// returns — вид растения;
	Species getSpecies();

	// Геттер страны поставщика посадочного материала;
	// returns — страна поставщика;
	Country getMaterialCountry();

	// Геттер показателя необходимого объёма почвы;
	// returns — показатель объёма;
	double getSoilNeedRatio();

	// Геттер показателя необходимого удобрения;
	// returns — показатель удобрения (в баллах);
	size_t getFertilizerNeed();

	// Геттер показателя необходимого химиката;
	// returns — показатель химиката (в баллах);
	size_t getChemicalsNeed();

	// Геттер стоимости посадочного материала;
	// returns — стоимости материала (за юнит);
	double getPlantingPrice();

	//;
	//;
	double getPackagingPrice();

	// Геттер времени выращивания;
	// returns — время выращивания;
	size_t getGrowingTime();

	// Геттер показателя выручки;
	// returns — выручка;
	double getRevenue();

	// Геттер показателя собственных трудозатрат;
	// returns — показатель трудозатрат (баллы);
	size_t getSelfloadRequired();

	// Геттер показателя наёмных трудозатрат;
	// returns — показатель трудозатрат (деньги за юнит);
	double getHiredWorkload();

	// Геттер уязвимости к вредителям ;
	// returns — вероятность подверженности (в баллах);
	size_t getVerminExposure();


#pragma endregion GET



#pragma region SET

	// Сеттер вида;
	// "eSpecies" — вид растения;
	void setSpecies(Species eSpecies);

	void setPlantingMaterial(Planting ePlantingMaterial);

	// Сеттер страны поставщика посадочного материала;
	// "ePlantingMaterialCountry" — страна поставщика;
	void setMaterialCountry(Country ePlantingMaterialCountry);

	// Сеттер показателя необходимого объёма почвы;
	// "dSoilNeedRatio" — показатель объёма;
	void setSoilNeedRatio(double dSoilNeedRatio);

	// Сеттер показателя необходимого удобрения;
	// "nFertilizerNeed" — показатель удобрения (в баллах);
	void setFertilizerNeed(size_t nFertilizerNeed);

	// Сеттер показателя необходимого химиката;
	// "nChemicalsNeed" — показатель химиката (в баллах);
	void setChemicalsNeed(size_t nChemicalsNeed);

	// Сеттер стоимости посадочного материала;
	// "dPlantingPrice" — стоимости материала (за юнит);
	void setPackagingPrice(double dPackagingPrice);

	// Сеттер стоимости посадочного материала;
	// "dPlantingPrice" — стоимости материала (за юнит);
	void setPlantingPrice(double dPlantingPrice);

	// Сеттер времени выращивания;
	// "nGrowingTime" — время выращивания;
	void setGrowingTime(size_t nGrowingTime);

	// Сеттер показателя выручки;
	// "dRevenuePerUnit" — выручка;
	void setRevenue(double dRevenuePerUnit);

	// Сеттер показателя собственных трудозатрат;
	// "nSelfloadRequired" — показатель трудозатрат (баллы);
	void setSelfloadRequired(size_t nSelfloadRequired);

	// Сеттер показателя наёмных трудозатрат;
	// "nHiredWorkload" — показатель трудозатрат (деньги за юнит);
	void setHiredWorkload(double dHiredWorkload);

	// Сеттер уязвимости к вредителям ;
	// "nVerminExposure" — вероятность подверженности (в баллах);
	void setVerminExposure(size_t nVerminExposure);


#pragma endregion SET



#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Plant();

	// Деструктор;
	virtual ~Plant();

	// Конструктор копирования;
	// "anotherPlant" — копируемый объект;
	Plant(Plant& anotherPlant);

	// Перегрузка оператора "=";
	// "anotherPlant" — копируемый объект;
	// returns — левый операнд;
	Plant& operator=(Plant& anotherPlant);


#pragma endregion CONSTRUCTION



protected:

	// Тип посадочного материала;
	Planting ePlantingMaterial;

	// Биологический вид растения;
	Species eSpecies;

	// Страна посадочного материала;
	Country ePlantingMaterialCountry;

	// Потребность в посадочной смеси (литр на одно растение);
	double dSoilNeedRatio;

	// Потребность в удобрении, в баллах от 1 до 5;
	size_t nFertilizerNeed;

	// Потребность в ядохимикатах, в баллах от 1 до 5;
	size_t nChemicalsNeed;

	// Стоимость посадочного материала за штуку;
	double dPlantingPrice;

	double dPackagingPrice;

	// Время на выращивание (в днях);
	size_t nGrowingTime;

	// Выручка за единицу товара;
	double dRevenuePerUnit;

	// Показатель собственной трудоёмкости (в баллах);
	size_t nSelfloadRequired;

	// Показатель наёмной трудоёмкости (в деньгах за единицу товара);
	double dHiredWorkload;

	// Подверженность вредителям; 
	size_t nVerminExposure;


};


#endif

