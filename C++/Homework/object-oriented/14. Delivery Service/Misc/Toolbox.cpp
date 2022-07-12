#include "Toolbox.h"


#define GETCH_CHAR_TO_INT 48 // The starting number of digits returned by conio::getch, for quick parsing;
#define CIN_CHAR_TO_INT 35 // The starting number of digits returned by iostream::cin, for quick parsing;



std::string Toolbox::getCurrentTime()
{
    time_t now = time(NULL);
    char* time = ctime(&now);
    std::string result;

    for (size_t i = 0ULL; i < strlen(time); i++)
    {
        result.push_back(time[i]);
    }

    return result;
}


void Toolbox::setCursor(int x, int y)
{
    std::cout << "\033[" << std::to_string(y) << ";" << std::to_string(x) << "f";
}


void Toolbox::clearScreenInRange(size_t x_start, size_t x_end, size_t y_start, size_t y_end)
{
    if (x_start > x_end) std::swap(x_start, x_end);
    if (y_start > y_end) std::swap(y_start, y_end);
    for (size_t i = y_start; i <= y_end; i++)
    {
        for (size_t j = x_start; j <= x_end; j++)
        {
            setCursor(j, i);
            std::cout << RESETTEXT << " ";
        }
    }
}



int Toolbox::getchSelector(size_t nOptionsQuantity)
{
    char user;
    do
    {
        user = _getwch();
    } while ((int)user < GETCH_CHAR_TO_INT && (int)user > GETCH_CHAR_TO_INT + nOptionsQuantity); // In case of IllegalArgument

    return (int)user - GETCH_CHAR_TO_INT;

}


int Toolbox::cinSelector(size_t nOptionsQuantity)
{
    size_t user;
    do
    {
        std::cin >> user;
    } while ((int)user < 0 || (int)user > nOptionsQuantity); // In case of IllegalArgument

    return (int)user;

}


int Toolbox::getchSelector(size_t nLowerValue, size_t nOptionsQuantity)
{
    char user;
    do
    {
        user = _getwch();
    } while ((int)user < nLowerValue + GETCH_CHAR_TO_INT && (int)user > GETCH_CHAR_TO_INT + nOptionsQuantity); // In case of IllegalArgument

    return (int)user - GETCH_CHAR_TO_INT;

}


int Toolbox::cinSelector(size_t nLowerValue, size_t nOptionsQuantity)
{
    size_t user;
    do
    {
        std::cin >> user;
    } while ((int)user < nLowerValue || (int)user > nOptionsQuantity); // In case of IllegalArgument

    return (int)user;

}


void Toolbox::showContinue()
{
    const int len = 47;
    std::cout << "\t" << char(201);
    for (int i = 0; i < len; i++)
    {
        std::cout << char(205);
    }
    std::cout << char(187);
    std::cout << "\n\t" << char(186);

    std::cout << "  Do you want to continue the programme? (y/n) ";

    std::cout << char(186) << "\n";
    std::cout << "\t" << char(200);
    for (int i = 0; i < len; i++)
    {
        std::cout << char(205);
    }
    std::cout << char(188) << "\n";
}