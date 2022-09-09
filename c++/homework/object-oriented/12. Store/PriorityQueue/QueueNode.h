#ifndef QUEUE_NODE_H
#define QUEUE_NODE_H


// Класс "Нода" для очереди с приоритетом;
template <typename T>
class QueueNode
{


public:

	// Значение ноды;
	T* value;

	// Приоритет ноды;
	size_t priority;

	// Конструктор по умолчанию;
	QueueNode();

	// Деструктор;
	virtual ~QueueNode();
	
	// Конструктор с приоритетом;
	// "nPriority" — значение приоритета;
	QueueNode(int nPriority);

	// Конструктор со значением и с приоритетом;
	// "value" — значение ноды;
	// "nPriority" — приоритет ноды (в очереди);
	explicit QueueNode(T* value, int nPriority);

	// Конструктор копирования;
	// "copyNode" — копируемый экземпляр;
	QueueNode(QueueNode& copyNode);

	// Перегрузка оператора "=";
	// "right" — правый операнд;
	QueueNode& operator=(QueueNode& right);

};

template <typename T>
QueueNode<T>::QueueNode()
{
	value = nullptr;
	priority = 0;
}

template<typename T>
QueueNode<T>::~QueueNode()
{
}


template <typename T>
QueueNode<T>::QueueNode(int nPriority)
{
	value = nullptr;
	priority = nPriority;
}


template <typename T>
QueueNode<T>::QueueNode(T* value, int nPriority)
{
	this->value = value;
	priority = nPriority;
}


template <typename T>
QueueNode<T>::QueueNode(QueueNode& copyNode)
{
	this->value = copyNode.value;
	this->priority = copyNode.priority;
}


template <typename T>
QueueNode<T>& QueueNode<T>::operator=(QueueNode& right)
{
	if (nullptr != right.value)
	{
		this->value = right.value;

		this->priority = right.priority;
	}

	return *this;
}


#endif