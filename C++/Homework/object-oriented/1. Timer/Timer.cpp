#include "Timer.h"

class Timer {

public:

	int nSec;
	int nMin;
	int nHrs;
	bool bIsOn;

	// auxiliary function needed for drawing
	void setCursor(int x, int y) {
		HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
		COORD coord;
		coord.X = x;
		coord.Y = y;
		SetConsoleCursorPosition(hConsole, coord);
	}

	void run() {

		/*
		* 
			COORD's
			(x;y) -- "1. Start"
			(x;y-3) -- "Clock"
		
		*/

		wchar_t cUser;
		bool bIsOn = false;				// switch the "start-stop" button in menu
		bool bKeyFlag = false;			// determins whether the key is pressed
		bool bLaunchFlag = false;		// redundancy button 
		int x = 10, y = 10;
		nSec = 0;
		nMin = 0;
		nHrs = 0;
		int speed = 20;
		int tick = 0;
		cUser = L'1';

		while (cUser == L'1') { // programme menu
			
			// could be turned off at will and/or changed with "cls"
			setCursor(x, y-3);
			std::cout << "00:00:00";

			setCursor(x, y);
			std::cout << "1. Start";
			setCursor(x, y + 1);
			std::cout << "2. Exit";
			setCursor(x, y - 3);
			cUser = L'a';
			cUser = _getwch(); // this getch is broken. It works only once

			if (cUser == L'1') bLaunchFlag = true;
			bIsOn = false;
			if (bLaunchFlag) {
				bKeyFlag = false;
				nSec = 0;
				nMin = 0;
				nHrs = 0;
				while (!bKeyFlag) { // redraw menu and start the clock

					if (!bIsOn) {	
						setCursor(x, y);
						std::cout << "1. Stop ";
					}

					else if (tick == speed) {  // timing
						nSec++;
						if (nSec >= 60) {
							nMin++;
							nSec = 0;
						}
						if (nMin >= 60) {
							nHrs++;
							nMin = 0;
						}

						// draw Clock

						setCursor(x, y - 3);
						if (nHrs < 10) std::cout << "0";
						std::cout << nHrs << ":";
						if (nMin < 10) std::cout << "0";
						std::cout << nMin << ":";
						if (nSec < 10) std::cout << "0";
						std::cout << nSec << " ";

						//

						tick = 0;
					}
					bIsOn = true;
					std::this_thread::sleep_for(std::chrono::milliseconds(50));
					tick++;
					if (_kbhit()) break;
				}

			}
			else break;
			cUser = L'a';
			cUser = _getwch();
			setCursor(10, 15);
		}
		setCursor(10, 15);
	}

};
