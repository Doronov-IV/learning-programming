#ifndef COORD_H
#define COORD_H


// Класс "Координаты" для управления консолью;
class Coord
{


public:


#pragma region CONSTRUCTION

	// Конструктор по умолчанию;
	Coord();

	// Конструктор с координатами;
	Coord(int x, int y);

	// Конструктор копирования;
	// "anotherCoord" — копиируемый объект;
	Coord(Coord& anotherCoord);

	// Деструктор (Деконструктор?);
	virtual ~Coord();

	// Перегрузка оператора "=";
	// "anotherCoord" — копиируемый объект;
	// returns — ссылка на левый операнд;
	Coord& operator=(Coord& anotherCoord);


#pragma endregion CONSTRUCTION




#pragma region ACCESS

	// Сеттер координат;
	void set(int x, int y);

	// Геттер x-координаты;
	int getX();

	// Геттер y-координаты;
	int getY();


#pragma endregion ACCESS


private:

	// x-координата;
	int x;

	// y-координата;
	int y;


};


#endif

