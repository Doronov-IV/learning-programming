#include "Student.h"


#pragma region LOGIC


void Student::setRandomSubjectList()
{
	srand(time(NULL));

	int random = 1 + rand() % 10;										// Роллим кол-во предметов

	ptrSubjects = new Subject[random];

	while (random++ < 5)											// Если кол-во предметов < 5, то докидываем до 5
	{
	}

	int* ptrTemp = new int[random];


#pragma region FLAGS
	bool bBioFlag = false;
	bool bChemFlag = false;
	bool bCSFlag = false;
	bool bEngFlag = false;
	bool bFRFlag = false;
	bool bDeutschFlag = false;
	bool bLitFlag = false;
	bool bMathFlag = false;
	bool bPhysFlag = false;
	bool bRusFlag = false;
#pragma endregion FLAGS

	int i = 0;

	do
	{

		ptrTemp[i] = 1 + rand() % 10;

		switch (ptrTemp[i])
		{
		case 1:
			if (!bBioFlag)
			{
				bBioFlag = true;
				addSubject("Biology");
				i++;
			}
			else continue;
			break;
		case 2:
			if (!bChemFlag)
			{
				bChemFlag = true;
				addSubject("Chemistry");
				i++;
			}
			else continue;
			break;
		case 3:
			if (!bCSFlag)
			{
				bCSFlag = true;
				addSubject("Computer Science");
				i++;
			}
			else continue;
			break;
		case 4:
			if (!bEngFlag)
			{
				bEngFlag = true;
				addSubject("English");
				i++;
			}
			else continue;
			break;
		case 5:
			if (bFRFlag) continue;
			else
			{
				bFRFlag = true;
				addSubject("French");
				i++;
			}
			break;
		case 6:
			if (bDeutschFlag) continue;
			else
			{
				bDeutschFlag = true;
				addSubject("German");
				i++;
			}
			break;
		case 7:
			if (bLitFlag) continue;
			else
			{
				bLitFlag = true;
				addSubject("Literature");
				i++;
			}
			break;
		case 8:
			if (bMathFlag) continue;
			else
			{
				bMathFlag = true;
				addSubject("Math");
				i++;
			}
			break;
		case 9:
			if (bPhysFlag) continue;
			else
			{
				bPhysFlag = true;
				addSubject("Physics");
				i++;
			}
			break;
		case 10:
			if (bRusFlag) continue;
			else
			{
				bRusFlag = true;
				addSubject("Russian");
				i++;
			}
			break;
		default:
			break;
		}

	} while (i < random);


}

void Student::setRandomGrades()
{
	int size = getSubjectCounter();

	ptrGradeCounters = new int[size];

	ptrGrades = new int* [size];

	for (int i = 0; i < size; i++)
	{
		ptrGradeCounters[i] = 1 + rand() % 10;
		ptrGrades[i] = new int[ptrGradeCounters[i]];
		for (int j = 0; j < ptrGradeCounters[i]; j++)
		{
			ptrGrades[i][j] = 1 + rand() % 12;
		}
	}

}

void Student::addSubject(std::string sSubjectName)
{
	Subject* buf = new Subject[getSubjectCounter() + 1];					   // создание буфера на единицу больше старого массива

	if (getSubjectCounter() != 0)
	{
		for (int i = 0; i < getSubjectCounter(); i++)				   // копирование старого массива
		{
			buf[i] = ptrSubjects[i];
		}
	}

	Subject newSubject(sSubjectName);							 // создание нового предмета

	buf[getSubjectCounter()] = newSubject;
	nSubjectCounter += 1;

	delete[]ptrSubjects;
	ptrSubjects = buf;
}

bool Student::deleteSubject(std::string sName)
{
	if (0 == getSubjectCounter()) return false;

	bool bReturnFlag = false;
	for (int i = 0; i < getSubjectCounter(); i++)
	{
		if (ptrSubjects[i].getName() == sName)
		{
			bReturnFlag = true;
			break;
		}
	}

	if (true == bReturnFlag)
	{

		Subject* buf = new Subject[getSubjectCounter() - 1];

		for (int i = 0, j = 0; i < getSubjectCounter(); i++)
		{
			if (ptrSubjects[i].getName() != sName)
			{
				buf[j] = ptrSubjects[i];
				j++;
			}
		}

		nSubjectCounter -= 1;

		delete[]ptrSubjects;
		ptrSubjects = buf;

		return true;
	}

	return false;
}

void Student::deleteAllSubjects()
{
	nSubjectCounter = 0;
	delete[]ptrSubjects;
	ptrSubjects = nullptr;
}


#pragma endregion LOGIC


#pragma region CONSTRUCT_DESTRUCT


Student::Student()
{
	ptrGrades = nullptr;
	ptrGradeCounters = nullptr;
	ptrSubjects = nullptr;
	nSubjectCounter = 0;
}

Student::~Student()
{
	delete[]ptrGrades;
	delete[]ptrGradeCounters;
	delete[]ptrSubjects;
	setSubjectCounter(0);
}

Student::Student(std::string sName) : Student::Student()
{

	this->sName = sName;
}

#pragma endregion CONSTRUCT_DESTRUCT


#pragma region SETGET


int** Student::getGrades()
{
	return this->ptrGrades;
}


Subject* Student::getSubjectsPointer()
{
	return this->ptrSubjects;
}

void Student::setSubjectCounter(int nValue)
{
	this->nSubjectCounter = nValue;
}

std::string Student::getName()
{
	return this->sName;
}

void Student::setName(std::string sName)
{
	this->sName = sName;
}

int Student::getSubjectCounter()
{
	return nSubjectCounter;
}

int* Student::getGradeCounters()
{
	return this->ptrGradeCounters;
}

int* Student::getGradesCounterForSpecificSubject(std::string sSubjectName)
{
	int pointer = 0;										// Переменная указывает на порядковый номер предмета в массиве предметов 
	for (int i = 0; i < getSubjectCounter(); i++)
	{
		if (sSubjectName == ptrSubjects[i].getName())
		{
			pointer = i;
			break;
		}
	}

	return ptrGrades[pointer];
}


#pragma endregion SETGET