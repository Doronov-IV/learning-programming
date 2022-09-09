#ifndef FILE_BUFFERED_READER
#define FILE_BUFFERED_READER


#include <iostream>
#include <string>
#include <vector>
#include <fstream>


using std::string;
using std::vector;
using std::getline;
using std::ifstream;


#ifndef using_reader 
#define using_reader FileBufferedReader* reader = new FileBufferedReader();
#endif


// Вспомогательный класс для работы с файлами. Фанатская поделка на тему всяких Bufferedreader'ов;
class FileBufferedReader
{


public:



#pragma region LOGIC


	// Копирует содержимое файла в буфер;
	// "sPath" — Путь к файлу;
	// returns — true в случае валидности пути;
	bool read(std::string sPath);

	// Приводит массив символов к типу std::string;
	// returns — содержимое буфера в виде строки;
	std::string toString();


#pragma endregion LOGIC




#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	FileBufferedReader();


	// Деструктор;
	~FileBufferedReader();


#pragma endregion CONSTRUCTION




#pragma region ACCESS


	// Геттер массива строк;
	vector<string> getBuffer();


#pragma endregion ACCESS



private:

	// Массив строк, полученных из файла;
	vector<string> vFileBuffer;

};


#endif