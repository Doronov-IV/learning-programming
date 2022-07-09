#include "PersonBinary.h"

PersonBinary::PersonBinary(Person& person, string filePath) 
	: person(person), filePath(filePath)
{
}



Person PersonBinary::read(string sPath)
{
	Person person;

	ifstream stream(sPath, ios::binary);

	stream.read((char *) &person, sizeof(person));

	stream.close();

	return person;
}



void PersonBinary::write() const
{
	if ("" == this->filePath)
		throw "Invalid file path";

	ofstream stream(filePath, ios::binary);

	stream.write(reinterpret_cast<const char*>(&person), sizeof(person));

	stream.close();
}
