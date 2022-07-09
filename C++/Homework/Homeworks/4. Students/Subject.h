#ifndef SUBJECT_H
#define SUBJECT_H
#endif

#include <string>

/// <summary>
/// Класс "Предмет"
/// </summary>
class Subject
{

#pragma region PRIVATE_VARIABLES


	/// <summary>
	/// Имя "Предмета"
	/// </summary>
	std::string sName;


#pragma endregion PRIVATE_VARIABLES


public:


#pragma region CONSTRUCT


	/// <summary>
	/// Конструктор имени
	/// <param name="sName">Имя предмета</param>
	/// </summary>
	Subject(std::string sName);

	/// <summary>
	/// Пустой конструктор
	/// </summary>
	Subject();


#pragma endregion CONSTRUCT


#pragma region SETGET


	/// <summary>
	/// Сеттер для поля "Имя"
	/// <param name="sName">Новое имя предмета</>
	/// </summary>
	void setName(std::string sName);

	/// <summary>
	/// Геттер для поля "Имя"
	/// <returns>Имя предмета</returns>
	/// </summary>
	std::string getName();


#pragma endregion SETGET


};