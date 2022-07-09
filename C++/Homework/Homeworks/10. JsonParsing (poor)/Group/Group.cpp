#include "Group.h"





#pragma region PREPROCESSING




#pragma region SWITCHES


#ifndef DEBUG
#define DEBUG
#endif


#pragma endregion SWITCHES




#pragma region SEMANTICS


#ifndef NEW_ELEMENT
#define NEW_ELEMENT 1
#endif

#ifndef SECOND_MATCH
#define SECOND_MATCH 1
#endif

#ifndef THIRD_MATCH
#define THIRD_MATCH 1
#endif

#ifndef NAME_POSITION
#define NAME_POSITION 8
#endif

#ifndef STUDENTS_POSITION
#define STUDENTS_POSITION 10
#endif

#ifndef SUBJECTS_POSITION
#define SUBJECTS_POSITION 10
#endif

#ifndef GRADES_POSITION
#define GRADES_POSITION 8
#endif


#pragma endregion SEMANTICS





#pragma endregion PREPROCESSING





#pragma region LOGIC



void Group::printGroupStats(Group& group)
{
	std::cout << "\n\t";

	std::cout << BACKGRN << FOREBLK << group.getName() << BACKBLK << FOREWHT << "\n";

	std::cout << "\n\n\t>> " << FORECYN <<"Subjects:"<< FOREWHT <<"\n";

	for (size_t i = 0ULL; i < group.getSubjectCounter(); i++)
	{
		std::cout << "\n\t";
		std::cout << group.getSubjectListPointer()[i].getName();
	}

	std::cout << "\n\n\t";

	std::cout << ">> " << FORECYN <<"Students:" << FOREWHT;

	std::cout << "\n\n";

	for (size_t i = 0ULL; i < group.getStudentCounter(); i++)
	{
		std::cout << "\t" << BACKGRN << FOREBLK << group.getCrewPointer()[i].getName() << BACKBLK << FOREWHT << "\n";
		for (size_t j = 0ULL; j < group.getSubjectCounter(); j++)
		{
			std::cout << "\t\t" << group.getSubjectListPointer()[j].getName() << ":\t";

			if (group.getCrewPointer()[i].isInit)
			{
				for (size_t k = 1ULL; k <= group.getCrewPointer()[i].getGrades()[j][0]; k++)
				{
					std::cout << group.getCrewPointer()[i].getGrades()[j][k] << " ";
				}
			}
			std::cout << "\n";
		}
		std::cout << "\n\n";
	}

}


float Group::getGroupAverageGrade()
{
	int size = getStudentCounter();
	float S = 0;

	for (int i = 0; i < size; i++)
	{
		S += getStudentsAverageGrades()[i];
	}

	if (size != 0) S /= size;
	else if (size == 0) return -1.0;

	return S;
}


float* Group::getStudentsAverageGrades()
{
	int size = getStudentCounter();
	float* A = new float[size];

	int S, c;

	for (int i = 0; i < size; i++)
	{

		A[i] = 0;
		S = 0;
		c = 0;

		for (int j = 0; j < ptrCrew[i].getSubjectCounter(); j++)
		{
			for (int k = 0; k < ptrCrew[i].getGrades()[j][0]; k++)
			{
				S += ptrCrew[i].getGrades()[j][k];
				c++;
			}
		}

		if (c != 0) A[i] = (float)S / c;
		else break;
	}

	return A;
}


float** Group::getStudentsAverageGradesForSubjects()
{
	int size = getStudentCounter();
	float S = 0;
	float** A = new float* [size];

	for (int i = 0; i < size; i++)
	{
		for (int j = 0; j < getSubjectCounter(); j++)
		{
			A[i] = new float[ptrCrew[i].getSubjectCounter()];
		}

		for (int j = 0; j < ptrCrew[i].getSubjectCounter(); j++)
		{
			A[i][j] = 0;
			S = 0;
			for (int k = 0; k < ptrCrew[i].getGrades()[j][0]; k++)
			{
				S += ptrCrew[i].getGrades()[j][k];
			}
			if (ptrCrew[i].getGrades()[j][0] != 0) A[i][j] = (float)S / ptrCrew[i].getGrades()[j][0];
			else break;
		}

	}

	return A;
}


void Group::bind()
{
	if (getSubjectCounter() != 0)
	{
		for (int i = 0; i < getStudentCounter(); i++)
		{
			ptrCrew[i].deleteAllSubjects();

			for (int j = 0; j < getSubjectCounter(); j++)
			{
				ptrCrew[i].addSubject(getSubjectListPointer()[j].getName());
			}
		}
	}
}


