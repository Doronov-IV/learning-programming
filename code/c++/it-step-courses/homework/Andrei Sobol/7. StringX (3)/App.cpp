#include "App.h"

#include <iostream>
#include <string>

using namespace std;


/*
    
   см -- region OBJECTIVE


*/

#define demo_one demoStringOne // Имя объекта для демонстрации
#define demo_two demoStringTwo // Второй объект, необходимый для демонстрации
#define demo_three demoStringThree // Третий объект, необходимый для демонстрации
#define INDEX 48 // Номер единицы в табице чаров

#define space std::cout << "\n\n"
#define tab std::cout<<"\t"



#pragma region OBJECTIVE

/*

    Дополнить класс String новыми методами и перегрузками операций.

    - дополнить класс методами isEmpty, size, clear == NEW_METHODS, StringEx.cpp, см меню демонстрации, строка 469
    - добавить по необходимости иные методы, упрощающие работу с классом == метод addSymbol переехал в private

    App.cpp строка 165 - добавить перегрузку оператора вывода в консоль +
    App.cpp строка 160 - добавить конструктор копирования и перегрузку оператора присваивания +
    App.cpp строка 190 - добавить перегрузку оператора индексации +
    App.cpp строка 241 - добавить перегрузку операторов сравнения


*/

#pragma endregion OBJECTIVE




#pragma region CONSTRUCT


App::App()
{

}


#pragma endregion CONSTRUCT





void App::run()
{
    

#pragma region PREPARATIONS


    StringEx demo_one = "Hello";

    StringEx demo_two = "_";

    StringEx demo_three = "World!";


#pragma endregion PREPARATIONS






#pragma region MENU_LOOP


    int i = 0;

    char c_cont = 'y';

    while (c_cont != 'n' && c_cont != 'N')
    {
        drawMenuStart();
        i = selector(6);

        switch (i)
        {
        case 1:
            drawConcatenation(demo_one, demo_two, demo_three);
            break;
        case 2:
            drawSplit(demo_one, demo_two, demo_three);
            break;
        case 3:
            drawInsert(demo_one);
            break;
        case 4:
            drawContains(demo_one, demo_two);
            break;
        case 5:
            drawNewMethodsDemo(demo_one, demo_two);
            break;
        case 6:
            system("cls");
            space;
            std::cout << "\t\tYou've quit! Have a nice day!\n\t\t(Press any key) .....";
            space;
            return;
        default:
            break;
        }
        space;
        showContinue();
        c_cont = _getwch();
        system("cls");
    }
    space;
    std::cout << "\t\tYou've quit! Have a nice day!\n\t\t(Press any key) .....";
    space;


#pragma endregion MENU_LOOP
}




#pragma region TASK_DEMONSTRATION


void App::drawConcatenation(StringEx& demo_one, StringEx& demo_two, StringEx& demo_three)
{


#pragma region DRAWING_1

    demo_one.setString("Hello");

    demo_two.setString("_");

    demo_three.setString("World!");

    space;
    space;
    tab;
    std::cout << "There are three objects:";
    space;
    space;
    tab;
    std::cout << " > demo_one is \"";

#pragma endregion DRAWING_1


    demo_one = demo_three;


    std::cout << demo_one; // ===============================
    


#pragma region DRAWING_2

    std::cout << "\"";
    space;
    tab;
    std::cout << " > demo_two is \"";

#pragma endregion DRAWING_2


    std::cout << demo_two; // ===============================


#pragma region DRAWING_3

    std::cout << "\"";
    space;
    tab;
    std::cout << " > demo_three is \"";
#pragma endregion DRAWING_3

    int size = demo_three.size(); // ========================
    for (int i = 0; i < size; i++)
    {
        std::cout << demo_three[i];
    }
 

#pragma region DRAWING_4
    std::cout << "\"";

    space;
    space;
    tab;
    std::cout << "Combining 1 with 2:";


    demo_one + demo_two; // =======================================


    tab;
    tab;
    
    for (int i = 0; i < demo_one.getLength(); i++)
    {
        std::cout << demo_one.getArrayPtr()[i];
    }

    space;
    tab;
    std::cout << "Combining 1 and 2 with 3:";


    demo_one + demo_three; // =====================================



    tab;

    for (int i = 0; i < demo_one.getLength(); i++)
    {
        std::cout << demo_one.getArrayPtr()[i];
    }

    space;
    space;

#pragma endregion DRAWING_4




#pragma region COMPARISON_OPERATOR


    space;
    space;
    tab;

    demo_one = demo_three;

    int a = demo_one == demo_three;
    switch (a)
    {
    case 0:
        std::cout << "demo_one and demo_three differ in size"; // у строк разный размер
        break;
    case 1:
        std::cout << "demo_one and demo_three got same size but their contents differ"; // размер схож, но содержимое различается
        break;
    case 10:
        std::cout << "demo_one and demo_three are completely even"; // строки абсолютно идентичны
        break;
    }


#pragma endregion COMPARISON_OPERATOR

}


