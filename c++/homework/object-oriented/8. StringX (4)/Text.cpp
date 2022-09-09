#include "Text.h"


#define FOR_LINES_QUANTITY 1
#define TERMINAL 1



#pragma region PROCESSING


void Text::forEach(std::ostream& outstream)
{
	if (-1 != sDiscription.contains("rogram") || -1 != sDiscription.contains("+")) // Если это текст программы (на C++)
	{
		bool bCommentFlag = false;
		for (size_t i = 0ULL; i < nLength; i++)
		{
			for (size_t j = 0ULL; j < strlen(ptrText[i]) + 1; j++)
			{
				if (j < strlen(ptrText[i]) - 1) // Когда комменты открываются -- смена цвета на зелёный
					if (ptrText[i][j] == '/' && ptrText[i][j + 1] == '*')
					{
						bCommentFlag = true;
						outstream << FOREGRN;
					}

				if (j > 0 || j == strlen(ptrText[i])) // Когда закрываются -- дёрнуть флаг
					if (ptrText[i][j - 1] == '*' && ptrText[i][j] == '/')
					{
						bCommentFlag = false;
					}


				if (j < strlen(ptrText[i]) - 1) // Если "//"
					if (ptrText[i][j] == '/' && ptrText[i][j + 1] == '/')
						outstream << FOREGRN;


				outstream << getTextPtr()[i][j];
			}
			outstream << "\n";
			if (!bCommentFlag) outstream << FOREWHT;
		}
	}
	else // Если это просто любой другой текст
	{
		for (size_t i = 0ULL; i < nLength; i++)
		{
			for (size_t j = 0ULL; j < strlen(ptrText[i]) + 1; j++)
			{
				outstream << getTextPtr()[i][j];
			}
			outstream << "\n";
		}
	}
}


void Text::cutComments()
{
	StringEx* newText = new StringEx[nLength];


	if (-1 != sDiscription.contains("rogram") || -1 != sDiscription.contains("+")) // Если это текст программы (на C++)
	{
		bool bCommentLine = false;
		bool bCommentBlock = false;
		bool bTempFlag = false;
		for (size_t i = 0ULL, v = 0ULL; i < nLength; i++, v++)
		{
			bCommentLine = false;
			bTempFlag = true;
			for (size_t j = 0ULL; j < strlen(ptrText[i]); j++)
			{
				if (j < strlen(ptrText[i]) - 1) // Когда комменты открываются -- остановить запись
					if (ptrText[i][j] == '/' && ptrText[i][j + 1] == '*')
					{
						bCommentBlock = true;
					}

				if (j > 0 || j == strlen(ptrText[i])) // Когда закрываются -- дёрнуть флаг
					if (ptrText[i][j - 1] == '*' && ptrText[i][j] == '/')
					{
						bTempFlag = false;
					}


				if (j < strlen(ptrText[i]) - 1) // Если "//"
					if (ptrText[i][j] == '/' && ptrText[i][j + 1] == '/')
					{
						bCommentLine = true;
					}

				if (!bCommentBlock && !bCommentLine)
				{
					newText[i].addSymbol(ptrText[i][j]);
				}

				if (!bTempFlag) bCommentBlock = false;

			}
		}

		setText(newText, nLength);
	}
}


void Text::trim()
{
	int nActualTextLength = 0;

	int nVanguardSpaces = 0;

	bool bStartOfText = false;

	for (size_t i = 0ULL, v = 0ULL; i < nLength; i++, v++)
	{
		bStartOfText = false;
		for (size_t j = 0ULL; j < strlen(ptrText[i]); j++)
		{
			if (' ' != ptrText[i][j] && '\n' != ptrText[i][j] && '\0' != ptrText[i][j] && '\t' != ptrText[i][j] && 0 != strlen(ptrText[i]))
			{
				bStartOfText = true;
				break;
			}
		}
		if (bStartOfText) break;
		else nVanguardSpaces += 1;
	}

	int counter = nVanguardSpaces;
	nActualTextLength = nLength - nVanguardSpaces;
	char** buf = new char* [nActualTextLength];

	for (size_t i = 0ULL; i < nActualTextLength; i++)
	{
		buf[i] = new char[strlen(ptrText[i]) + TERMINAL];
	}

	for (size_t i = 0ULL, k = 0ULL; i < nLength; i++)
	{
		for (size_t j = 0ULL; j < strlen(ptrText[i]) + TERMINAL; j++)
		{
			if (counter == 0)
			{
				buf[k] = ptrText[i];
				buf[k][strlen(ptrText[i])] = '\0';
				k += 1;
				break;
			}
		}
		if (counter > 0) counter -= 1;
	}

	nLength = nActualTextLength;
	delete[]ptrText;
	ptrText = buf;
}


