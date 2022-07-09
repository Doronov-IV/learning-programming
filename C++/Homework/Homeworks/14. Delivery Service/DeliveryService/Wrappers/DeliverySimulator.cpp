#include "DeliverySimulator.h"



DeliverySimulator* DeliverySimulator::getInstance()
{
    if (nullptr == instance)
    {
        instance = new DeliverySimulator();
    }

    return instance;
}


void DeliverySimulator::run(size_t nDaysCounter)
{
    size_t nCurrentDaysCounter = 0ULL;

    size_t input = 0ULL;

    char c_cont = 'y';

    size_t nUserMealsInput = 0ULL;

    std::cout << FOREWHT; // Чтобы видеть текст можно было при любых настройках консоли;

    do
    {
        ptrCurrentOrder = new Order();

        system("cls");
        nCurrentDaysCounter = nDaysCounter;
        drawLoopMenu();

        input = Toolbox::getchSelector(LOOP_MENU_OPTIONS);

        switch (input)
        {
        case 1:
            initializeWrappers(PRESET_ONE, nDaysCounter);
            drawFeedBack(5, PRESET_MENU_OPTIONS + PRESET_MENU_OPTIONS);
            break;
        case 2:
            initializeWrappers(PRESET_TWO, nDaysCounter);
            drawFeedBack(5, PRESET_MENU_OPTIONS + PRESET_MENU_OPTIONS);
            break;
        case 3:
            initializeWrappers(PRESET_THREE, nDaysCounter);
            drawFeedBack(5, PRESET_MENU_OPTIONS + PRESET_MENU_OPTIONS);
            break;
        case 4:
            currentDirector = new OrderDirector(PRESET_MANUAL, nDaysCounter); // might be a leak;
            ptrCurrentOrder->setDaysCounter(nDaysCounter);

            drawPresetMenu();
            nUserMealsInput = Toolbox::getchSelector(PRESET_MENU_OPTIONS);
            drawFeedBack(5, PRESET_MENU_OPTIONS + PRESET_MENU_OPTIONS + 3);

            while (0 != nCurrentDaysCounter)
            {
                commonSelectingProcedure(nUserMealsInput, ptrCurrentOrder->getDaysCounter() - nCurrentDaysCounter);
                nCurrentDaysCounter -= ONE_ELEMENT;
            }

            currentDecorator = new OrderDecorator(ptrCurrentOrder);
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
            return;
        default:
            currentDecorator = new OrderDecorator();
            currentDirector = new OrderDirector();
            break;
        } // end of switch;
        space;
        currentDecorator->printDetails();

        space;
        std::cout << "Press any key ....";
        c_cont = _getwch();
        system("cls");

        release();
    } while (false);// end of while-do loop;
    space;
    doubleTab;
    std::cout << "You've quit! Have a nice day!";
    line;
    doubleTab;
    std::cout << "(Press any key) .....";
    space;
}



void DeliverySimulator::drawFeedBack(size_t x_coord, size_t y_coord)
{
    setCursor(x_coord, y_coord);
    std::cout << BACKGRN << "> Done!" << BACKBLK;
    this_thread::sleep_for(1s);
    clearScreenInRange(x_coord, x_coord + 8, y_coord, y_coord);
}


void DeliverySimulator::commonSelectingProcedure(size_t nUserMealsInput, size_t nCurrentDaysCounter)
{
    Course* currentCourse = new Course();
    Drink* currentDrink = new Drink();

    size_t nCoursesCounter = 0ULL;
    size_t nDrinksCounter = 0ULL;
    size_t nMealsCounter = 0ULL;
    size_t nCommonCounter = 0ULL;

    for (size_t i = 0ULL; i < MEALS_COUNTER; i++)
    {
        do
        {
            system("cls");
            setCursor(10, MENU_LEFT_UP_Y - 3);
            std::cout << "(day " << nCurrentDaysCounter + NUMERATION_ISSUE << ", meal " << i + NUMERATION_ISSUE << ")";

            drawCoursesMenu();

            nCommonCounter = cinSelector(COURSES_MENU_SIZE + EXIT_MENU_OPTION);
            nCommonCounter -= NUMERATION_ISSUE;

            if (14 - NUMERATION_ISSUE == nCommonCounter)
            {
                currentCourse->setName("Null");
                currentCourse->setPrice("0");
                currentCourse->setCalories("0");
            }
            else
            {
                currentCourse->setName(currentDirector->getBuilderPointer()->getCoursesMenuPointer()[nCommonCounter * PROPERTIES_QUANTITY]);
                currentCourse->setPrice(currentDirector->getBuilderPointer()->getCoursesMenuPointer()[nCommonCounter * PROPERTIES_QUANTITY + PRICE]);
                currentCourse->setCalories(currentDirector->getBuilderPointer()->getCoursesMenuPointer()[nCommonCounter * PROPERTIES_QUANTITY + CALORIES]);
            }

            ptrCurrentOrder->addCourse(*currentCourse);
            nCoursesCounter += NEW_ELEMENT;

            ptrCurrentOrder->setCourseCounter(nCoursesCounter);

        } while (nUserMealsInput > nCoursesCounter);

        nCoursesCounter = 0ULL;
        nCommonCounter = 0ULL;

        system("cls");
        setCursor(10, MENU_LEFT_UP_Y - 3);
        std::cout << "(day " << nCurrentDaysCounter + NUMERATION_ISSUE << ", meal " << i + NUMERATION_ISSUE << ")";

        drawDrinksMenu();
        nCommonCounter = cinSelector(DRINKS_MENU_SIZE + EXIT_MENU_OPTION);
        nCommonCounter -= NUMERATION_ISSUE;

        if (8 - NUMERATION_ISSUE == nCommonCounter)
        {
            currentDrink->setName("Null");
            currentDrink->setPrice("0");
            currentDrink->setCalories("0");
        }
        else
        {
            currentDrink->setName(currentDirector->getBuilderPointer()->getDrinksMenuPointer()[nCommonCounter * PROPERTIES_QUANTITY]);
            currentDrink->setPrice(currentDirector->getBuilderPointer()->getDrinksMenuPointer()[nCommonCounter * PROPERTIES_QUANTITY + PRICE]);
            currentDrink->setCalories(currentDirector->getBuilderPointer()->getDrinksMenuPointer()[nCommonCounter * PROPERTIES_QUANTITY + CALORIES]);
        }

        ptrCurrentOrder->addDrink(*currentDrink);

    }
}


