#ifndef CONSOLE_MANAGER_H
#define CONSOLE_MANAGER_H


#include "Coord.h"
#include "../Misc/Toolbox.h"
#include <iostream>
#include <string>


using namespace Toolbox;
 

// Класс для управления консолью;
class ConsoleManager
{

public:

	// Перемещение курсора в заданную позицию;
	// Скопирован с "Toolbox.h";
	// "x_coord" — x-координата;
	// "y_coord" — y-координата;
	void setCursor(size_t x_coord, size_t y_coord);

	// Вывод текста определённого цвета;
	// "sColor" — цвет текста;
	// "sText" — само текстовое сообщение;
	void printColoredText(std::string sColor, std::string sText);

	// Очистка консоли;
	void clear();

	// Геттер координат курсора;
	// returns — текущие коорднаты курсора;
	Coord getCursorPosition();

	// Геттер текущего цвета текста;
	// returns — цвет текста;
	std::string getCurrentTextColor();



	//Запрашивает экземпляр manager'a;
	static ConsoleManager* getInstance();

	// Очищает "instance";
	void release();



private:

	// Экземпляр manager'a;
	static ConsoleManager* instance;

	// Конструктор по умолчанию
	ConsoleManager();

	// Деструктор
	virtual ~ConsoleManager();

	// Отмена копирования;
	ConsoleManager(ConsoleManager& right) = delete;

	// Отмена присваивания;
	ConsoleManager& operator=(ConsoleManager& right) = delete;

	// Текущие координаты;
	Coord сurrentCoords;

	// Текущий цвет текста;
	std::string sTextColor;


};


#endif