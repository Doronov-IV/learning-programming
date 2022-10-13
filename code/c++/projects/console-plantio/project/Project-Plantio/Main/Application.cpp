#include "Application.h"



/*
 
                                                               README


        @See ".Readme\README.txt"


*/





#pragma region OBJECTIVE

/*

                @See ".Readme\README.txt"


*/

#pragma endregion OBJECTIVE





#pragma region STRATEGY

/*


    General:

    — main.cpp file: starts the main function;
    — Application class: runs the programme;
    — Toolbox namespace: provides utulity features; 

    — VisualPlantio class: draws console and displays plantio info;
    — Plantio class: is a wrapper for stocks, schedules and other basic entities;


*/

#pragma endregion STRATEGY




#pragma region CONSTRUCT


static Application* instance;


Application* Application::instance = nullptr;


Application::Application()
{

}


Application::~Application()
{
}


#pragma endregion CONSTRUCT




Application* Application::getInstance()
{
    if (nullptr == instance)
    {
        instance = new Application();
    }
    return instance;
}




void Application::run()
{


#pragma region DEMO

    #define fDEBUG

    #ifdef DEBUG
    Debug();
    #endif

    #ifndef DEBUG
    RunPlantio();
    #endif


#pragma endregion DEMO



}




void Application::RunPlantio()
{
    Toolbox::setFullscreenMode();
    Toolbox::fontsize(24,24);

    space;

    VisualPlantio* shell = VisualPlantio::getInstance();
    shell->run();

    Toolbox::fontsize(DEFAULT_TEXT_SIZE, DEFAULT_TEXT_SIZE);
    space;


}




void Application::Debug()
{
   
    space;
    for (size_t i = 0ULL; i < 50; i++)
    {
        std::cout << i;
        line;
    }
    
    Toolbox::ScrollByAbsoluteCoord(23);
    Toolbox::setCursor(0,0);


    std::cout << "result";

    space;

}







#pragma region MENU_DRAWING


void Application::drawMenuStart()
{
    space;
    space;
    tab;
    std::cout << "Choose an action:";
    space;
    tab;
    std::cout << "1. Main\n\t";
    std::cout << "2. Debug\n\t";
    line;
    tab;
    std::cout << "3. Exit";
    space;
}


#pragma endregion MENU_DRAWING