void DeliverySimulator::initializeWrappers(size_t nPresetNumber, size_t nDaysCounter)
{
    currentDirector = new OrderDirector(nPresetNumber, nDaysCounter);
    currentDirector->buildOrder();
    ptrCurrentOrder = new Order(*currentDirector->getBuilderPointer()->getOrderPointer());
    currentDecorator = new OrderDecorator(ptrCurrentOrder);

}



void DeliverySimulator::drawCoursesMenu()
{
    OrderBuilder* tempBuilder = new OrderBuilder();
    size_t start_x = MENU_LEFT_UP_X;
    size_t start_y = MENU_LEFT_UP_Y;

    for (size_t i = 0ULL, j = 0ULL; i < COURSES_MENU_SIZE * PROPERTIES_QUANTITY; i += PROPERTIES_QUANTITY, j++)
    {
        setCursor(start_x, start_y);
        std::cout << j + NUMERATION_ISSUE << ". " << tempBuilder->getCoursesMenuPointer()[i] << "(" <<
            tempBuilder->getCoursesMenuPointer()[i + PRICE] << " EUR)";
        start_y += 1;
    }

    start_y += 1;
    setCursor(start_x, start_y);

    std::cout << COURSES_MENU_SIZE + 1 << ". Exit";

    start_y += 2;
    setCursor(start_x, start_y);
    std::cout << "You: ";

    delete tempBuilder;

}


void DeliverySimulator::drawDrinksMenu()
{
    OrderBuilder* tempBuilder = new OrderBuilder();
    size_t start_x = MENU_LEFT_UP_X;
    size_t start_y = MENU_LEFT_UP_Y;

    for (size_t i = 0ULL, j = 0ULL; i < DRINKS_MENU_SIZE * PROPERTIES_QUANTITY; i += PROPERTIES_QUANTITY, j++)
    {
        setCursor(start_x, start_y);
        std::cout << j + NUMERATION_ISSUE << ". " << tempBuilder->getDrinksMenuPointer()[i] << "(" <<
            tempBuilder->getDrinksMenuPointer()[i + PRICE] << " EUR)";
        start_y += 1;
    }

    start_y += 1;
    setCursor(start_x, start_y);

    std::cout << DRINKS_MENU_SIZE + 1 << ". Exit";

    start_y += 2;
    setCursor(start_x, start_y);
    std::cout << "You: ";
    delete tempBuilder;
}


void DeliverySimulator::drawLoopMenu()
{
    Toolbox::setCursor(MENU_LEFT_UP_X, MENU_LEFT_UP_Y);
    tab;
    std::cout << "1. Preset #1 (three courses)";
    line;
    tab;
    std::cout << "2. Preset #2 (four courses)";
    line;
    tab;
    std::cout << "3. Preset #3 (five courses)";
    line;
    tab;
    std::cout << "4. Manual input.";
    line;
    tab;
    std::cout << "5. Exit.";
}


void DeliverySimulator::drawPresetMenu()
{
    system("cls");
    Toolbox::setCursor(MENU_LEFT_UP_X, MENU_LEFT_UP_Y);
    tab;
    std::cout << "1. One course";
    line;
    tab;
    std::cout << "2. Two courses";
    line;
    tab;
    std::cout << "3. Three courses";
    line;
    tab;
    std::cout << "4. Four courses.";
    line;
    tab;
    std::cout << "5. Five courses";
    space;
    tab;
    std::cout << "6. Exit.";
    space;
    tab;
}




void DeliverySimulator::drawWarning(std::string sWarningMessage)
{

}




static DeliverySimulator* instance;


DeliverySimulator* DeliverySimulator::instance = nullptr;


DeliverySimulator::DeliverySimulator()
{
}


void DeliverySimulator::release()
{
    if (nullptr != currentDecorator)
    {
        delete currentDecorator;
        currentDecorator = nullptr;
    }

    if (nullptr != currentDirector)
    {
        delete currentDirector;
        currentDirector = nullptr;
    }
}


DeliverySimulator::~DeliverySimulator()
{
    if (nullptr != currentDecorator)
    {
        delete currentDecorator;
        currentDecorator = nullptr;
    }

    if (nullptr != currentDirector)
    {
        delete currentDirector;
        currentDirector = nullptr;
    }
}