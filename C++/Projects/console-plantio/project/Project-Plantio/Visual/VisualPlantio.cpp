#include "VisualPlantio.h"





VisualPlantio* VisualPlantio::getInstance()
{
    if (nullptr == instance)
    {
        instance = new VisualPlantio();
    }
    return instance;
}


void VisualPlantio::run()
{
    init();
    while (1)
    {
        if (_kbhit)
        {
            auto key = _getch();

#pragma region ARROWS

            if (224 == key) key = _getch();

            if (75 == key) moveLeft();
            else if (77 == key) moveRight();

            if (72 == key) moveUpGeneralMenu();
            else if (80 == key) moveDownGeneralMenu();


#pragma endregion ARROWS

            if (VK_RETURN == key && getSelectedOption()->getName() == "Exit") break;

            if (VK_RETURN == key && getSelectedOption()->getName() == "View") drawView();

            if (VK_RETURN == key && getSelectedOption()->getName() != "View" && getSelectedOption()->getName() != "Options" 
                && true == bisViewDown) bisViewDown = false;

            if (VK_RETURN == key && getSelectedOption()->getName() == "Help") drawHelpOption();

            if (VK_RETURN == key && 1 == getSelectedOptionRow()) useSort();

            if (VK_RETURN == key && 0 != getSelectedOption()->getDropDownMenu().size()) 
            {
                drawDropDownMenu();
                do
                {
                    key = _getch();
                    if (224 == key) key = _getch();

                    if (72 == key) moveUpDropDownMenu();
                    else if (80 == key) moveDownDropDownMenu();

                    if (VK_RETURN == key && getSelectedDropDownOption()->getName() == " Currency ")
                    {
                        drawCurrencyOptions();
                        break;
                    }

                    else if (VK_RETURN == key && getSelectedDropDownOption()->getName() == " New ")
                    {
                        drawNewStock();
                        break;
                    }
                    else if (VK_RETURN == key && getSelectedDropDownOption()->getName() == " Load ")
                    {
                        drawLoadStock();
                        break;
                    }


                } while (VK_ESCAPE != key);
                resetSubmenuSelection();
                drawScreenInRange(0, 60, 0, 8);
            }

            this_thread::sleep_for(100ms);
        }
    }
    system("cls");

    space;
    doubleTab;
    std::cout << "You've quit! Have a nice day!";
    line;
    doubleTab;
    std::cout << "(Press any key) .....";
    space;
}




#pragma region CONSTRUCTION


static VisualPlantio* instance;

VisualPlantio* VisualPlantio::instance = nullptr;


VisualPlantio::VisualPlantio()
{
    bisViewDown = false;

    ptrPlantio = Plantio::getInstance();
    ptrCurrencies = Currency::getInstance();
    ptrCurrencies->setFileName("Misc/Currency.list");
    ptrCurrencies->getCurrencyList();
    ptrCurrencies->setActiveCurrency(L"RUB");

#pragma region TOP_MENU_OPTIONS


    MenuOption* stock = new MenuOption("Stock");
    std::vector<MenuOption*> ptrStockDropDownMenuOptions;
    MenuOption* stock_new = new MenuOption(" New ");
    stock_new->isSelected = true;
    MenuOption* stock_load = new MenuOption(" Load ");
    ptrStockDropDownMenuOptions.push_back(stock_new);
    ptrStockDropDownMenuOptions.push_back(stock_load);
    stock->setDropDownMenu(ptrStockDropDownMenuOptions);

    MenuOption* options = new MenuOption("Options");
    std::vector<MenuOption*> ptrOtpionsDropDownMenuOptions;
    MenuOption* options_currency = new MenuOption(" Currency ");
    options_currency->isSelected = true;
    MenuOption* options_font = new MenuOption(" Font ");
    ptrOtpionsDropDownMenuOptions.push_back(options_currency);
    //ptrOtpionsDropDownMenuOptions.push_back(options_font);
    options->setDropDownMenu(ptrOtpionsDropDownMenuOptions);

    MenuOption* view = new MenuOption("View");
    MenuOption* help = new MenuOption("Help");
    MenuOption* exit = new MenuOption("Exit");

    std::vector<MenuOption*> vTempVector;

    vTempVector.push_back(stock);
    vTempVector.push_back(view);
    vTempVector.push_back(options);
    vTempVector.push_back(help);
    vTempVector.push_back(exit);


#pragma endregion TOP_MENU_OPTIONS



    prepareDefaultSchedules();



#pragma region TABLE_MENU_OPTIONS


    MenuOption* species = new MenuOption("Species");

    MenuOption* fertilizer = new MenuOption("F");
    MenuOption* chemicals = new MenuOption("C");
    MenuOption* planting_price = new MenuOption("P");
    MenuOption* soil = new MenuOption("S");
    MenuOption* self_workload = new MenuOption("H");
    MenuOption* quantity = new MenuOption("Q");
    MenuOption* vermin_exposure = new MenuOption("L");

    MenuOption* revenue = new MenuOption("Revenue");

    MenuOption* days = new MenuOption("Days");


    vViewTableOptions.push_back(species);
    vViewTableOptions.push_back(fertilizer);
    vViewTableOptions.push_back(chemicals);
    vViewTableOptions.push_back(planting_price);
    vViewTableOptions.push_back(soil);
    vViewTableOptions.push_back(self_workload);
    vViewTableOptions.push_back(quantity);
    vViewTableOptions.push_back(vermin_exposure);
    vViewTableOptions.push_back(revenue);
    vViewTableOptions.push_back(days);


#pragma endregion TABLE_MENU_OPTIONS

    vAllMajorMenuOptions.push_back(vTempVector);
    vAllMajorMenuOptions.push_back(vViewTableOptions);

    Stock* stkDefaultStock = new Stock();
    stkDefaultStock->setName("Default");

    ptrPlantio->setActiveStock(stkDefaultStock);



}


