// Sic Parvis Magna


#pragma region README


/*
 
                                                            README


                 @See ".Readme\README.txt"


*/


#pragma endregion README


#include "Application.h"


#pragma warning(disable : 4996)


int main()
{
    system("title Plant.io");
    Application* app = Application::getInstance();
    app->run();
    return 0;
}
