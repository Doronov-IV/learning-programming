#ifndef PLANT_BUILDER_H
#define PLANT_BUILDER_H


#ifndef PLANT_H
#include "../BaseClasses/Plant.h"
#endif

#include "../JSON/Parsing/JsonParser.h"
#include "../Misc/FileBufferedReader.h"

using namespace JsonParsing;


class PlantBuilder
{


public:

	// Собирает растение по его виду;
	void build(Species eSpecies);


#pragma region ACCESS

	// Геттер указателя на растение;
	// returns — указатель на растение;
	Plant* getPlantPointer();

	// Сеттер указателя на растение;
	// "ptrPlant" — указатель на растение;
	void setPlantPointer(Plant* ptrPlant);


#pragma endregion ACCESS



#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	PlantBuilder();

	// Деструктор;
	virtual ~PlantBuilder();

	// Конструктор с указателем на растение;
	// "ptrPlant" — указатель на растение;
	PlantBuilder(Plant* ptrPlant);

	// Конструктор с видом растения;
	// "eSpecies" — вид растения;
	PlantBuilder(Species eSpecies);

	// Конструктор копирования;
	// "anotherBuilder" — копируемый объект;
	PlantBuilder(PlantBuilder& anotherBuilder);

	// Перегрузка оператора "=";
	// "anotherBuilder" — копируемый объект;
	// returns — левый операнд;
	PlantBuilder& operator=(PlantBuilder& anotherBuilder);


#pragma endregion CONSTRUCTION


protected:

	// Указатель на растение;
	Plant* ptrPlant;


#pragma region SPECIFIC_PLANTS

	// Собрать Петунию ампельную;
	void buildPetuniaPendula();

	// Собрать Петунию обыкновенную;
	void buildPetuniaAtkinsiana();

	// Собрать Герань;
	void buildGeranium();

	// Собрать Фуксию;
	void buildFuchsia();

	// Собрать Цинерарию;
	void buildCineraria();

	// Собрать Гиацинт;
	void buildHyacinthus();

	// Собрать Виолу;
	void buildViola();

	// Собрать Петунию ампельную кашпо;
	void buildPetuniaPendulaCachepot();

	// Собрать Вербену;
	void buildVerbena();

	// Собрать Тагетес;
	void buildTagetes();

	// Собрать Львиный зев;
	void buildAntirrhinum();

	// Собрать Остеоспермум;
	void buildOsteospermum();

	// Собрать Георгин;
	void buildDahlia();

	// Собрать Калибрахоа;
	void buildCalibrachoa();

	// Собрать Сальвию;
	void buildSalvia();

	// Собрать Катарантус;
	void buildCatharanthus();


#pragma endregion SPECIFIC_PLANTS


};


#endif