VisualPlantio::~VisualPlantio()
{
    if (nullptr != ptrPlantio)
    {
        ptrPlantio->release();
        ptrPlantio = nullptr;
    }

    for (auto &option : vAllMajorMenuOptions)
    {
        for (auto& option1 : option)
        {
            if (nullptr != option1)
            {
                delete option1;
                option1 = nullptr;
            }
        }
    }

    for (auto& schedule : vDefaultSchedules)
    {
        if (nullptr != schedule)
        {
            delete schedule;
            schedule = nullptr;
        }
    }

    vDefaultSchedules.clear();
    vAllMajorMenuOptions.clear();
}


#pragma endregion CONSTRUCTION


#pragma region VISUALIZE


void VisualPlantio::init()
{
    vAllMajorMenuOptions.operator[](0).operator[](0)->isSelected = true;
    clear_screen;
}


void VisualPlantio::moveLeft()
{
    size_t nActiveOptionIndexVertical = 0ULL;
    size_t nActiveOptionIndexHorizontal = 0ULL;

    for (size_t j = 0ULL, nSize = vAllMajorMenuOptions.size(); j < nSize; j++)
    {
        for (size_t i = 0ULL, nSize1 = vAllMajorMenuOptions[j].size(); i < nSize1; i++)
        {
            if (vAllMajorMenuOptions[j].operator[](i)->isSelected)
            {
                nActiveOptionIndexVertical = j;
                nActiveOptionIndexHorizontal = i;
            }
        }
    }

    if (nActiveOptionIndexHorizontal != 0ULL)
    {
        vAllMajorMenuOptions[nActiveOptionIndexVertical].operator[](nActiveOptionIndexHorizontal)->isSelected = false;
        vAllMajorMenuOptions[nActiveOptionIndexVertical].operator[](nActiveOptionIndexHorizontal - 1)->isSelected = true;
        if (bisViewDown) drawViewTableMenu();
        refreshTopMenu();
    }
}


void VisualPlantio::moveRight()
{
    size_t nActiveOptionIndexVertical = 0ULL;
    size_t nActiveOptionIndexHorizontal = 0ULL;

    for (size_t j = 0ULL, nSize = vAllMajorMenuOptions.size(); j < nSize; j++)
    {
        for (size_t i = 0ULL, nSize1 = vAllMajorMenuOptions[j].size(); i < nSize1; i++)
        {
            if (vAllMajorMenuOptions[j].operator[](i)->isSelected)
            {
                nActiveOptionIndexVertical = j;
                nActiveOptionIndexHorizontal = i;
            }
        }
    }

    if (nActiveOptionIndexHorizontal != vAllMajorMenuOptions[nActiveOptionIndexVertical].size() - NUMERATION_ISSUE)
    {
        vAllMajorMenuOptions[nActiveOptionIndexVertical].operator[](nActiveOptionIndexHorizontal)->isSelected = false;
        vAllMajorMenuOptions[nActiveOptionIndexVertical].operator[](nActiveOptionIndexHorizontal + 1)->isSelected = true;
        if (bisViewDown) drawViewTableMenu();
        refreshTopMenu();
    }
}


void VisualPlantio::moveUpDropDownMenu()
{
    size_t nActiveOptionIndex = 0ULL;

    for (size_t i = 0ULL, nSize = getSelectedOption()->getDropDownMenu().size(); i < nSize; i++)
    {
        if (getSelectedOption()->getDropDownMenu().operator[](i)->isSelected) nActiveOptionIndex = i;
    }

    if (nActiveOptionIndex != 0ULL)
    {
        getSelectedOption()->getDropDownMenu().operator[](nActiveOptionIndex)->isSelected = false;
        getSelectedOption()->getDropDownMenu().operator[](nActiveOptionIndex - 1)->isSelected = true;

        drawDropDownMenu();
    }
}


void VisualPlantio::moveDownDropDownMenu()
{
    size_t nActiveOptionIndex = 0ULL;

    for (size_t i = 0ULL, nSize = getSelectedOption()->getDropDownMenu().size(); i < nSize; i++)
    {
        if (getSelectedOption()->getDropDownMenu().operator[](i)->isSelected) nActiveOptionIndex = i;
    }

    if (nActiveOptionIndex != getSelectedOption()->getDropDownMenu().size() - NUMERATION_ISSUE)
    {
        getSelectedOption()->getDropDownMenu().operator[](nActiveOptionIndex)->isSelected = false;
        getSelectedOption()->getDropDownMenu().operator[](nActiveOptionIndex + 1)->isSelected = true;

        drawDropDownMenu();
    }
}


void VisualPlantio::moveUpGeneralMenu()
{
    if (bisViewDown)
    {
        size_t nActiveOptionIndexVertical = 0ULL;
        size_t nActiveOptionIndexHorizontal = 0ULL;

        for (size_t j = 0ULL, nSize = vAllMajorMenuOptions.size(); j < nSize; j++)
        {
            for (size_t i = 0ULL, nSize1 = vAllMajorMenuOptions[j].size(); i < nSize1; i++)
            {
                if (vAllMajorMenuOptions[j].operator[](i)->isSelected)
                {
                    nActiveOptionIndexVertical = j;
                    nActiveOptionIndexHorizontal = i;
                }
            }
        }

        if (nActiveOptionIndexVertical != 0)
        {
            vAllMajorMenuOptions[1].operator[](nActiveOptionIndexHorizontal)->isSelected = false;
            vAllMajorMenuOptions[0].operator[](0)->isSelected = true;
            drawViewTableMenu();
            refreshTopMenu();
        }
    }
}


void VisualPlantio::moveDownGeneralMenu()
{
    if (bisViewDown)
    {
        size_t nActiveOptionIndexVertical = 0ULL;
        size_t nActiveOptionIndexHorizontal = 0ULL;

        for (size_t j = 0ULL, nSize = vAllMajorMenuOptions.size(); j < nSize; j++)
        {
            for (size_t i = 0ULL, nSize1 = vAllMajorMenuOptions[j].size(); i < nSize1; i++)
            {
                if (vAllMajorMenuOptions[j].operator[](i)->isSelected)
                {
                    nActiveOptionIndexVertical = j;
                    nActiveOptionIndexHorizontal = i;
                }
            }
        }

        if (nActiveOptionIndexVertical != vAllMajorMenuOptions.size() - NUMERATION_ISSUE)
        {
            vAllMajorMenuOptions[0].operator[](nActiveOptionIndexHorizontal)->isSelected = false;
            vAllMajorMenuOptions[1].operator[](0)->isSelected = true;
            drawViewTableMenu();
            refreshTopMenu();
        }
    }
}


