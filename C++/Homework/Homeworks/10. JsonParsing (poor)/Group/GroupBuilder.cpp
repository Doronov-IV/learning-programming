#include "GroupBuilder.h"



#pragma region PREPROCESSING




#pragma region SWITCHES


#ifndef DEBUG
#define _DEBUG
#endif


#pragma endregion SWITCHES




#pragma region SEMANTICS


#ifndef NEW_ELEMENT
#define NEW_ELEMENT 1
#endif

// Студентов в группе 3, маскимальный индекс -- 2
#ifndef ITERATION_ISSUE
#define ITERATION_ISSUE 1
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

// Индекс, с которого начинаются числа в таблице чаров
#ifndef CHAR_TO_INT
#define CHAR_TO_INT 48
#endif


#pragma endregion SEMANTICS




#pragma region FLAGS


#ifndef FILE_START
#define FILE_START 0
#endif

#ifndef FILE_END
#define FILE_END 1
#endif

#ifndef NAME_START
#define NAME_START 2
#endif

#ifndef NAME_END
#define NAME_END 3
#endif

#ifndef STUDENTS_START
#define STUDENTS_START 4
#endif

#ifndef STUDENTS_END
#define STUDENTS_END 5
#endif

#ifndef SUBJECTS_START
#define SUBJECTS_START 6
#endif

#ifndef SUBJECTS_END
#define SUBJECTS_END 7
#endif

#ifndef GRADES_START
#define GRADES_START 8
#endif

#ifndef GRADES_END
#define GRADES_END 9
#endif


#pragma endregion FLAGS




#pragma endregion PREPROCESSING




#pragma region CONSTRUCT


static GroupBuilder* instance;


GroupBuilder* GroupBuilder::instance = nullptr;


GroupBuilder::GroupBuilder()
{

}


GroupBuilder::~GroupBuilder()
{
    delete instance;
}


#pragma endregion CONSTRUCT




