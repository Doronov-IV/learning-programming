#include "App.h"
#include "../ConsoleManager/ConsoleManager.h"


/*
                                                                   README


            Написать класс управлением консолью

    Класс должен быть синглтоном
    Должны быть методы:
     - установки курсора в нужную позицию;
     - ввода текста определенного цвета;
     - очистки консоли
     - возвращающие текущее положение курсора
     - возвращающий текущий цвет текста
 
    Задание выполнять весьма желательно при помощи последовательностей виртуального терминала




        GitHub:     https://github.com/Doronov-IV/Home-Assignments


*/





#pragma region OBJECTIVE

/*

            Написать класс управлением консолью

    Класс должен быть синглтоном
    Должны быть методы:
     - установки курсора в нужную позицию;
     - ввода текста определенного цвета;
     - очистки консоли
     - возвращающие текущее положение курсора
     - возвращающий текущий цвет текста

    Задание выполнять весьма желательно при помощи последовательностей виртуального терминала


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
        i = Toolbox::selector(5);

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

    int x_coord = 15;
    int y_coord = 15;

    ConsoleManager* manager = ConsoleManager::getInstance();

#pragma region BEFORE_CLS

    manager->setCursor(x_coord, y_coord);
    manager->printColoredText("Yellow", "Hello, World!");

    y_coord += 1;
    manager->setCursor(x_coord, y_coord);
    std::cout << "Current text color: " << manager->getCurrentTextColor();
    manager->printColoredText("White", " ");

    y_coord += 1;
    manager->setCursor(x_coord, y_coord);
    std::cout << "Cursor position: x = " << manager->getCursorPosition().getX() << ", y = " << manager->getCursorPosition().getY();

    y_coord += 1;
    manager->setCursor(x_coord, y_coord);
    std::cout << "Current text color: " << manager->getCurrentTextColor();

#pragma endregion BEFORE_CLS


    this_thread::sleep_for(3000ms);
    manager->clear();


#pragma region AFTER_CLS

    manager->setCursor(x_coord, y_coord);
    manager->printColoredText("White", "Text cleared.");

    manager->release();
    manager = ConsoleManager::getInstance();


    setCursor(15, 15); // Меняю положение курсора через другую функцию;


    std::cout << "Cursor position: x = " << manager->getCursorPosition().getX() << ", y = " << manager->getCursorPosition().getY();

#pragma endregion AFTER_CLS


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

