#ifndef APP_H
#define APP_H

#include "../Misc/Toolbox.h"
#include "../FileBufferedReader/FileBufferedReader.h"
#include "../Printer/PrinterSimulator.h"




// Класс "Приложение"
// Программа выполняется в методе Run()
class App
{


public:


	//Запрашивает экземпляр приложения
	static App* getInstance();


	// Основной метод, запускающий приложение
	void run();

	
private:


	// Экземпляр нашего приложения
	static App* instance;


	// Демо первого задания про удаление лишних пробелов в строке
	void TaskOne();


	// Демо второго задания про удаление комментов в тексте
	void TaskTwo();


	// Демо третьего задания про поиск подстроки с её выделением
	void TaskThree();


	// Конструктор по умолчанию
	App();

	// Деструктор
	virtual ~App();


#pragma region MENU_DRAWING


	// Рисует меню действий
	void drawMenuStart();


#pragma endregion MENU_DRAWING


};


#endif

