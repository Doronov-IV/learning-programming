#ifndef APP_H
#define APP_H
#endif

#include "StringEx.h"

/// <summary>
/// Класс-приложение
/// Программа выполняется в методе Run()
/// </summary>
class App
{


public:

	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	App();

	/// <summary>
	/// Основной метод, запускающий приложение
	/// </summary>
	void run();


#pragma region MENU_DRAWING


	/// <summary>
	/// Рисует меню действий
	/// </summary>
	void drawMenuStart();

	/// <summary>
	/// Проверяет ввод и возвращает номер действия, выбранного пользователем
	/// </summary>
	/// <param name="nOptionsQuantity">Кол-во действий, для проверки</param>
	/// <returns>Выбранный пункт меню</returns>
	int selector(int nOptionsQuantity);

	/// <summary>
	/// Предлагает продолжить выполнение программы
	/// </summary>
	void showContinue();

	/// <summary>
	/// Демонстрирует конкатенацию
	/// </summary>
	/// <param name="demo_one">Первая строка</param>
	/// <param name="demo_two">Вторая строка</param>
	/// <param name="demo_three">Третья строка</param>
	void drawConcatenation(StringEx& demo_one, StringEx& demo_two, StringEx& demo_three);

	/// <summary>
	/// Демонстрирует метод Split
	/// </summary>
	/// <param name="demo_one">Первая строка</param>
	/// <param name="demo_two">Вторая строка</param>
	/// <param name="demo_three">Третья строка</param>
	void drawSplit(StringEx& demo_one, StringEx& demo_two, StringEx& demo_three);

	/// <summary>
	/// Демонстрирует метод Insert
	/// </summary>
	/// <param name="demo_one">Первая строка</param>
	void drawInsert(StringEx& demo_one);


	/// <summary>
	/// Демонстрирует метод Contains
	/// </summary>
	/// <param name="demo_one">Первая строка</param>
	/// <param name="demo_two">Вторая строка</param>
	void drawContains(StringEx& demo_one, StringEx& demo_two);

	
#pragma endregion MENU_DRAWING


};