void VisualPlantio::refreshTopMenu()
{
    drawScreenInRange(0, SCREEN_WIDTH, 0, 1);
}


void VisualPlantio::drawScreenInRange(size_t start_x, size_t end_x, size_t start_y, size_t end_y)
{


#pragma region STRINGS_HARDCODE
    size_t nVisibleOptionsNamesLength = 2;

    for (size_t i = 0ULL; i < vAllMajorMenuOptions[0].size(); i++)
    {
        nVisibleOptionsNamesLength += vAllMajorMenuOptions[0].operator[](i)->getName().length() + TWO_SPACES;
    }

    //
    //
    //

    std::string sTopMenuOptionsNames = " ";

    for (size_t i = 0ULL; i < vAllMajorMenuOptions[0].size(); i++)
    {
        sTopMenuOptionsNames.append(" ");
        sTopMenuOptionsNames.append(vAllMajorMenuOptions[0].operator[](i)->getName());
        sTopMenuOptionsNames.append(" ");
    }

#pragma endregion STRINGS_HARDCODE


    Toolbox::setCursor(start_x, start_y);
    for (size_t y = start_y; y < end_y; y++)
    {
        for (size_t x = start_x; x < end_x; x++)
        {
            // thin white lines on black background;
            if (1 == y && 0 == x) std::cout << BACKBLK << FOREWHT << L_U_S;
            else if (1 == y && SCREEN_WIDTH > x) std::cout << BACKBLK << FOREWHT << H_S; // probably here as well;
            else if (0 == x && 1 < y && SCREEN_HEIGHT - 1 > y) std::cout << BACKBLK << FOREWHT << V_S;

            else if (0 == y && sTopMenuOptionsNames.length() < x) std::cout << BACKWHT << " ";


#pragma region TOP_MENU_VISUALISATION

            else if (0 == y) // here!;
            {
                std::cout << BACKWHT << " ";
                for (size_t options = 0ULL, nSize = vAllMajorMenuOptions[0].size(); options < nSize; options++)
                {
                    (vAllMajorMenuOptions[0].operator[](options)->isSelected) ?
                        std::cout << BACKBLK << FOREWHT
                        << " " << vAllMajorMenuOptions[0].operator[](options)->getName() << " " << DEFAULT_TEXT :
                        std::cout << BACKWHT << FOREBLK
                        << " " << vAllMajorMenuOptions[0].operator[](options)->getName() << " " << DEFAULT_TEXT;
                    x += vAllMajorMenuOptions[0].operator[](options)->getName().length() + TWO_SPACES;
                }

                for (size_t v = x, nSize = end_x - NUMERATION_ISSUE; v < nSize; v++, x++)
                {
                    std::cout << BACKWHT << " ";
                }
            }

#pragma endregion TOP_MENU_VISUALISATION


            // bottom white line;
            else if (SCREEN_HEIGHT-1 == y && SCREEN_WIDTH > x) std::cout << BACKWHT << " ";

            else if (1 < y && 0 < x && SCREEN_HEIGHT - 1 > y) std::cout << BACKBLU << " ";

            else std::cout << BACKBLK << " ";
        }
        if (0 != start_y) Toolbox::setCursor(start_x, y + NEW_ELEMENT); // X3;
        else Toolbox::setCursor(start_x, y + NEW_ELEMENT + NUMERATION_ISSUE);
    }
    Toolbox::setCursor(0, 0);
    Toolbox::deleteCursor();
    std::cout << DEFAULT_TEXT;
}


void VisualPlantio::drawDropDownMenu()
{
    size_t start_x;
    if ("Stock" == getSelectedOption()->getName())
    {
        start_x = STOCK_MENU_X;
    }
    else if ("Options" == getSelectedOption()->getName())
    {
        start_x = OPTIONS_MENU_X;

    }

        size_t start_y = DROP_DOWN_MENU_Y;

        size_t max_y = getSelectedOption()->getDropDownMenu().size() + TWO_SPACES + NUMERATION_ISSUE;

        size_t current_x = start_x;
        size_t current_y = start_y;

        for (size_t y = current_y, i = 0ULL; y < max_y + NUMERATION_ISSUE; y++)
        {
            for (size_t x = current_x; x < start_x + DROP_DOWN_MENU_WIDTH; x++)
            {
                std::cout << FOREBLK << BACKWHT;
                Toolbox::setCursor(x, y);

                // vertical & horizontal;
                if ((current_x == x || start_x + DROP_DOWN_MENU_WIDTH - 1 == x) && (y != start_y && y != max_y))
                    std::cout << V_S;
                else if ((current_x != x && start_x + DROP_DOWN_MENU_WIDTH - 1 != x) && (start_y == y || max_y == y))
                    std::cout << H_S;

                // corners;
                else if (y == current_y && x == current_x) std::cout << L_U_S;
                else if (current_y == y && start_x + DROP_DOWN_MENU_WIDTH - 1 == x) std::cout << R_U_S;
                else if (max_y == y && start_x == x) std::cout << L_B_S;
                else if (max_y == y && start_x + DROP_DOWN_MENU_WIDTH - 1 == x) std::cout << R_B_S;
                else if (max_y != y && start_y != y && start_x + DROP_DOWN_MENU_WIDTH - 1 != x && start_x != x) std::cout << " ";
            }
        }

        for (size_t i = 0ULL, nSize = getSelectedOption()->getDropDownMenu().size(); i < nSize; i++)
        {
            Toolbox::setCursor(current_x + TWO_SPACES, current_y + NUMERATION_ISSUE);
            if (getSelectedOption()->getDropDownMenu().operator[](i)->isSelected) std::cout << FOREWHT << BACKBLK;
            else std::cout << FOREBLK << BACKWHT;

            std::cout << getSelectedOption()->getDropDownMenu().operator[](i)->getName();
            current_y += 1;
        }


#pragma region SHADOW

        current_x = start_x;
        current_y = start_y;

        for (size_t i = 0ULL; i < DROP_DOWN_MENU_WIDTH; i++)
        {
            Toolbox::setCursor(current_x, max_y + 1);
            std::cout << BACKBLK << " ";
            current_x += 1;
        }

        current_x = start_x;
        current_y = start_y;

        for (size_t i = 0ULL; i < max_y; i++)
        {
            std::cout << BACKBLK;

            Toolbox::setCursor(start_x + DROP_DOWN_MENU_WIDTH, current_y);
            std::cout << " ";
            Toolbox::setCursor(start_x + DROP_DOWN_MENU_WIDTH + 1, current_y);
            std::cout << " ";
            current_y += 1;
        }

#pragma endregion SHADOW


    

}


