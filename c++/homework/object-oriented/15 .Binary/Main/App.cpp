#include "App.h"





/*
                                                                   README


    Сделать бинарную сереализацию и десереализацию (чтение и запись в файл) класса Group из предыдущих ДЗ

    К заданию приступить по окончанию темы "Работа с файлами"



        GitHub:     https://github.com/Doronov-IV/Cpp-Homework


*/





#pragma region OBJECTIVE

/*

    Сделать бинарную сереализацию и десереализацию (чтение и запись в файл) класса Group из предыдущих ДЗ

    К заданию приступить по окончанию темы "Работа с файлами"


*/

#pragma endregion OBJECTIVE




#pragma region STRATEGY

/*
       

       


*/

#pragma endregion STRATEGY




#pragma region CONSTRUCT


static App* instance;


App* App::instance = nullptr;


App::App()
{
    
}


App::~App()
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
        i = Toolbox::getchSelector(5);

        switch (i)
        {
        case 1:
            TaskOne();
            break;
        case 2:
            system("cls");
            space;
            doubleTab;
            std::cout << "You've quit! Have a nice day!";
            line;
            doubleTab;
            std::cout << "(Press any key) .....";
            space;
            return;
        default:
            break;
        }
        space;
        Toolbox::showContinue();
        c_cont = _getwch();
        system("cls");
    }
    space;
    doubleTab;
    std::cout << "You've quit! Have a nice day!";
    line;
    doubleTab;
    std::cout << "(Press any key) .....";
    space;


#pragma endregion DEMO



}




void App::TaskOne()
{

    space;


    auto path = "Output\\person.bin";

    Person person;
    person.name = "Anton";
    person.age = 37;
    person.group = 'A';
    person.height = 180.5f;
    person.isMale = true;

    PersonBinary writer(person, path);

    writer.write();

    Person _person = writer.read(path);

    std::cout << _person.name;
    line;
    std::cout << _person.age;
    line;
    std::cout << _person.group;
    line;
    std::cout << _person.height;
    line;
    std::cout << _person.isMale;


    space;


}




void App::TaskTwo()
{
    space;
    tab;
    std::cout << BACKMAG << "Task Two" << RESETTEXT << FOREWHT;
    space;
    
    
}




void App::TaskThree()
{
    space;
    tab;
    std::cout << BACKMAG << "Task Three" << RESETTEXT << FOREWHT;
    space;


}






#pragma region MENU_DRAWING


void App::drawMenuStart()
{
    space;
    space;
    tab;
    std::cout << "Choose an action:";
    space;
    tab;
    std::cout << "1. Run\n\t";
    std::cout << "2. Exit";
    space;
}


#pragma endregion MENU_DRAWING

