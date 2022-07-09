#ifndef FILE_BUFFERED_READER
#define FILE_BUFFERED_READER

#include <iostream>
#include <string>

/// <summary>
/// Вспомогательный класс для работы с файлами. Фанатская поделка на тему всяких Bufferedreader'ов
/// </summary>
class FileBufferedReader
{


public:


#pragma region SCAN_READ


	/// <summary>
	/// Копирует содержимое файла в буфер
	/// </summary>
	/// <param name="sPath">Путь к файлу</param>
	void read(std::string sPath);


#pragma endregion SCAN_READ


#pragma region CONSTRUCT


	/// <summary>
	/// Пустой конструктор класса
	/// </summary>
	FileBufferedReader();


	/// Деструктор
	~FileBufferedReader();


#pragma endregion CONSTRUCT




#pragma region ACCESS


	/// <summary>
	/// Геттер буфера
	/// </summary>
	/// <returns>Указатель на двумерный массив чаров</returns>
	char** getBuffer();


	/// <summary>
	/// Геттер размера файла/буфера
	/// </summary>
	/// <returns>Размер буфера</returns>
	int getBufferSize();


#pragma endregion ACCESS



private:


	/// <summary>
	/// Получает скан файла, записывает результат в массив длин строк
	/// </summary>
	/// <param name="sPath">Путь к файлу</param>
	void scan(std::string sPath);


	/// <summary>
	/// Указатель на буфер
	/// </summary>
	char** ptrFileBuffer;


	/// <summary>
	/// Указатель на "скан" файла
	/// Представляет собой массив длин строк файла
	/// </summary>
	int* ptrFileScan;


	/// <summary>
	/// Кол-во строк в файле
	/// </summary>
	int nFileLength;

};


#endif