void GroupBuilder::build(Group& group, const char* sPath)
{

#pragma region STRATEGY

	/*

		flags:

			FILE_START 0
			FILE_END 1
			NAME_START 2
			NAME_END 3
			STUDENTS_START 4
			STUDENTS_END 5
			SUBJECTS_START 6
			SUBJECTS_END 7
			GRADES_START 8
			GRADES_END 9

		conditions:

			0 -- file_start
			1 -- whether previous condition isn't false
			2 -- whether there is enough room for THREE matches, not two as I thought previous [!] Might be changed since we would certainly add
		check for condition of the previous flag
			3 -- particular


			> [TEMPLATE]: "if ((-1 == marks[NAME_START] && -1 == marks[NAME_END]) || (-1 != marks[NAME_START] && -1 != marks[NAME_END]))"
			> this is a condition in case JSON file would not specify some fields. For example there might not be a name for a group
			or, no students at some point. Thus we either have some previous field ended or none of it at all, preventing method from
			starting new phase inside prevoius one.



	*/
#pragma endregion STRATEGY




#pragma region PREPARATIONS
	FileBufferedReader reader;
	reader.read(sPath);

	Text page("JSON info");
	page.setText(reader.getBuffer(), reader.getBufferSize());

	StringX data = page.toStringX().getArrayPtr();

	int marks_size = 10;
	int* flags = new int[marks_size];
	for (size_t i = 0ULL; i < marks_size; i++)
	{
		flags[i] = -1;
	}
	int nCurrentTabCounter = 0;
	int nPreviousTabCounter = 0;

	std::string sGroupName = "";


	bool bNameSwitch = false;
	bool bStudentsSwitch = false;
	bool bSubjectsSwitch = false;
	bool bGradesSwitch = false;
#pragma endregion PREPARATIONS



#pragma region PROCESSING

	/*

	TODO:

		-- students end
		-- in fact, bind all ends with previous flags
		-- turn all tabs with spaces and/or calculate all tabs and spaces

	*/


	// This is the loop that fills the "marks" array with indexes of certain objects
	for (size_t i = 0ULL; i < data.length; i++)
	{


#pragma region CONDITIONS


		if ('\t' == data[i]) nCurrentTabCounter += 1;

		if ('{' == data[i]) flags[FILE_START] = i;


		if (-1 != flags[FILE_START])
		{
			// name start
			if (i < data.length - SECOND_MATCH)
				if ('n' == data[i] && 'a' == data[i + SECOND_MATCH])
					flags[NAME_START] = i + NAME_POSITION;

			 // name end
			if (-1 != flags[NAME_START] && -1 == flags[NAME_END])
				if (i < data.length - SECOND_MATCH)
					if ('\"' == data[i] && ',' == data[i + SECOND_MATCH])
						flags[NAME_END] = i;



			// students start +
			if ((-1 == flags[NAME_START] && -1 == flags[NAME_END]) || (-1 != flags[NAME_START] && -1 != flags[NAME_END]))
				if (i < data.length - SECOND_MATCH + STUDENTS_POSITION)
					if ('s' == data[i] && 't' == data[i + SECOND_MATCH])
						flags[STUDENTS_START] = i + STUDENTS_POSITION;

			 // students end
			if (-1 != flags[STUDENTS_START] && -1 == flags[STUDENTS_END])
				if (i < data.length - SECOND_MATCH)
					if (']' == data[i] && ',' == data[i + SECOND_MATCH])
						flags[STUDENTS_END] = i;



			 // subjects start +
			if ((-1 == flags[SUBJECTS_START] && -1 == flags[SUBJECTS_END]) || (-1 != flags[SUBJECTS_START] && -1 != flags[SUBJECTS_END]))
				if (i < data.length - 1 + SUBJECTS_POSITION)
					if ('s' == data[i] && 'u' == data[i + SECOND_MATCH])
						flags[SUBJECTS_START] = i + SUBJECTS_POSITION;

			 // subjects end
			if (-1 != flags[SUBJECTS_START] && -1 == flags[SUBJECTS_END])
				if (i < data.length - SECOND_MATCH)
					if (']' == data[i] && ',' == data[i + SECOND_MATCH])
						flags[SUBJECTS_END] = i;



			 // grades start +
			if ((-1 == flags[GRADES_START] && -1 == flags[GRADES_END]) || (-1 != flags[GRADES_START] && -1 != flags[GRADES_END]))
				if (i < data.length - 1 + GRADES_POSITION)
					if ('m' == data[i] && 'a' == data[i + SECOND_MATCH])
						flags[GRADES_START] = i + GRADES_POSITION;

			// grades end +
			if (-1 != flags[GRADES_START] && -1 == flags[GRADES_END])
				if (i < data.length - SECOND_MATCH)
					if (']' == data[i] && '\t' == data[i + SECOND_MATCH])
						flags[GRADES_END] = i;


		}


		if ('}' == data[i]) flags[FILE_END] = i;

		nPreviousTabCounter = nCurrentTabCounter;

#pragma endregion CONDITIONS


#pragma region ASSIGNMENT



		// Group name
		if (-1 != flags[FILE_START])
			if (-1 != flags[NAME_START] && -1 == flags[NAME_END])
				if (!bNameSwitch)
				{
					for (size_t j = flags[NAME_START]; data[j] != '\"'; j++)
					{
						sGroupName.push_back(data[j]);
					}
					bNameSwitch = true;
				}


		// Group students
		if (-1 != flags[FILE_START])
			if (-1 != flags[STUDENTS_START] && -1 == flags[STUDENTS_END])
				if (!bStudentsSwitch)
				{
					std::string sStudentName = "";
					for (size_t j = flags[STUDENTS_START]; data[j] != ']'; j++)
					{
						if ('\"' == data[j])
						{
							j += 1;
							for (size_t k = j; data[k] != '\"'; k++)
							{
								sStudentName.push_back(data[k]);
								j += 1;
							}
							j += 1;
							group.addStudent(sStudentName);
							sStudentName = "";
						}
					}
					bStudentsSwitch = true;
				}


		// Group subjects
		if (-1 != flags[FILE_START])
			if (-1 != flags[SUBJECTS_START] && -1 == flags[SUBJECTS_END])
				if (!bSubjectsSwitch)
				{
					std::string sSubjectName = "";
					for (size_t j = flags[SUBJECTS_START]; data[j] != ']'; j++)
					{
						if ('\"' == data[j])
						{
							j += 1;
							for (size_t k = j; data[k] != '\"'; k++)
							{
								sSubjectName.push_back(data[k]);
								j += 1;
							}
							j += 1;
							group.addSubject(sSubjectName); // a bug here
							sSubjectName = "";
						}
					}
					bSubjectsSwitch = true;
				}

		group.bind();


		//Group grades
		if (-1 != flags[FILE_START])
			if (-1 != flags[GRADES_START] && -1 == flags[GRADES_END])
				if (!bGradesSwitch)
				{

					int nGradesCounter = group.getSubjectCounter();
					
					for (size_t j = flags[GRADES_START], stud_counter = 0ULL; stud_counter < group.getStudentCounter(); j++)
					{
						if ('[' == data[j] && '\t' == data[j - 1] && '\t' == data[j - 2]) // default json doc
						{
							j += 1;
							for (size_t subject_counter = 0ULL, grades_counter = 0ULL; subject_counter < group.getSubjectCounter(); subject_counter++, grades_counter++)
							{
								Student::addGrade(group.getCrewPointer()[stud_counter], 
									group.getSubjectListPointer()[subject_counter], (int)data[j] - CHAR_TO_INT); // adding grades
								j += 3;

								/*
								
									По неведомой причине, добавленные оценки у студентов сохраняются, а счётчики этих оценок -- нет.
									
									Таким образом, мной было принято стратегическое решение хранить кол-во оценок в нулевой ячейке массива оценок.

									Поле ptrGradesCounters было вырезано из класса Student.

									¯\_(ツ)_/¯
								
								*/

							}
							j += 1;
							stud_counter += 1;
						}
					}
					bGradesSwitch = true;
				}

		// write your code here...


#pragma endregion ASSIGNMENT

	}


#pragma endregion PROCESSING


#pragma region EPILOGUE


	if ("" == sGroupName) sGroupName = "Null";
	group.setName(sGroupName);


#pragma endregion EPILOGUE

#ifdef  DEBUG
	std::cout << data;
	std::cout << "\n\n";
	std::cout << "name = " << sGroupName;
	std::cout << "\n\n";
	for (size_t i = 0ULL; i < 10; i++)
	{
		std::cout << "\t" << flags[i] << "\n";
	}
	std::cout << "\n\n";
	std::cout << "\t\tGradeCounters " << group.getCrewPointer()[0].getGradeCounters()[1];
#endif //  DEBUG


	// END of method
}


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
			for (int k = 0; k < ptrCrew[i].getGrades()[j][0]; k++)
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



GroupBuilder* GroupBuilder::getInstance()
{
    if (nullptr == instance)
    {
        instance = new GroupBuilder();
    }
    return instance;
}
