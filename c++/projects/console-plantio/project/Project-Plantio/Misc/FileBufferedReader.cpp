#include "FileBufferedReader.h"



#pragma region LOGIC


bool FileBufferedReader::read(std::string sPath)
{
	if (0 != vFileBuffer.size()) vFileBuffer.clear();
	bool res = false;
	ifstream in;
	in.open(sPath);
	string line;

	if (in.is_open())
	{
		res = true;
		while (getline(in, line))
		{
			vFileBuffer.push_back(line);
		}
	}

	return res;
}


std::string FileBufferedReader::toString()
{
	string sRes = "Null";
	if (vFileBuffer.size() != 0)
	{
		sRes = "";
		for (string unit : vFileBuffer)
		{
			sRes.append(unit);
		}
	}
	return sRes;
}


#pragma endregion LOGIC




#pragma region CONSTRUCTION


FileBufferedReader::FileBufferedReader()
{
}


FileBufferedReader::~FileBufferedReader()
{
	vFileBuffer.clear();
}


#pragma endregion CONSTRUCTION




#pragma region ACCESS


vector<string> FileBufferedReader::getBuffer()
{
	return this->vFileBuffer;
}


#pragma endregion ACCESS