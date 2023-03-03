
/*
    Задание 1. Написать программу, которая содержит функцию Action, принимающую в качестве аргумента, указатели на два массива (А и В) и размеры массивов, а также
        указатель на функцию.
        Пользователю отображается меню, в котором он
        может выбрать max, min, avg. Если выбран max — передается указатель на функцию, которая ищет максимум,
        если выбран min — передается указатель на функцию,
        которая ищет минимум, если выбран avg — передается
        указатель на функцию, которая ищет среднее.
        Возвращаемое значение функции Action результат
        выбора пользователя max, min, avg.
    
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

using namespace std;

#define space cout<<"\n\n"

/* main */

void TaskOne();

/*auxiliary */

int Action(int* pA, int cA, int* pB, int cB, int** pF);

int* mymax(int* pA, int* pB);
int* mymin(int* pA, int* pB);
int* myavg(int* pA, int* pB);
void fillArray(int*& pA);
void showContinue();
void showSubContinue(int task_number);

int main() {
    char user;
    char c_con;
    std::string menu = "\nChoose the number of a task (1).\n\nYou: ";
    setlocale(LC_CTYPE, ".1251");
    setlocale(LC_CTYPE, ".866");
    do {
        std::cout << menu;
        user = _getch();

        switch (user) {
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

    system("pause>0");
    return 0;

}

void TaskOne() {
    cout << "\n\nTask One.\n\n";
    int cA, cB;
    cout << "Enter A cap: ";
    cin >> cA;
    int* A = new int[cA];
    fillArray(A);
    cout << "Enter B cap: ";
    cin >> cB;
    int* B = new int[cB];
    fillArray(B);

    int** pF = new int*[3];
    for (int i = 0; i < 3; i++) {
        pF[i] = new int[2];
    }

    pF[0] = mymax(A, B);
    pF[1] = mymin(A, B);
    pF[2] = myavg(A, B);

    int v = Action(A, cA, B, cB, pF);
    space;
    cout << "\t Array A: " << pF[v][0] << "\n";
    cout << "\t Array B: " <<pF[v][1] << "\n";
    space;
    
}

int Action(int* pA, int cA, int* pB, int cB, int** pF) {
    space;
    char user;
    int n = -1;
    cout << "\tChoose an action.\n\n\t1. max\n\t2. min\n\t3. avg\n\t4. exit";
    space;
    user = _getch();

    switch (user) {
    case '1':
    {
        n = 0;
        break;
    }

    case '2':
    {
        n = 1;
        break;
    }

    case '3':
    {
        n = 2;
        break;
    }

    case '4':
    {
        break;
    }

    default:
        break;
    }

    return n;
}

int* mymax(int* pA, int* pB) {
    int* res = new int[2];
    int sizeA = _msize(pA) / sizeof(pA[0]);
    int sizeB = _msize(pB) / sizeof(pB[0]);
    int tempA = pA[0];
    int tempB = pB[0];
    for (int i = 0; i < sizeA; i++) {
        if (pA[i] > tempA) tempA = pA[i];
    }
    for (int i = 0; i < sizeB; i++) {
        if (pB[i] > tempB) tempB = pB[i];
    }
    res[0] = tempA;
    res[1] = tempB;
    return res;
}

int* mymin(int* pA, int* pB) {
    int* res = new int[2];
    int sizeA = _msize(pA) / sizeof(pA[0]);
    int sizeB = _msize(pB) / sizeof(pB[0]);
    int tempA = pA[0];
    int tempB = pB[0];
    for (int i = 0; i < sizeA; i++) {
        if (pA[i] < tempA) tempA = pA[i];
    }
    for (int i = 0; i < sizeB; i++) {
        if (pB[i] < tempB) tempB = pB[i];
    }
    res[0] = tempA;
    res[1] = tempB;
    return res;
}

int* myavg(int* pA, int* pB) {
    int* res = new int[2];
    int sizeA = _msize(pA) / sizeof(pA[0]);
    int sizeB = _msize(pB) / sizeof(pB[0]);
    int SA = 0;
    int SB = 0;
    for (int i = 0; i < sizeA; i++) {
        SA += pA[i];
    }
    for (int i = 0; i < sizeB; i++) {
        SB += pB[i];
    }
    res[0] = SA / sizeA;
    res[1] = SB / sizeB;
    return res;
}

void fillArray(int*& pA) {
    int size = _msize(pA) / sizeof(pA[0]);
    for (int i = 0; i < size; i++) {
        pA[i] = 1 + rand() % 9;
    }
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