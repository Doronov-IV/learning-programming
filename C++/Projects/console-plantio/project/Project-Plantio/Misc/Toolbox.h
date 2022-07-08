//  TOOLBOX.h (Colors.h originaly)
//
//  Posted by Gon1332 May 15 2015 on StackOverflow
//  Modified by Shades Aug. 14 2018
//  Modified by DoronovIV Jan. 25 2022

// PLEASE carefully read comments before using this tool, this will save you a lot of bugs that are going to be just about impossible to find.
#ifndef TOOLBOX_H
#define TOOLBOX_H

#include <iostream>
#include <string>
#include <codecvt>
#include <locale>
#include <ctime>
#include <conio.h>
#include <fstream>
#include <windows.h>


#ifndef FILE_BUFFERED_READER_H
#include "FileBufferedReader.h"
#endif




#pragma warning(disable : 4996)



namespace Toolbox
{


#pragma region METHODS

    // Hides cursor from sight;
    void deleteCursor();

    // Sets console atrubutes to some specificators;
    void setConsoleWindowSize(size_t width, size_t height);

    // Uses HANDLE to make console fullscreen;
    void setFullscreenMode();


    // Gets current PC time;
    // returns — current time in string format;
    std::string getCurrentTime();


    // Sets cursor into specific position via escape sequence;
    // "x" — the X-coordinate, columns;
    // "y" — the Y-coordinate, rows.
    void setCursor(int x, int y);


    // Checks the input and returns the number of action, chosen by user;
    // "nOptionsQuantity" — amount of options available for user;
    // returns — Chosen menu option.
    int getchSelector(size_t nOptionsQuantity);

    // Checks the input and returns the number of action, chosen by user;
    // "nOptionsQuantity" — amount of options available for user;
    // returns — Chosen menu option.
    int cinSelector(size_t nOptionsQuantity);

    // Checks the input and returns the number of action, chosen by user with lower border;
    // "nLowerValue" — lower border;
    // "nOptionsQuantity" — amount of options available for user;
    // returns — Chosen menu option.
    int getchSelector(size_t nLowerValue, size_t nOptionsQuantity);

    // Checks the input and returns the number of action, chosen by user with lower border;
    // "nLowerValue" — lower border;
    // "nOptionsQuantity" — amount of options available for user;
    // returns — Chosen menu option.
    int cinSelector(size_t nLowerValue, size_t nOptionsQuantity);


    // Askes user for continuation;
    // Produces a large bar with text in the middle of a screen.
    // Example of use:
    // 
    // while (c_cont != 'n' && c_cont != 'N')
    // // some code
    // space;
    // showContinue();
    // c_cont = _getwch();
    void showContinue();

    // Clears console in specific range;
    void clearScreenInRange(size_t x_start, size_t x_end, size_t y_start, size_t y_end);

    //;
    int ScrollByAbsoluteCoord(int iRows);

    //;
    int StringToWString(std::wstring& ws, const std::string& s);

    //;
    void fontsize(size_t a, size_t b);