void Group::setRandomStudentList()
{

	int randomStudentQuantity = 1 + rand() % 11;

	if (randomStudentQuantity < 8) randomStudentQuantity += 8 - randomStudentQuantity;

	ptrCrew = new Student[randomStudentQuantity];

	int iterator = 0;

	int randomName = 0;

	FileBufferedReader reader;

	reader.read("AuxiliaryFiles/NamePool.txt");

	char** namePool = reader.getBuffer();


#pragma region FLAGS
	bool bBob = false;
	bool bDavid = false;
	bool bJack = false;
	bool bJohn = false;
	bool bKevin = false;
	bool bMark = false;
	bool bMike = false;
	bool bNick = false;
	bool bSteve = false;
	bool bTom = false;
	bool bRoger = false;
	bool bYan = false;
#pragma endregion FLAGS


	do
	{

		randomName = 0 + rand() % 12;

		switch (randomName)
		{
		case 0:
			if (!bBob)
			{
				bBob = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 1:
			if (!bDavid)
			{
				bDavid = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 2:
			if (!bJack)
			{
				bJack = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 3:
			if (!bJohn)
			{
				bJohn = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 4:
			if (!bKevin)
			{
				bKevin = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 5:
			if (!bMark)
			{
				bMark = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 6:
			if (!bMike)
			{
				bMike = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 7:
			if (!bNick)
			{
				bNick = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 8:
			if (!bSteve)
			{
				bSteve = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 9:
			if (!bTom)
			{
				bTom = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 10:
			if (!bRoger)
			{
				bRoger = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;
		case 11:
			if (!bYan)
			{
				bYan = true;
				ptrCrew[iterator].setName(namePool[randomName]);
				iterator++;
			}
			else continue;
			break;

		}


	} while (iterator < randomStudentQuantity);

	setStudentCounter(randomStudentQuantity);

}


void Group::addSubject(std::string sSubjectName)
{
	Subject* buf = new Subject[getSubjectCounter() + NEW_ELEMENT];				   // создание буфера на единицу больше старого массива

	if (0 != getSubjectCounter())
	{
		for (size_t i = 0ULL; i < getSubjectCounter(); i++)				   // копирование старого массива
		{
			buf[i] = ptrSubjectList[i];
		}
	}

	Subject newSubject(sSubjectName);						     // создание нового предмета

	buf[getSubjectCounter()] = newSubject;
	nSubjectCounter += NEW_ELEMENT;

	delete[]ptrSubjectList;
	ptrSubjectList = buf;
}


void Group::addStudent(std::string sNewStudentName)
{
	Student* buf = new Student[getStudentCounter() + NEW_ELEMENT];				   // создание буфера на единицу больше старого массива

	if (0 != getStudentCounter())
	{
		for (size_t i = 0ULL; i < getStudentCounter(); i++)					  // копирование старого массива
		{
			buf[i] = ptrCrew[i];
		}
	}

	Student newStudent(sNewStudentName);

	buf[getStudentCounter()] = newStudent;
	nStudentCounter += NEW_ELEMENT;

	delete[]ptrCrew;
	ptrCrew = buf;
}


void Group::setRandomSubjectList()
{

	delete[]ptrSubjectList;

	int random = 1 + rand() % 10;

	while (random++ < 5)											// Если кол-во предметов < 5, то поднимаем до 5
	{
	}

	ptrSubjectList = new Subject[random];

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

	delete[]ptrTemp;
}


void Group::setRandomGradesForEveryone()
{
	for (int i = 0; i < getStudentCounter(); i++)
	{
		ptrCrew[i].setRandomGrades();
		this_thread::sleep_for(50ms);
	}

}


#pragma endregion LOGIC




#pragma region CONSTRUCT


Group::Group()
{
	this->sName = "Unknown";
	this->nStudentCounter = 0;
	this->nSubjectCounter = 0;
	this->ptrCrew = nullptr;
	this->ptrSubjectList = nullptr;
}

Group::Group(std::string sName)
{
	this->sName = sName;
	this->nStudentCounter = 0;
	this->nSubjectCounter = 0;
	this->ptrCrew = nullptr;
	this->ptrSubjectList = nullptr;
}

Group::~Group()
{
	if (nullptr != ptrCrew)
	{
		delete[]ptrCrew;
		ptrCrew = nullptr;
	}
	if (nullptr != ptrSubjectList)
	{
		delete[]ptrSubjectList;
		ptrSubjectList = nullptr;
	}
}

Group::Group(Group& newGroup)
{
	newGroup.sName = this->sName;

	for (int i = 0; i < getStudentCounter(); i++)
	{
		newGroup.ptrCrew[i] = ptrCrew[i];
	}

	for (int i = 0; i < getSubjectCounter(); i++)
	{
		newGroup.ptrSubjectList[i] = ptrSubjectList[i];
	}

}


#pragma endregion CONSTRUCT




#pragma region ACCESS




void Group::setName(std::string sName)
{
	this->sName = sName;
}


std::string Group::getName()
{
	return this->sName;
}


void Group::setStudentCounter(int nStudentCounter)
{
	this->nStudentCounter = nStudentCounter;
}


int Group::getStudentCounter()
{
	return this->nStudentCounter;
}


void Group::setSubjectCounter(int nSubjectCounter)
{
	this->nSubjectCounter = nSubjectCounter;
}


int Group::getSubjectCounter()
{
	return this->nSubjectCounter;
}


Subject* Group::getSubjectListPointer()
{
	return this->ptrSubjectList;
}


Student* Group::getCrewPointer()
{
	return this->ptrCrew;
}




#pragma endregion ACCESS


