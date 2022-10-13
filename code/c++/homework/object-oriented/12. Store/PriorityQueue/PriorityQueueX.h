#ifndef PRIORITY_QUEUE_X
#define PRIORITY_QUEUE_X

#include <iostream>
#include "../PriorityQueue/QueueNode.h"



#pragma region PREPROCESSING


// В моей реализации очереди, наиболее приоритетные ноды имеют
// наименьший показатель приоритета;
#ifndef PRIORITY_LESS_THEN
#define PRIORITY_LESS_THEN >
#endif

// В моей реализации очереди, наиболее приоритетные ноды имеют
// наименьший показатель приоритета;
#ifndef PRIORITY_MORE_THEN
#define PRIORITY_MORE_THEN <
#endif

#ifndef NEW_ELEMENT
#define NEW_ELEMENT 1
#endif

// Означает, что алгоритм предполагает заглядывание в следующий элемент
#ifndef NEXT_ELEMENT
#define NEXT_ELEMENT 1
#endif

// Элемент первый, а индекс у него нулевой
#ifndef ITERATION_ISSUE
#define ITERATION_ISSUE 1
#endif


#pragma endregion PREPROCESSING



// Шаблонный класс Списка-очереди-с-приоритетом
template <typename T>
class PriorityQueueX
{

public:

	// Конструктор по умолчанию; 
	PriorityQueueX();

	// Деструктор; 
	virtual ~PriorityQueueX();

	// Добавить элемент в конец списка;
	// "value" — значение;
	// "nPriority" — приоритет;
	bool enqueue(T* value, size_t nPriority);

	// Изъять элемент из очереди (с удалением);
	// returns — указатель на изъятый элемент;
	T* dequeue();

	// Получить ссылку на первый элемент очереди (без удаления);
	// returns — указатель на изъятый элемент;
	T* peek();

	// Получить ссылку на произвольный элемент очереди (без удаления);
	// returns — указатель на изъятый элемент;
	T* peek(size_t nIndex);

	// Проверка на пустоту списка;
	bool isEmpty() const;

	// Напечатать список;
	// "outstream" — поток вывода;
	void forEach(std::ostream& outstream) const;

	// Узнать текущий размер очереди;
	// returns — текущий размер массива;
	size_t size() const;


private:

	// Сортирует ноды по приоритету;
	void sort();

	// Выписывает элемент из массива
	// "index" — номер выписываемого элемента
	void unsignElement(size_t index);

	// Указатель на массив элементов
	QueueNode<T>* ptrQueue;

	// Указатель на начало списка; (Рудимент, голова всегда в нулевом элементе)
	T* head;

	// Указатель на конец списка; (Рудимент, хвост всегда в size()-1 элементе)
	T* tail;

	// Максимальный приоритет в очереди
	size_t nMaxPriority;

	// Минимальынй приоритет в очереди
	size_t nMinPriority;

	// Текущий размер массива
	size_t pvtSize;


};


template<typename T>
PriorityQueueX<T>::PriorityQueueX()
{
	ptrQueue = new QueueNode<T>[0];
	head = nullptr;
	tail = nullptr;
	nMaxPriority = 0;
	nMinPriority = INT_MAX;
	pvtSize = 0;
}

template<typename T>
PriorityQueueX<T>::~PriorityQueueX()
{
	if (nullptr != head) delete head;
	if (nullptr != tail) delete tail;
	if (nullptr != ptrQueue) delete[] ptrQueue;
}


template<typename T>
bool PriorityQueueX<T>::enqueue(T* value, size_t nPriority)
{
	bool res = false;
	bool bSuccessAddingFlag = false;

	pvtSize += NEW_ELEMENT;

	QueueNode<T>* newElement = new QueueNode<T>(value, nPriority);

	QueueNode<T>* buf = new QueueNode<T>[pvtSize];

	pvtSize -= NEW_ELEMENT;

	if (isEmpty())
	{
		buf[0] = *newElement;

		ptrQueue = new QueueNode<T>[1];

		ptrQueue[0] = buf[0]; // precaution

		bSuccessAddingFlag = true;
	}
	else
	{
		for (size_t i = 0ULL; i < pvtSize; i++)
		{
			buf[i] = ptrQueue[i];
		}

		buf[pvtSize] = *newElement;

		bSuccessAddingFlag = true;
	}

	pvtSize += NEW_ELEMENT;

	delete[] ptrQueue; // here +
	ptrQueue = buf;


	sort();

	if (bSuccessAddingFlag) res = true;

	return res;

}


template<typename T>
T* PriorityQueueX<T>::dequeue()
{
	if (!isEmpty())
	{
		QueueNode<T> result(ptrQueue[0]);

		unsignElement(0);

		return result.value;
	}
	else return nullptr;
}


template<typename T>
T* PriorityQueueX<T>::peek()
{
	if (!isEmpty())
	{
		QueueNode<T> result(ptrQueue[0]);

		return result.value;
	}
	else return nullptr;
}

template<typename T>
T* PriorityQueueX<T>::peek(size_t nIndex)
{
	if (!isEmpty())
	{
		if (pvtSize > nIndex)
		{
			QueueNode<T> result(ptrQueue[nIndex]);

			return result.value;
		}
		else return nullptr;
	}
	else return nullptr;
}

template<typename T>
bool PriorityQueueX<T>::isEmpty() const
{
	bool res = false;
	(0 == pvtSize) ? res =  true : res = false;
	return res;
}

template<typename T>
void PriorityQueueX<T>::forEach(std::ostream& outstream) const
{
	for (size_t i = 0ULL; i < size(); i++)
	{
		outstream << "[" << *ptrQueue[i].value << " - " << ptrQueue[i].priority << "]" << "\n";
	}
}


template<typename T>
size_t PriorityQueueX<T>::size() const
{
	return pvtSize;
}


template<typename T>
void PriorityQueueX<T>::sort()
{
	bool bIsSorted;
	for (size_t i = 0ULL; i < pvtSize - NEXT_ELEMENT; i++)
	{
		bIsSorted = true;
		for (size_t j = 0ULL; j < pvtSize - NEXT_ELEMENT; j++)
		{
			if (ptrQueue[i].priority PRIORITY_LESS_THEN ptrQueue[i + NEXT_ELEMENT].priority)
			{
				// swap begin
				QueueNode<T>* thirdValue = new QueueNode<T>(ptrQueue[i + NEXT_ELEMENT]);
				ptrQueue[i + NEXT_ELEMENT] = ptrQueue[i];
				ptrQueue[i] = *thirdValue;
				// swap end

				if (i > 1) i -= 2;
				else if (i > 0) i -= 1;
				else continue;

				bIsSorted = false;
			}
			if (bIsSorted) break;
		}
	}
}


template<typename T>
void PriorityQueueX<T>::unsignElement(size_t index)
{
	if (!isEmpty())
	{
		pvtSize -= NEW_ELEMENT;
		QueueNode<T>* buf = new QueueNode<T>[pvtSize];
		pvtSize += NEW_ELEMENT;

		for (size_t i = 0ULL, j = 0ULL; i < pvtSize; i++)
		{
			if (index != i)
			{
				buf[j] = ptrQueue[i];
				j += 1;
			}
			else continue;
		}

		delete[] ptrQueue;
		ptrQueue = buf;

		pvtSize -= NEW_ELEMENT;
	}
	else return;
}



#endif