void VisualPlantio::drawView()
{
    if (!bisViewDown)
    {
        clear_screen;
    }

    drawViewTableMenu();

    drawSchedulesList();

    bisViewDown = true;
}


void VisualPlantio::drawViewTableMenu()
{
    Toolbox::setCursor(12, 12);

    drawOption(vViewTableOptions[0]);


    Toolbox::setCursor(38, 12);

    for (size_t i = 1ULL; i < 8; i++)
    {
        drawOption(vViewTableOptions[i]);
        std::cout << BACKBLU << FOREWHT << " ";
    }


    Toolbox::setCursor(57, 12);

    drawOption(vViewTableOptions[8]);

    Toolbox::setCursor(71, 12);

    drawOption(vViewTableOptions[9]);

    std::cout << RESETTEXT;
}


void VisualPlantio::drawSchedulesList()
{
    if (ptrPlantio->getCurrentStock()->getName() != "Default")
    {

        loadCurrentSchedulesDecorators();

        //expanding field;
        if (!bisViewDown)
        {
            Toolbox::setCursor(0, SCREEN_HEIGHT);
            for (size_t y = 0; y < ptrPlantio->getCurrentStockSchedules().size(); y++)
            {
                for (size_t x = 0ULL; x < SCREEN_WIDTH; x++)
                {
                    if (1 <= x) std::cout << FOREBLK << BACKBLU << " ";
                    else std::cout << BACKBLK << FOREWHT << V_S;
                }
                line;
            }
        }

        std::cout << DEFAULT_TEXT;

        Toolbox::setCursor(0, 0);


        size_t current_x = 10, current_y = 14;
        Toolbox::setCursor(current_x, current_y);

        for (size_t i = 0ULL; i < ptrPlantio->getCurrentStockSchedules().size(); i++)
        {
            Toolbox::setCursor(current_x, current_y);
            vCurrentSchedules[i]->drawSchedule(current_x, current_y, getColorSchemeForSchedule(vCurrentSchedules[i]->getSchedule()));
            //current_y += 4;

            if (current_y + 4 <= SCREEN_HEIGHT) current_y += 2;
            else
            {
                for (size_t c = 0ULL; c < 2; c++) line;
                current_y += 2;
            }
        }

        Toolbox::setCursor(0, 0);
    }
    else
    {
        //expanding field;
        if (!bisViewDown)
        {
            Toolbox::setCursor(0, SCREEN_HEIGHT);
            for (size_t y = 0; y < vDefaultSchedules.size(); y++)
            {
                for (size_t x = 0ULL; x < SCREEN_WIDTH; x++)
                {
                    if (1 <= x) std::cout << FOREBLK << BACKBLU << " ";
                    else std::cout << BACKBLK << FOREWHT << V_S;
                }
                line;
            }
        }

        std::cout << DEFAULT_TEXT;

        Toolbox::setCursor(0, 0);


        size_t current_x = 10, current_y = 14;
        Toolbox::setCursor(current_x, current_y);

        for (size_t i = 0ULL; i < vDefaultSchedules.size(); i++)
        {
            Toolbox::setCursor(current_x, current_y);
            vDefaultSchedules[i]->drawSchedule(current_x, current_y, getColorSchemeForSchedule(vDefaultSchedules[i]->getSchedule()));
            //current_y += 4;

            if (current_y + 4 <= SCREEN_HEIGHT) current_y += 2;
            else
            {
                for (size_t c = 0ULL; c < 2; c++) line;
                current_y += 2;
            }
        }

        Toolbox::setCursor(0, 0);
    }
}


void VisualPlantio::drawOption(MenuOption* option)
{
    if (option->isSelected) std::cout << BACKBLK << FOREBLU;
    else std::cout << BACKBLU << FOREBLK;
    std::cout << option->getName();
}


