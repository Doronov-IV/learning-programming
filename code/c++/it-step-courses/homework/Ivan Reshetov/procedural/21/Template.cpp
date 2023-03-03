
/*
    21.11.2021
    
    Задание 1. Дополните свою библиотеку функций реализациями нижеследующих функций:
        ■ int mystrcmp (const char * str1, const char * str2); —
        функция сравнивает две строки, и , если строки равны
        возвращает 0, если первая строка больше второй, то
        возвращает 1, иначе –1.
        ■ int StringToNumber(char * str); — функция конвертирует строку в число и возвращает это число.
        ■ char * NumberToString (int number); — функция конвертирует число в строку и возвращает указатель на
        эту строку.
        ■ char * Uppercase (char * str1); — функция преобразует
        строку в верхний регистр.
        ■ char * Lowercase (char * str1); — функция преобразует
        строку в нижний регистр.
        ■ char * mystrrev (char * str); — функция реверсирует
        строку и возвращает указатель на новую строку.

*/

#include <iostream>
#include <conio.h>
#include <string>
#include <fstream>

#ifndef UNICODE
#define UNICODE
#endif
#ifndef _UNICODE
#define _UNICODE
#endif

#include <Windows.h>
#include "String.h"

#define space cout<<"\n\n"
#define CHARS for (int i = 0; i < 255; i++) { cout << i << " = " << char(i) << "\n"; }

using namespace std;

/* main */

void TaskOne();

/*auxiliary */

void showContinue();
void showSubContinue(int task_number);

int main() {
    char user;
    char c_con;
    std::string menu = "\nPress \"1\" if you want to see the demonstraion of a task.\n\nYou: ";
    setlocale(LC_CTYPE, ".1251");
    setlocale(LC_CTYPE, ".866");
    do
    {
        std::cout << menu;
        user = _getch();

        switch (user)
        {
        case '1':
        {
            TaskOne();
            break;
        }
        default:
            break;
        }
        showContinue();
        c_con = _getch();
        std::cout << "\n\n";
        system("cls");
    } while (c_con != 'n' && c_con != 'N');
    std::cout << "\n\n\tYou've quit. Have a nice day!\n\t   (Press any key...)\n\n\n";
    space;
    system("pause>0");
    return 0;

}

void TaskOne() {
#ifdef CHAR
    space;
    CHARS
    space;
#endif

    // #1
    space;
    space;
    cout << "\t" << char(254) << " \"mystrcmp\"";
    space;
    string str0 = "Hello";
    string str1 = "Hello";
    string str2 = "Hell";

    char* pC0 = new char[str0.length()];
    for (int i = 0; i < str0.length(); i++) {
        pC0[i] = str0[i];
    }
    pC0[str0.length()] = '\0';
    char* pC1 = new char[str1.length()];
    for (int i = 0; i < str1.length(); i++) {
        pC1[i] = str1[i];
    }
    pC1[str1.length()] = '\0';
    char* pC2 = new char[str2.length()];
    for (int i = 0; i < str2.length(); i++) {
        pC2[i] = str2[i];
    }
    pC2[str2.length()] = '\0';

    cout << "\tFirst string: " << str0 << "\n";
    cout << "\tSecond string: " << str1 << "\n";
    cout << "\tThird string: " << str2 << "\n";
    
    cout << "\n\tFirst + second = " << mystrcmp(pC0, pC1);
    cout << "\n\tFirst + third = " << mystrcmp(pC0, pC2);
    space;
    space;

    pC1 = nullptr;
    delete[]pC1;
    pC2 = nullptr;
    delete[]pC2;

    // #2
    str0 = "79221";
    for (int i = 0; i < str0.length(); i++) {
        pC0[i] = str0[i];
    }
    pC0[str0.length()] = '\0';
    cout << "\t" << char(254) << " \"StringToNumber\"";
    space;
    cout << "\tString: " << str0 << "\n";
    cout << "\tNumber: " << StringToNumber(pC0);
    space;
    space;

    // #3
    cout << "\t" << char(254) <<" \"NumberToString\"";
    space;
    int n = 12345;
    pC0 = NumberToString(n);
    cout << "\tNumber: " << n << "\n\tString: ";
    for (int i = 0; pC0[i] != '\0'; i++) {
        cout << pC0[i];
    }
    space;
    space;

    // #4
    cout << "\t" << char(254) << " \"Uppercase\"";
    space;
    str0 = "abcdefg";
    for (int i = 0; i < str0.length(); i++) {
        pC0[i] = str0[i];
    }
    pC0[str0.length()] = '\0';
    cout << "\tString: " << str0 << "\n";
    cout << "\tUppercase: ";
    pC0 = Uppercase(pC0);
    for (int i = 0; pC0[i] != '\0'; i++) {
        cout << pC0[i];
    }
    space;
    space;

    // #5
    cout << "\t" << char(254) << " \"Lowercase\"";
    space;
    str0 = "QWERTY";
    for (int i = 0; i < str0.length(); i++) {
        pC0[i] = str0[i];
    }
    pC0[str0.length()] = '\0';
    cout << "\tString: " << str0 << "\n";
    cout << "\tUppercase: ";
    pC0 = Lowercase(pC0);
    for (int i = 0; pC0[i] != '\0'; i++) {
        cout << pC0[i];
    }
    space;
    space;

    // #6
    cout << "\t" << char(254) << " \"mystrrev\"";
    space;
    str0 = "Sator arepo tenet";
    for (int i = 0; i < str0.length(); i++) {
        pC0[i] = str0[i];
    }
    pC0[str0.length()] = '\0';
    cout << "\tString: " << str0 << "\n";
    cout << "\tReverse: ";
    pC0 = mystrrev(pC0);
    for (int i = 0; pC0[i] != '\0'; i++) {
        cout << pC0[i];
    }
    space;
    space;

    pC0 = nullptr;
    delete[]pC0;
}

void showContinue() {
    const int len = 47;
    cout << "\t" << char(201);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(187);
    cout << "\n\t" << char(186);

    cout << "  Do you want to continue the programme? (y/n) ";

    cout << char(186) << "\n";
    cout << "\t" << char(200);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(188) << "\n";
    cout << "\t You: ";
}

void showSubContinue(int task_number) {
    const int len = 47;
    cout << "\t" << char(218);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(191);
    cout << "\n\t" << char(179);

    cout << "     Do you want to continue Task " << task_number << "? (y/n)     ";

    cout << char(179) << "\n";
    cout << "\t" << char(192);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(217) << "\n";
    cout << "\t You: ";
}