#include "StoreSimulator.h"


// Координаты меню;
#define MENU_X 9
#define MENU_Y 2

// Координаты фидбека;
#define DONE_X 4
#define DONE_Y 9

// Точки, в которых пересекаются линии;
#define CROSS_X_ONE 20
#define CROSS_X_TWO 40

// Нулевые координаты вывода списка клиентов;
#define CUSTOMERS_X  3
#define CUSTOMERS_Y  11

// Нулевые координаты вывода списка сотрудников;
#define EMPLOYEES_X  24
#define EMPLOYEES_Y  11

// Нулевые координаты вывода истории заказов;
#define HISTORY_X 45
#define HISTORY_Y 11



void StoreSimulator::run()
{
    example = new Store("Example");

    // testing section


    // /testing section

    int input = 0;
    
    char c_cont = 'y';
    
    std::cout << FOREWHT; // Чтобы видеть текст можно было при любых настройках консоли
    system("cls");
    drawField();
    drawMenu();
    
    while (c_cont != 'n' && c_cont != 'N')
    {
        input = Toolbox::selector(5);
    
        switch (input)
        {
        case 1:
            example->addRandomCustomer();
            clearCustomersField();
            drawCustomersList();
            drawFeedback();
            break;
        case 2:
            example->addRandomWorker();
            clearEmployeeField();
            drawEmployeeList();
            drawFeedback();
            break;
        case 3:
            if (-1 == example->makeRandomContract()) drawWarning();
            else
            {
                clearHistoryField();
                drawHistoryList();
                drawFeedback();
            }
            break;
        case 4:
            clearHistoryField();
            example->clearHistory();
            break;
        case 5:
            clearCustomersField();
            clearEmployeeField();
            clearHistoryField();
            example->clearAllLists();
            break;
        case 6:
            system("cls");
            space;
            doubleTab;
            std::cout << "You've quit! Have a nice day!";
            line;
            doubleTab;
            std::cout << "(Press any key) .....";
            space;
            delete example;
            return;
        default:
            break;
        }
    }
    space;
    doubleTab;
    std::cout << "You've quit! Have a nice day!";
    line;
    doubleTab;
    std::cout << "(Press any key) .....";
    space;

    

    delete example;
}




void StoreSimulator::drawMenu()
{
    Toolbox::setCursor(5, 2);
    tab;
    std::cout << FOREWHT << "1. Add random customer";
    line;
    tab;
    std::cout << "2. Add random worker";
    line;
    tab;
    std::cout << "3. Add random order";
    line;
    tab;
    std::cout << "4. Clear orders history";
    line;
    tab;
    std::cout << "5. Clear all";
    line;
    tab;
    std::cout << "6. Exit.";
}


void StoreSimulator::drawField()
{
    int xCoord = 2;
    int yCoord = 10;


#pragma region ONE_HORIZONTAL
    const int pillar_len = 150;
    for (size_t i = 1; i < pillar_len; i++)
    {
        if (i == CROSS_X_ONE || i == CROSS_X_TWO)
        {
            Toolbox::setCursor(i, yCoord);
            std::cout << FOREGRN << char(194);
        }
        else
        {
            Toolbox::setCursor(i, yCoord);
            std::cout << FOREGRN << char(196);
        }
    }
#pragma endregion ONE_HORIZONTAL




#pragma region TWO_VERTICAL
    const int vertical_pillar_len = 10;

    for (size_t i = 0ULL; i < vertical_pillar_len; i++)
    {
        Toolbox::setCursor(CROSS_X_ONE, i + 11);
        std::cout << FOREGRN << char(179);
    }

    for (size_t i = 0ULL; i < vertical_pillar_len; i++)
    {
        Toolbox::setCursor(CROSS_X_TWO, i + 11);
        std::cout << FOREGRN << char(179);
    }
#pragma endregion TWO_VERTICAL


}


// На пол пути осознал, что я делаю CTRL-C, CTRL-V.
// Скорее всего, в проекте я использую этот "паттерн" в отдельном классе,
// можно заметить, что визуал полностью копирован с прошлой дз, просто списков больше.
// Пока что, времени переписывать с нуля особо нет, но я обратил внимание на ошибку.
#pragma region REPEATING_CODE


