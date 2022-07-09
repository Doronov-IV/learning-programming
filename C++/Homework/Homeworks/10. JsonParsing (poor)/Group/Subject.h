#ifndef SUBJECT_H
#define SUBJECT_H


#include <string>

/// <summary>
/// Класс "Предмет"
/// </summary>
class Subject
{


public:


#pragma region CONSTRUCT


	/// <summary>
	/// Конструктор имени
	/// <param name="sName">Имя предмета</param>
	/// </summary>
	Subject(std::string sName);

	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	Subject();


#pragma endregion CONSTRUCT


#pragma region ACCESS


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


#pragma endregion ACCESS


	/// <summary>
	/// Имя "Предмета"
	/// </summary>
	std::string sName;


};


#endif