#ifndef STRING_EX_H
#define STRING_EX_H
#endif

/// <summary>
/// Класс "Строка"
/// </summary>
class StringEx
{


public:


#pragma region OVERLOADING


	/// <summary>
	/// Перегрузка метода "merge" a.k.a. "Concatenation"
	/// </summary>
	/// <param name="right">Правый операнд</param>
	void operator + (StringEx& right);

	/// <summary>
	/// Перегрузка метода "split"
	/// </summary>
	/// <param name="right">Правый операнд</param>
	/// <returns>Указатель на массив подстрок</returns>
	StringEx* operator / (const char& right);

	/// <summary>
	/// Перегрузка метода "contains"
	/// </summary>
	/// <param name="right">Правый операнд</param>
	/// <returns>Индекс начала подстроки или -1 в случае неудачи</returns>
	int operator % (const char* right);




#pragma endregion OVERLOADING


#pragma region LOGIC



	/// <summary>
	/// Добавление нового элемента в массив (Так удобнее что-то записывать)
	/// </summary>
	/// <param name="cSymbol">Новый символ</param>
	void addSymbol(char cSymbol);

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
	void merge(StringEx& secondString);

	/// <summary>
	/// Разделение строк на подстроки
	/// </summary>
	/// <param name="separator">Разделяющий символ</param>
	/// <returns>Массив подстрок</returns>
	StringEx* split(char separator);

	/// <summary>
	/// Вставка подстроки в строку
	/// </summary>
	/// <param name="sInsertedString">Вставляемая строка</param>
	/// <param name="nPosition">Позиция</param>
	void insert(StringEx sInsertedString, int nPosition);


#pragma endregion LOGIC




#pragma region CONSTRUCT_DESTRUCT_COPY


	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	StringEx();

	/// <summary>
	/// Конструктор с параметром массива чаров
	/// </summary>
	/// <param name="Array">Массив чаров</param>
	StringEx(const char* Array);

	/// <summary>
	/// Конструктор копирования
	/// </summary>
	/// <param name="copyString">Другой экземпляр класса</param>
	StringEx(StringEx& copyString);

	/// Деструктор
	virtual ~StringEx();


#pragma endregion CONSTRUCT_DESTRUCT_COPY




#pragma region SETGET


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


#pragma endregion SETGET



private:


	/// <summary>
	/// Массив символов
	/// </summary>
	char* ptrArray;
	
	/// <summary>
	/// Длина строки, для удобства
	/// </summary>
	int nLength;


};

