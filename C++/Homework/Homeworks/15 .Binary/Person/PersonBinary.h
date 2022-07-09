#ifndef PERSON_BINARY
#define PERSON_BINARY



#include "Person.h"
#include <fstream>
#include <filesystem>
#include <sstream>
#include <cstring>

#include "../FileBufferedReader/FileBufferedReader.h"




// Класс который сохраняет (сереализует) объект класса Person
class PersonBinary
{
public:

	// Конструктор
	// person - объект класса который мы собираемся записать в файл
	PersonBinary(Person& person, string filePath);

	// Запись в файл
	void write() const;

	// Чтение из файла;
	Person read(string sPath);

private:

	// объект целевого класса
	Person& person;

	// Путь к файлу
	string filePath;
};


#endif