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
#include <ctime>
#include <conio.h>
#include <fstream>


namespace Toolbox
{


#pragma region METHODS


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


#pragma endregion METHODS




#pragma region NUMBERS


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


#define space std::cout << "\n\n"	 // skip one line blank
#define line std::cout << "\n"		 // next line symbol
#define tab std::cout<<"\t"			 // tabulation symbol
#define doubleTab std::cout<<"\t\t"	 // tabulation symbol twice


#pragma endregion FORMATING




#pragma region SYMBOLS


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


}

#endif /* TOOLBOX_h */
