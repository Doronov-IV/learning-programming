#include "FileBufferedReader.h"

#include <fstream>

#define TERMINAL 1

using namespace std;


#pragma region READ_WRITE


void FileBufferedReader::scan(std::string sPath)
{
	if (nullptr != ptrFileScan)
	{
		delete[]ptrFileScan;
		ptrFileScan = nullptr;
	}

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
		nFileLength = counter;
		ptrFileScan = new int[nFileLength];

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
			ptrFileScan[j] = counter;
			j++;
		}
	}
	in.close();
}


void FileBufferedReader::read(std::string sPath)
{
	if (nullptr != ptrFileBuffer)
	{
		delete[]ptrFileBuffer;
		ptrFileBuffer = nullptr;
	}

	scan(sPath);

	ptrFileBuffer = new char* [nFileLength];

	int temp = 0;

	for (int i = 0; i < nFileLength; i++)
	{
		temp = ptrFileScan[i] + TERMINAL;
		ptrFileBuffer[i] = new char[temp];
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
					ptrFileBuffer[c][j] = line[j];
				}
				ptrFileBuffer[c][ptrFileScan[c]] = '\0';
				c++;
			}
		}
	}
	in.close();
}


#pragma endregion READ_WRITE




#pragma region CONSTRUCT


FileBufferedReader::FileBufferedReader()
{
	this->ptrFileBuffer = nullptr;
	this->ptrFileScan = nullptr;
	this->nFileLength = 0;
}


FileBufferedReader::~FileBufferedReader()
{

	if (nullptr != ptrFileBuffer)
		delete[]ptrFileBuffer;

	if (nullptr != ptrFileScan)
		delete[]ptrFileScan;

}


#pragma endregion CONSTRUCT




#pragma region ACCESS


char** FileBufferedReader::getBuffer()
{
	return this->ptrFileBuffer;
}


int FileBufferedReader::getBufferSize()
{
	return nFileLength;
}


#pragma endregion ACCESS