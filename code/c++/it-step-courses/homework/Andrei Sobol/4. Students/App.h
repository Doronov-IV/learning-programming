#ifndef APP_H
#define APP_H
#endif

#include "Group.h"

#include <iostream>
#include <conio.h>
#include <string>
#include <iomanip>



/// <summary>
/// Основной класс, содержащий пользовательское меню
/// </summary>
class App
{
	
public:

	/// <summary>
	/// Основной метод, который запускает приложение
	/// </summary>
	void run();

	/// <summary>
	/// Вспомогательный метод для удобства
	/// </summary>
	void showContinue();

	/// <summary>
	/// Рисует меню действий
	/// </summary>
	void drawMenuStart();

	/// <summary>
	/// Необходим для диалога с пользователем
	/// </summary>
	/// <param name="nOptionsQuantity"></param>
	/// <returns></returns>
	int selector(int nOptionsQuantity);

	/// <summary>
	/// Выводит на экран массив студентов и их оценки
	/// </summary>
	/// <param name="demo">Группа студентов</param>
	void showGrades(Group& demo);

	/// <summary>
	/// Выводит на экран показатель средней оценки для всей группы
	/// </summary>
	/// <param name="demo">Группа студентов</param>
	void showGroupAvg(Group& demo);

	/// <summary>
	/// Выводит на экран показатель средней оценки каждого студента
	/// </summary>
	/// <param name="demo">Группа студентов</param>
	void showAvg(Group& demo);

	/// <summary>
	/// Выводит на экран показатель средней оценки каждого студента по предметам
	/// </summary>
	/// <param name="demo">Группа студентов</param>
	void showAvgForSubjects(Group& demo);

	/// <summary>
	/// Выводит на экран показатель минимума и максимума по группе для каждого предмета
	/// </summary>
	/// <param name="demo">Группа студентов</param>
	void showMinMax(Group& demo);


};

