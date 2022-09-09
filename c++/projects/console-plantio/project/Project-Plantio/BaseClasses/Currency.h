#ifndef CURRENCY_H
#define CURRENCY_H


#include "../Misc/Toolbox.h"


#include <iostream>
#include <string>
#include <vector>


using std::vector;
using std::string;
using std::wstring;



#ifndef using_currency
#define using_currency Currency* currency = Currency::getInstance()
#endif


// Класс "Валюта", который предоставляет собой переключатель режимов приложения;
class Currency
{

public:

	// Геттер экземпляра обменника;
	static Currency* getInstance();

	// Геттер списка валют;
	vector<wstring> getCurrencyList();

	// Сеттер активной валюты;
	// "sCurrencyName" — имя валюты;
	// returns — true в случае успеха;
	bool setActiveCurrency(wstring sCurrencyName);

	// Геттер используемой валюты;
	// returns – имя используемой валюты;
	wstring getActiveCurrency();

	// Сеттер имени файла для получения списка валют;
	// "sFileName" — имя файла со списком валют;
	// return — true если файл удалось открыть;
	bool setFileName(string sFileName);

	// Геттер относительного пути к файлу со списком валют;
	// returns — путь к файлу;
	string getFileName();



protected:

	// Список валют;
	vector<wstring> vCurrencyList;

	// Имя активной валюты;
	wstring sActiveCurrencyName;

	// Имя файла, в котором содержится список валют;
	string sFileName;

	// Экземпляр обменника;
	static Currency* instance;

	// Конструктор по умолчанию;
	Currency();

	// Деструктор;
	virtual ~Currency();



};


#endif