#pragma endregion PROCESSING




#pragma region CONSTRUCTION




Text::Text()
{
	ptrText = nullptr;
}


Text::~Text()
{
	delete[]ptrText;
	ptrText = nullptr;
}


Text::Text(Text& anotherText)
{
	int lines = anotherText.getLinesQuantity();

	ptrText = new char* [lines];

	for (size_t i = 0ULL; i < lines; i++)
	{
		ptrText[i] = new char[strlen(anotherText.getTextPtr()[i])];
	}

	for (size_t i = 0ULL; i < lines; i++)
	{
		for (size_t j = 0ULL; strlen(anotherText.getTextPtr()[i]); j++)
		{
			ptrText[i][j] = anotherText.getTextPtr()[i][j];
		}
		ptrText[i][strlen(anotherText.getTextPtr()[i])] = '\0';
	}
	nLength = lines;
}


Text::Text(char** someText, int size)
{
	ptrText = new char* [size];

	for (size_t i = 0ULL; i < size; i++)
	{
		ptrText[i] = new char[strlen(someText[i])];
	}

	for (size_t i = 0ULL; i < size; i++)
	{
		for (size_t j = 0ULL; someText[i][j] != '\0'; j++)
		{
			ptrText[i][j] = someText[i][j];
		}
		ptrText[i][strlen(someText[i])] = '\0';
	}
	nLength = size;
}


Text::Text(Text& anotherText, StringEx discription)
{
	int lines = anotherText.getLinesQuantity();

	ptrText = new char* [lines];

	for (size_t i = 0ULL; i < lines; i++)
	{
		ptrText[i] = new char[strlen(anotherText.getTextPtr()[i])];
	}

	for (size_t i = 0ULL; i < lines; i++)
	{
		for (size_t j = 0ULL; strlen(anotherText.getTextPtr()[i]); j++)
		{
			ptrText[i][j] = anotherText.getTextPtr()[i][j];
		}
		ptrText[i][strlen(anotherText.getTextPtr()[i])] = '\0';
	}
	nLength = lines;
	this->sDiscription = anotherText.sDiscription;
}


Text::Text(char** someText, int size, StringEx discription)
{
	ptrText = new char* [size];

	for (size_t i = 0ULL; i < size; i++)
	{
		ptrText[i] = new char[strlen(someText[i])];
	}

	for (size_t i = 0ULL; i < size; i++)
	{
		for (size_t j = 0ULL; someText[i][j] != '\0'; j++)
		{
			ptrText[i][j] = someText[i][j];
		}
		ptrText[i][strlen(someText[i])] = '\0';
	}
	nLength = size;
	this->sDiscription = discription;
}




#pragma endregion CONSTRUCTION




#pragma region ACCESS




char** Text::getTextPtr()
{
	return ptrText;
}


int Text::getLinesQuantity()
{
	return nLength;
}


void Text::setText(char** newText, int size)
{
	delete[]ptrText;
	ptrText = new char* [size];

	for (size_t i = 0ULL; i < size; i++)
	{
		ptrText[i] = new char[strlen(newText[i])];
	}

	for (size_t i = 0ULL; i < size; i++)
	{
		for (size_t j = 0ULL; newText[i][j] != '\0'; j++)
		{
			ptrText[i][j] = newText[i][j];
		}
		ptrText[i][strlen(newText[i])] = '\0';
	}
	nLength = size;
}


void Text::setText(StringEx* newText, int size)
{
	delete[]ptrText;
	ptrText = new char* [size];

	for (size_t i = 0ULL; i < size; i++)
	{
		ptrText[i] = new char[newText[i].size() + TERMINAL];
	}

	for (size_t i = 0ULL; i < size; i++)
	{
		for (size_t j = 0ULL; j < newText[i].size(); j++)
		{
			ptrText[i][j] = newText[i][j];
		}
		ptrText[i][newText[i].getLength()] = '\0';
	}
	nLength = size;
}


StringEx Text::getDiscription()
{
	return sDiscription;
}


void Text::setDiscription(StringEx sDiscription)
{
	this->sDiscription = sDiscription;
}




#pragma endregion ACCESS




