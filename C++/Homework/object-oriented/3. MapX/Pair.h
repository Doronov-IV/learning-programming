#ifndef PAIR_H
#define PAIR_H


/// <summary>
/// Класс "пара", для удобства. 
/// Добавлять пары в ассоциативную коллекцию можно вручную по очереди, а можно добавить парами
/// </summary>
/// <typeparam name="T">Первое значение</typeparam>
/// <typeparam name="D">Второе значение</typeparam>
template<typename T, typename D>
class Pair
{


public:


#pragma	region CONSTRUCT


	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	Pair()
	{
		firstValue = 0;
		secondValue = 0;
	}


	/// <summary>
	/// Конструктор с парой значений
	/// </summary>
	/// <param name="firstValue">Первое значение</param>
	/// <param name="secondValue">Второе значение</param>
	Pair(T firstValue, D secondValue)
	{
		this->firstValue = firstValue;
		this->secondValue = secondValue;
	}


#pragma	endregion CONSTRUCT




#pragma region ACCESS


	/// <summary>
	/// Сеттер первого
	/// </summary>
	/// <param name="first">Новое значение</param>
	void setFirst(T first)
	{
		this->firstValue = first;
	}


	/// <summary>
	/// Сеттер второго
	/// </summary>
	/// <param name="second">Новое значение</param>
	void setSecond(D second)
	{
		this->secondValue = second;
	}


	/// <summary>
	/// Присваивает значения последовательно всей структуре
	/// </summary>
	/// <param name="first">Первое значение</param>
	/// <param name="second">Второе значение</param>
	void set(T first, D second)
	{
		this->firstValue = first;
		this->secondValue = second;
	}


	/// <summary>
	/// Геттер первого значения типа T
	/// </summary>
	/// <returns>Первое значение</returns>
	T& getFirst()
	{
		return this->firstValue;
	}


	/// <summary>
	/// Геттер второго значения типа D
	/// </summary>
	/// <returns>Второе значение</returns>
	D& getSecond()
	{
		return this->secondValue;
	}


#pragma endregion ACCESS


private:


	/// <summary>
	/// Первое значение
	/// </summary>
	T firstValue;

	/// <summary>
	/// Второе значение
	/// </summary>
	D secondValue;


};


#endif