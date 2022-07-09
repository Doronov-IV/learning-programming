#include "Toolbox.h"


#define CHAR_TO_INT 48 // The starting number of digits in chars table, for quick parsing;


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


std::string* Toolbox::getPoolNameArray()
{
    FileBufferedReader* reader = new FileBufferedReader();

    reader->read("Resources/NamePool.txt");

    int size = reader->getBufferSize();

    std::string* names_array = new std::string[reader->getBufferSize()];

    for (size_t i = 0ULL; i < size; i++)
    {
        for (size_t j = 0ULL; j < strlen(reader->getBuffer()[i]); j++)
        {
            names_array[i].push_back(reader->getBuffer()[i][j]);
        }
    }

    return names_array;
}


size_t Toolbox::getNamePoolSize()
{
    return 12;
}


int Toolbox::selector(int nOptionsQuantity)
{
    char user;
    do
    {
        user = _getwch();
    } while ((int)user < CHAR_TO_INT && (int)user > CHAR_TO_INT + nOptionsQuantity); // In case of IllegalArgument

    return (int)user - CHAR_TO_INT;

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