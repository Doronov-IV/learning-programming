#ifndef GROUP_BUILDER_H
#define GROUP_BUILDER_H


#include "Group.h"


/// <summary>
/// Класс для создания групп на основе прочитанного файла
/// </summary>
class GroupBuilder
{

public:

	/// <summary>
	/// Запрашивает экземпляр builder'a
	/// </summary>
	/// <returns>Экземпляр класса</returns>
	static GroupBuilder* getInstance();


	/// <summary>
	/// Запрашивает экземпляр builder'a
	/// </summary>
	/// <returns>Построенную группу</returns>
	void build(Group& group, const char* sPath);


	/// <summary>
	/// Деструктор
	/// </summary>
	virtual ~GroupBuilder();

private:


	/// <summary>
	/// Экземпляр нашего приложения
	/// </summary>
	static GroupBuilder* instance;


	/// <summary>
	/// Конструктор по умолчанию
	/// </summary>
	GroupBuilder();

};


#endif

