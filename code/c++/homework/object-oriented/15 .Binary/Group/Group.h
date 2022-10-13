#ifndef GROUP_H
#define GROUP_H
#endif

using namespace std;
#include <thread>

#include <string>
#include "Student.h"


class Group
{

#pragma region PRIVATE_VARIALES


	/// <summary>
	/// Имя группы
	/// </summary>
	std::string sName;

	/// <summary>
	/// Указатель на массив учников
	/// </summary>
	Student* ptrCrew;

	/// <summary>
	/// Набор предметов группы
	/// </summary>
	Subject* ptrSubjectList;

	/// <summary>
	/// Счётчик предметов
	/// </summary>
	int nSubjectCounter;

	/// <summary>
	/// Счётчик учеников
	/// </summary>
	int nStudentCounter;


#pragma endregion PRIVATE_VARIALES


public :


#pragma region LOGIC


	/// <summary>
	/// Присваивает групповой список предметов каждому ученику, состоящему в группе
	/// </summary>
	void bind();

	/// <summary>
	/// Создаёт случайный массив учеников
	/// </summary>
	void setRandomStudentList();

	/// <summary>
	/// Генерирует случайный список предметов для группы
	/// </summary>
	void setRandomSubjectList();

	/// <summary>
	/// Добавляет предмет группе учеников
	/// </summary>
	void addSubject(std::string sSubjectName);

	/// <summary>
	/// Заполнение списка оценок случайными числами от 1 до 12
	/// </summary>
	void setRandomGradesForEveryone();

	/// <summary>
	/// Считает средние оценки студентов по предметам
	/// </summary>
	/// <returns>Указатель на массив средних баллов для каждого студента</returns>
	float** getStudentsAverageGradesForSubjects();

	/// <summary>
	/// Средние оценки студентов
	/// </summary>
	/// <returns>Указатель на массив средних баллов для каждого студента</returns>
	float* getStudentsAverageGrades();

	/// <summary>
	/// Считает средний балл группы (среднее арифметическое из оценок по предметам)
	/// </summary>
	/// <returns>Средний балл группы</returns>
	float getGroupAverageGrade();

	/// <summary>
	/// Максимальный балл группы по предметам
	/// </summary>
	/// <returns>Указатель на массив максимальных баллов</returns>
	int** MaxMin();


#pragma endregion LOGIC




#pragma region CONSTRUCTION


	/// <summary>
	/// Пустой конструктор
	/// </summary>
	Group();

	/// <summary>
	/// Конструктор имени группы
	/// </summary>
	Group(std::string sName);

	/// Деструктор
	~Group();

	/// <summary>
	/// Конструктор копирования
	/// </summary>
	Group(Group& newGroup);


#pragma endregion CONSTRUCTION




#pragma region SETGET

	/// <summary>
	/// Сеттер имени
	/// </summary>
	/// <param name="sName">Новое имя</param>
	void setName(std::string sName);

	/// <summary>
	/// Геттер имени
	/// </summary>
	/// <returns>Имя группы</returns>
	std::string getName();

	/// <summary>
	/// Сеттер счётчика предметов
	/// </summary>
	/// <param name="nSubjectCounter">Счётчик предметов группы</param>
	void setSubjectCounter(int nSubjectCounter);

	/// <summary>
	/// Геттер счётчика предметов
	/// </summary>
	/// <returns>Счётчик предметов группы</returns>
	int getSubjectCounter();

	/// <summary>
	/// Сеттер счётчика учеников
	/// </summary>
	/// <param name="nStudentCounter">Счётчик учеников группы</param>
	void setStudentCounter(int nStudentCounter);

	/// <summary>
	/// Геттер счётчика учеников
	/// </summary>
	/// <returns>Счётчик учеников группы</returns>
	int getStudentCounter();

	/// <summary>
	/// Геттер массива предметов
	/// </summary>
	/// <returns>Указатель на массив предметов</returns>
	Subject* getSubjectListPointer();

	/// <summary>
	/// Геттер массива учеников
	/// </summary>
	/// <returns>Указатель на массив учеников</returns>
	Student* getCrewPointer();


#pragma endregion SETGET


};

