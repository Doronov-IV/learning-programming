#ifndef FILE_BUFFERED_READER
#define FILE_BUFFERED_READER


#include <iostream>
#include <string>


// Вспомогательный класс для работы с файлами. Фанатская поделка на тему всяких Bufferedreader'ов
class FileBufferedReader
{


public:


#pragma region SCAN_READ


	// Копирует содержимое файла в буфер
	// "sPath" — Путь к файлу
	void read(std::string sPath);


#pragma endregion SCAN_READ


#pragma region CONSTRUCT



	// Конструктор по умолчанию
	FileBufferedReader();


	// Деструктор
	~FileBufferedReader();


#pragma endregion CONSTRUCT




#pragma region ACCESS



	// Геттер буфера
	// returns — указатель на двумерный массив чаров
	char** getBuffer();


	// Геттер размера файла/буфера
	// returns — размер буфера
	int getBufferSize();


#pragma endregion ACCESS



private:



	// Получает скан файла, записывает результат в массив длин строк
	// "sPath" — путь к файлу
	void scan(std::string sPath);



	// Указатель на буфер
	char** ptrFileBuffer;


	// Указатель на "скан" файла
	// Представляет собой массив длин строк файла
	int* ptrFileScan;


	// Кол-во строк в файле
	int nFileLength;

};


#endif
