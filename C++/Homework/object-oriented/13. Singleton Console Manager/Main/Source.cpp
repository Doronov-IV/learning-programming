
/*
                                                                   README


    см App.cpp — Task One & ConsoleManager.h;


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