void VisualPlantio::drawCurrencyOptions()
{
    size_t start_y = DROP_DOWN_MENU_Y + 1;
    size_t start_x = CURRENCY_MENU_X;

    size_t current_y = start_y;
    size_t current_x = start_x;

    size_t max_y = start_y + 4 + TWO_SPACES;
    size_t max_x = start_x + CURRENCY_DROP_DOWN_MENU_WIDTH;
    
    Toolbox::setCursor(start_x, start_y);
    
    for (size_t y = start_y; y < max_y; y++)
    {
        for (size_t x = start_x; x < max_x; x++)
        {
            Toolbox::setCursor(x,y);
            std::cout << BACKWHT << FOREBLK;
            if ((current_x == x || max_x - 1 == x) && (y != start_y && y != max_y - 1))
                std::cout << V_S;
            else if ((current_x != x && max_x - 1 != x) &&
                (start_y == y || max_y - NUMERATION_ISSUE == y))
                std::cout << H_S;

            // corners;
            else if (y == current_y && x == current_x) std::cout << L_U_S;
            else if (current_y == y && max_x - 1 == x) std::cout << R_U_S;
            else if (max_y - NUMERATION_ISSUE == y && current_x == x) std::cout << L_B_S;
            else if (max_y - NUMERATION_ISSUE == y && max_x - 1 == x) std::cout << R_B_S;
            else if (max_y != y && start_y != y &&
                start_x + CURRENCY_DROP_DOWN_MENU_WIDTH - 1 != x && start_x != x) std::cout << " ";
        }
    }

#pragma region SHADOW

    current_y = max_y;
    current_x = start_x;

    Toolbox::setCursor(current_x, current_y);

    for (size_t x = start_x; x < max_x; x++)
    {
        Toolbox::setCursor(x, current_y);
        std::cout << BACKBLK << " ";
    }

    for (size_t y = start_y; y != max_y + 1; y++)
    {
        Toolbox::setCursor(max_x, y);
        std::cout << BACKBLK << "  ";
    }

    std::cout << DEFAULT_TEXT;

#pragma endregion SHADOW

    current_y = start_y + NUMERATION_ISSUE;
    current_x = start_x + NUMERATION_ISSUE + TWO_SPACES;

    Toolbox::setCursor(current_x, current_y);
    std::cout << FOREBLK << BACKWHT << "\tPlease, specify the name of currency";
    current_y += 1;
    Toolbox::setCursor(current_x, current_y);
    std::cout << FOREBLK << BACKWHT << "that you want to work with (";

    for (size_t i = 0ULL, nSize = ptrCurrencies->getCurrencyList().size(); i < nSize; i++)
    {
        auto unit = ptrCurrencies->getCurrencyList()[i];
        // underlined text;
        if (unit == ptrCurrencies->getActiveCurrency())
            std::wcout << "\033[4m" << unit << "\033[0m" << BACKWHT << FOREBLK;
        else std::wcout << unit;
        if (i != nSize - 1) std::cout << ", ";
    }

    std::cout << ")";

    current_y += 1;
    Toolbox::setCursor(current_x, current_y);
    current_y += 1;
    Toolbox::setCursor(current_x, current_y);
    std::cout << FOREBLK << BACKWHT << "You: ";

     // here!

     getUserCurrencyInput();

     drawScreenInRange(0, 94, 0, 9);

     if (bisViewDown) drawView();

     return;

}


void VisualPlantio::getUserCurrencyInput()
{
    wint_t key;

    string sUserCurrencyInput = "";

    vector<wint_t> vUserCurrencyInput;

    wstring sUserInputString;

    ptrCurrencies->getCurrencyList();

    do
    {
        Toolbox::setCursor(CURRENCY_USER_INPUT_X, CURRENCY_USER_INPUT_Y);

        do
        {
            if (_kbhit) key = _getwche();

            if ((key != VK_ESCAPE && key != VK_RETURN)) vUserCurrencyInput.push_back(key);

        } while (key != VK_ESCAPE && key != VK_RETURN && vUserCurrencyInput.size() < 10);

        sUserInputString = L"";

        for (auto unit : vUserCurrencyInput)
        {
            sUserInputString.push_back(wchar_t(unit));
        }

        if (ptrCurrencies->setActiveCurrency(sUserInputString) != true)
        {
            sUserInputString = L"";
            vUserCurrencyInput.clear();
        }
        else break;

        Toolbox::setCursor(CURRENCY_USER_INPUT_X, CURRENCY_USER_INPUT_Y);
        std::cout << BACKWHT;
        for (size_t i = 0ULL; i < 15; i++) std::cout << " ";

    } while (key != VK_ESCAPE);

    if (bisViewDown) drawView();
}


void VisualPlantio::drawHelpOption()
{
    string* sHelp = new string("Help");
    drawDialogeSheet(*sHelp);

    size_t nCurrentY = WHITE_SHEET_START_Y;

    wstring wsHelpText = L"";

    wsHelpText += wFOREBLK;

    // string one;

    wsHelpText += L"\t||To visualize possible profits and spendings this program packs plants into so-called ";

    wsHelpText += wFOREGRN;

    wsHelpText += L"Schedules";

    wsHelpText += wFOREBLK;
    
    wsHelpText += L".|";

    // string two;

    wsHelpText += L"Like those you can see in a ";

    wsHelpText += wFOREGRN;

    wsHelpText += L"View";

    wsHelpText += wFOREBLK;

    wsHelpText += L" tab. Apart from obvious pieces of data they also have some encrypted stats |";

    // string three;

    wsHelpText += L"that you can find below:|";



    // list of traits;

    wsHelpText += wFOREGRN;

    wsHelpText += L"F";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - firtilizer need,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"C";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - chemicals need,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"P";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - planting material price,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"S";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - soil need,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"H";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - hardwork need (i.e. self workload),|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"Q";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - quantity of product units (more plants => more workload),|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"L";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - luck (i.e. vermin exposure).|||";

    //;

    //;

    //;

    // string one;

    /*

    wsHelpText += L"\tДля визуализации возможных выгод и затрат, программа группирует растения в так называемые";

    wsHelpText += wFOREGRN;

    wsHelpText += L"Схемы (Schedules)";

    wsHelpText += wFOREBLK;

    wsHelpText += L".|";

    // string two;

    wsHelpText += L"Как те, что вы можете видеть во вкладке ";

    wsHelpText += wFOREGRN;

    wsHelpText += L"Вид (View)";

    wsHelpText += wFOREBLK;

    wsHelpText += L". Кроме очевидных ключевых слов, там имеются так же зашифрованные характеристики, |";

    // string three;

    wsHelpText += L"с которыми вы можете ознакомиться ниже:|";



    // list of traits;

    wsHelpText += wFOREGRN;

    wsHelpText += L"F";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - необходимость в удобрении,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"C";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - потребность в ядохимикатах,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"P";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - стоимость посадочного материала,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"S";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - необходимое кол-во почвы,|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"H";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - трудозатраты (собственные),|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"Q";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - кол-во продукта в единицах (больше продукта => больше нести),|";

    //;

    wsHelpText += wFOREGRN;

    wsHelpText += L"L";

    wsHelpText += wFOREBLK;

    wsHelpText += L" - \"удача\" (т.е. подверженность вредителям).|";
    */

    //;


    Toolbox::setCursor(WHITE_SHEET_START_X, WHITE_SHEET_START_Y);

    for (size_t i = 0ULL, nSize = wsHelpText.length(); i < nSize; i++)
    {
        if ('|' != wsHelpText[i]) std::wcout << wsHelpText[i];
        else
        {
            nCurrentY += 1;
            Toolbox::setCursor(WHITE_SHEET_START_X + TWO_SPACES, nCurrentY);
        }
    }

    Toolbox::deleteCursor();

    delete sHelp;
}


