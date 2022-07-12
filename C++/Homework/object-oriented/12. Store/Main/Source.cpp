
/*
                                                                   README


    Стек вызова: Source->App->StoreSimulator->Store ....

    Очевидные проблемы: 

    — StoreSimulator.cpp ... line 185
    — Store.cpp          ... line 173
    — PriorityQueueX     ... line 87


                        Создать иерархию классов для приложения "Магазин"

    В магазине есть работники и клиенты. 
    Все они имеют фио.
    У работников есть график работы. 
    Работники делятся на продавцов и ремонтников. 
    И продавцы и ремонтники могут оформлять заказы.
    У заказа есть стоимость, в нем указан работник, оформивший заказ, и клиент.
    Но у заказов на ремонт есть еще срок выполнения.
    Магазин хранит историю заказов, список работников и клиентов.


        GitHub:     https://github.com/Doronov-IV/Home-Assignments


*/

#include <iostream>

#include "App.h"


int main()
{
    srand(time(NULL));
    App* app = App::getInstance();
    app->run();
    system("pause>0");
    return 0;
}


