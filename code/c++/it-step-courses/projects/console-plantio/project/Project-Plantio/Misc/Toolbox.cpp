#include "Toolbox.h"


#define GETCH_CHAR_TO_INT 48 // The starting number of digits returned by conio::getch, for quick parsing;
#define CIN_CHAR_TO_INT 35 // The starting number of digits returned by iostream::cin, for quick parsing;


#pragma region WINDOWS


void Toolbox::setConsoleWindowSize(size_t width, size_t height)
{
    HWND hWindowConsole = GetConsoleWindow();
    RECT r;
    GetWindowRect(hWindowConsole, &r); //stores the console's current dimensions
    MoveWindow(hWindowConsole, r.left, r.top, width, height, TRUE);
}


void Toolbox::deleteCursor()
{
    HANDLE console = GetStdHandle(STD_OUTPUT_HANDLE);
    CONSOLE_CURSOR_INFO info{ 100,false };
    SetConsoleCursorInfo(console, &info);
}


void Toolbox::setFullscreenMode()
{
    system("mode con COLS=700");
    ShowWindow(GetConsoleWindow(), SW_MAXIMIZE);
    SendMessage(GetConsoleWindow(), WM_SYSKEYDOWN, VK_RETURN, 0x20000000);
}


int Toolbox::ScrollByAbsoluteCoord(int iRows)
{
    HANDLE hStdout = GetStdHandle(STD_OUTPUT_HANDLE);;

    CONSOLE_SCREEN_BUFFER_INFO csbiInfo;
    SMALL_RECT srctWindow;

    // Get the current screen buffer size and window position.

    if (!GetConsoleScreenBufferInfo(hStdout, &csbiInfo))
    {
        printf("GetConsoleScreenBufferInfo (%d)\n", GetLastError());
        return 0;
    }

    // Set srctWindow to the current window size and location.

    srctWindow = csbiInfo.srWindow;

    // Check whether the window is too close to the screen buffer top

    if (srctWindow.Top >= iRows)
    {
        srctWindow.Top -= (SHORT)iRows;     // move top up
        srctWindow.Bottom -= (SHORT)iRows;  // move bottom up

        if (!SetConsoleWindowInfo(
            hStdout,          // screen buffer handle
            TRUE,             // absolute coordinates
            &srctWindow))     // specifies new location
        {
            printf("SetConsoleWindowInfo (%d)\n", GetLastError());
            return 0;
        }
        return iRows;
    }
    else
    {
        printf("\nCannot scroll; the window is too close to the top.\n");
        return 0;
    }
}


void Toolbox::fontsize(size_t a, size_t b)
{
    HANDLE out = GetStdHandle(STD_OUTPUT_HANDLE);
    PCONSOLE_FONT_INFOEX lpConsoleCurrentFontEx = new CONSOLE_FONT_INFOEX();
    lpConsoleCurrentFontEx->cbSize = sizeof(CONSOLE_FONT_INFOEX);
    GetCurrentConsoleFontEx(out, 0, lpConsoleCurrentFontEx);
    lpConsoleCurrentFontEx->dwFontSize.X = a;
    lpConsoleCurrentFontEx->dwFontSize.Y = b;
    SetCurrentConsoleFontEx(out, 0, lpConsoleCurrentFontEx);
}



#pragma endregion WINDOWS





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


int Toolbox::StringToWString(std::wstring& ws, const std::string& s)
{
    std::wstring wsTmp(s.begin(), s.end());

    ws = wsTmp;

    return 0;
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


bool Toolbox::is_number(const std::string& s)
{
    try
    {
        std::stod(s);
    }
    catch (...)
    {
        return false;
    }
    return true;
}


