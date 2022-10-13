#include "App.h"



#include "../DeliveryService/BaseClasses/Drink.h"



/*
                                                                   README


            Написать программу "Доставка еды на день"

    Пользователь должен иметь возможность выбрать рацион на день. 
    Для этого ему необходимо выбрать 5 блюд на 5 приемов пищи (завтрак, обед и т.д.) и напиток.
    Причем ему не обязательно выбирать все пять.
    Программа должна подсчитать итоговое кол-во калорий и сумму заказа на день.
    Пользователь может выбрать время доставки и период (например, один день, неделя и т.д.)
    В таком случае должна быть показанна итоговая сумма кроме суммы за день.
    При большом периоде доставки предусмотреть наличие скидки.
    Кроме того, клиент может выбрат набор из пресетов 
    (например, обед - 2 блюда и напиток, фитнес - 4 блюда и напиток, фулл - 5 блюд и напиток)
    
    Программу реализовать при помощи паттерна Строитель, для реализации наборов используйте Директора.



        GitHub:     https://github.com/Doronov-IV/Home-Assignments


*/





#pragma region OBJECTIVE

/*

            Написать программу "Доставка еды на день"

    Пользователь должен иметь возможность выбрать рацион на день.
    Для этого ему необходимо выбрать 5 блюд на 5 приемов пищи (завтрак, обед и т.д.) и напиток.
    Причем ему не обязательно выбирать все пять.
    Программа должна подсчитать итоговое кол-во калорий и сумму заказа на день.
    Пользователь может выбрать время доставки и период (например, один день, неделя и т.д.)
    В таком случае должна быть показанна итоговая сумма кроме суммы за день.
    При большом периоде доставки предусмотреть наличие скидки.
    Кроме того, клиент может выбрат набор из пресетов
    (например, обед - 2 блюда и напиток, фитнес - 4 блюда и напиток, фулл - 5 блюд и напиток)

    Программу реализовать при помощи паттерна Строитель, для реализации наборов используйте Директора.


*/

#pragma endregion OBJECTIVE




#pragma region STRATEGY

/*
       
       Заказ представляет собой два умных массива из блюд и напитков соответственно.
       Так же, в заказе имеется чек, который представляет собой строку.

       Билдер умеет добавлять по одному приёму пищи (я убрал отдельные методы для завтрака, обеда и тд,
       потому что в моей реализации, программа обозначает конкретный приём пищи на этапе вывода на экран).
       Директор зацикливает метод addMeal() билдера на кол-во приёмов пищи и на общее кол-во дней.

       Цикл приёмов пищи повторяется раз в: кол-во блюд (зависит от пресета) * 5 (кол-во приёмов пищи, константа).

       По поводу храниния данных в txt файлах: нулевым номером идёт название, первым — калорийность, вторым — цена (в EUR).
       Каждый приём пищи включает в себя N блюд и 1 (константа) напиток.
       


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

    std::cout << "Please, choose the amount of days that you would like to have our meals (1-7)";
    space;
    std::cout << "You: ";
    size_t nDays = Toolbox::cinSelector(1,7);
    DeliverySimulator* sim = DeliverySimulator::getInstance();
    sim->run(nDays);


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

