#include <iostream>
#include <Windows.h>
#include <ctime>
#include <thread>
using namespace std;
HANDLE out;
COORD pos;
void setCursor(int x, int y)
{
	HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
	COORD coord;
	coord.X = x;
	coord.Y = y;
	SetConsoleCursorPosition(hConsole, coord);
}
void draw_tree(int h)
{
	int color = rand() % 15 + 1;
	for (int i = 0; i <= h; i++)
	{
		SetConsoleTextAttribute(out, 0);
		for (int j = 0; j < h - i; j++)
		{
			cout << " ";
		}
		if (i % 3 == 0)
		{
			for (int j = i * 2 - 1; j > 0; j--)
			{
				if (j % 2 == 0)
				{
					SetConsoleTextAttribute(out, color * 16);
				}
				else
				{
					SetConsoleTextAttribute(out, 160);
				}
				cout << " ";
			}
		}
		else
		{
			SetConsoleTextAttribute(out, 160);
			for (int j = i * 2 - 1; j > 0; j--)
			{
				cout << " ";
			}
		}
		cout << "\n";

	}
	for (int j = 21; j < 24; j++)
	{
		for (int i = 17; i < 22; i++)
		{
			SetConsoleTextAttribute(out, 100);
			setCursor(i, j);
			cout << " ";
		}
	}
}
void en_draw_text()
{
	int color = rand() % 15 + 1;
	SetConsoleTextAttribute(out, color);
	setCursor(12, 25);
	cout << "Hapy New Year!";
}

int main()
{
	out = GetStdHandle(STD_OUTPUT_HANDLE);
	srand(time(0));
	int h = 20;

	system("CLS");
	setlocale(LC_ALL, "Russian");
	while (true)
	{
		pos.X = 0; pos.Y = 0;
		SetConsoleCursorPosition(out, pos);
		draw_tree(h);
		cout << endl;
		pos.X = (h - 8); pos.Y = h + 2;
		SetConsoleCursorPosition(out, pos);
		en_draw_text();

		this_thread::sleep_for(500ms);
	}
}