void VisualPlantio::drawDialogeSheet(std::string& sHeaderText)
{
    clear_screen_view;

    std::cout << BACKWHT << FOREBLK;

    for (size_t i = WHITE_SHEET_START_Y, nHeightSize = SCREEN_HEIGHT - WHITE_SHEET_START_Y + TWO_SPACES; i < nHeightSize; i++)
    {
        for (size_t j = WHITE_SHEET_START_X, nWidwnSize = SCREEN_WIDTH - WHITE_SHEET_START_X; j < nWidwnSize; j++)
        {
            Toolbox::setCursor(j, i);
            if (WHITE_SHEET_START_Y == i && WHITE_SHEET_START_X != j && nWidwnSize - 1 != j ||
                nHeightSize - 1 == i && WHITE_SHEET_START_X != j && nWidwnSize - 1 != j) std::cout << H_D;

            else if (WHITE_SHEET_START_Y == i && WHITE_SHEET_START_X == j) std::cout << L_U_D;
            else if (WHITE_SHEET_START_Y == i && nWidwnSize - 1 == j) std::cout << R_U_D;
            else if (nHeightSize - 1 == i && WHITE_SHEET_START_X == j) std::cout << L_B_D;
            else if (nHeightSize - 1 == i && nWidwnSize - 1 == j) std::cout << R_B_D;

            else if ((WHITE_SHEET_START_Y != i && nHeightSize - 1 != i) && (WHITE_SHEET_START_X == j || nWidwnSize - 1 == j)) std::cout << V_D;

            else std::cout << " ";

        }

        if (nHeightSize - 1 == i)
        {
            Toolbox::setCursor((SCREEN_WIDTH - WHITE_SHEET_START_X - WHITE_SHEET_START_X) / 2, WHITE_SHEET_START_Y);
            std::cout << " " << sHeaderText << " ";
        }
    }
}


void VisualPlantio::drawNewStock()
{
    string* sMessageText = new string("New Stock");


    drawDialogeSheet(*sMessageText);


    Toolbox::setCursor(10,14);
    std::cout << BACKWHT << FOREBLK;

    std::cout << "Name: ";

    Toolbox::setCursor(10, 16);
    std::cout << BACKWHT << FOREBLK;

    std::cout << "Soil: ";


    wint_t key;

    string sUserCurrencyInput = "";

    vector<wint_t> vUserStockNameInput;

    wstring sUserInputString;

    Toolbox::setCursor(8, 14);
    std::cout << BACKWHT << FOREBLU;
    std::cout << ">";
    std::cout << BACKWHT << FOREBLK;
    Toolbox::setCursor(16, 14);

    do
    {
        if (_kbhit) key = _getwche();

        if ((key != VK_ESCAPE && key != VK_RETURN)) vUserStockNameInput.push_back(key);

    } while (key != VK_ESCAPE && key != VK_RETURN && vUserStockNameInput.size() < 10);

    sUserInputString = L"";

    if (vUserStockNameInput.size() == 0) sUserInputString = L"unnamed";
    else
    {

        for (auto unit : vUserStockNameInput)
        {
            sUserInputString.push_back(wchar_t(unit));
        }
    }

    Toolbox::setCursor(16, 14);
    std::cout << BACKWHT;


    Toolbox::setCursor(8, 14);
    std::cout << " ";
    


    double nSoilValueInput = 0.0;

    key = 1;


    Toolbox::setCursor(8, 16);
    std::cout << BACKWHT << FOREBLU;
    std::cout << ">";
    std::cout << BACKWHT << FOREBLK;
    Toolbox::setCursor(16, 16);

    double dSoilInput = 0.0;

    try
    {
        cin >> dSoilInput;
    }
    catch (...)
    {
        dSoilInput = 0.0;
    }

    Toolbox::setCursor(16, 14);
    std::cout << BACKWHT;


    Toolbox::setCursor(8, 16);
    std::cout << BACKWHT << FOREBLU;
    std::cout << " ";

    Soil* newStockSoil = new Soil(dSoilInput);

    string nsStockName(sUserInputString.begin(), sUserInputString.end());

    Stock* newStock = new Stock(newStockSoil);
    (nsStockName != "") ? newStock->setName(nsStockName) : newStock->setName("Unknown");

    ptrPlantio->setActiveStock(newStock);

    delete sMessageText;

    clear_screen_view;
}


void VisualPlantio::drawLoadStock()
{
    std::string* sText = new std::string("Load Stock");

    drawDialogeSheet(*sText);

    wint_t key;

    string sUserCurrencyInput = "";

    vector<wint_t> vUserStockNameInput;

    wstring sUserInputString;

    bool isThisStockReal = false;

    wstring wsCurrentStockName;

    Stock* NewActiveStock;

    Toolbox::setCursor(10, 14);

    std::cout << "Stocks: ";

    Toolbox::setCursor(20, 14);

    for (size_t i = 0, j = -1, nSize = ptrPlantio->getStocksList().size(); i < nSize; i++)
    {
        while (i % 5 != 0 && i < nSize)
        {
            if (i < ptrPlantio->getStocksList().size() - 1) std::cout << ptrPlantio->getStocksList()[i]->getName() << ", ";
            else std::cout << ptrPlantio->getStocksList()[i]->getName() << ".";
            i++;
        }
        if (i % 5 == 0) j += 1;
        Toolbox::setCursor(20, 14 + j);
    }

    do
    {

        Toolbox::setCursor(10, 16);
        std::cout << BACKWHT << FOREBLK;

        std::cout << "Name: ";
        for (size_t i = 0; i < 15; ++i) std::cout << " ";

        Toolbox::setCursor(8, 16);
        std::cout << BACKWHT << FOREBLU;
        std::cout << ">";
        std::cout << BACKWHT << FOREBLK;
        Toolbox::setCursor(16, 16);

        do
        {
            if (_kbhit) key = _getwche();

            if ((key != VK_ESCAPE && key != VK_RETURN)) vUserStockNameInput.push_back(key);

        } while (key != VK_ESCAPE && key != VK_RETURN);

        sUserInputString = L"";

        for (auto unit : vUserStockNameInput)
        {
            sUserInputString.push_back(wchar_t(unit));
        }

        for (auto unit : ptrPlantio->getStocksList())
        {
            Toolbox::StringToWString(wsCurrentStockName, unit->getName());
            if (wsCurrentStockName == sUserInputString)
            {
                isThisStockReal = true;
                ptrPlantio->setActiveStock(unit);
                break;
            }
        }

    } while (!isThisStockReal && (key != VK_ESCAPE && key != VK_RETURN));

    clear_screen_view;

    // write your code here ...

    delete sText;
}