    //;
    bool is_number(const std::string& s);


#pragma endregion METHODS




#pragma region NUMBERS


#define DEFAULT_TEXT_SIZE 16

// A feature of order-quantity numeration;
// Example: an array got 5 elements, the index of last element is 4;
#ifndef NUMERATION_ISSUE
#define NUMERATION_ISSUE 1
#endif

// For addition of a new element to some structure;
#ifndef NEW_ELEMENT
#define NEW_ELEMENT 1
#endif

// For delition or modifiction of a new element of some structure;
#ifndef ONE_ELEMENT
#define ONE_ELEMENT 1
#endif

// For the "exit" option in menu;
#ifndef EXIT_MENU_OPTION
#define EXIT_MENU_OPTION 1
#endif


#pragma endregion NUMBERS




#pragma region FORMATING


#define space std::cout << "\n\n"	    // skip one line blank;
#define line std::cout << "\n"		    // next line symbol;
#define tab std::cout<<"\t"			    // tabulation symbol;
#define doubleTab std::cout<<"\t\t"	    // tabulation symbol twice;
#define up std::cout << "\x1b[A";       // goes 1 line up the screen; 


#pragma endregion FORMATING




#pragma region KEYS





#pragma endregion KEYS




#pragma region SYMBOLS

// A char rectangle;
#ifndef REC
#define REC char(219)
#endif

// A char square;
#ifndef SQR
#define SQR char(254)
#endif


// Right Up Single corner;
#ifndef R_U_S
#define R_U_S char(191)
#endif

// Left Up Single corner;
#ifndef L_U_S
#define L_U_S char(218)
#endif

// Right Botton Single corner;
#ifndef R_B_S
#define R_B_S char(217)
#endif

// Left Up Single corner;
#ifndef L_B_S
#define L_B_S char(192)
#endif

// Horizontal Single line;
#ifndef H_S
#define H_S char(196)
#endif

// Verticle Single line;
#ifndef V_S
#define V_S char(179)
#endif


// Right Up Double corner;
#ifndef R_U_D
#define R_U_D char(187)
#endif

// Left Up Double corner;
#ifndef L_U_D
#define L_U_D char(201)
#endif

// Right Botton Double corner;
#ifndef R_B_D
#define R_B_D char(188)
#endif

// Left Up Double corner;
#ifndef L_B_D
#define L_B_D char(200)
#endif

// Horizontal Double line;
#ifndef H_D
#define H_D char(205)
#endif

// Verticle Double line;
#ifndef V_D
#define V_D char(186)
#endif


/*

        0 =
        1 = ☺
        2 = ☻
        3 = ♥
        4 = ♦
        5 = ♣
        6 = ♠
        7 =
        8 =
        9 =
        10 =

        11 = ♂
        12 = ♀
        13 =
        14 = ♫
        15 = ☼
        16 = ►
        17 = ◄
        18 = ↕
        19 = ‼
        20 = ¶

        21 = §
        22 = ▬
        23 = ↨
        24 = ↑
        25 = ↓
        26 = →
        27 =
        28 = ∟
        29 = ↔
        30 = ▲

        31 = ▼

        176 = ░
        177 = ▒
        178 = ▓
        179 = │
        180 = ┤

        181 = ╡
        182 = ╢
        183 = ╖
        184 = ╕
        185 = ╣
        186 = ║
        187 = ╗
        188 = ╝
        189 = ╜
        190 = ╛

        191 = ┐
        192 = └
        193 = ┴
        194 = ┬
        195 = ├
        196 = ─
        197 = ┼
        198 = ╞
        199 = ╟
        200 = ╚

        201 = ╔
        202 = ╩
        203 = ╦
        204 = ╠
        205 = ═
        206 = ╬
        207 = ╧
        208 = ╨
        209 = ╤
        210 = ╥

        211 = ╙
        212 = ╘
        213 = ╒
        214 = ╓
        215 = ╫
        216 = ╪
        217 = ┘
        218 = ┌
        219 = █
        220 = ▄

        221 = ▌
        222 = ▐
        223 = ▀

        248 = °
        249 = ∙
        250 = ·

        251 = √
        252 = №
        253 = ¤
        254 = ■

*/


#pragma endregion SYMBOLS




#pragma region COLORS



#define DEFAULT_TEXT "\x1B[37m\x1B[40m"


/* FOREGROUND */
// These codes set the actual text to the specified color
#define RESETTEXT  "\x1B[0m" // Set all colors back to normal.
#define FOREBLK  "\x1B[30m" // Black
#define FORERED  "\x1B[31m" // Red
#define FOREGRN  "\x1B[32m" // Green
#define FOREYEL  "\x1B[33m" // Yellow
#define FOREBLU  "\x1B[34m" // Blue
#define FOREMAG  "\x1B[35m" // Magenta
#define FORECYN  "\x1B[36m" // Cyan
#define FOREWHT  "\x1B[37m" // White

/* BACKGROUND */
// These codes set the background color behind the text.
#define BACKBLK "\x1B[40m"
#define BACKRED "\x1B[41m"
#define BACKGRN "\x1B[42m"
#define BACKYEL "\x1B[43m"
#define BACKBLU "\x1B[44m"
#define BACKMAG "\x1B[45m"
#define BACKCYN "\x1B[46m"
#define BACKWHT "\x1B[47m"

// These will set the text color and then set it back to normal afterwards.
#define BLK(x) FOREBLK x RESETTEXT
#define RED(x) FORERED x RESETTEXT
#define GRN(x) FOREGRN x RESETTEXT
#define YEL(x) FOREYEL x RESETTEXT
#define BLU(x) FOREBLU x RESETTEXT
#define MAG(x) FOREMAG x RESETTEXT
#define CYN(x) FORECYN x RESETTEXT
#define WHT(x) FOREWHT x RESETTEXT

// Example usage: cout << BLU("This text's color is now blue!") << endl;

// These will set the text's background color then reset it back.
#define BackBLK(x) BACKBLK x RESETTEXT
#define BackRED(x) BACKRED x RESETTEXT
#define BackGRN(x) BACKGRN x RESETTEXT
#define BackYEL(x) BACKYEL x RESETTEXT
#define BackBLU(x) BACKBLU x RESETTEXT
#define BackMAG(x) BACKMAG x RESETTEXT
#define BackCYN(x) BACKCYN x RESETTEXT
#define BackWHT(x) BACKWHT x RESETTEXT

// Example usage: cout << BACKRED(FOREBLU("I am blue text on a red background!")) << endl;

// These functions will set the background to the specified color indefinitely.
// NOTE: These do NOT call RESETTEXT afterwards. Thus, they will set the background color indefinitely until the user executes cout << RESETTEXT
// OR if a function is used that calles RESETTEXT i.e. cout << RED("Hello World!") will reset the background color since it calls RESETTEXT.
// To set text COLOR indefinitely, see SetFore functions below.
#define SetBackBLK BACKBLK
#define SetBackRED BACKRED
#define SetBackGRN BACKGRN
#define SetBackYEL BACKYEL
#define SetBackBLU BACKBLU
#define SetBackMAG BACKMAG
#define SetBackCYN BACKCYN
#define SetBackWHT BACKWHT

// Example usage: cout << SetBackRED << "This text's background and all text after it will be red until RESETTEXT is called in some way" << endl;

// These functions will set the text color until RESETTEXT is called. (See above comments)
#define SetForeBLK FOREBLK
#define SetForeRED FORERED
#define SetForeGRN FOREGRN
#define SetForeYEL FOREYEL
#define SetForeBLU FOREBLU
#define SetForeMAG FOREMAG
#define SetForeCYN FORECYN
#define SetForeWHT FOREWHT

// Example usage: cout << SetForeRED << "This text and all text after it will be red until RESETTEXT is called in some way" << endl;

#define BOLD(x) "\x1B[1m" x RESETTEXT // Embolden text then reset it.
#define BRIGHT(x) "\x1B[1m" x RESETTEXT // Brighten text then reset it. (Same as bold but is available for program clarity)
#define UNDL(x) "\x1B[4m" x RESETTEXT // Underline text then reset it.

// Example usage: cout << BOLD(BLU("I am bold blue text!")) << endl;

// These functions will embolden or underline text indefinitely until RESETTEXT is called in some way.

#define SetBOLD "\x1B[1m" // Embolden text indefinitely.
#define SetBRIGHT "\x1B[1m" // Brighten text indefinitely. (Same as bold but is available for program clarity)
#define SetUNDL "\x1B[4m" // Underline text indefinitely.

// Example usage: cout << setBOLD << "I and all text after me will be BOLD/Bright until RESETTEXT is called in some way!" << endl;


#pragma endregion COLORS


#pragma region WIDE_COLORS


#define wDEFAULT_TEXT L"\x1B[37m\x1B[40m"


/* FOREGROUND */
// These codes set the actual text to the specified color
#define wRESETTEXT  L"\x1B[0m" // Set all colors back to normal.
#define wFOREBLK  L"\x1B[30m" // Black
#define wFORERED  L"\x1B[31m" // Red
#define wFOREGRN  L"\x1B[32m" // Green
#define wFOREYEL  L"\x1B[33m" // Yellow
#define wFOREBLU  L"\x1B[34m" // Blue
#define wFOREMAG  L"\x1B[35m" // Magenta
#define wFORECYN  L"\x1B[36m" // Cyan
#define wFOREWHT  L"\x1B[37m" // White

/* BACKGROUND */
// These codes set the background color behind the text.
#define wBACKBLK L"\x1B[40m"
#define wBACKRED L"\x1B[41m"
#define wBACKGRN L"\x1B[42m"
#define wBACKYEL L"\x1B[43m"
#define wBACKBLU L"\x1B[44m"
#define wBACKMAG L"\x1B[45m"
#define wBACKCYN L"\x1B[46m"
#define wBACKWHT L"\x1B[47m"

// These will set the text color and then set it back to normal afterwards.
#define wBLK(x) wFOREBLK x wRESETTEXT
#define wRED(x) wFORERED x wRESETTEXT
#define wGRN(x) wFOREGRN x wRESETTEXT
#define wYEL(x) wFOREYEL x wRESETTEXT
#define wBLU(x) wFOREBLU x wRESETTEXT
#define wMAG(x) wFOREMAG x wRESETTEXT
#define wCYN(x) wFORECYN x wRESETTEXT
#define wWHT(x) wFOREWHT x wRESETTEXT

// Example usage: cout << BLU("This text's color is now blue!") << endl;

// These will set the text's background color then reset it back.
#define wBackBLK(x) wBACKBLK x wRESETTEXT
#define wBackRED(x) wBACKRED x wRESETTEXT
#define wBackGRN(x) wBACKGRN x wRESETTEXT
#define wBackYEL(x) wBACKYEL x wRESETTEXT
#define wBackBLU(x) wBACKBLU x wRESETTEXT
#define wBackMAG(x) wBACKMAG x wRESETTEXT
#define wBackCYN(x) wBACKCYN x wRESETTEXT
#define wBackWHT(x) wBACKWHT x wRESETTEXT

// Example usage: cout << BACKRED(FOREBLU("I am blue text on a red background!")) << endl;

// These functions will set the background to the specified color indefinitely.
// NOTE: These do NOT call RESETTEXT afterwards. Thus, they will set the background color indefinitely until the user executes cout << RESETTEXT
// OR if a function is used that calles RESETTEXT i.e. cout << RED("Hello World!") will reset the background color since it calls RESETTEXT.
// To set text COLOR indefinitely, see SetFore functions below.
#define wSetBackBLK wBACKBLK
#define wSetBackRED wBACKRED
#define wSetBackGRN wBACKGRN
#define wSetBackYEL wBACKYEL
#define wSetBackBLU wBACKBLU
#define wSetBackMAG wBACKMAG
#define wSetBackCYN wBACKCYN
#define wSetBackWHT wBACKWHT

// Example usage: cout << SetBackRED << "This text's background and all text after it will be red until RESETTEXT is called in some way" << endl;

// These functions will set the text color until RESETTEXT is called. (See above comments)
#define wSetForeBLK wFOREBLK
#define wSetForeRED wFORERED
#define wSetForeGRN wFOREGRN
#define wSetForeYEL wFOREYEL
#define wSetForeBLU wFOREBLU
#define wSetForeMAG wFOREMAG
#define wSetForeCYN wFORECYN
#define wSetForeWHT wFOREWHT

// Example usage: cout << SetForeRED << "This text and all text after it will be red until RESETTEXT is called in some way" << endl;

#define wBOLD(x) L"\x1B[1m" x wRESETTEXT // Embolden text then reset it.
#define wBRIGHT(x) L"\x1B[1m" x wRESETTEXT // Brighten text then reset it. (Same as bold but is available for program clarity)
#define wUNDL(x) L"\x1B[4m" x wRESETTEXT // Underline text then reset it.

// Example usage: cout << BOLD(BLU("I am bold blue text!")) << endl;

// These functions will embolden or underline text indefinitely until RESETTEXT is called in some way.

#define wSetBOLD L"\x1B[1m" // Embolden text indefinitely.
#define wSetBRIGHT L"\x1B[1m" // Brighten text indefinitely. (Same as bold but is available for program clarity)
#define wSetUNDL L"\x1B[4m" // Underline text indefinitely.

// Example usage: cout << setBOLD << "I and all text after me will be BOLD/Bright until RESETTEXT is called in some way!" << endl;


#pragma endregion WIDE_COLORS


}

#endif /* TOOLBOX_h */