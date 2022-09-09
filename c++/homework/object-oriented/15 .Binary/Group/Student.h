#ifndef STUDENT_H
#define STUDENT_H
#endif

#include "Subject.h"
#include "../FileBufferedReader/FileBufferedReader.h"

#include <string>
#include <ctime>


/// <summary>
/// Класс "Студент" ("ученик")
/// Каждый студент имеет массив предметов
/// Каждый студент имеет двумерный массив оценок 
/// 
/// Список предметов можно назначить каждому студенту индивидуально, а мжно объединить студентов в группу, 
/// и затем назначить группе общий перечень дисциплин
/// </summary>
class Student
{

#pragma region PRIVATE_VARIABLES

	/// <summary>
	/// Имя студента
	/// </summary>
	std::string sName;

	/// <summary>
	/// Массив предметов студента
	/// </summary>
	Subject* ptrSubjects;

	/// <summary>
	/// Двумерный массив оценок
	/// </summary>
	int** ptrGrades;

	/// <summary>
	/// Счётчик предметов
	/// Каждый студент имеет массив предметов
	/// </summary>
	int nSubjectCounter;

	/// <summary>
	/// Счётчик оценок по каждому предмету
	/// </summary>
	int* ptrGradeCounters;

#pragma endregion PRIVATE_VARIABLES


public:


	/// <summary>
	/// Задаёт студенту случайный набор предметов
	/// </summary>
	void setRandomSubjectList();

	/// <summary>
	/// Случайный набор оценок для текущего набора предметов
	/// </summary>
	void setRandomGrades();

	/// <summary>
	/// Добавляет предмет студенту
	/// </summary>
	void addSubject(std::string sSubjectName);

	/// <summary>
	/// Удаляет предмет ученика
	/// </summary>
	/// <param name="sName">Имя предмета</param>
	/// <returns>true - успех, false - неудача</returns>
	bool deleteSubject(std::string sName);

	/// <summary>
	/// Обнуляет список предметов
	/// </summary>
	void deleteAllSubjects();


#pragma region CONSTRUCT_DESTRUCT_COPY

	/// <summary>
	/// Пустой конструктор
	/// </summary>
	Student();

	/// <summary>
	/// Деструктор
	/// </summary>
	~Student();

	/// <summary>
	///Конструктор имени
	/// </summary>
	/// <param name="sName">Имя студента</param>
	Student(std::string sName);


#pragma endregion CONSTRUCT_DESTRUCT_COPY


#pragma region SETGET


	/// <summary>
	/// Геттер массива оценок
	/// </summary>
	/// <returns>Указатель на массив оценок</returns>
	int** getGrades();

	/// <summary>
	/// Сеттер для счётчика предметов
	/// </summary>
	/// <param name="nValue">Новое значение</param>
	void setSubjectCounter(int nValue);

	/// <summary>
	/// Сеттер поля "Имя"
	/// </summary>
	/// <param name="sName">Имя студента</param>
	void setName(std::string sName);

	/// <summary>
	/// Геттер поля "Имя"
	/// </summary>
	/// <returns>Имя студента</returns>
	std::string getName();

	/// <summary>
	/// Геттер для указателя на массив предметов студента
	/// </summary>
	/// <returns>Указатель на массив предметов</returns>
	Subject* getSubjectsPointer();

	/// <summary>
	/// Кол-во оценок по каждому предмету
	/// </summary>
	/// <returns>Указатель на массив кол-ва оценок</returns>
	int* getGradeCounters();

	/// <summary>
	/// Геттер для счётчика предметов для студента
	/// </summary>
	/// <returns>Счётчик предметов</returns>
	int getSubjectCounter();

	/// <summary>
	/// Геттер массива оценок по определённому предмету
	/// </summary>
	/// <param name="sSubjectName">Имя искомого предмета</param>
	/// <returns>Массив оценок по предмету</returns>
	int* getGradesCounterForSpecificSubject(std::string sSubjectName);


#pragma endregion SETGET


};