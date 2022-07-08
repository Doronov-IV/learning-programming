#pragma once
#include <iostream>
#include <conio.h>
#include <string>
#include <thread>
#include <ctime>

#include "Properties.h";
#include "Structures.h";

#ifndef UNICODE
#define UNICODE
#endif
#ifndef _UNICODE
#define _UNICODE
#endif

#include <Windows.h>
#include <stdlib.h>

using namespace std;

#define underon "\033[4m"
#define underoff "\033[0m"
#define space cout<<"\n\n"
#define testChar for(int i = 0; i < 256; i++) { cout <<"\n\n" << i << " = " << char(i); } // testChars
#define showDecks
#define redtext "\033[1m\033[31m"
#define greentext "\033[1m\033[32m" 
#define resetcolor "\033[0m"

int menuX = 89;
int menuY = 5;

int nPlayer1X = 5;
int nPlayer1Y = 5;

int nPlayer2X = 61;
int nPlayer2Y = 5;

void showSpeciesList(Player & player);

void setCursor(int x, int y) {
    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
    COORD coord;
    coord.X = x;
    coord.Y = y;
    SetConsoleCursorPosition(hConsole, coord);
}

void clearMenuField() {
    for (int i = menuY-5; i < menuY + 30; i++) {
        for (int j = menuX-5; j < menuX+50; j++) {
            setCursor(j, i);
            cout << " ";
        }
    }
}

void clearMenuFieldMid() {
    for (int i = menuY - 5; i < menuY + 50; i++) {
        for (int j = menuX - 5; j < menuX + 35; j++) {
            setCursor(j, i);
            cout << " ";
        }
    }
}

void clearMenuFieldLong() {
    for (int i = menuY - 5; i < menuY + 100; i++) {
        for (int j = menuX - 5; j < menuX + 50; j++) {
            setCursor(j, i);
            cout << " ";
        }
    }
}

