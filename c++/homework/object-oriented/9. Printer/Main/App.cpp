#include "App.h"



/*
                                                                   README


            Разработать приложение, имитирующее очередь печати принтера. Должны быть клиенты, посылающие
        запросы на принтер, у каждого из которых есть свой приоритет. Каждый новый клиент попадает в очередь
        в зависимости от своего приоритета. Необходимо сохранять статистику печати (пользователь, время)
        в отдельной очереди. Предусмотреть вывод статистики на экран




        GitHub:     https://github.com/Doronov-IV/Home-Assignments


*/





#pragma region OBJECTIVE

/*

            Разработать приложение, имитирующее очередь печати принтера. Должны быть клиенты, посылающие
        запросы на принтер, у каждого из которых есть свой приоритет. Каждый новый клиент попадает в очередь
        в зависимости от своего приоритета. Необходимо сохранять статистику печати (пользователь, время)
        в отдельной очереди. Предусмотреть вывод статистики на экран


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
    PrinterSimulator* simulator = PrinterSimulator::getInstance();
    simulator->run();
    
    space;

    

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

