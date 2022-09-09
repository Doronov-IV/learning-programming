#ifndef MAPX_H
#define MAPX_H


#include <iostream>
#include "Pair.h"



#define COLLECTION_SIZE 10 // Размер коллекции



/// <summary>
/// Ассоциативный массив
/// Первое значение - "ключ". Второе - "значение"
/// @see "Pair.h"
/// </summary>
template<typename T, typename D>
class MapX
{


public:


#pragma region CONSTRUCT


	/// <summary>
	/// Конструктор
	/// </summary>
	MapX()
	{
		currentSize = 0;

		for (size_t i = 0ULL; i < COLLECTION_SIZE; i++)
		{
			ptrPairs[i].set(0, 0);
		}
	}


	/// <summary>
	/// Конструктор копирования
	/// </summary>
	/// <param name="copy"></param>
	MapX(MapX& copy)
	{
		this->currentSize = copy.currentSize;

		for (size_t i = 0ULL; i < COLLECTION_SIZE; i++)
		{
			put(copy.ptrPairs[i]);
		}
	}


#pragma endregion CONSTRUCT




#pragma region OPERATORS


	/// <summary>
	/// Перегрузка оператора присваивания
	/// </summary>
	/// <param name="right">Правый операнд</param>
	/// <returns>Левый операнд</returns>
	MapX& operator=(MapX& right)
	{
		this->currentSize = right.currentSize;

		for (size_t i = 0ULL; i < COLLECTION_SIZE; i++)
		{
			ptrPairs[i].set(right.ptrPairs[i].getFirst(), right.ptrPairs[i].getSecond());
		}

		return *this;
	}


	/// <summary>
	/// Перегрузка оператора индексации
	/// </summary>
	/// <param name="index">Порядковый номер элемента</param>
	/// <returns>Ключ по номеру</returns>
	T& operator[](const int& index)
	{	
		return ptrPairs[index].getFirst();
	}


#pragma endregion OPERATORS





#pragma region ACCESS


	/// <summary>
	/// Геттер кол-ва ненулевых элементов
	/// </summary>
	/// <returns>Текущий размер коллекции</returns>
	size_t getSize()
	{
		return this->currentSize;
	}


	/// <summary>
	/// Геттер для значения
	/// </summary>
	/// <param name="key">Ключ значения</param>
	/// <returns>Значение</returns>
	D& getValue(T key)
	{
		for (size_t i = 0; i < currentSize; i++)
		{
			if (key == ptrPairs[i].getFirst()) return ptrPairs[i].getSecond();
		}
		return -1;
	}


	/// <summary>
	/// Геттер для ключа
	/// </summary>
	/// <param name="key">Значение ключа</param>
	/// <returns>Ключ</returns>
	T& getKey(D value)
	{
		for (size_t i = 0; i < currentSize; i++)
		{
			if (value == ptrPairs[i].getSecond()) return ptrPairs[i].getFirst();
		}
		return -1;
	}


#pragma endregion ACCESS


	/// <summary>
	/// Добавление пары в коллекцию
	/// </summary>
	/// <param name="couple">Те же значения, собранные в пару</param>
	void put(Pair<T,D> couple)
	{
		if (currentSize < COLLECTION_SIZE)
		{
			ptrPairs[currentSize].set(couple.getFirst(), couple.getSecond());

			currentSize += 1;
		}
		else return;
	}


	/// <summary>
	/// Добавление пары в коллекцию
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <param name="value">Значение</param>
	void put(T key, D value)
	{
		if (currentSize < COLLECTION_SIZE)
		{
			Pair<T,D> couple(key, value);

			put(couple);
		}
		else return;
	}


	/// <summary>
	/// Вывод ненулевых элементов в консоль
	/// </summary>
	/// <param name="outstream">Поток вывода</param>
	void forEach(std::ostream& outstream)
	{
		for (size_t i = 0ULL; i < currentSize; i++)
		{
			outstream << "[" << ptrPairs[i].getFirst() << " - " << ptrPairs[i].getSecond() << "]\n";
		}
	}


	/// <summary>
	/// Проверка наличия ключа в коллекции
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <returns>1 -- true, 2 -- false</returns>
	int containsKey(const T& key) 
	{
		for (size_t i = 0ULL; i < currentSize; i++)
		{
			if (key == ptrPairs[i].getFirst()) return 1;
		}
		return 0;
	}


	/// <summary>
	/// Поиск ключа коллекции
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <returns>Порядковый номер</returns>
	int findKey(const T& key) 
	{
		int position = 0;
		for (size_t i = 0ULL; i < currentSize; i++) 
		{
			if (key == ptrPairs[i].getFirst()) position = i;
		}
		return position;
	}


	/// <summary>
	/// Изменение значения по ключу
	/// </summary>
	/// <param name="key">Ключ</param>
	/// <param name="newValue">Новое значение элемента</param>
	void renew(const T& key, const D& newValue) 
	{
		if (!containsKey(key)) return;
		else 
		{
			ptrPairs[findKey(key)].setSecond(newValue);
		}
	}


private:


	/// <summary>
	/// Текущий размер коллекции (кол-во занятых ячеек)
	/// </summary>
	size_t currentSize;


	/// <summary>
	/// Массив пар "ключ-значение"
	/// </summary>
	Pair<T,D> ptrPairs[10];


};


#endif

