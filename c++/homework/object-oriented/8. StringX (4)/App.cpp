#include "App.h"
#include "Colors.h"
#include "FileBufferedReader.h"
#include "Text.h"

#include <iostream>


/*
                                                                   README


           1. Сжатие текста
    В строке текста записаны слова, разделенные пробелами в произвольном количестве. Сжатие текста состоит
    в том, что между словами оставляется по одному пробелу, а после последнего слова пробелы удаляются (пробелы
    перед первым словом сохраняются). Если строка содержит только пробелы, то все они сохраняются.
    Написать функцию, сжимающую текст.


    Реализовано .....................................   StringEx.cpp line 13
    Продемонстрировано  .............................   App.cpp (this) line 151


        2. Удаление комментариев
    Дан текст программы на С++. Комментарии начинаются с "//" и продолжаются до конца текущей строки
    или начинаются с "<комментарий открывается>" и заканчиваются "</комментарий закрывается>". В последнем
    случае комментарии могут быть в середине строки или располагаться на нескольких строках.
    Написать функцию, удаляющую комментарии.


    Реализовано .....................................   Text.cpp line 60
    Продемонстрировано  .............................   App.cpp (this) line 220


        3. Поиск и выделение
    Написать функцию, принимающую в качестве параметров две строки и возвращающую копию первого
    параметра, все вхождения второго параметра в которой взяты в «()»
    Например, если параметрами были строки
    "abaracadabra" и "ab",
    то вернуть надо
    "(ab)aracad(ab)ra".


    Реализовано .....................................   StringEx.cpp line 101
    Продемонстрировано  .............................   App.cpp (this) line 281


        GitHub:     https://github.com/Doronov-IV/Home-Assignments


*/


#define space std::cout << "\n\n"
#define tab std::cout<<"\t"


#define INDEX 48 // Номер единицы в табице чаров



#pragma region OBJECTIVE

/*

        1. Сжатие текста
    В строке текста записаны слова, разделенные пробелами в произвольном количестве. Сжатие текста состоит
    в том, что между словами оставляется по одному пробелу, а после последнего слова пробелы удаляются (пробелы
    перед первым словом сохраняются). Если строка содержит только пробелы, то все они сохраняются.
    Написать функцию, сжимающую текст.

        2. Удаление комментариев
    Дан текст программы на С++. Комментарии начинаются с "//" и продолжаются до конца текущей строки
    или начинаются с "<комментарий открывается>" и заканчиваются "</комментарий закрывается>". В последнем
    случае комментарии могут быть в середине строки или располагаться на нескольких строках.
    Написать функцию, удаляющую комментарии.

        3. Поиск и выделение
    Написать функцию, принимающую в качестве параметров две строки и возвращающую копию первого
    параметра, все вхождения второго параметра в которой взяты в «()»
    Например, если параметрами были строки
    "abaracadabra" и "ab",
    то вернуть надо
    "(ab)aracad(ab)ra".


*/

#pragma endregion OBJECTIVE




#pragma region CONSTRUCT


static App* instance;


App* App::instance = nullptr;


App::App()
{
    
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
    std::cout << BACKMAG << "Task One." << RESETTEXT;
    std::cout << FOREWHT; // Чтобы видеть текст можно было при любых настройках консоли
    space;
    space;
    tab;

    StringEx one = "           both delayed     as a result of the      COVID-19 pandemic. The";
    StringEx two = "           both delayed     as a result of the      COVID-19 pandemic. The      ";
    StringEx three = "                                                                            ";

    std::cout << "String one:\t " << BACKGRN << one << BACKBLK << "\n";
    tab;
    std::cout << "String two:\t " << BACKGRN << two << BACKBLK << "\n";
    tab;
    std::cout << "String three:\t " << BACKGRN << three << BACKBLK;

    space;
    space;
    tab;
    std::cout << BACKMAG << ">> Compressing..." << RESETTEXT;
    std::cout << FOREWHT; // Чтобы видеть текст можно было при любых настройках консоли
    space;
    space;
    tab;

    one.compress();
    two.compress();
    three.compress();

    std::cout << "String one:\t " << BACKGRN << one << BACKBLK << "\n";
    tab;
    std::cout << "String two:\t " << BACKGRN << two << BACKBLK << "\n";
    tab;
    std::cout << "String three:\t " << BACKGRN << three << BACKBLK;

    space;
}