#pragma endregion VISUALIZE


#pragma region SORTINGS


void VisualPlantio::useSort()
{
    using_sorter;

    if ("Default" != ptrPlantio->getCurrentStock()->getName())
    {
        sorter->sort(getSelectedOption()->getName(), vCurrentSchedules);
    }
    else
    {
        sorter->sort(getSelectedOption()->getName(), vDefaultSchedules);
    }

    drawView();
}


#pragma endregion SORTINGS


#pragma region AUXILIARY


void VisualPlantio::prepareDefaultSchedules()
{
    ScheduleDecorator* decorator;
    for (Schedule* sch : ptrPlantio->getDefaultSchedulesList())
    {
        decorator = new ScheduleDecorator(sch);
        vDefaultSchedules.push_back(decorator);
    }
}


void VisualPlantio::loadCurrentSchedulesDecorators()
{
    ScheduleDecorator* decorator;
    for (Schedule* sch : ptrPlantio->getCurrentStockSchedules())
    {
        decorator = new ScheduleDecorator(sch);
        vCurrentSchedules.push_back(decorator);
    }
}


MenuOption* VisualPlantio::getSelectedOption()
{
    MenuOption* result = nullptr;

    for (size_t j = 0ULL; j < vAllMajorMenuOptions.size(); j++)
    {
        for (size_t i = 0ULL; i < vAllMajorMenuOptions[j].size(); i++)
        {
            if (vAllMajorMenuOptions[j].operator[](i)->isSelected)
            {
                result = vAllMajorMenuOptions[j].operator[](i);
                break;
            }
            if (nullptr != result) break;
        }
    }

    return result;
}


size_t VisualPlantio::getSelectedOptionRow()
{
    size_t res = 0ULL;
    for (size_t i = 0ULL; i < vAllMajorMenuOptions.size(); i++)
    {
        for (size_t j = 0ULL; j < vAllMajorMenuOptions[i].size(); j++)
        {
            if (vAllMajorMenuOptions[i][j]->isSelected) return res = i;
        }
    }
    return res;
}


MenuOption* VisualPlantio::getSelectedDropDownOption()
{
    MenuOption* result = nullptr;

    for (size_t i = 0ULL; i < getSelectedOption()->getDropDownMenu().size(); i++)
    {
        if (getSelectedOption()->getDropDownMenu().operator[](i)->isSelected)
        {
            result = getSelectedOption()->getDropDownMenu().operator[](i);
        }
    }

    return result;
}


void VisualPlantio::resetSubmenuSelection() 
{
    getSelectedDropDownOption()->isSelected = false;
    getSelectedOption()->getDropDownMenu()[0]->isSelected = true;
}