void drawPlayer(Player& player, int x, int y) {
    if (player.bMarker == true) {
        bool carn = false;
        setCursor(x, y);
        cout << player.nickname;
        setCursor(x, y + 1);
        for (int v = 0; v < player.nickname.length(); v++) {
            cout << char(196);
        }
        setCursor(x, y);
        for (int i = 0; i < playersSpeciesTotal(player); i++) { // showing players species with traits
            y += 5;
            setCursor(x, y);
            carn = false;
            for (int j = 0; j < 3; j++) { // species traits check for carnivore 
                if (player.pl_species[i].spc_traits[j].sName == "Carnivore" && player.pl_species[i].spc_traits[j].bFacedown == false) {
                    carn = true;
                    break;
                }
            }

            if (carn == true) {
                cout << redtext;
            }
            else cout << greentext;

            for (int j = 0; j < 3; j++) { // species traits
                if (player.pl_species[i].spc_traits[j].sName != "None") {
                    if (player.pl_species[i].spc_traits[j].sName == "Fat Tissue") {
                        cout << player.pl_species[i].spc_traits[j].sName << "(" << player.pl_species[i].nFatTissue << ")";
                        y += 1;
                        setCursor(x, y);
                    }
                    else {
                        cout << player.pl_species[i].spc_traits[j].sName << "";
                        y += 1;
                        setCursor(x, y);
                    }
                }
            }
            x -= 1;
            setCursor(x, y);

            int oldx = x + 1, oldy = y + 1;

            cout << char(218); // left corner
            for (int i = 0; i < 22; i++) { // cieling
                cout << char(196);
            }
            cout << char(191); // right corner

            for (int j = 0; j < 8; j++) { // sides
                y += 1;
                setCursor(x, y);
                cout << char(179);
                setCursor(x + 23, y);
                cout << char(179);
            }

            y += 1;
            setCursor(x, y);

            cout << char(192); // left corner
            for (int i = 0; i < 22; i++) { // bottom
                cout << char(196);
            }
            cout << char(217); // right corner

            setCursor(oldx, oldy);
            x = oldx + 1;
            y = oldy;

            for (int j = 1; j < 7; j++) { // digits
                x += 3;
                setCursor(x, y);
                cout << j;
            }

            x -= 18;
            y += 2;
            setCursor(x, y);
            cout << "F";
            oldx = x;
            for (int j = 0; j < player.pl_species[i].nFood; j++) { // food
                x += 3;
                setCursor(x, y);
                cout << char(254);
            }
            x = oldx;
            y += 2;
            setCursor(x, y);
            cout << "B";
            oldx = x;
            for (int j = 0; j < player.pl_species[i].nBodysize; j++) { // bodysize
                x += 3;
                setCursor(x, y);
                cout << char(254);
            }
            x = oldx;
            y += 2;
            setCursor(x, y);
            cout << "P";
            oldx = x;
            for (int j = 0; j < player.pl_species[i].nPopulation; j++) { // population
                x += 3;
                setCursor(x, y);
                cout << char(254);
            }
            x = oldx - 1;

            cout << resetcolor;
        }
    }
    else {
        bool carn = false;
        setCursor(x, y);
        cout << player.nickname;
        setCursor(x, y + 1);
        for (int v = 0; v < player.nickname.length(); v++) {
            cout << char(196);
        }
        setCursor(x, y);
        for (int i = 0; i < playersSpeciesTotal(player); i++) { // showing players species with traits
            y += 5;
            setCursor(x, y);
            carn = false;
            for (int j = 0; j < 3; j++) { // species traits check for carnivore 
                if (player.pl_species[i].spc_traits[j].sName == "Carnivore" && player.pl_species[i].spc_traits[j].bFacedown == false) {
                    carn = true;
                    break;
                }
            }

            if (carn == true) {
                cout << redtext;
            }
            else cout << greentext;

            for (int j = 0; j < 3; j++) { // species traits
                if (player.pl_species[i].spc_traits[j].sName != "None") {
                    if (player.pl_species[i].spc_traits[j].bFacedown == true) { 
                        cout << "Evolution" << "";
                        y += 1;
                        setCursor(x, y);
                    }
                    else {
                        if (player.pl_species[i].spc_traits[j].sName == "Fat Tissue") {
                            cout << player.pl_species[i].spc_traits[j].sName << "(" << player.pl_species[i].nFatTissue << ")";
                            y += 1;
                            setCursor(x, y);
                        }
                        else {
                            cout << player.pl_species[i].spc_traits[j].sName << "";
                            y += 1;
                            setCursor(x, y);
                        }
                    }
                }
            }
            x -= 1;
            setCursor(x, y);

            int oldx = x + 1, oldy = y + 1;

            cout << char(218); // left corner
            for (int i = 0; i < 22; i++) { // cieling
                cout << char(196);
            }
            cout << char(191); // right corner

            for (int j = 0; j < 8; j++) { // sides
                y += 1;
                setCursor(x, y);
                cout << char(179);
                setCursor(x + 23, y);
                cout << char(179);
            }

            y += 1;
            setCursor(x, y);

            cout << char(192); // left corner
            for (int i = 0; i < 22; i++) { // bottom
                cout << char(196);
            }
            cout << char(217); // right corner

            setCursor(oldx, oldy);
            x = oldx + 1;
            y = oldy;

            for (int j = 1; j < 7; j++) { // digits
                x += 3;
                setCursor(x, y);
                cout << j;
            }

            x -= 18;
            y += 2;
            setCursor(x, y);
            cout << "F";
            oldx = x;
            for (int j = 0; j < player.pl_species[i].nFood; j++) { // food
                x += 3;
                setCursor(x, y);
                cout << char(254);
            }
            x = oldx;
            y += 2;
            setCursor(x, y);
            cout << "B";
            oldx = x;
            for (int j = 0; j < player.pl_species[i].nBodysize; j++) { // bodysize
                x += 3;
                setCursor(x, y);
                cout << char(254);
            }
            x = oldx;
            y += 2;
            setCursor(x, y);
            cout << "P";
            oldx = x;
            for (int j = 0; j < player.pl_species[i].nPopulation; j++) { // population
                x += 3;
                setCursor(x, y);
                cout << char(254);
            }
            x = oldx - 1;

            cout << resetcolor;
        }
    }
    setCursor(5, 30);
}

