#ifndef FILE_BUFFERED_READER
#define FILE_BUFFERED_READER
#endif

#include <string>

/// <summary>
/// Вспомогательный класс для работы с файлами. Фанатская поделка на тему всяких Bufferedreader'ов
/// Просматривает файл, создавая двумерный массив оптимального размера
/// 
/// ObjectName.scan(path);
/// ObjectName.read(path);
/// 
/// </summary>
class FileBufferedReader
{

#pragma region PRIVATE_VARIABLES


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


#pragma endregion PRIVATE_VARIABLES


public:


#pragma region SCAN_READ


	/// <summary>
	/// Получает скан файла, записывает результат в массив длин строк
	/// </summary>
	/// <param name="sPath">Путь к файлу</param>
	void scan(std::string sPath);

	/// <summary>
	/// Копирует содержимое файла в буфер
	/// </summary>
	/// <param name="sPath">Путь к файлу</param>
	void read(std::string sPath);


#pragma endregion SCAN_READ


#pragma region CONSTRUCT_DESTRUCT

	/// <summary>
	/// Пустой конструктор класса
	/// </summary>
	FileBufferedReader();

	/// Деструктор
	~FileBufferedReader();


#pragma endregion CONSTRUCT_DESTRUCT


#pragma region SETGET

	/// <summary>
	/// По сути, геттер для буфера файла
	/// </summary>
	/// <param name="ptrNewBuffer">Указатель на массив, в который копируется содержимое буфера</param>
	void copyBuffer(char**& ptrNewBuffer);

	/// <summary>
	/// Геттер размера файла/буфера
	/// </summary>
	/// <returns></returns>
	int getBufferSize();


#pragma endregion SETGET


};
