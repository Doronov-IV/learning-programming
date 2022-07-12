#ifndef STRING_X_H
#define STRING_X_H

#include <iostream>
#include <ostream>


/// <summary>
/// Класс "Строка"
/// </summary>
class StringX
{


public:



	/// <summary>
	/// Длина строки, для удобства
	/// </summary>
	int length;



#pragma region NEW_METHODS


	/// <summary>
	/// Сжимает строку, удаляя лишние пробелы
	/// </summary>
	void compress();

	/// <summary>
	/// Метод получения длины строки
	/// </summary>
	/// <returns>Длина строки</returns>
	int size();

	/// <summary>
	/// Проверяет строку на пустоту
	/// </summary>
	/// <returns>True - пустая, false - не пустая</returns>
	bool isEmpty();

	/// <summary>
	/// Очищает строку
	/// </summary>
	void clear();

	/// <summary>
	/// Добавление нового элемента в массив (Так удобнее что-то записывать)
	/// </summary>
	/// <param name="cSymbol">Новый символ</param>
	void addSymbol(const char cSymbol);

	/// <summary>
	/// Выделяет все хождения экземпляра second в экземпляре first
	/// </summary>
	/// <param name="first">Первая строка</param>
	/// <param name="second">Вторая строка</param>
	/// <returns>Первая строка в изменённом виде</returns>
	StringX embrace(StringX& first, StringX second);



#pragma endregion NEW_METHODS




#pragma region OVERLOADING_OPERATORS

	/// <summary>
	/// Сравнение двух строк. Результат: 0 - не равны, res 1 - общий размер и содержимое различается, res 10 - содержимое идентично
	/// </summary>
	/// <param name="right">Правый операнд</param>
	/// <returns>0, 1 или 10</returns>
	int operator == (StringX& right);

	/// <summary>
	/// Перегрузка операторов индексации
	/// </summary>
	char& operator[](const int& index) const;

	/// <summary>
	/// Перегрузка оператора присваивания
	/// </summary>
	/// <param name="right">Правый операнд</param>
	/// <returns>Ссылка на текущий экземпляр</returns>
	StringX& operator=(StringX& right);

	/// <summary>
	/// Перегрузка оператора присваивания (для ручного ввода)
	/// </summary>
	StringX& operator=(const char* right);

	/// <summary>
	/// Перегрузка оператора вывода в консоль
	/// </summary>
	friend std::ostream& operator << (std::ostream& outputStream, StringX& right);

	/// <summary>
	/// Перегрузка метода "merge" a.k.a. "Concatenation"
	/// </summary>
	/// <param name="right">Правый операнд</param>
	void operator + (StringX& right);

	/// <summary>
	/// Перегрузка метода "split"
	/// </summary>
	/// <param name="right">Правый операнд</param>
	/// <returns>Указатель на массив подстрок</returns>
	StringX* operator / (const char& right);

	/// <summary>
	/// Перегрузка метода "contains"
	/// </summary>
	/// <param name="right">Правый операнд</param>
	/// <returns>Индекс начала подстроки или -1 в случае неудачи</returns>
	int operator % (const char* right);


#pragma endregion OVERLOADING_OPERATORS




#pragma region LOGIC



	/// <summary>
	/// Вставка подстроки в строку
	/// </summary>
	/// <param name="sInsertedString">Вставляемая строка</param>
	/// <param name="nPosition">Позиция</param>
	void insert(const char* sInsertedString, int nPosition);

	/// <summary>
	/// Поиск подстроки в строке
	/// </summary>
	/// <param name="sSearchedString">Подстрока, которую мы ищем</param>
	/// <returns>Индекс первого символа, или минус единица в случае неудачи</returns>
	int contains(const char* sSearchedString);

	/// <summary>
	/// Конкатенация
	/// </summary>
	/// <param name="secondString">Вторая строка</param>
	void merge(StringX secondString);

	/// <summary>
	/// Разделение строк на подстроки
	/// </summary>
	/// <param name="separator">Разделяющий символ</param>
	/// <returns>Массив подстрок</returns>
	StringX* split(const char separator);

	/// <summary>
	/// Вставка подстроки в строку
	/// </summary>
	/// <param name="sInsertedString">Вставляемая строка</param>
	/// <param name="nPosition">Позиция</param>
	void insert(StringX sInsertedString, int nPosition);


#pragma endregion LOGIC




#pragma region CONSTRUCT


	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	StringX();

	/// <summary>
	/// Конструктор с параметром массива чаров
	/// </summary>
	/// <param name="Array">Массив чаров</param>
	StringX(const char* Array);

	/// <summary>
	/// Конструктор копирования
	/// </summary>
	/// <param name="copyString">Другой экземпляр класса</param>
	StringX(StringX& copyString);

	/// Деструктор
	virtual ~StringX();


#pragma endregion CONSTRUCT




#pragma region ACCESS


	/// <summary>
	/// Геттер указателя на массив чаров
	/// </summary>
	/// <returns>Указатель на массив чаров</returns>
	const char* getArrayPtr();

	/// <summary>
	/// Геттер размера массива чаров 
	/// </summary>
	/// <returns>Длина массива</returns>
	int getLength();

	/// <summary>
	/// Сеттер массива чаров (Повторяет конструктор с аргументом)
	/// </summary>
	/// <param name="newString">Новая строка</param>
	void setString(const char* newString);


#pragma endregion ACCESS



private:


	/// <summary>
	/// Массив символов
	/// </summary>
	char* ptrArray;


};


#endif