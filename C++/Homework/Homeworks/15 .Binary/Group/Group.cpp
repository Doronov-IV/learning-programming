#include "Group.h"



#pragma region LOGIC



int** Group::MaxMin()
{
	int sizeGroup = getStudentCounter();
	int sizeSubject = getSubjectCounter();

	int** A = new int* [sizeSubject];

	for (int i = 0; i < sizeSubject; i++)
	{
		A[i] = new int[4];
	}

	for (int i = 0; i < sizeSubject; i++)
	{
		A[i][0] = ptrCrew[0].getGrades()[0][0];
		A[i][1] = ptrCrew[0].getGrades()[0][0];
		A[i][2] = 0;
		A[i][3] = 0;
	}

	for (int i = 0; i < sizeGroup; i++)
	{
		for (int j = 0; j < ptrCrew[i].getSubjectCounter(); j++)
		{
			for (int k = 0; k < ptrCrew[i].getGradeCounters()[j]; k++)
			{
				if (ptrCrew[i].getGrades()[j][k] > A[j][0])
				{
					A[j][0] = ptrCrew[i].getGrades()[j][k];
					A[j][2] = i;
				}
				if (ptrCrew[i].getGrades()[j][k] < A[j][1])
				{
					A[j][1] = ptrCrew[i].getGrades()[j][k];
					A[j][3] = i;
				}
			}
		}
	}

	return A;
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
			for (int k = 0; k < ptrCrew[i].getGradeCounters()[j]; k++)
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
	float** A = new float*[size];

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
			for (int k = 0; k < ptrCrew[i].getGradeCounters()[j]; k++)
			{
				S+=ptrCrew[i].getGrades()[j][k];
			}
			if (ptrCrew[i].getGradeCounters()[j] != 0) A[i][j] = (float)S / ptrCrew[i].getGradeCounters()[j];
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
	using_reader;

	int randomStudentQuantity = 1 + rand() % 11;

	if (randomStudentQuantity < 8) randomStudentQuantity += 8 - randomStudentQuantity;

	ptrCrew = new Student[randomStudentQuantity];

	int iterator = 0;

	int randomName = 0;

	reader->read("NamePool.txt");

	char** namePool = nullptr;

	for (size_t i = 0ULL, iSize = reader->getBuffer().size(); i < iSize; ++i)
	{
		for (size_t j = 0ULL, jSize = reader->getBuffer()[i].size(); j < jSize; ++j)
		{
			namePool[i][j] = reader->getBuffer()[i][j];
		}
	}

#pragma region FLAGS
	bool bBob   = false;
	bool bDavid = false;
	bool bJack  = false;
	bool bJohn  = false;
	bool bKevin = false;
	bool bMark  = false;
	bool bMike  = false;
	bool bNick  = false;
	bool bSteve = false;
	bool bTom   = false;
	bool bRoger = false;
	bool bYan   = false;
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
	Subject* buf = new Subject[getSubjectCounter() + 1];				   // создание буфера на единицу больше старого массива

	if (getSubjectCounter() != 0)
	{
		for (int i = 0; i < getSubjectCounter(); i++)				   // копирование старого массива
		{
			buf[i] = ptrSubjectList[i];
		}
	}

	Subject newSubject(sSubjectName);						     // создание нового предмета

	buf[getSubjectCounter()] = newSubject;
	nSubjectCounter += 1;

	delete[]ptrSubjectList;
	ptrSubjectList = buf;
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




#pragma region CONSTRUCT_DESTRUCT_COPY


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
	delete[]ptrCrew;
	delete[]ptrSubjectList;
}

Group::Group(Group& newGroup)
{
	newGroup.sName = this->sName;

	for (int i = 0; i < getStudentCounter(); i++)
	{
		newGroup.ptrCrew[i] = this->ptrCrew[i];
	}

	for (int i = 0; i < getSubjectCounter(); i++)
	{
		newGroup.ptrSubjectList[i] = this->ptrSubjectList[i];
	}

}


#pragma endregion CONSTRUCT_DESTRUCT_COPY




#pragma region SETGET


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


#pragma endregion SETGET