void drawHubForTwo(Player & player1, Player & player2, int nRound, int nPhase, int nPoolFod) {
    drawPlayer(player1, nPlayer1X, nPlayer1Y);
    drawPlayer(player2, nPlayer2X, nPlayer2Y);

    setCursor(34, 6);
    cout << char(201);
    for (int i = 0; i < 18; i++) {
        cout << char(205);
    }
    cout << char(187);

    for (int i = 7; i < 28; i++) {
        setCursor(34, i);
        cout << char(186);
    }


    for (int i = 0; i < 18; i++) {
        setCursor(35 + i, 5);
        cout << " ";
    }

    setCursor(35, 4);
    
    if (player1.bMarker == true && player2.bMarker == false) {
        cout << player1.nickname;
        setCursor(35, 5);
        for (int v = 0; v < player1.nickname.length(); v++) {
            cout << char(196);
        }
    }
    else if (player2.bMarker == true && player1.bMarker == false) {
        cout << player2.nickname;
        setCursor(35, 5);
        for (int v = 0; v < player2.nickname.length(); v++) {
            cout << char(196);
        }
    }

    setCursor(34, 7); // round
    cout << "\tRound " << nRound << "\t";


    for (int i = 7; i < 28; i++) {
        setCursor(53, i);
        cout << char(186);
    }
    setCursor(34, 28);
    cout << char(200);
    for (int i = 0; i < 18; i++) {
        cout << char(205);
    }
    cout << char(188);

    setCursor(35, 10); // phase

    switch (nPhase) {
    case 1:
        cout << "\tDealing!\t";
        break;
    case 2:
        cout << " Choosing Food!";
        break;
    case 3:
        cout << " Playing Cards!";
        break;
    case 4:
        cout << " Feeding!";
        break;
    default: 
        cout << " Testing!";
        break;
    }

    setCursor(35, 27);

    if (nRound == 1 && nPhase < 4) cout << "FOD";
    else cout << "FOD: " << nPoolFod;

    
}

void drawMenuForPlayer(Player& player, int nPhase) {
    int tempX = 0, tempY = 0;
    switch (nPhase) {
    case 2:
        setCursor(menuX, menuY);
        cout << "Choose one of your traits for banking food:";
        for (int i = 0; player.pl_traits[i].sName != "None"; i++) {
            setCursor(menuX, menuY + 2 + i);
            cout << i+1 << ". " << player.pl_traits[i].sName << " (" << player.pl_traits[i].nFoodCount << ")\n";
        } 
        break;
    case 3:
        setCursor(menuX, menuY);
        tempX = menuX;
        tempY = menuY + 2;
        cout << "Choose a trait: ";
        for (int i = 0; player.pl_traits[i].sName != "None"; i++) {
            tempY += 1;
            setCursor(tempX, tempY);
            cout << i + 1 << ". " << player.pl_traits[i].sName << " (" << player.pl_traits[i].nFoodCount << ")\n";
        }
        tempY += 2;
        setCursor(tempX, tempY);
        cout << "\tor";
        tempY += 2;
        setCursor(tempX, tempY);
        cout << "0. End turn.";
        break;
    case 4:
        setCursor(menuX, menuY+1);
        cout << "1. Feed. ";
        setCursor(menuX, menuY+2);
        cout << "2. End turn. ";
        break;
    }
}

void drawMenuForIntelligence(Player& player) {
    setCursor(menuX, menuY+1);
    cout << "1. Feed. ";
    setCursor(menuX, menuY+2);
    cout << "3. Use Intelligence perk. ";
    setCursor(menuX, menuY+3);
    cout << "3. End turn. ";

}

void drawIntelligenceMenu(Player& player) {
    clearMenuField();
    setCursor(menuX, menuY);
    cout << "Choose a trait you want to discard: ";
    for (int i = 0; player.pl_traits[i].sName != "None"; i++) {
        setCursor(menuX, menuY + 2 + i);
        cout << i + 1 << ". " << player.pl_traits[i].sName << " (" << player.pl_traits[i].nFoodCount << ")\n";
    }

}

void drawSubMenuForPlayer(Player& player, int number) {
    clearMenuField();
    setCursor(menuX, menuY);
    cout << "What would you like to do with your trait?";
    setCursor(menuX, menuY + 2);
    cout << "1. Info.";
    setCursor(menuX, menuY + 3);
    cout << "2. Give it to a species.";
    setCursor(menuX, menuY + 4);
    cout << "3. Invest in a Body Size (B) of a species.";
    setCursor(menuX, menuY + 5);
    cout << "4. Invest in a Population (P) of a species.";
    setCursor(menuX, menuY + 6);
    cout << "5. Create new species.";
    setCursor(menuX, menuY + 7);
    cout << "6. End turn.";
}