void App::drawSplit(StringEx& demo_one, StringEx& demo_two, StringEx& demo_three)
{


#pragma region DRAWING_1
    space;
    space;
    tab;
    demo_one.setString("Hello_World!");
    std::cout << "There is an object:";
    space;
    tab;
    std::cout << " > demo_one is \"";

    for (int i = 0; i < demo_one.getLength(); i++)
    {
        std::cout << demo_one.getArrayPtr()[i];
    }

    std::cout << "\"";

    space;
    space;
    tab;
    std::cout << "Now, this object has been split into:";
#pragma endregion DRAWING_1


    StringEx* demo_ptr = demo_one / '_'; // ================================


#pragma region DRAWING_2
    
    demo_two = demo_ptr[0];

    demo_three = demo_ptr[1];

    space;
    tab;
    std::cout << " > demo_two is \"";

    for (int i = 0; i < demo_two.getLength(); i++)
    {
        std::cout << demo_two.getArrayPtr()[i];
    }

    std::cout << "\"";
    space;
    tab;
    std::cout << " > demo_three is \"";

    for (int i = 0; i < demo_three.getLength(); i++)
    {
        std::cout << demo_three.getArrayPtr()[i];
    }
    std::cout << "\"";

    space;
    space;

#pragma endregion DRAWING_2


}


void App::drawInsert(StringEx& demo_one)
{
    space;
    space;
    tab;
    demo_one.setString("Hello_World!");
    std::cout << "There is an object:";
    space;
    tab;
    std::cout << " > demo_one is \"";

    for (int i = 0; i < demo_one.getLength(); i++)
    {
        std::cout << demo_one.getArrayPtr()[i];
    }

    std::cout << "\"";

    space;
    space;
    tab;
    std::cout << "Now, We'll insert a hash symbol (\"#\") after each other symbols:";

    space;
    space;
    tab;

    int length = demo_one.getLength();
    for (int i = 1; i < length * 2; i += 2)
    {
        demo_one.insert("#", i);
    }


    std::cout << "Result:";
    space;
    tab;
    std::cout << " > demo_one is \"";

    for (int i = 0; i < demo_one.getLength(); i++)
    {
        std::cout << demo_one.getArrayPtr()[i];
    }

    std::cout << "\"";

    space;
    space;
}


void App::drawContains(StringEx& demo_one, StringEx& demo_two)
{


#pragma region DRAWING_1

    demo_two = "_";

    space;
    space;
    tab;
    demo_one.setString("Hello_World!");
    std::cout << "There are two objects:";
    space;
    tab;
    std::cout << " > demo_one is \"";

    for (int i = 0; i < demo_one.getLength(); i++)
    {
        std::cout << demo_one.getArrayPtr()[i];
    }

    std::cout << "\"";

    space;
    tab;
    std::cout << " > demo_two is \"";

    for (int i = 0; i < demo_two.getLength(); i++)
    {
        std::cout << demo_two.getArrayPtr()[i];
    }

    std::cout << "\"";

    space;
    space;

    tab;
    std::cout << "Checking for containment of Char \"#\" in demo_one:";
    std::cout << "\n\tResult:\t";
#pragma endregion DRAWING_1

    int l = demo_one % "#"; // По какой-то причине, с перегрузкой перестаёт работать оператор "<<"
    std::cout << l;


#pragma region DRAWING_2
    space;

    tab;
    std::cout << "Checking for containment of Const Char* \"ell\" in demo_one:";
    std::cout << "\n\tResult:\t";
#pragma endregion DRAWING_2

    l = demo_one % "ell"; // По какой-то причине, с перегрузкой перестаёт работать оператор "<<"
    std::cout << l;


#pragma region DRAWING_3
    space;

    tab;
    std::cout << "Checking for containment of another StringEx <demo_two> in demo_one:";
    std::cout << "\n\tResult:\t";
#pragma endregion DRAWING_3

    l = demo_one % (demo_two.getArrayPtr());
    std::cout << l;

#pragma region REST

    space;
    space;

#pragma endregion REST
}


void App::drawNewMethodsDemo(StringEx& demo_one, StringEx& demo_two)
{

    demo_one = "Hello_World!";

    space;
    space;
    tab;

    std::cout << "demo_one = " << demo_one;

    space;
    space;
    tab;

    std::cout << "New methods:";
    space;
    tab;

    std::cout << "isEmpty() = " << demo_one.isEmpty();

    space;
    tab;

    std::cout << "size() = " << demo_one.size();

    space;
    tab;

    std::cout << "clear() = ";
    demo_one.clear();

    std::cout << "demo_one = " << demo_one;

    space;
    space;
}


#pragma endregion TASK_DEMONSTRATION




#pragma region MENU_DRAWING


void App::showContinue()
{
    const int len = 47;
    std::cout << "\t" << char(201);
    for (int i = 0; i < len; i++)
    {
        std::cout << char(205);
    }
    std::cout << char(187);
    std::cout << "\n\t" << char(186);

    std::cout << "  Do you want to continue the programme? (y/n) ";

    std::cout << char(186) << "\n";
    std::cout << "\t" << char(200);
    for (int i = 0; i < len; i++)
    {
        std::cout << char(205);
    }
    std::cout << char(188) << "\n";
}


void App::drawMenuStart()
{
    space;
    space;
    tab;
    std::cout << "Choose an action:";
    space;
    tab;
    std::cout << "1. Concatenation\n\t";
    std::cout << "2. Split\n\t";
    std::cout << "3. Insert\n\t";
    std::cout << "4. Contains\n\t";
    std::cout << "5. isEmpty, size and clear\n\t";
    std::cout << "6. Exit";
    space;
}


int App::selector(int nOptionsQuantity)
{
    char user;
    do
    {
        user = _getwch();
    } while ((int)user < INDEX && (int)user > INDEX + nOptionsQuantity); // Пока ввод пользователя меньше 1 или больше кол-ва вариантов в меню

    return (int)user - INDEX;

}


#pragma endregion MENU_DRAWING