void App::TaskTwo()
{
    space;
    tab;
    std::cout << BACKMAG << "Task Two." << RESETTEXT;
    std::cout << FOREWHT;
    space;
    space;
    tab;

    std::cout << BACKGRN << "Programme code:" << RESETTEXT;
    std::cout << FOREWHT;

    space;
    space;

    std::string pathIn = "temp.txt";
    std::string pathOut = "temp1.txt";

    FileBufferedReader reader;

    reader.scan(pathIn);
    reader.read(pathIn);

    char** ptrBuffer;

    reader.copyBuffer(ptrBuffer);

    Text text(ptrBuffer, reader.getBufferSize(), "C++");

    space;

    text.forEach(std::cout);
    text.cutComments();
    
    space;
    const int len = 87;
    for (int i = 0; i < len; i++)
    {
        std::cout << char(196);
    }
    space;
    tab;

    std::cout << BACKMAG << "Result:" << RESETTEXT;
    std::cout << FOREWHT;
    space;
    space;

    text.trim();
    text.forEach(std::cout);
    
}




void App::TaskThree()
{

    StringEx str1 = "abaracadabra";
    StringEx str2 = "ab";
    space;
    tab;
    std::cout << BACKMAG << "Task Three." << RESETTEXT << FOREWHT;
    space;
    space;
    tab;
    std::cout << "String 1: ";
    std::cout << BACKGRN << str1 << RESETTEXT << FOREWHT;
    space;
    tab;
    std::cout << "String 2: ";
    std::cout << BACKGRN << str2 << RESETTEXT << FOREWHT;
    space;

    str1 = str1.embrace(str1,str2).getArrayPtr();

    space;
    tab;
    std::cout << BACKMAG << "Result" << RESETTEXT << FOREWHT <<", String 1: ";
    std::cout << BACKGRN << str1 << RESETTEXT << FOREWHT;

    space;

    const int len = 47;
    std::cout << "\t";
    for (int i = 0; i < len; i++)
    {
        std::cout << char(196);
    }

    str1 = "abababababab";
    str2 = "ab";
    str1 = str1.embrace(str1, str2).getArrayPtr();

    space;
    tab;
    std::cout << "String 1: ";
    std::cout << BACKGRN << str1 << RESETTEXT << FOREWHT;
    space;
    tab;
    std::cout << "String 2: ";
    std::cout << BACKGRN << str2 << RESETTEXT << FOREWHT;
    space;

    str1 = str1.embrace(str1, str2).getArrayPtr();

    space;
    tab;
    std::cout << BACKMAG << "Result" << RESETTEXT << FOREWHT << ", String 1: ";
    std::cout << BACKGRN << str1 << RESETTEXT << FOREWHT;
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
    std::cout << "1. Task One (spare spaces)\n\t";
    std::cout << "2. Task Two (clear comments)\n\t";
    std::cout << "3. Task Three (outline substring)\n\t";
    std::cout << "4. Exit";
    space;
}


int App::selector(int nOptionsQuantity)
{
    char user;
    do
    {
        user = _getwch();
    } while ((int)user < INDEX && (int)user > INDEX + nOptionsQuantity); // Ïîêà ââîä ïîëüçîâàòåëÿ ìåíüøå 1 èëè áîëüøå êîë-âà âàðèàíòîâ â ìåíþ

    return (int)user - INDEX;

}


#pragma endregion MENU_DRAWING