void showSpeciesList(Player& player) {
    bool carn = false;
    if (playersSpeciesTotal(player) == 2) clearMenuFieldMid();
    else if (playersSpeciesTotal(player) >= 3) clearMenuFieldLong();
    else clearMenuField();
    setCursor(menuX, menuY);
    cout << "Your species:";
    int x = menuX;
    int y = menuY;
    for (int i = 0; i < playersSpeciesTotal(player); i++) { // showing players species with traits
        setCursor(x, y+4);
        cout << i + 1 << ".";
        y += 5;
        setCursor(x, y);
        for (int j = 0; j < 3; j++) { // species traits check for carnivore 
            carn = false;
            if (player.pl_species[i].spc_traits[j].sName == "Carnivore" && player.pl_species[i].spc_traits[j].bFacedown == false) {
                carn = true;
                break;
            }
        }

        if (carn == true) {
            cout << redtext;
        }
        else cout << greentext;

        for (int j = 0; j < 3; j++) { // species traits
            if (player.pl_species[i].spc_traits[j].sName != "None") {
                cout << player.pl_species[i].spc_traits[j].sName << "";
                y += 1;
                setCursor(x, y);
            }
        }
        x -= 1;
        setCursor(x, y);

        int oldx = x + 1, oldy = y + 1;

        cout << char(218); // left corner
        for (int i = 0; i < 22; i++) { // cieling
            cout << char(196);
        }
        cout << char(191); // right corner

        for (int j = 0; j < 8; j++) { // sides
            y += 1;
            setCursor(x, y);
            cout << char(179);
            setCursor(x + 23, y);
            cout << char(179);
        }

        y += 1;
        setCursor(x, y);

        cout << char(192); // left corner
        for (int i = 0; i < 22; i++) { // bottom
            cout << char(196);
        }
        cout << char(217); // right corner

        setCursor(oldx, oldy);
        x = oldx + 1;
        y = oldy;

        for (int j = 1; j < 7; j++) { // digits
            x += 3;
            setCursor(x, y);
            cout << j;
        }

        x -= 18;
        y += 2;
        setCursor(x, y);
        cout << "F";
        oldx = x;
        for (int j = 0; j < player.pl_species[i].nFood; j++) { // food
            x += 3;
            setCursor(x, y);
            cout << char(254);
        }
        x = oldx;
        y += 2;
        setCursor(x, y);
        cout << "B";
        oldx = x;
        for (int j = 0; j < player.pl_species[i].nBodysize; j++) { // bodysize
            x += 3;
            setCursor(x, y);
            cout << char(254);
        }
        x = oldx;
        y += 2;
        setCursor(x, y);
        cout << "P";
        oldx = x;
        for (int j = 0; j < player.pl_species[i].nPopulation; j++) { // population
            x += 3;
            setCursor(x, y);
            cout << char(254);
        }
        x = oldx - 1;

        cout << resetcolor;
    }
}

void drawTurn(Player& player1, Player& player2, int nRound, int nPhase, int nPoolFod) {
    system("cls");
    setCursor(12, 10);
    if (player1.bMarker == true) { // determining who goes first
        cout << player1.nickname << ", it's your turn now!";
    }
    else if (player2.bMarker == true) {
        cout << player2.nickname << ", it's your turn now!";
    }
    this_thread::sleep_for(3000ms);
    system("cls");
    drawHubForTwo(player1, player2, nRound, nPhase, nPoolFod);
}

void drawPlayersTraits(Player& player) {
    clearMenuField();
    setCursor(menuX, menuY);
    for (int i = 0; player.pl_traits[i].sName != "None"; i++) {
        setCursor(menuX, menuY+2+i);
        cout << i + 1 << ". " << player.pl_traits[i].sName << " (" << player.pl_traits[i].nFoodCount << ")\n";
    }
}

void drawSpeciesTraits(Species& species) {
    clearMenuField();
    setCursor(menuX, menuY);
    for (int i = 0; species.spc_traits[i].sName != "None"; i++) {
        setCursor(menuX, menuY + 2 + i);
        if (species.spc_traits[i].bFacedown == true) {
            cout << i + 1 << ". " << "Evolution";
        }
        else cout << i + 1 << ". " << species.spc_traits[i].sName;
    }
}