vector<string*> VisualPlantio::getColorSchemeForSchedule(Schedule* ptrSchedule)
{
    string* sTempString = new string("");
    vector<string*> res;

    Plant* pDefaultSchedulesMiddleValues = new Plant();
    Schedule* defaultPlantShell = new Schedule(pDefaultSchedulesMiddleValues);
    defaultPlantShell->setPlantsQuantity(0);


    for (auto unit : vDefaultSchedules)
    {
        pDefaultSchedulesMiddleValues->setFertilizerNeed(pDefaultSchedulesMiddleValues->getFertilizerNeed() +
            unit->getSchedule()->getPlant()->getFertilizerNeed());

        pDefaultSchedulesMiddleValues->setChemicalsNeed(pDefaultSchedulesMiddleValues->getChemicalsNeed() +
            unit->getSchedule()->getPlant()->getChemicalsNeed());

        pDefaultSchedulesMiddleValues->setPlantingPrice(pDefaultSchedulesMiddleValues->getPlantingPrice() +
            unit->getSchedule()->getPlant()->getPlantingPrice());

        pDefaultSchedulesMiddleValues->setSelfloadRequired(pDefaultSchedulesMiddleValues->getSelfloadRequired() +
            unit->getSchedule()->getPlant()->getSelfloadRequired());

        pDefaultSchedulesMiddleValues->setSoilNeedRatio(pDefaultSchedulesMiddleValues->getSoilNeedRatio() +
            unit->getSchedule()->getPlant()->getSoilNeedRatio());

        //pDefaultSchedulesMiddleValues->setVerminExposure(pDefaultSchedulesMiddleValues->getVerminExposure() +
            //unit->getSchedule()->getPlant()->getVerminExposure());

        defaultPlantShell->setPlantsQuantity(defaultPlantShell->getPlantsQuantity() + unit->getSchedule()->getPlantsQuantity());
    }

    size_t nSize = vDefaultSchedules.size();

    pDefaultSchedulesMiddleValues->setFertilizerNeed(pDefaultSchedulesMiddleValues->getFertilizerNeed() / nSize);

    pDefaultSchedulesMiddleValues->setChemicalsNeed(pDefaultSchedulesMiddleValues->getChemicalsNeed() / nSize);

    pDefaultSchedulesMiddleValues->setPlantingPrice(pDefaultSchedulesMiddleValues->getPlantingPrice() / nSize);

    pDefaultSchedulesMiddleValues->setSelfloadRequired(pDefaultSchedulesMiddleValues->getSelfloadRequired() / nSize);

    pDefaultSchedulesMiddleValues->setSoilNeedRatio(pDefaultSchedulesMiddleValues->getSoilNeedRatio() / nSize);

    //pDefaultSchedulesMiddleValues->setVerminExposure(pDefaultSchedulesMiddleValues->getVerminExposure() / nSize);

    defaultPlantShell->setPlantsQuantity(defaultPlantShell->getPlantsQuantity() / nSize);


    if ((pDefaultSchedulesMiddleValues->getFertilizerNeed() + (0.16 * pDefaultSchedulesMiddleValues->getFertilizerNeed())) <
        ptrSchedule->getPlant()->getFertilizerNeed()) sTempString->append(FOREGRN);
    else if ((pDefaultSchedulesMiddleValues->getFertilizerNeed() + (0.16 * pDefaultSchedulesMiddleValues->getFertilizerNeed())) >
        ptrSchedule->getPlant()->getFertilizerNeed() &&
        (pDefaultSchedulesMiddleValues->getFertilizerNeed() - (0.16 * pDefaultSchedulesMiddleValues->getFertilizerNeed())) <
        ptrSchedule->getPlant()->getFertilizerNeed()) sTempString->append(FOREYEL);
    else sTempString->append(FORERED);

    res.push_back(sTempString);
    sTempString = new string("");


    if ((pDefaultSchedulesMiddleValues->getChemicalsNeed() + (0.16 * pDefaultSchedulesMiddleValues->getChemicalsNeed())) <
        ptrSchedule->getPlant()->getChemicalsNeed()) sTempString->append(FOREGRN);
    else if ((pDefaultSchedulesMiddleValues->getChemicalsNeed() + (0.16 * pDefaultSchedulesMiddleValues->getChemicalsNeed())) >
        ptrSchedule->getPlant()->getChemicalsNeed() &&
        (pDefaultSchedulesMiddleValues->getChemicalsNeed() - (0.16 * pDefaultSchedulesMiddleValues->getChemicalsNeed())) <
        ptrSchedule->getPlant()->getChemicalsNeed()) sTempString->append(FOREYEL);
    else sTempString->append(FORERED);

    res.push_back(sTempString);
    sTempString = new string("");


    if ((pDefaultSchedulesMiddleValues->getPlantingPrice() + (0.16 * pDefaultSchedulesMiddleValues->getPlantingPrice())) <
        ptrSchedule->getPlant()->getPlantingPrice()) sTempString->append(FOREGRN);
    else if ((pDefaultSchedulesMiddleValues->getPlantingPrice() + (0.16 * pDefaultSchedulesMiddleValues->getPlantingPrice())) >
        ptrSchedule->getPlant()->getPlantingPrice() &&
        (pDefaultSchedulesMiddleValues->getPlantingPrice() - (0.16 * pDefaultSchedulesMiddleValues->getPlantingPrice())) <
        ptrSchedule->getPlant()->getPlantingPrice()) sTempString->append(FOREYEL);
    else sTempString->append(FORERED);

    res.push_back(sTempString);
    sTempString = new string("");


    if ((pDefaultSchedulesMiddleValues->getSoilNeedRatio() + (0.16 * pDefaultSchedulesMiddleValues->getSoilNeedRatio())) <
        ptrSchedule->getPlant()->getSoilNeedRatio()) sTempString->append(FOREGRN);
    else if ((pDefaultSchedulesMiddleValues->getSoilNeedRatio() + (0.16 * pDefaultSchedulesMiddleValues->getSoilNeedRatio())) >
        ptrSchedule->getPlant()->getSoilNeedRatio() &&
        (pDefaultSchedulesMiddleValues->getSoilNeedRatio() - (0.16 * pDefaultSchedulesMiddleValues->getSoilNeedRatio())) <
        ptrSchedule->getPlant()->getSoilNeedRatio()) sTempString->append(FOREYEL);
    else sTempString->append(FORERED);

    res.push_back(sTempString);
    sTempString = new string("");


    if ((pDefaultSchedulesMiddleValues->getSelfloadRequired() + (0.16 * pDefaultSchedulesMiddleValues->getSelfloadRequired())) <
        ptrSchedule->getPlant()->getSelfloadRequired()) sTempString->append(FOREGRN);
    else if ((pDefaultSchedulesMiddleValues->getSelfloadRequired() + (0.16 * pDefaultSchedulesMiddleValues->getSelfloadRequired())) >
        ptrSchedule->getPlant()->getSelfloadRequired() &&
        (pDefaultSchedulesMiddleValues->getSelfloadRequired() - (0.16 * pDefaultSchedulesMiddleValues->getSelfloadRequired())) <
        ptrSchedule->getPlant()->getSelfloadRequired()) sTempString->append(FOREYEL);
    else sTempString->append(FORERED);

    res.push_back(sTempString);
    sTempString = new string("");

    // the signes are reversed cause more plants == more weight to pull (?);
    if ((defaultPlantShell->getPlantsQuantity() + (0.16 * defaultPlantShell->getPlantsQuantity())) >
        ptrSchedule->getPlantsQuantity()) sTempString->append(FOREGRN);
    else if ((defaultPlantShell->getPlantsQuantity() + (0.16 * defaultPlantShell->getPlantsQuantity())) <
        ptrSchedule->getPlantsQuantity() &&
        (defaultPlantShell->getPlantsQuantity() - (0.16 * defaultPlantShell->getPlantsQuantity())) >
        ptrSchedule->getPlantsQuantity()) sTempString->append(FOREYEL);
    else sTempString->append(FORERED);

    res.push_back(sTempString);
    sTempString = new string("");


    if (ptrSchedule->getPlant()->getVerminExposure() == 5) sTempString->append(FORERED);
    else if (ptrSchedule->getPlant()->getVerminExposure() >=
        2 &&
        ptrSchedule->getPlant()->getVerminExposure() <= 4)
        sTempString->append(FOREYEL);
    else sTempString->append(FOREGRN);

    res.push_back(sTempString);

    return res;
}


#pragma endregion AUXILIARY