#ifndef APP_H
#define APP_H


#include "StringEx.h"


/// <summary>
/// Класс-приложение
/// Программа выполняется в методе Run()
/// </summary>
class App
{


public:


	/// <summary>
	/// Запрашивает экземпляр приложения
	/// </summary>
	/// <returns></returns>
	static App* getInstance();


	/// <summary>
	/// Основной метод, запускающий приложение
	/// </summary>
	void run();

	
private:


	/// <summary>
	/// Экземпляр нашего приложения
	/// </summary>
	static App* instance;


	/// <summary>
	/// Демо первого задания про удаление лишних пробелов в строке
	/// </summary>
	void TaskOne();


	/// <summary>
	/// Демо второго задания про удаление комментов в тексте
	/// </summary>
	void TaskTwo();


	/// <summary>
	/// Демо третьего задания про поиск подстроки с её выделением
	/// </summary>
	void TaskThree();


	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	App();


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


#pragma endregion MENU_DRAWING


};


#endif

