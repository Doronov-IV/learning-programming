#include "App.h"
#include "../Misc/Colors.h"
#include "../FileBufferedReader/FileBufferedReader.h"
#include "../Texting/Text.h"

#include <iostream>


/*
                                                                   README


    класс GorupBuilder -- build

    продемонстрировано в методах TaskOne, TaskTwo, TaskThree


        Написать примитивный JSON-парсер
    Дополнить проект Students который мы писали в классе парсером который читает
    единственный файл group.json и создает на его основе класс группы со всем содержимым:
    списком студентов, список предметов и список оценок.
    
    Пример файла ищите в прикрепленных файлах.




        GitHub:     https://github.com/Doronov-IV/Home-Assignments


*/


#define space std::cout << "\n\n"
#define line std::cout << "\n"
#define tab std::cout<<"\t"


#define INDEX 48 // Номер единицы в табице чаров



#pragma region OBJECTIVE

/*

        Написать примитивный JSON-парсер
    Дополнить проект Students который мы писали в классе парсером который читает
    единственный файл group.json и создает на его основе класс группы со всем содержимым:
    списком студентов, список предметов и список оценок.

    Пример файла ищите в прикрепленных файлах.


*/

#pragma endregion OBJECTIVE




#pragma region CONSTRUCT


static App* instance;


App* App::instance = nullptr;


App::App()
{
    
}


App::~App()
{
    delete instance;
}


#pragma endregion CONSTRUCT



App* App::getInstance()
{
    if (nullptr == instance)
    {
        instance = new App();
    }
    return instance;
}



void App::run()
{
    

#pragma region DEMO


    int i = 0;

    char c_cont = 'y';

    while (c_cont != 'n' && c_cont != 'N')
    {
        std::cout << FOREWHT; // Чтобы видеть текст можно было при любых настройках консоли
        drawMenuStart();
        i = selector(5);

        switch (i)
        {
        case 1:
            TaskOne();
            break;
        case 2:
            TaskTwo();
            break;
        case 3:
            TaskThree();
            break;
        case 4:
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


#pragma endregion DEMO



}



void App::TaskOne()
{
    space;
    tab;
    std::cout << BACKMAG << "Task One" << RESETTEXT << FOREWHT;
    space;

    FileBufferedReader reader;
    reader.read("group.json");
    Text text("JSON");
    text.setText(reader.getBuffer(), reader.getBufferSize());

    space;
    space;
    tab;

    Group fellowship;
    GroupBuilder* builder = GroupBuilder::getInstance();
    builder->build(fellowship, "RESOURCES/default_group.json");

    line;
    Group::printGroupStats(fellowship);
    space;
}




void App::TaskTwo()
{
    space;
    tab;
    std::cout << BACKMAG << "Task One" << RESETTEXT << FOREWHT;
    space;

    FileBufferedReader reader;
    reader.read("group.json");
    Text text("JSON");
    text.setText(reader.getBuffer(), reader.getBufferSize());

    space;
    space;
    tab;

    Group fellowship;
    GroupBuilder* builder = GroupBuilder::getInstance();
    builder->build(fellowship, "RESOURCES/unnamed_group.json");

    line;
    Group::printGroupStats(fellowship);
    space;
}




void App::TaskThree()
{
    space;
    tab;
    std::cout << BACKMAG << "Task One" << RESETTEXT << FOREWHT;
    space;

    FileBufferedReader reader;
    reader.read("group.json");
    Text text("JSON");
    text.setText(reader.getBuffer(), reader.getBufferSize());

    space;
    space;
    tab;

    Group fellowship;
    GroupBuilder* builder = GroupBuilder::getInstance();
    builder->build(fellowship, "RESOURCES/nomarks_group.json");

    line;
    Group::printGroupStats(fellowship);
    space;
}






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
    std::cout << "1. Default (group building)\n\t";
    std::cout << "2. Without name (group building)\n\t";
    std::cout << "3. Without grades (group building)\n\t";
    std::cout << "4. Exit";
    space;
}


int App::selector(int nOptionsQuantity)
{
    char user;
    do
    {
        user = _getwch();
    } while ((int)user < INDEX && (int)user > INDEX + nOptionsQuantity); // In case of IllegalArgument

    return (int)user - INDEX;

}


#pragma endregion MENU_DRAWING

