#include "Student.h"


#ifndef NEW_ELEMENT
#define NEW_ELEMENT 1
#endif

// once again, чтобы кол-во оценок совпадало с кол-вом ячеек в массиве
#ifndef ELEMENT_QUANTITY
#define ELEMENT_QUANTITY 1
#endif




#pragma region LOGIC


int Student::addGrade(Student& student, Subject subject, int nGradeValue)
{
	int res = -1;
	int nSubjectNumber = 0;
	bool bMainFlag = false;

	for (size_t i = 0ULL; i < student.nSubjectCounter; i++)
	{
		if (subject.getName() == student.getSubjectsPointer()[i].getName())
		{
			bMainFlag = true;
			break;
		}
		nSubjectNumber += 1;
	}

	if (bMainFlag)
	{

		if (student.isInit)
		{

			int** buf = new int* [student.getSubjectCounter()];

			for (size_t i = 0ULL; i < student.getSubjectCounter(); i++)
			{
				if (i == nSubjectNumber) buf[i] = new int[student.getGrades()[i][0] + NEW_ELEMENT];
				else buf[i] = new int[student.getGrades()[i][0]];
			}

			for (size_t i = 0ULL; i < student.getSubjectCounter(); i++)
			{
				for (size_t j = 0ULL; j < student.getGrades()[j][0] + ELEMENT_QUANTITY; j++)
				{
					buf[i][j] = student.ptrGrades[i][j];
				}
			}

			buf[nSubjectNumber][student.getGrades()[nSubjectNumber][0]] = nGradeValue;

			student.ptrGrades[nSubjectNumber][0] += ELEMENT_QUANTITY;

			delete[]student.ptrGrades;
			student.ptrGrades = buf;
		}
		else
		{
			student.ptrGrades = new int* [student.getSubjectCounter()];

			for (size_t i = 0ULL; i < student.getSubjectCounter(); i++)
			{
				if (i == nSubjectNumber)
				{
					student.ptrGrades[i] = new int[ELEMENT_QUANTITY + NEW_ELEMENT];
					student.ptrGrades[i][0] = ELEMENT_QUANTITY;
					student.ptrGrades[i][1] = nGradeValue;
				}
				else
				{
					student.ptrGrades[i] = new int[ELEMENT_QUANTITY];
					student.ptrGrades[i][0] = ELEMENT_QUANTITY;
				}
			}

			student.isInit = true;
		}

		res = 0;

		return res;
	}
	else return res;
}


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
	int nTempValue = 0;

	ptrGrades = new int* [size];

	for (int i = 0; i < size; i++)
	{
		nTempValue = 1 + rand() % 10;
		ptrGrades[i] = new int[nTempValue];
		ptrGrades[i][0] = nTempValue;
		for (int j = 1; j < ptrGrades[i][0]; j++)
		{
			ptrGrades[i][j] = 1 + rand() % 12;
		}
	}

}


void Student::addSubject(std::string sSubjectName)
{
	Subject* bufSubjects = new Subject[getSubjectCounter() + NEW_ELEMENT];					// создание буфера на единицу больше старого массива

	if (getSubjectCounter() != 0)
	{
		for (int i = 0; i < getSubjectCounter(); i++)				    // копирование старого массива
		{
			bufSubjects[i] = ptrSubjects[i];
		}
	}


	Subject newSubject(sSubjectName);							 // создание нового предмета

	bufSubjects[getSubjectCounter()] = newSubject;
	nSubjectCounter += 1;

	delete[]ptrSubjects;
	ptrSubjects = bufSubjects;
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




#pragma region CONSTRUCT


Student::Student()
{
	isInit = false;
	ptrGrades = nullptr;
	ptrSubjects = nullptr;
	nSubjectCounter = 0;
}


Student::~Student()
{
	if (nullptr != ptrGrades)
	{
		delete[]ptrGrades;
		ptrGrades = nullptr;
	}
	if (nullptr != ptrSubjects)
	{
		delete[]ptrSubjects;
		ptrSubjects = nullptr;
	}
	setSubjectCounter(0);
}


Student::Student(std::string sName) : Student::Student()
{

	this->sName = sName;
}


#pragma endregion CONSTRUCT




#pragma region ACCESS


void Student::increaseGradesCounter(Student& student, Subject subject)
{
	int nSubjectNumer = 0;

	for (size_t i = 0ULL; i < student.nSubjectCounter; i++, nSubjectNumer++)
	{
		if (student.ptrSubjects[i].getName() == subject.getName()) break;
	}

	student.ptrGrades[nSubjectNumer][0] += 1;
}


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
	return sName;
}


void Student::setName(std::string sName)
{
	this->sName = sName;
}


int Student::getSubjectCounter()
{
	return nSubjectCounter;
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


#pragma endregion ACCESS