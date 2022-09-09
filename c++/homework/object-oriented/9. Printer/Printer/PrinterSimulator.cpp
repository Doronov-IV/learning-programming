#include "PrinterSimulator.h"



// После имён Admin, Manager и тд, ставится пробел и открывающая скобка;
#define SPACES_AND_OPEN_BRACKET 2

// 100 процентов;
#define HUNDRED_PERCENT 100

// Координаты меню;
#define MENU_X 9
#define MENU_Y 2

// Координаты фидбека
#define DONE_X 4
#define DONE_Y 8

#define STATISTICS_X 62
#define STATISTICS_Y 10

#define BRACKET_X 56
#define BRACKET_Y 13



void PrinterSimulator::run()
{
    machine = new Printer(); // 

    int input = 0;

    char c_cont = 'y';

    std::cout << FOREWHT; // Чтобы видеть текст можно было при любых настройках консоли
    system("cls");
    drawMenu();

    while (c_cont != 'n' && c_cont != 'N')
    {
        input = Toolbox::selector(5);

        switch (input)
        {
        case 1:
            machine->addEmployee("Admin");
            drawFeedback();
            break;
        case 2:
            machine->addEmployee("Manager");
            drawFeedback();
            break;
        case 3:
            machine->addEmployee("Clerk");
            drawFeedback();
            break;
        case 4:
            if (machine->getPrintingQueuePointer()->isEmpty())
            {
                drawWarning();
                break;
            }
            else startPrinting();
            break;
        case 5:
            system("cls");
            space;
            doubleTab;
            std::cout << "You've quit! Have a nice day!";
            line;
            doubleTab;
            std::cout << "(Press any key) .....";
            space;
            delete machine;
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
    delete machine;
}




void PrinterSimulator::startPrinting()
{
    machine->setPrintingSpeed(80);
    clearMenuField();

    int active_employee_list_x = 2;
    int active_employee_list_y = 10;

    int horizontal_stick_x = active_employee_list_x;
    int horizontal_stick_y = active_employee_list_y - 1;


    drawField();


    int press_any_key_x = 9;
    int press_any_key_y = 5;
    Toolbox::setCursor(press_any_key_x, press_any_key_y);
    std::cout << FOREYEL << "Press any key to pause..." << FOREWHT;

    // Active Employee List

    drawActiveEmployeeList();

    const int loadbarX_start = 24;
    const int loadbarX_end = 56;
    const int loadbarY = 13;
    int nPageComparator = 0;
    int nPriorityCounter = INT_MIN;
    while (!_kbhit() && !machine->getPrintingQueuePointer()->isEmpty()) // Пока не нажата любая клавиша
    {
        std::cout << FOREBLU;
        for (size_t i = loadbarX_start; i <= loadbarX_end; i++) // Этот цикл нужен, чтобы разбить один большой слип на несколько меньших
        {
            Toolbox::setCursor(i, loadbarY);
            std::cout << char(219);
            for (size_t j = 0ULL; j < 100 - machine->getPrintingSpeed(); j++)
            {
                this_thread::sleep_for(5ms);
            }
        }

        // Очистка лоадбара
        std::cout << FOREWHT;
        Toolbox::clearScreenInRange(loadbarX_start, loadbarX_end, horizontal_stick_y + 4, horizontal_stick_y + 4);
        std::cout << FOREBLU;

        // Отрисовка лоадбара

        Toolbox::setCursor(loadbarX_start, BRACKET_Y);
        std::cout << "[";

        Toolbox::setCursor(BRACKET_X, BRACKET_Y);
        std::cout << "]";
        std::cout << FOREWHT;

        this_thread::sleep_for(100ms);

        nPageComparator = machine->getPrintingQueuePointer()->peek()->getPagesToPrint() - machine->getPrintingQueuePointer()->peek()->getPagesPrinted() - ITERATION_ISSUE - ITERATION_ISSUE;

#pragma region PAGES_COUNTDOWN_ANIMATION
        if (nPageComparator > 1 - ITERATION_ISSUE)
        {
            machine->getPrintingQueuePointer()->peek()->setPagesPrinted(machine->getPrintingQueuePointer()->peek()->getPagesPrinted()+1);
            active_employee_list_x = 2;
            active_employee_list_y = 10;
            Toolbox::setCursor(active_employee_list_x + machine->getPrintingQueuePointer()->peek()->getRank().length() + SPACES_AND_OPEN_BRACKET, active_employee_list_y);
            std::cout << FOREWHT << nPageComparator << FOREBLU;
        }
        else
        {
            
            // documenting

            nPriorityCounter += 1;
            std::string* sCurrentStatistics = new std::string(machine->getPrintingQueuePointer()->peek()->getRank());
            sCurrentStatistics->append(", pages -- ");
            sCurrentStatistics->append(to_string(machine->getPrintingQueuePointer()->peek()->getPagesToPrint() - ITERATION_ISSUE));
            sCurrentStatistics->append(", time -- ");
            sCurrentStatistics->append(Toolbox::getCurrentTime());
            machine->getStatisticsPointer()->enqueue(sCurrentStatistics, nPriorityCounter); // fixed

            clearStatistics();
            drawStatistics();

            //

            machine->getPrintingQueuePointer()->dequeue();
            Toolbox::clearScreenInRange(2, 15, 10, 20);
            drawActiveEmployeeList();
            machine->setClientsServed(machine->getClientsServed() + 1);
        }
#pragma endregion PAGES_COUNTDOWN_ANIMATION


        machine->setPagesPrinted(machine->getPagesPrinted()+1);
    }

    Toolbox::setCursor(36, horizontal_stick_y + 2);
    std::cout << FOREBLU << "Pause" << "   " << FOREWHT;


    // Ending

    Toolbox::clearScreenInRange(9,40,5,5); // clearing pressAnyKeyToPause 

    clearActiveEmployeeList();

    Toolbox::setCursor(MENU_X, MENU_Y);

    drawMenu();

}




void PrinterSimulator::drawMenu()
{
    Toolbox::setCursor(5, 2);
    tab;
    std::cout << "1. Add admin;";
    line;
    tab;
    std::cout << "2. Add manager;";
    line;
    tab;
    std::cout << "3. Add clerk;";
    line;
    tab;
    std::cout << "4. Start.";
    line;
    tab;
    std::cout << "5. Exit.";
}


void PrinterSimulator::drawField()
{
    int xCoord = 2;
    int yCoord = 10 - 1;


#pragma region ONE_HORIZONTAL
    const int pillar_len = 150;
    for (size_t i = 1; i < pillar_len; i++)
    {
        if (i == 20 || i == 60)
        {
            Toolbox::setCursor(i, yCoord);
            std::cout << FORECYN << char(194);
        }
        else
        {
            Toolbox::setCursor(i, yCoord);
            std::cout << FORECYN << char(196);
        }
    }
#pragma endregion ONE_HORIZONTAL




#pragma region TWO_VERTICAL
    const int vertical_pillar_len = 10;

    for (size_t i = 0ULL; i < vertical_pillar_len; i++)
    {
        Toolbox::setCursor(20, i + 10);
        std::cout << FORECYN << char(179);
    }

    for (size_t i = 0ULL; i < vertical_pillar_len; i++)
    {
        Toolbox::setCursor(60, i + 10);
        std::cout << FORECYN << char(179);
    }
#pragma endregion TWO_VERTICAL



#pragma region LOADBAR
    std::cout << FOREBLU;
    Toolbox::setCursor(24, yCoord + 4);
    std::cout << "[";

    Toolbox::setCursor(36, yCoord + 2);
    std::cout << "Printing" << "   ";

    Toolbox::setCursor(56, yCoord + 4);
    std::cout << "]";
    std::cout << FOREWHT;
#pragma endregion LOADBAR


}


void PrinterSimulator::drawActiveEmployeeList()
{
    int active_employee_list_x = 2;
    int active_employee_list_y = 10;
    
    int nInitialPagesCounter;
    for (size_t i = 0ULL; i < machine->getPrintingQueuePointer()->size(); i++)
    {
        nInitialPagesCounter = machine->getPrintingQueuePointer()->peek(i)->getPagesToPrint() - machine->getPrintingQueuePointer()->peek(i)->getPagesPrinted() - ITERATION_ISSUE;
        Toolbox::setCursor(active_employee_list_x, active_employee_list_y);
        std::cout << machine->getPrintingQueuePointer()->peek(i)->getRank() << " (" << nInitialPagesCounter
            << ")";
        active_employee_list_y += 1;
    }
}


void PrinterSimulator::drawFeedback()
{
    Toolbox::setCursor(DONE_X, DONE_Y);
    std::cout << BACKGRN << ">> Done!" << BACKBLK;
    this_thread::sleep_for(1000ms);
    Toolbox::clearScreenInRange(DONE_X, 20, DONE_Y, DONE_Y);
}


void PrinterSimulator::drawWarning()
{
    Toolbox::setCursor(DONE_X, DONE_Y);
    std::cout << BACKRED << "The printing Queue is empty!" << BACKBLK;
    this_thread::sleep_for(2500ms);
    Toolbox::clearScreenInRange(DONE_X, 40, DONE_Y, DONE_Y);
}


void PrinterSimulator::drawStatistics()
{
    for (size_t i = 0ULL; i < machine->getStatisticsPointer()->size(); i++)
    {
        Toolbox::setCursor(STATISTICS_X, STATISTICS_Y + i);
        std::cout << *machine->getStatisticsPointer()->peek(machine->getStatisticsPointer()->size() - ITERATION_ISSUE - i);
    }
}


void PrinterSimulator::clearMenuField()
{
    Toolbox::clearScreenInRange(1, 30, 1, 10);
}


void PrinterSimulator::clearActiveEmployeeList()
{
    Toolbox::clearScreenInRange(2, 19, 10, 10 + machine->getPrintingQueuePointer()->size());
}


void PrinterSimulator::clearStatistics()
{
    Toolbox::clearScreenInRange(STATISTICS_X, STATISTICS_X+60, STATISTICS_Y, STATISTICS_Y + machine->getStatisticsPointer()->size());
}




PrinterSimulator* PrinterSimulator::getInstance()
{
    if (nullptr == instance)
    {
        instance = new PrinterSimulator();
    }
    return instance;
}


#pragma region CONSTRUCT


static PrinterSimulator* instance;


PrinterSimulator* PrinterSimulator::instance = nullptr;


PrinterSimulator::PrinterSimulator()
{
    machine = nullptr;
}


PrinterSimulator::~PrinterSimulator()
{
    if (nullptr != machine) delete machine;
}


#pragma endregion CONSTRUCT



