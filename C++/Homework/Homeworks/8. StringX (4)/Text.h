#ifndef TEXT_H
#define TEXT_H

#include <iostream>
#include "Colors.h"
#include "StringEx.h"


class Text
{


public:




#pragma region PROCESSING


	/// <summary>
	/// Вывод массива в поток (Если это текст программы, то комментарии выделяются зелёным цветом)
	/// </summary>
	/// <param name="outstream">Поток вывода</param>
	void forEach(std::ostream& outstream);


	/// <summary>
	/// Удаляет комментарии
	/// </summary>
	void cutComments();


	/// <summary>
	/// Чистит пустые строки в начале и в конце 
	/// </summary>
	void trim();


#pragma endregion PROCESSING




#pragma region CONSTRUCTION


	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	Text();


	/// <summary>
	/// Деструктор
	/// </summary>
	virtual ~Text();


	/// <summary>
	/// Конструктор копирования
	/// </summary>
	/// <param name="anotherText"></param>
	Text(Text& anotherText);


	/// <summary>
	/// Коструктор с массивом чаров
	/// </summary>
	/// <param name="someText"></param>
	/// <param name="size">Длина текста</param>
	Text(char** someText, int size);


	/// <summary>
	/// Конструктор копирования
	/// </summary>
	/// <param name="anotherText"></param>
	Text(Text& anotherText, StringEx discription);


	/// <summary>
	/// Коструктор с массивом чаров
	/// </summary>
	/// <param name="someText"></param>
	/// <param name="size">Длина текста</param>
	Text(char** someText, int size, StringEx discription);


#pragma endregion CONSTRUCTION





#pragma region ACCESS


	/// <summary>
	/// Геттер текста
	/// </summary>
	/// <returns>Указатель на массив чаров</returns>
	char** getTextPtr();


	/// <summary>
	/// Геттер кол-ва строк
	/// </summary>
	/// <returns>Кол-во строк</returns>
	int getLinesQuantity();


	/// <summary>
	/// Задаёт текст
	/// </summary>
	/// <param name="newText">Указатель на двумерный массив чаров</param>
	/// <param name="size">Длина текста</param>
	void setText(char** newText, int size);


	/// <summary>
	/// Задаёт текст
	/// </summary>
	/// <param name="newText">Массив строк</param>
	/// <param name="size">Длина текста</param>
	void setText(StringEx* newText, int size);


	/// <summary>
	/// Геттер описания
	/// </summary>
	/// <returns>Описание строки</returns>
	StringEx getDiscription();


	/// <summary>
	/// Сеттер описания текста
	/// </summary>
	/// <param name="sDiscription">Новое описание текста</param>
	void setDiscription(StringEx sDiscription);




#pragma endregion ACCESS




private:


	/// <summary>
	/// Указатель на массив чаров
	/// </summary>
	char** ptrText;

	
	/// <summary>
	/// Кол-во строк
	/// </summary>
	int nLength;


	/// <summary>
	/// Описание нашего текста, его предназначение или суть
	/// </summary>
	StringEx sDiscription;
	

};


#endif

