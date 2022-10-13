#include "StringEx.h"

#define TERMINAL 1 // Единица для подсчёта длины массива с учётом терминального нуля
#define NEW_ELEMENT 1 // Единица для добавления нового элемента
#define ITERATION_ISSUE 1 // Лишняя итерация в методе insert()

#define NEGATIVE -1 // Отрицательный результат для метода contains()



#pragma region OVERLOADING


void StringEx::operator + (StringEx& right)
{
	return merge(right);
}


StringEx* StringEx::operator / (const char& right)
{
	return split(right);
}


int StringEx::operator % (const char* right)
{
	return contains(right);
}


#pragma endregion OVERLOADING




#pragma region LOGIC


void StringEx::addSymbol(char cSymbol)
{
	char* buf = new char[nLength + NEW_ELEMENT + TERMINAL];

	for (int i = 0; i < nLength; i++)
	{
		buf[i] = ptrArray[i];
	}

	buf[nLength] = cSymbol;

	buf[nLength + TERMINAL] = '\0';

	nLength += NEW_ELEMENT;

	delete[]ptrArray;

	ptrArray = buf;

}


int StringEx::contains(const char* sSearchedString)
{
	int nReturnValue = NEGATIVE;

	int nSearchedSize = 0;

	int counter = 0;

	for (int i = 0; sSearchedString[i] != '\0'; i++)
	{
		nSearchedSize += 1;
	}

	if (nSearchedSize > getLength()) return -1;

	for (int i = 0; i < nLength; i++)
	{
		counter = 0;

		if (ptrArray[i] == sSearchedString[0])
		{
			nReturnValue = i;

			for (int j = 0, k = i; j < nSearchedSize && k < nLength; j++, k++)
			{
				if (ptrArray[k] == sSearchedString[j]) counter++;
			}

			if (nSearchedSize == counter) return nReturnValue;
			else nReturnValue = NEGATIVE;
		}
	}

	return nReturnValue;
}


void StringEx::insert(const char* sInsertedString, int nPosition)
{
	int sizeIns = 0, sizeAll = TERMINAL, tempIterator = 0;

	for (int i = 0; sInsertedString[i] != '\0'; i++)
	{
		sizeIns += 1;
	}

	sizeAll += nLength + sizeIns;

	char* buf = new char[sizeAll];

	sizeAll -= TERMINAL;

	buf[sizeAll] = '\0';

	for (int i = 0, j = 0; i < sizeAll; i++)
	{
		if (i != nPosition)
		{
			buf[i] = ptrArray[j++];
		}
		else
		{
			tempIterator = i;

			for (int k = 0; k < sizeIns; k++)
			{
				buf[tempIterator++] = sInsertedString[k];
			}

			tempIterator -= i;

			i += tempIterator - ITERATION_ISSUE;
		}
	}

	nLength = sizeAll;

	delete[]ptrArray;
	ptrArray = buf;
}


void StringEx::insert(StringEx sInsertedString, int nPosition)
{
	int sizeIns = 0, sizeAll = TERMINAL, tempIterator = 0;

	const char* sInsertedStringPtr = sInsertedString.getArrayPtr();

	for (int i = 0; sInsertedStringPtr[i] != '\0'; i++)
	{
		sizeIns += 1;
	}

	sizeAll += nLength + sizeIns;

	char* buf = new char[sizeAll];

	sizeAll -= TERMINAL;

	buf[sizeAll] = '\0';

	for (int i = 0, j = 0; i < sizeAll; i++)
	{
		if (i != nPosition)
		{
			buf[i] = ptrArray[j++];
		}
		else
		{
			tempIterator = i;

			for (int k = 0; k < sizeIns; k++)
			{
				buf[tempIterator++] = sInsertedStringPtr[k];
			}

			tempIterator -= i;

			i += tempIterator - ITERATION_ISSUE;
		}
	}

	nLength = sizeAll;

	delete[]ptrArray;
	ptrArray = buf;
}


void StringEx::merge(StringEx& secondString)
{
	int sizeFirst = getLength();
	int sizeSecond = secondString.getLength();
	int sizeAll = sizeFirst + sizeSecond + TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	char* buf = new char[sizeAll];

	sizeAll -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; i < sizeFirst && i < sizeAll; i++)
	{
		buf[i] = ptrArray[i];
	}

	for (int i = 0, j = sizeFirst; i < sizeSecond && j < sizeAll; i++, j++)
	{
		buf[j] = secondString.getArrayPtr()[i];
	}
	
	nLength = sizeAll;
	buf[sizeAll] = '\0';

	delete[] ptrArray;
	ptrArray = buf;
}


StringEx* StringEx::split(char separator)
{
	const int size = nLength;

	StringEx* buf = new StringEx[1];

	buf[0].setString("false"); // В случае неудачи, вернём эту же строку

	int counter = 0; // Счётчик символов в строке

	for (int i = 0; i < size; i++)
	{
		if (separator == ptrArray[i]) counter++;
	}

	if (0 != counter)
	{
		counter += 1; // Строк всегда будет на 1 больше, чем символов

		delete[]buf; // 

		buf = new StringEx[counter];

		int j = 0;

		for (int i = 0; i < counter && j < size; i++, j++)
		{
			while (ptrArray[j] != separator)
			{
				if (size == j) break;
				buf[i].addSymbol(ptrArray[j]);
				j+=1;
			}
		}

	}
	return buf;
}


#pragma endregion LOGIC




#pragma region CONSTRUCT_DESTRUCT_COPY


StringEx::StringEx()
{
	ptrArray = nullptr;
	nLength = 0;
}


StringEx::StringEx(const char* Array)
{
	int size = TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; Array[i] != '\0'; i++)
	{
		size += 1;
	}

	ptrArray = new char[size];

	size -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	nLength = size;

	for (int i = 0; i < size; i++)
	{
		ptrArray[i] = Array[i];
	}

	ptrArray[size] = '\0';
}


StringEx::StringEx(StringEx& copyString)
{
	int size = copyString.getLength() + TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	ptrArray = new char[size];

	size -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; i < size; i++)
	{
		ptrArray[i] = copyString.getArrayPtr()[i];
	}

	ptrArray[size] = '\0';
}


StringEx::~StringEx()
{
	delete[]ptrArray;
}


#pragma endregion CONSTRUCT_DESTRUCT_COPY




#pragma region SETGET


int StringEx::getLength()
{
	return this->nLength;
}


void StringEx::setString(const char* newString)
{
	int size = TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	for (int i = 0; newString[i] != '\0'; i++)
	{
		size += 1;
	}

	ptrArray = new char[size];

	size -= TERMINAL; // Это необходимо, чтобы избавиться от предупреждения и Heap corruption'a

	nLength = size;

	for (int i = 0; i < size; i++)
	{
		ptrArray[i] = newString[i];
	}

	ptrArray[size] = '\0';
}


const char* StringEx::getArrayPtr()
{
	return this->ptrArray;
}


#pragma endregion SETGET
