#ifndef PLANTIO_H
#define PLANTIO_H


#include "Stock.h"
#include "Schedule.h"
#include "../Wrappers/EnumAdapter.h"
#include <vector>


using std::vector;


#pragma region PREPROCESSING

// Кол-во видов растений в программе на текущий момент;
#ifndef OVERALL_PLANT_QUANTITY
#define OVERALL_PLANT_QUANTITY 16
#endif

#pragma endregion PREPROCESSING


// Класс "Plantio", который хранит в себе все основные данные;
class Plantio
{


public:

	// Получить ссылку на приложение;
	static Plantio* getInstance();

	// Очистить все данные;
	void release();

	// Получить список растений;
	vector<Plant*> getPlantList();

	// Получить ссылку схем по умолчанию;
	vector<Schedule*> getDefaultSchedulesList();

	// Установить активный склад;
	void setActiveStock(Stock* ptrStock);

	// Создать схемы по умолчанию;
	void createDefaultSchedules();

	// Создать новый склад;
	bool createNewStock(string sNtockName, Soil* ptrSoil);

	// Загрузить склад;
	bool loadStock(string sStockName);

	// Получить схемы текущего склада;
	vector<Schedule*> getCurrentStockSchedules();
	
	// Получить ссылку на текущий склад;
	Stock* getCurrentStock();

	// Очистить схемы предыдущего склада;
	void cleansePreviousStockSchedules();

	// Получить список складов;
	vector<Stock*> getStocksList();

private:

	// Создать базовые программы к просмотру;
	void init();

	// Синхронизировать файл с складами с массивом складов;
	void runStocksSync();

	// Составить схемы для текущего склада;
	void setCurrentStockSchedules();

	// Список растений;
	vector<Plant*> vPlantsList;

	// Список схем для текущего склада;
	vector<Schedule*> vCurrentStockSchedules;

	// Схемы по умолчанию;
	vector<Schedule*> vDefaultSchedules;

	// Список всех складов;
	vector<Stock*> vStocks;

	// Ссылка на текущий склад;
	Stock* ptrCurrentStock;

	// Экземпляр приложения;
	static Plantio* instance;

	// Конструктор по умолчанию;
	Plantio();

	// Деструктор;
	virtual ~Plantio();

};


#endif