void StoreSimulator::drawCustomersList()
{
    int x_coord = CUSTOMERS_X;
    int y_coord = CUSTOMERS_Y;

    Toolbox::setCursor(x_coord, y_coord);
    std::cout << FOREWHT << "Customers:";
    y_coord += 2;
    for (size_t i = y_coord, j = 0ULL; j < example->getCustomersPointer().size(); i++, j++)
    {
        Toolbox::setCursor(x_coord, y_coord);
        std::cout << example->getCustomersPointer()[j].getName();
        y_coord += 1;
    }
}


void StoreSimulator::clearCustomersField()
{
    int x_coord = CUSTOMERS_X;
    int y_coord = CUSTOMERS_Y;

    Toolbox::clearScreenInRange(x_coord, x_coord + 12, y_coord, y_coord + example->getCustomersPointer().size() + 1);
}


void StoreSimulator::drawEmployeeList()
{
    int x_coord = EMPLOYEES_X;
    int y_coord = EMPLOYEES_Y;

    Toolbox::setCursor(x_coord, y_coord);
    std::cout << FOREWHT << "Employees:";
    y_coord += 2;
    for (size_t i = y_coord, j = 0ULL; j < example->getSalesmenPointer().size(); i++, j++)
    {
        Toolbox::setCursor(x_coord, y_coord);
        std::cout << example->getSalesmenPointer()[j].getName();
        y_coord += 1;
    }

    for (size_t i = y_coord, j = 0ULL; j < example->getRepairmenPointer().size(); i++, j++)
    {
        Toolbox::setCursor(x_coord, y_coord);
        std::cout << example->getRepairmenPointer()[j].getName();
        y_coord += 1;
    }
}


void StoreSimulator::clearEmployeeField()
{
    int x_coord = EMPLOYEES_X;
    int y_coord = EMPLOYEES_Y;

    Toolbox::clearScreenInRange(x_coord, x_coord + 12, y_coord, y_coord + example->getRepairmenPointer().size() + example->getSalesmenPointer().size() + 1);
}


void StoreSimulator::drawHistoryList()
{
    int x_coord = HISTORY_X;
    int y_coord = HISTORY_Y;

    Toolbox::setCursor(x_coord, y_coord);
    std::cout << FOREWHT << "Orders:";
    y_coord += 2;
    for (size_t i = y_coord, j = 0ULL; j < example->getHistoryPointer()->size(); i++, j++)
    {
        Toolbox::setCursor(x_coord, y_coord);
        std::cout << *example->getHistoryPointer()->peek(j);
        y_coord += 1;
    }
}


void StoreSimulator::clearHistoryField()
{
    int x_coord = HISTORY_X;
    int y_coord = HISTORY_Y;

    Toolbox::clearScreenInRange(x_coord, x_coord + 70, y_coord, y_coord + example->getHistoryPointer()->size() + 1);
}


#pragma endregion REPEATING_CODE



void StoreSimulator::drawFeedback()
{
    Toolbox::setCursor(DONE_X, DONE_Y);
    std::cout << BACKGRN << FOREWHT << ">> Done!" << BACKBLK;
    this_thread::sleep_for(1000ms);
    Toolbox::clearScreenInRange(DONE_X, 20, DONE_Y, DONE_Y);
}


void StoreSimulator::drawWarning()
{
    Toolbox::setCursor(DONE_X, DONE_Y);
    std::cout << BACKRED << "Please, add some customers or employees!" << BACKBLK;
    this_thread::sleep_for(2500ms);
    Toolbox::clearScreenInRange(DONE_X, 50, DONE_Y, DONE_Y);
}




StoreSimulator* StoreSimulator::getInstance()
{
    if (nullptr == instance)
    {
        instance = new StoreSimulator();
    }
    return instance;
}




#pragma region CONSTRUCT


static StoreSimulator* instance;


StoreSimulator* StoreSimulator::instance = nullptr;


StoreSimulator::StoreSimulator()
{
}


StoreSimulator::~StoreSimulator()
{
}


#pragma endregion CONSTRUCT