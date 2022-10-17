#include "FileBufferedReader.h"

#include <fstream>

using namespace std;


#pragma region READ_WRITE


void FileBufferedReader::scan(std::string sPath)
{
	delete[]ptrFileScan;

	std::string line = "";
	int j = 0, counter = 0;

	ifstream in;
	in.open(sPath);
	if (in.is_open())
	{
		while (getline(in, line))
		{
			counter += 1;
		}
		this->nFileLength = counter;
		this->ptrFileScan = new int[nFileLength];

	}
	in.close();
	in.open(sPath);
	if (in.is_open())
	{
		while (getline(in, line))
		{
			counter = 0;
			for (int i = 0; line[i] != '\0'; i++)
			{
				counter += 1;
			}
			this->ptrFileScan[j] = counter;
			j++;
		}
	}
	in.close();
}


void FileBufferedReader::read(std::string sPath)
{
	delete[]ptrFileBuffer;
	ptrFileBuffer = new char* [nFileLength];

	for (int i = 0; i < nFileLength; i++)
	{
		ptrFileBuffer[i] = new char[ptrFileScan[i]];
	}

	int c = 0;
	std::string line = "";
	ifstream in;
	in.open(sPath);
	if (in.is_open())
	{
		while (getline(in, line))
		{
			if (c < nFileLength)
			{
				for (int j = 0; j < ptrFileScan[c]; j++)
				{
					this->ptrFileBuffer[c][j] = line[j];
				}
				this->ptrFileBuffer[c][line.length()] = '\0';
				c++;
			}
		}
	}
	in.close();
}


#pragma endregion READ_WRITE


#pragma region CONSTRUCT_DESTRUCT


FileBufferedReader::FileBufferedReader()
{
	this->ptrFileBuffer = nullptr;
	this->ptrFileScan = nullptr;
	this->nFileLength = 0;
}

FileBufferedReader::~FileBufferedReader()
{

	delete[]ptrFileBuffer;
	delete[]ptrFileScan;
}


#pragma endregion CONSTRUCT_DESTRUCT


#pragma region SET_GET


void FileBufferedReader::copyBuffer(char**& ptrNewBuffer)
{
	ptrNewBuffer = this->ptrFileBuffer;
}


int FileBufferedReader::getBufferSize()
{
	return nFileLength;
}


#pragma endregion SET_GET
