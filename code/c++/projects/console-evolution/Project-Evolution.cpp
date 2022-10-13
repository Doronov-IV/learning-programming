#pragma region COMMENTS
/* 
    08--09.11.2021 2:04 GTM+3 .....

    Today my teacher offered me to make for the project a board-game of my choice and suggested "Evolution".
        I've found this game both funny and cool, and yet easy-to-implement (At least I think so at the beginning).

    During my investigation on the rules and gameplay, I've actually made up my mind with these key-features of the gameplay:
    
    0) The game shall be set-up whith an array of players. The capacity of which is determined with the first i/o dialogue. 
    1) The while-true-loop will consist of four phases representing the actual round.
    3) At this point, I got three structures in my mind which are "Trait", "Species" and "Player" placed in a sequential order.
    4) The deck consist of 13 unique traits with ??? cards total (I'll specify the exact amount tomorrow. Or actually later today).
    5) Hell... how much have I just spent on that piece of trash......


    9.11.2021 14:18 GMT+3 

    Creating two decks depending on how many players do we have.
    Also, I've already made the table of properties.
    I'll made some demo functions to check the shuffling of the deck. (just tested it, looks pretty fine)

    9--10.11.2021 0:19 GTM+3

    I've linked up all the fuctions and objects.
    Drew something on the screen and it seems to be working (somehow).
    Now I only need to make up a system of handling turns from one player to another and half of the project is done (kidding).

    10.11.21 14:23 GTM+3 

    Objectives: make an action menu that will represent at least three branches:
    -- in hub
    -- in traits
    -- in "use" trait

    *Also, I'll need to create a function that will draw players' species and their traits. Piece of cake.
    **Also, think about splitting main file in two or more files to improve readabiliy. 
    

    11.11.21 17:18 GTM+3 

    I've found a bug in giving-card function. There was a break missing in the inner loop.
    Also, I've managed to implement the second phase of the game (4 total).

    Yet, I need to test it for the redundancy. I'll try and make a recursive function for the third phase as well. (Done)

    18:07 GTM+3

    Now, it's ready for the third phase, which would be extremely difficult.

    ====================================================================================
    Special note: after card is taken out of player's deck, the deck MUST be compressed. 
    Otherwise it would be difficult to navigate throughout it.
    I'm going to perform a kind of sorting just for cards.
    ====================================================================================

    20:10 GMT+3

    There's annoying bug somewhere, that makes traits disappear.
    I suppose it's somewhere in "acceptFoodCard". I'll test it out in a moment.

    UPD:
    It was waaaaaay much easier than I thought! There was a spare break inside an if statement. It was
    somehow, well, breaking something, eather loop or case, idk. Eather way it's fixed now.
    I'll comment out testing function.

    *UPD: It seems compression function is also working wrong :\
     Eather way I was going to change it from scratch.  
     Also, the phase-3 part of the loop is not working properly.
    **UPD: FIXED!!
    
    Now, implementing a phase-three menu.

    23:16 GMT+3
    
    That was hell of a programming. Now, looking at the piles of functions the only thing I feel is disgust.
    May be tomorrow I'll manage to organize those better.

    btw, tomorrow objectives: repair recursive function from phase three. It paints data in a wrong place a couple of times.

    12--13.11.2021 0:22 GTM+3

    I've fixed several bugs in phase 3, since I was bored in a class. Now it works just fine. 
    May be I'll try and paint red some carnivore species.
    At this point, the game is ready for 75%.

    Things I want to do after I would make phase 4 properly:
    -- Endgame
    -- AI player(s)
    -- 3-player mode

    I think it will be easy to make same functions work for three players. May be I'm wrong.

    13.11.2021 22:16 GTM+3 

    The programme warks fine for now. Before I go forward, I'd like to place the dialogue menu to the right somewhere at (89;5).
    I've also managed to find out about the colors.

    14.11.2021 20:01 GTM+3

    Some minor fixes have been put, may be I'll even try and implement carnivore tree this evening.
    Also, the menu seems to fit at the new place.
    
    [!] There's an issue with properties visualisation. I'll think of it tomorrow.
        -- one possible decision could be splitting them in half by making a new structure.
        -- the other one is cout'ing one letter at a time according to it's coordinates, which is actually extremely stupid. 
        -- ¯\_(ツ)_/¯
        (solved)

    16.11.2021 19:07 GTM+3

    I've decided to assign the presentation of my project to Friday, not Wednesday. 
    I'd rather do it better than release it two days before deadline.

    18.11.2021 21:21 GTM+3

    This is one of the last comments on my project. I suppose my groupmates have some difficulties reading Enlish text, and may be tl;dr, so
    the presentation is appointed for tomorrow and my programme is barely functioning. So, I've decided to cut out the three-person mode cause
    it would take too much time to implement, though the programm itself is build the way it can be modified with ease, at least I think so.

    Let's see what I will improve.

    To do list: 
    -- drawSubMenuForPlayer blanc field fix; +
    -- clearFieldMenu should clear deeper;
    -- gameover fix;
    -- you cannot eat more fix;

    See: line 386
    
*/
#pragma endregion COMMENTS

#pragma region PREPROCESSING

#include <iostream>
#include <conio.h>
#include <string>
#include <thread>
#include <ctime>

#include "Properties.h";
#include "Structures.h";
#include "Drawing.h";

#ifndef UNICODE
#define UNICODE
#endif
#ifndef _UNICODE
#define _UNICODE
#endif

#include <Windows.h>

using namespace std;

#define underon "\033[4m"
#define underoff "\033[0m"
#define space cout<<"\n\n"
#define testChar for(int i = 0; i < 256; i++) { cout <<"\n\n" << i << " = " << char(i); } // testChars
#define showDecks
#define redtext "\033[1m\033[31m"
#define greentext "\033[1m\033[32m" 
#define resetcolor "\033[0m"

#pragma endregion PREPROCESSING

#pragma region GLOBAL

bool bGameOver = false;     // gameover flag
bool bLastRound = false;    // last round flag
int players_counter = 0;    // the actual number of players
int nRound = 1;             // rounds counter
int nPhase = 1;             // phase counter
int nPoolFod = 0;
int nTempFodContain = 0;
int nBankedFood = 0;

int nTemp;                  // temporal one for selecting a trait 

bool bTurnOver = false;     // is the turn over or not
bool bPlayer1 = false;      // flags for phase 4
bool bPlayer2 = false;
bool bPlayersLastTurn = false;

Trait* largeDeck = new Trait[129];
Trait* smallDeck = new Trait[89];

Trait* usedCards = new Trait[129];

#pragma endregion GLOBAL

#pragma region MAIN

// preparations

void shuffleLargeDeck();

// phase 1
void giveRandomCardTwoPerson(Player & player1);
void dealCardsTwoPerson(Player & player1, Player & player2);

// phase 2

void acceptFoodCard(Player player);

// phase 3

void performActionPhaseThree(Player & player, int nTraitNumber);
void recursivePhaseThreeMenu(Player & player);

// phase 4

void flipCards(Player & player1, Player & player2);
void phaseFourRecursiveMenu(Player & playerOff, Player & playerDef);
void feedSpecies(Player & playerOff, Player & playerDef, int n);

bool hasFatTissue(Species & species);
bool hasIntelligence(Species & species);
bool hasFertile(Species & species);
bool checkIfFoodNeeded(Player & player);

int phaseFourSelectForSpecialTraits();
int phaseFourSelect();
int checkForAttack(Player & playerOff, int spc_off, Player & playerDef, int spc_def);

#pragma endregion MAIN

#pragma region AUXILIARY

void testingFunction(Player player1);
void compressPlayersDeck(Player & player);
void changeRoles(Player & player1, Player & player2);

bool checkIfPlayerHasThatTrait(Player& player, int n);
bool checkIfPlayerHasThatSpecies(Player& player, int number);
bool checkIfSpeciesHasThatTrait(Species & species, int number);

int universalSelectorTest(string type, Player & playerOff, Player & playerDef, int nPhase);
int selectTrait(Player & player);
int selectSpecies(Player & player);
int playersTraitsTotal(Player & player);
int playersSpeciesTotal(Player & player);

#pragma endregion AUXILIARY

int main() {
    srand(time(NULL));
    //
    shuffleLargeDeck();
    players_counter = 2;
    if (players_counter == 2) {
        //preparing for the main loop

#pragma region PREP4LOOP

        Player player1;
        player1 = createNewPlayer("player", 1);
        Player player2;
        player2 = createNewPlayer("player", 2);
        cout << "\n\n\tPlayer 1, please enter your name: \n\t >> ";
        cin >> player1.nickname;
        space;
        cout << "\n\tPlayer 2, please enter your name: \n\t >> ";
        cin >> player2.nickname;
        cout << "\n\n\t>>Fine! Let's begin then ..... ";
        this_thread::sleep_for(2000ms);
        // flip a coin
        int coin = 1 + rand() % 2;
        switch (coin) {
        case 1:
            player1.bMarker = true;
            player2.bMarker = false;
            break;
        case 2:
            player2.bMarker = true;
            player1.bMarker = false;
            break;
        }
        system("cls");

#pragma endregion PREP4LOOP

        while (!bGameOver) {

            nPhase = 1;

            // phase 1 ============================================

            dealCardsTwoPerson(player1, player2);

            // phase 2 ============================================

#pragma region PHASE2

            nPhase = 2;
            drawTurn(player1, player2, nRound, nPhase, nPoolFod);
            
            if (player1.bMarker == true) { // first player's turn
                drawMenuForPlayer(player1, nPhase);
                setCursor(33, 5);
                acceptFoodCard(player1);
                compressPlayersDeck(player1);
            }
            else if (player2.bMarker == true) {
                drawMenuForPlayer(player2, nPhase);
                setCursor(33, 5);
                acceptFoodCard(player2);
                compressPlayersDeck(player2);
            }

            changeRoles(player1, player2);
            drawTurn(player1, player2, nRound, nPhase, nPoolFod);

            if (player1.bMarker == true) { // second player's turn
                drawMenuForPlayer(player1, nPhase);
                setCursor(33, 5);
                acceptFoodCard(player1);
                compressPlayersDeck(player1);
            }
            else if (player2.bMarker == true) {
                drawMenuForPlayer(player2, nPhase);
                setCursor(33, 5);
                acceptFoodCard(player2);
                compressPlayersDeck(player2);
            }

            clearMenuField();
            changeRoles(player1, player2);

#pragma endregion PHASE2

            // phase 3 ============================================

#pragma region PHASE3

            nPhase = 3;

            system("cls");
            drawTurn(player1, player2, nRound, nPhase, nPoolFod);
            bTurnOver = false;

            if (player1.bMarker == true) { // first player's turn
                while (!bTurnOver) {
                    if (playersTraitsTotal(player1) == 0) bTurnOver = true;
                    recursivePhaseThreeMenu(player1);
                    system("cls");
                    drawHubForTwo(player1, player2, nRound, nPhase, nPoolFod);
                }
            }
            else if (player2.bMarker == true) {
                while (!bTurnOver) {
                    if (playersTraitsTotal(player2) == 0) bTurnOver = true;
                    recursivePhaseThreeMenu(player2);
                    system("cls");
                    drawHubForTwo(player1, player2, nRound, nPhase, nPoolFod);
                }
            }

            system("cls");

            changeRoles(player1, player2);
            drawTurn(player1, player2, nRound, nPhase, nPoolFod);
            bTurnOver = false;


            if (player1.bMarker == true) { // second player's turn
                while (!bTurnOver) {
                    if (playersTraitsTotal(player1) == 0) bTurnOver = true;
                    recursivePhaseThreeMenu(player1);
                    system("cls");
                    drawHubForTwo(player1, player2, nRound, nPhase, nPoolFod);
                }
            }
            else if (player2.bMarker == true) {
                while (!bTurnOver) {
                    if (playersTraitsTotal(player2) == 0) bTurnOver = true;
                    recursivePhaseThreeMenu(player2);
                    system("cls");
                    drawHubForTwo(player1, player2, nRound, nPhase, nPoolFod);
                }
            }

#pragma endregion PHASE3

            // phase 4 ============================================

#pragma region PHASE4

            nPhase = 4;
            flipCards(player1, player2);

            for (int i = 0; i < 5; i++) {
                if (hasFertile(player1.pl_species[i])) player1.pl_species[i].nPopulation++;
            }

            for (int i = 0; i < 5; i++) {
                if (hasFertile(player1.pl_species[i])) player2.pl_species[i].nPopulation++;
            }

            if (nTempFodContain >= 0) nPoolFod += nTempFodContain;
            else nPoolFod = 0;

            nTempFodContain = 0;
            bTurnOver = false;

            bPlayer1 = false;
            bPlayer2 = false;
            bPlayersLastTurn = false;

            if (nPoolFod > 0) {

                while (bPlayer1 == false || bPlayer2 == false) {
                    //////////////////////////////////////////////////////// here!

                    if (checkIfFoodNeeded(player1) == false) bPlayer1 = true;
                    else if (checkIfFoodNeeded(player2) == false) bPlayer2 = true;

                    if (bPlayersLastTurn == false) {
                        changeRoles(player1, player2);
                        drawTurn(player1, player2, nRound, nPhase, nPoolFod);
                    }

                    if (!bPlayer1 && !bPlayer2) {
                        bPlayersLastTurn = false;
                    }
                    else bPlayersLastTurn = true;

                    if (player1.bMarker == true && bPlayer1 == false) { // first player's turn
                        phaseFourRecursiveMenu(player1, player2);

                    }
                    else if (player2.bMarker == true && bPlayer2 == false) {
                        phaseFourRecursiveMenu(player2, player1);

                    }
                }
            }

#pragma endregion PHASE4


            // preparations for the next round ==============================
#pragma region PREP

            // changing roles back to complete a loop
            changeRoles(player1, player2);
            
            // summarising the players' score
            for (int i = 0; i < 5; i++) {
                if (player1.pl_species[i].nPopulation != 0) {
                    player1.nScore += player1.pl_species[i].nFood;
                }
                if (player2.pl_species[i].nPopulation != 0) {
                    player2.nScore += player1.pl_species[i].nFood;
                }
            }
            // cutting population down according to their food status
            for (int i = 0; i < 5; i++) {
                if (player1.pl_species[i].nPopulation > player1.pl_species[i].nFood + player1.pl_species[i].nFatTissue) player1.pl_species[i].nPopulation = player1.pl_species[i].nFood + player1.pl_species[i].nFatTissue;
                if (player2.pl_species[i].nPopulation > player2.pl_species[i].nFood + player2.pl_species[i].nFatTissue) player2.pl_species[i].nPopulation = player2.pl_species[i].nFood + player1.pl_species[i].nFatTissue;
            }
            // clearing the food scale of all species
            for (int i = 0; i < 5; i++) {
                if (player1.pl_species[i].nPopulation != 0) {
                    player1.pl_species[i].nFood = 0;
                    player1.pl_species[i].nFatTissue = 0;
                }
                if (player2.pl_species[i].nPopulation != 0) {
                    player2.pl_species[i].nFood = 0;
                    player2.pl_species[i].nFatTissue = 0;
                }
            }
            // switching the inactive traits
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 3; j++) {
                    if (player1.pl_species[i].spc_traits[j].bTrigger == false) player1.pl_species[i].spc_traits[j].bTrigger = true;
                    if (player2.pl_species[i].spc_traits[j].bTrigger == false) player2.pl_species[i].spc_traits[j].bTrigger = true;
                }
            }
            // checking for the victory in case of a sudden death
            if (playersSpeciesTotal(player1) == 0) {
                bGameOver = true;
                break;
                system("cls");
                setCursor(20, 10);
                cout << player2.nickname << " has won!";
                setCursor(20, 25);
            }
            else if (playersSpeciesTotal(player2) == 0) {
                bGameOver = true;
                break;
                system("cls");
                setCursor(20, 10);
                cout << player1.nickname << " has won!";
                setCursor(20, 25);
            }
            else if (playersSpeciesTotal(player1) == 0 && playersSpeciesTotal(player2) == 0) {
                bGameOver = true;
                break;
                system("cls");
                setCursor(20, 10);
                cout << "Draw!";
                setCursor(20, 25);
            }
            // same procedure but for the score victory
            if (bLastRound == true) {
                bGameOver = true;
                break;
                if (player1.nScore > player2.nScore) {
                    system("cls");
                    setCursor(20, 10);
                    cout << player1.nickname << " has won!";
                    setCursor(20, 25);
                }
                else if (player1.nScore < player2.nScore) {
                    system("cls");
                    setCursor(20, 10);
                    cout << player2.nickname << " has won!";
                    setCursor(20, 25);
                }
            }
            nRound++;
            system("cls");
#pragma endregion PREP

        }
    }
    system("pause>0");
    return 0;
}

bool checkIfFoodNeeded(Player& player) {
    for (int i = 0; i < 5; i++) {
        if (player.pl_species[i].nFood < player.pl_species[i].nPopulation) return true;
    }
    return false;
}

void testingFunction(Player player) {
    setCursor(24, 60);
    cout << player.pl_traits[5].sName;
} 

void flipCards(Player& player1, Player& player2) {
    for (int i = 0; i < 5; i++) {
        for (int j = 0; j < 3; j++) {
            if (player1.pl_species[i].spc_traits[j].bFacedown == true) player1.pl_species[i].spc_traits[j].bFacedown = false;
        }
    }

    for (int i = 0; i < 5; i++) {
        for (int j = 0; j < 3; j++) {
            if (player2.pl_species[i].spc_traits[j].bFacedown == true) player2.pl_species[i].spc_traits[j].bFacedown = false;
        }
    }
}

bool hasFertile(Species& species) {
    for (int i = 0; i < 3; i++) {
        if (species.spc_traits[i].sName == "Fertile") return true;
    }
    return false;
}

void phaseFourRecursiveMenu(Player& playerOff, Player& playerDef) {
    if (playersSpeciesTotal(playerOff) == 0) {
        if (playerOff.nNumber == 1) bPlayer1 = true;
        else bPlayer2 = true;
        return;
    }
    int tempMenu = 0, tempSpecies = 0, tempTrait = 0;
    drawMenuForPlayer(playerOff, nPhase);
    tempMenu = universalSelectorTest("action", playerOff, playerDef, 4); // phaseFourSelect()
    if (tempMenu == 0) {
        showSpeciesList(playerOff);
        tempSpecies = universalSelectorTest("species", playerOff, playerDef, nPhase);
        if (playerOff.pl_species[tempSpecies].nFood < playerOff.pl_species[tempSpecies].nPopulation && hasFatTissue(playerOff.pl_species[tempSpecies]) == false) {
            feedSpecies(playerOff, playerDef, tempSpecies);
            if (playerOff.bMarker == true) {
                if (playersSpeciesTotal(playerOff) == 2) clearMenuFieldMid();
                else if (playersSpeciesTotal(playerOff) >= 3) clearMenuFieldLong();
                else clearMenuField();
                if (playerOff.nNumber == 1) drawPlayer(playerOff, nPlayer1X, nPlayer1Y);
                else drawPlayer(playerOff, nPlayer2X, nPlayer2Y);
            }
        }
        else if (playerOff.pl_species[tempSpecies].nFood == playerOff.pl_species[tempSpecies].nPopulation && hasFatTissue(playerOff.pl_species[tempSpecies]) == false) {
            if (playersSpeciesTotal(playerOff) >= 3) clearMenuFieldLong();
            else clearMenuField();
            setCursor(menuX, menuY);
            cout << "Your species is already fed!";
            setCursor(menuX, menuY+1);
            phaseFourRecursiveMenu(playerOff, playerDef);
        }

        if (playerOff.pl_species[tempSpecies].nFood >= playerOff.pl_species[tempSpecies].nPopulation && hasFatTissue(playerOff.pl_species[tempSpecies]) == true) {
            if (playerOff.pl_species[tempSpecies].nFatTissue < playerOff.pl_species[tempSpecies].nBodysize) {
                playerOff.pl_species[tempSpecies].nFatTissue += 1;
                nPoolFod -= 1;
                if (playerOff.nNumber == 1) drawPlayer(playerOff, nPlayer1X, nPlayer1Y);
                else drawPlayer(playerOff, nPlayer2X, nPlayer2Y);
            }
            else {
                setCursor(menuX, menuY);
                cout << "You cannot eat more!";
                setCursor(menuX, menuY+1);
                phaseFourRecursiveMenu(playerOff, playerDef);
            }
        }
        else if (playerOff.pl_species[tempSpecies].nFood < playerOff.pl_species[tempSpecies].nPopulation && hasFatTissue(playerOff.pl_species[tempSpecies]) == true) { // added (fixed)
            feedSpecies(playerOff, playerDef, tempSpecies);
            if (playerOff.bMarker == true) {
                if (playerOff.nNumber == 1) drawPlayer(playerOff, nPlayer1X, nPlayer1Y);
                else drawPlayer(playerOff, nPlayer2X, nPlayer2Y);
                //phaseFourRecursiveMenu(playerOff, playerDef);
            }
        }
        else if (hasIntelligence(playerOff.pl_species[tempSpecies]) == true && playersTraitsTotal(playerOff) > 0) {
            drawIntelligenceMenu(playerOff);
            tempTrait = selectTrait(playerOff);

            playerOff.pl_traits[tempTrait].sName = "None";
            playerOff.pl_traits[tempTrait].sProperty = "None";
            playerOff.pl_traits[tempTrait].nFoodCount = 0;
            playerOff.pl_traits[tempTrait].bFacedown = false;
            playerOff.pl_traits[tempTrait].bTrigger = false;

            feedSpecies(playerOff, playerDef, tempSpecies);
            if (playerOff.bMarker == true) {
                if (playersSpeciesTotal(playerOff) == 2) clearMenuFieldMid();
                else if (playersSpeciesTotal(playerOff) >= 3) clearMenuFieldLong();
                else clearMenuField();
                //phaseFourRecursiveMenu(playerOff, playerDef);
            }
        }
        else if (hasIntelligence(playerOff.pl_species[tempSpecies]) == true && playersTraitsTotal(playerOff) == 0) {
            if (playersSpeciesTotal(playerOff) >= 3) clearMenuFieldLong();
            else clearMenuField();
            setCursor(menuX-5, menuY);
            cout << "You don't have spare traits to use intelligence!";
            setCursor(menuX, menuY+1);
            phaseFourRecursiveMenu(playerOff, playerDef);
        }
    }
    else if (tempMenu == 1) {
        if (playerOff.nNumber == 1) bPlayer1 = true;
        else bPlayer2 = true;
        return;
    }
    else {
        drawMenuForPlayer(playerOff, nPhase);
        tempMenu = universalSelectorTest("action", playerOff, playerDef, 4); // phaseFourSelect()
    }
}

void feedSpecies(Player& playerOff, Player& playerDef, int n) {
    int tempSpecies = 0;
    int tempSelectTrait = 0;
    if (playerOff.pl_species[n].isCarnivore == false) {

        if (nPoolFod > 0) {
            nPoolFod -= 1;
            playerOff.pl_species[n].nFood += 1;

            for (int i = 0; i < 3; i++) {
                if (playerOff.pl_species[n].spc_traits[i].bTrigger == true && playerOff.pl_species[n].spc_traits[i].sName == "Cooperation" && n < 2) {
                    if (playerOff.pl_species[n + 1].nPopulation != 0 && playerOff.pl_species[n + 1].nFood < playerOff.pl_species[n + 1].nPopulation) playerOff.pl_species[n + 1].nFood += 1;
                }
            }

            for (int i = 0; i < 3; i++) {
                if (playerOff.pl_species[n].spc_traits[i].bTrigger == true && playerOff.pl_species[n].spc_traits[i].sName == "Long Neck" && playerOff.pl_species[n].nPopulation < playerOff.pl_species[n].nFood) {
                    playerOff.pl_species[n].nFood += 1;
                }
            }

            for (int i = 0; i < 3; i++) {
                if (playerOff.pl_species[n].spc_traits[i].bTrigger == true && playerOff.pl_species[n].spc_traits[i].sName == "Foraging" && playerOff.pl_species[n].nPopulation < playerOff.pl_species[n].nFood) {
                    playerOff.pl_species[n].nFood += 1;
                    nPoolFod -= 1;
                }
            }
        }
        else if (nPoolFod == 0) {
            system("cls");
            setCursor(20, 20);
            cout << "Thus came the age of hunger.";
            this_thread::sleep_for(2500ms);
            system("cls");
        }
        

    }
    else if (playerOff.pl_species[n].isCarnivore == true) {
        bool bGeneralFlag = false;

        for (int i = 0; i < 5; i++) {
            if (checkForAttack(playerOff, n, playerDef, i) == 2) bGeneralFlag = true;
        }

        if (bGeneralFlag == true) {
            bool bIntelligenceFlag = false;
            bool bFlag = false;
            for (int i = 0; i < 3; i++) {
                if (playerOff.pl_species[n].spc_traits[i].sName == "Intelligence") bIntelligenceFlag = true;
            }

            clearMenuField();
            showSpeciesList(playerDef);
            setCursor(menuX, menuY);
            cout << "Choose the cpecies you want to attack:";
            tempSpecies = selectSpecies(playerDef);

            int counter = 0; // counting total defensive traits
            for (int i = 0; i < 3; i++) {
                if (playerDef.pl_species[tempSpecies].spc_traits[i].sName == "Warning Call" || playerDef.pl_species[tempSpecies].spc_traits[i].sName == "Burrowing" || playerDef.pl_species[tempSpecies].spc_traits[i].sName == "Climbing" || playerDef.pl_species[tempSpecies].spc_traits[i].sName == "Defensive Herding" || playerDef.pl_species[tempSpecies].spc_traits[i].sName == "Hard Shell" || playerDef.pl_species[tempSpecies].spc_traits[i].sName == "Symbiosis" || playerDef.pl_species[tempSpecies].spc_traits[i].sName == "Horns") counter++;
            }

            if (counter <= 1 || bIntelligenceFlag == false) {
                switch (checkForAttack(playerOff, n, playerDef, tempSpecies)) {
                case 1: // if defending player somehow has no defensive traits
                    playerOff.pl_species[n].nFood += 1;
                    playerDef.pl_species[tempSpecies].nPopulation -= 1;
                    clearMenuField();
                    break;
                case 2: // if defending player has only one defensive perk but offensive player got intelligence trait
                    playerOff.pl_species[n].nFood += 1;
                    playerDef.pl_species[tempSpecies].nPopulation -= 1;
                    for (int i = 0; i < 3; i++) {
                        if (playerOff.pl_species[n].spc_traits[i].sName == "Intelligence") {
                            playerOff.pl_species[n].spc_traits[i].sName = "None";
                            playerOff.pl_species[n].spc_traits[i].sProperty = "None";
                            playerOff.pl_species[n].spc_traits[i].bFacedown = false;
                            playerOff.pl_species[n].spc_traits[i].bTrigger = false;
                            playerOff.pl_species[n].spc_traits[i].nFoodCount = 0;
                            compressPlayersDeck(playerOff);
                        }
                    }
                    clearMenuField();
                    break;
                case -1: // if defending player's bodysize is larger
                    clearMenuField();
                    setCursor(menuX, menuY - 1);
                    cout << "Insufficient Body Size!";
                    phaseFourRecursiveMenu(playerOff, playerDef);
                    break;
                case -2: // if defending player's bodysize is larger
                    for (int i = 0; i < 3; i++) {
                        if (playerOff.pl_species[n].spc_traits[i].sName == "Ambush") {
                            bFlag = true;
                            break;
                        }
                    }
                    if (bFlag == true) {
                        playerOff.pl_species[n].nFood += 1;
                        playerDef.pl_species[tempSpecies].nPopulation -= 1;
                        clearMenuField();
                    }
                    else {
                        clearMenuField();
                        setCursor(menuX, menuY - 1);
                        cout << "This species has Warning Call!";
                        phaseFourRecursiveMenu(playerOff, playerDef);
                    }
                    break;
                case -3: // if defending player got burrowing
                    clearMenuField();
                    setCursor(menuX, menuY - 1);
                    cout << "This species has Burrowing!";
                    phaseFourRecursiveMenu(playerOff, playerDef);
                    break;
                case -4: // if defending player got climing
                    for (int i = 0; i < 3; i++) {
                        if (playerOff.pl_species[n].spc_traits[i].sName == "Climbing") {
                            bFlag = true;
                            break;
                        }
                    }
                    if (bFlag == true) {
                        playerOff.pl_species[n].nFood += 1;
                        playerDef.pl_species[tempSpecies].nPopulation -= 1;
                        clearMenuField();
                    }
                    else {
                        clearMenuField();
                        setCursor(menuX, menuY - 1);
                        cout << "This species has Climbing!";
                        phaseFourRecursiveMenu(playerOff, playerDef);
                    }
                    break;
                case -5: // if defending player got DH
                    clearMenuField();
                    setCursor(menuX, menuY - 1);
                    cout << "This species has Defensive Herding!";
                    phaseFourRecursiveMenu(playerOff, playerDef);
                    break;
                case -6: // if defending player got HS
                    clearMenuField();
                    setCursor(menuX, menuY - 1);
                    cout << "This species has Hard Shell!";
                    phaseFourRecursiveMenu(playerOff, playerDef);
                    break;
                case -7: // if defending player got Symbiosis
                    clearMenuField();
                    setCursor(menuX, menuY - 1);
                    cout << "This species has Symbiosis!";
                    phaseFourRecursiveMenu(playerOff, playerDef);
                    break;
                case -8: // if defending player got Horns
                    playerOff.pl_species[n].nFood += 1;
                    playerDef.pl_species[tempSpecies].nPopulation -= 1;
                    playerOff.pl_species[n].nPopulation -= 1;
                    clearMenuField();
                    break;
                }
            }

            else if (bIntelligenceFlag == true && counter > 1) {
                clearMenuField();
                drawSpeciesTraits(playerDef.pl_species[tempSpecies]);
                tempSelectTrait = selectSpecies(playerDef); ///////////////////////// At this point I assume that using this selector will be better in that specific case
                for (int i = 0; i < 3; i++) {
                    if (playerOff.pl_species[n].spc_traits[i].sName == "Intelligence") {
                        playerOff.pl_species[n].spc_traits[i].sName = "None";
                        playerOff.pl_species[n].spc_traits[i].sProperty = "None";
                        playerOff.pl_species[n].spc_traits[i].bFacedown = false;
                        playerOff.pl_species[n].spc_traits[i].bTrigger = false;
                        playerOff.pl_species[n].spc_traits[i].nFoodCount = 0;
                        compressPlayersDeck(playerOff);
                    }
                }
                playerDef.pl_species[tempSpecies].spc_traits[tempSelectTrait].sName = "None";
                playerDef.pl_species[tempSpecies].spc_traits[tempSelectTrait].sProperty = "None";
                playerDef.pl_species[tempSpecies].spc_traits[tempSelectTrait].bFacedown = false;
                playerDef.pl_species[tempSpecies].spc_traits[tempSelectTrait].bTrigger = false;
                playerDef.pl_species[tempSpecies].spc_traits[tempSelectTrait].nFoodCount = 0;
                bIntelligenceFlag = false;
                clearMenuField();
                phaseFourRecursiveMenu(playerOff, playerDef);
            }
            else {
                clearMenuField();
                setCursor(menuX, menuY - 1);
                cout << "Unexpected error!";
                phaseFourRecursiveMenu(playerOff, playerDef);
            }
        }
        
        else {
            clearMenuField();
            setCursor(menuX, menuY-1);
            cout << "To survive, you have to attack your own species!";
            showSpeciesList(playerDef);
            setCursor(menuX, menuY);
            cout << "Choose the cpecies you want to attack:";
            tempSpecies = selectSpecies(playerOff);
            playerOff.pl_species[n].nFood += 1;
            playerDef.pl_species[tempSpecies].nPopulation -= 1;
        }
    }
}

int checkForAttack(Player& playerOff, int spc_off, Player& playerDef, int spc_def) {
    bool bBodySizeFlag = false;             // -1
    bool bWarningCallFlag = false;          // -2
    bool bBurrowingFlag = false;            // -3
    bool bClimbingFlag = false;             // -4
    bool bDefensiveHerdingFlag = false;     // -5
    bool bHardShellFlag = false;            // -6
    bool bSymbiosisFlag = false;            // -7
    bool bHornsFlag = false;                // -8
    bool bIntelligenceFlag = false;         //  2

    bool bPackHuntingFlag = false; // some attacking traits

    for (int i = 0; i < 3; i++) {
        if (playerOff.pl_species[spc_off].spc_traits[i].sName == "Pack Hunting" && playerOff.pl_species[spc_off].spc_traits[i].bTrigger == true) {
            bPackHuntingFlag = true;
            break;
        }
    }

    if (bPackHuntingFlag == true) {
        playerOff.pl_species[spc_off].nBodysize += 3;
    }

    // Intelligence =============================================================================================
    for (int i = 0; i < 3; i++) {
        if (playerOff.pl_species[spc_off].spc_traits[i].sName == "Intelligence" && playerOff.pl_species[spc_off].spc_traits[i].bTrigger == true) bIntelligenceFlag = true;
    }

    // Body Size ================================================================================================
    if (playerOff.pl_species[spc_off].nBodysize <= playerDef.pl_species[spc_def].nBodysize) bBodySizeFlag = true;

    // Warning Call =============================================================================================
    if (spc_def == 0) {
        for (int i = 0; i < 3; i++) {
            if (playerDef.pl_species[spc_def + 1].spc_traits[i].sName == "Warning Call" && playerDef.pl_species[spc_def + 1].spc_traits[i].bTrigger == true) bWarningCallFlag = true;
        }
    }
    else if (spc_def == 1) {
        for (int i = 0; i < 3; i++) {
            if (playerDef.pl_species[spc_def - 1].spc_traits[i].sName == "Warning Call" && playerDef.pl_species[spc_def - 1].spc_traits[i].bTrigger == true) bWarningCallFlag = true;
            if (playerDef.pl_species[spc_def + 1].spc_traits[i].sName == "Warning Call" && playerDef.pl_species[spc_def + 1].spc_traits[i].bTrigger == true) bWarningCallFlag = true;
        }
    }
    else if (spc_def == 2) {
        for (int i = 0; i < 3; i++) {
            if (playerDef.pl_species[spc_def - 1].spc_traits[i].sName == "Warning Call" && playerDef.pl_species[spc_def - 1].spc_traits[i].bTrigger == true) bWarningCallFlag = true;
        }
    }

    // Burrowing ================================================================================================
    for (int i = 0; i < 3; i++) {
        if (playerDef.pl_species[spc_def].spc_traits[i].sName == "Burrowing" && playerDef.pl_species[spc_def].spc_traits[i].bTrigger == true) {
            if (playerDef.pl_species[spc_def].nFood == playerDef.pl_species[spc_def].nPopulation) bBurrowingFlag = true;
        }
    }

    // Climbing =================================================================================================
    bool bTempFlag = false;
    for (int i = 0; i < 3; i++) {
        bTempFlag = false;
        if (playerDef.pl_species[spc_def].spc_traits[i].sName == "Climbing" && playerDef.pl_species[spc_def].spc_traits[i].bTrigger == true) {
            for (int j = 0; j < 3; j++) {
                if (playerOff.pl_species[spc_off].spc_traits[i].sName == "Climbing" && playerOff.pl_species[spc_off].spc_traits[i].bTrigger == true) {
                    bTempFlag = true;
                    break;
                }
            }
            if (bTempFlag != true) bClimbingFlag = true;
        }
    }

    // Defensive Herding =========================================================================================
    for (int i = 0; i < 3; i++) {
        if (playerDef.pl_species[spc_def].spc_traits[i].sName == "Defensive Herding" && playerDef.pl_species[spc_def].spc_traits[i].bTrigger == true) {
            if (playerDef.pl_species[spc_def].nPopulation >= playerOff.pl_species[spc_off].nPopulation && playerDef.pl_species[spc_def].nBodysize >= playerOff.pl_species[spc_off].nBodysize) {
                bDefensiveHerdingFlag = true;
            }
        }
    }

    // Hard Shell ================================================================================================
    for (int i = 0; i < 3; i++) {
        if (playerDef.pl_species[spc_def].spc_traits[i].sName == "Hard Shell" && playerDef.pl_species[spc_def].spc_traits[i].bTrigger == true) {
            if (playerDef.pl_species[spc_def].nBodysize + 3 >= playerOff.pl_species[spc_off].nBodysize) bHardShellFlag = true;
        }
    }

    // Symbiosis =================================================================================================
    for (int i = 0; i < 3; i++) {
        if (playerDef.pl_species[spc_def].spc_traits[i].sName == "Symbiosis" && playerDef.pl_species[spc_def].spc_traits[i].bTrigger == true) {
            if (spc_def == 1) {
                if (playerOff.pl_species[spc_off].nBodysize <= playerDef.pl_species[spc_def + 1].nBodysize) bSymbiosisFlag = true;
            }
            else if (spc_def == 0) {
                if (playerOff.pl_species[spc_off].nBodysize <= playerDef.pl_species[spc_def + 1].nBodysize) bSymbiosisFlag = true;
            }
            else bSymbiosisFlag = false;
        }
    }

    // Horns ====================================================================================================
    for (int i = 0; i < 3; i++) {
        if (playerDef.pl_species[spc_def].spc_traits[i].sName == "Horns" && playerDef.pl_species[spc_def].spc_traits[i].bTrigger == true) bHornsFlag = true;
    }

    int counter = 0; // counting total defensive traits
    for (int i = 0; i < 3; i++) {
        if (playerDef.pl_species[spc_def].spc_traits[i].sName == "Warning Call" || playerDef.pl_species[spc_def].spc_traits[i].sName == "Burrowing" || playerDef.pl_species[spc_def].spc_traits[i].sName == "Climbing" || playerDef.pl_species[spc_def].spc_traits[i].sName == "Defensive Herding" || playerDef.pl_species[spc_def].spc_traits[i].sName == "Hard Shell" || playerDef.pl_species[spc_def].spc_traits[i].sName == "Symbiosis" || playerDef.pl_species[spc_def].spc_traits[i].sName == "Horns" && playerDef.pl_species[spc_def].spc_traits[i].bTrigger == true) counter++;
    }

    if (bPackHuntingFlag == true) {
        playerOff.pl_species[spc_off].nBodysize -= 3;
    }

    if (counter == 0) return 1;
    else if (counter == 1 && bIntelligenceFlag == true) return 2;
    
    if (bBodySizeFlag == true) return -1;
    else if (bWarningCallFlag == true) return -2;
    else if (bBurrowingFlag == true) return -3;
    else if (bClimbingFlag == true) return -4;
    else if (bDefensiveHerdingFlag == true) return -5;
    else if (bHardShellFlag == true) return -6;
    else if (bSymbiosisFlag == true) return -7;
    else if (bHornsFlag == true) return -8;
    else return -10; // although it shouldn't normally return something like this
}

bool hasFatTissue(Species& species) {
    for (int i = 0; i < 3; i++) {
        if (species.spc_traits[i].sName == "Fat Tissue") return true;
    }
    return false;
}

bool hasIntelligence(Species& species) {
    for (int i = 0; i < 3; i++) {
        if (species.spc_traits[i].sName == "Intelligence") return true;
    }
    return false;
}

int phaseFourSelect() {
    char user;
    int n = 0;
    user = _getch();
    switch (user) {
    case '1' :
        n = 0;
        break;
    case '2' :
        n = 1;
        break;
    }
    return n;
}

int universalSelectorTest(string type, Player& playerOff, Player& playerDef, int nPhase) {
    bool bTempFlag = false;
    if (type == "species") {
        char user = 0;
        int n = 0;
        do {
            user = _getch();
            switch (user) {
            case '1':
                n = 0;
                if (checkIfPlayerHasThatSpecies(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '2':
                n = 1;
                if (checkIfPlayerHasThatSpecies(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '3':
                n = 2;
                if (checkIfPlayerHasThatSpecies(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '4':
                n = 3;
                if (checkIfPlayerHasThatSpecies(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '5':
                n = 4;
                if (checkIfPlayerHasThatSpecies(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '0':
                n = 38;
                break;
            default:
                n = 38;
                break;
            }

            if (n >= 5 && n != 38) {
                if (playersSpeciesTotal(playerOff) >= 3) clearMenuFieldLong();
                else clearMenuField();
                setCursor(menuX, menuY - 1);
                cout << "Please, enter a proper one!";
                showSpeciesList(playerOff);
            }
            else break;

        } while (n >= 5 && n != 38);
        return n;
    }
    else if (type == "players trait") {
        char user = 0;
        int n = 0;
        do {
            user = _getch();
            switch (user) {
            case '1':
                n = 0;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '2':
                n = 1;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '3':
                n = 2;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '4':
                n = 3;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '5':
                n = 4;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '6':
                n = 5;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '7':
                n = 6;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '8':
                n = 7;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '9':
                n = 8;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '10':
                n = 9;
                if (checkIfPlayerHasThatTrait(playerOff, n) == true) {
                    break;
                }
                else n = 11;
                break;
            case '0':
                n = 38;
                break;
            }

            if (n > playersTraitsTotal(playerOff) && n != 38) {
                clearMenuField();
                setCursor(menuX, menuY - 1);
                cout << "Please, enter a proper one!";
                drawMenuForPlayer(playerOff, nPhase);
            }
            else break;

        } while (n > playersTraitsTotal(playerOff) && n != 38);
        return n;
    }
    else if (type == "species trait def") {
    char user = 0;
    int n = 0;
    do {
        bTempFlag = false;
        user = _getch();
        switch (user) {
        case '1':
            n = 0;
            for (int i = 0; i < 5; i++) {
                if (checkIfSpeciesHasThatTrait(playerDef.pl_species[i], n) == true) {
                    bTempFlag = true;
                    break;
                }
            }
            if (bTempFlag == true) {
                break;
            }
            else n = 11;
            break;
        case '2':
            n = 1;
            for (int i = 0; i < 5; i++) {
                if (checkIfSpeciesHasThatTrait(playerDef.pl_species[i], n) == true) {
                    bTempFlag = true;
                    break;
                }
            }
            if (bTempFlag == true) {
                break;
            }
            else n = 11;
            break;
        case '3':
            n = 2;
            for (int i = 0; i < 5; i++) {
                if (checkIfSpeciesHasThatTrait(playerDef.pl_species[i], n) == true) {
                    bTempFlag = true;
                    break;
                }
            }
            if (bTempFlag == true) {
                break;
            }
            else n = 11;
            break;
        
        default:
            n = 38;
            break;
        }

        if (n != 38) {
            clearMenuField();
            setCursor(menuX, menuY - 1);
            cout << "Please, enter a proper one!";
            drawIntelligenceMenu(playerOff);
        }
        else break;

    } while (n != 38);
    return n;
    }
    else if (type == "action") {
    char user1 = 0;
    int n1 = 0;
    switch (nPhase) {
    case 3:
        break;
    case 4:
        do {
            user1 = _getch();
            switch (user1) {
            case '1':
                n1 = 0;
                break;
            case '2':
                n1 = 1;
                break;
            default:
                n1 = 38;
                break;
            }

            if (n1 >= 2 && n1 != 38) {
                setCursor(menuX, menuY - 1);
                cout << "Please, enter a proper one!";
                drawMenuForPlayer(playerOff, nPhase);
            }
            else break;

        } while (n1 >= 5 && n1 != 38);
        return n1;
        break;
    default:
        return n1;
        }
    }
    else return -1;
}

int phaseFourSelectForSpecialTraits() {
    char user;
    int n = 0;
    user = _getch();
    switch (user) {
    case '1':
        n = 0;
        break;
    case '2':
        n = 1;
        break;
    }
    return n;
}

void recursivePhaseThreeMenu(Player& player) {
    drawMenuForPlayer(player, nPhase);
    nTemp = selectTrait(player);
    if (nTemp == 38) {
        bTurnOver = true;
        return;
    }
    drawSubMenuForPlayer(player, nTemp);
    performActionPhaseThree(player, nTemp);
    setCursor(33, 5);
    if (playersTraitsTotal(player) == 0) {
        bTurnOver = true;
        return;
    }
}

void performActionPhaseThree(Player& player, int nTraitNumber) {
    char user, subuser;
    bool bNoSpace = true;
    int temp = 0;
    user = _getch();
    switch (user) {
    case '1':
        clearMenuField();
        setCursor(3, 2);
        cout << player.pl_traits[nTraitNumber].sName << ": " << player.pl_traits[nTraitNumber].sProperty;
        setCursor(menuX, menuY+4);
        setCursor(3, 3);
        cout << "(press any key to get back)";
        _getch();
        for (int i = 3; i < 100; i++) {
            setCursor(i, 2);
            cout << " ";
            setCursor(i, 3);
            cout << " ";
        }
        clearMenuField();
        recursivePhaseThreeMenu(player);
        break;
    case '2':
        showSpeciesList(player);
        temp = selectSpecies(player);
        for (int i = 0; i < 3; i++) {
            if (player.pl_species[temp].spc_traits[i].sName == "None") {

                player.pl_species[temp].spc_traits[i] = player.pl_traits[nTraitNumber];

                player.pl_traits[nTraitNumber].sName = "None";
                player.pl_traits[nTraitNumber].sProperty = "None";
                player.pl_traits[nTraitNumber].nFoodCount = 0;
                player.pl_traits[nTraitNumber].bFacedown = false;
                player.pl_traits[nTraitNumber].bTrigger = false;

                compressPlayersDeck(player);

                if (player.pl_species[temp].spc_traits[i].sName == "Carnivore") player.pl_species[temp].isCarnivore = true;

                bNoSpace = false;
                break;
            }
        }
        if (bNoSpace) {
            clearMenuField();
            setCursor(menuX, menuY);
            cout << "A species already have 3 traits. It cannot have more than 3.";
            setCursor(menuX, menuY+2);
            cout << "(press any key to get back)";
            _getch();
            clearMenuField();
            recursivePhaseThreeMenu(player);
        }
        break;
    case '3':
        showSpeciesList(player);
        temp = selectSpecies(player);
        if (player.pl_species[temp].nBodysize < 6) {
            player.pl_species[temp].nBodysize += 1;

            player.pl_traits[nTraitNumber].sName = "None";
            player.pl_traits[nTraitNumber].sProperty = "None";
            player.pl_traits[nTraitNumber].nFoodCount = 0;
            player.pl_traits[nTraitNumber].bFacedown = false;
            player.pl_traits[nTraitNumber].bTrigger = false;

            compressPlayersDeck(player);
        }
        else if (player.pl_species[temp].nBodysize == 6) {
            clearMenuField();
            setCursor(menuX, menuY);
            cout << "A species cannot have Body Size more than 6.";
            setCursor(menuX, menuY+2);
            cout << "(press any key to get back)";
            _getch();
            clearMenuField();
            recursivePhaseThreeMenu(player);
        }
        break;

    case '4':
        showSpeciesList(player);
        temp = selectSpecies(player);
        if (player.pl_species[temp].nPopulation < 6) {
            player.pl_species[temp].nPopulation += 1;

            player.pl_traits[nTraitNumber].sName = "None";
            player.pl_traits[nTraitNumber].sProperty = "None";
            player.pl_traits[nTraitNumber].nFoodCount = 0;
            player.pl_traits[nTraitNumber].bFacedown = false;
            player.pl_traits[nTraitNumber].bTrigger = false;

            compressPlayersDeck(player);
        }
        else if (player.pl_species[temp].nPopulation == 6) {
            clearMenuField();
            setCursor(menuX, menuY);
            cout << "A species cannot have Population more than 6.";
            setCursor(menuX, menuY+2);
            cout << "(press any key to get back)";
            _getch();
            clearMenuField();
            recursivePhaseThreeMenu(player);
        }
        break;

    case '5':
        if (playersSpeciesTotal(player) < 5) {
            for (int i = 0; i < 5; i++) {
                if (player.pl_species[i].nPopulation == 0) {
                    player.pl_species[i] = createNewSpecies();
                    break;
                }
            }
            player.pl_traits[nTraitNumber].sName = "None";
            player.pl_traits[nTraitNumber].sProperty = "None";
            player.pl_traits[nTraitNumber].nFoodCount = 0;
            player.pl_traits[nTraitNumber].bFacedown = false;
            player.pl_traits[nTraitNumber].bTrigger = false;

            compressPlayersDeck(player);
        }
        else {
            clearMenuField();
            setCursor(menuX, menuY);
            cout << "You cannot have more than 5 species.";
            setCursor(menuX, menuY+2);
            cout << "(press any key to get back)";
            _getch();
            clearMenuField();
            recursivePhaseThreeMenu(player);
        }
        break;

    case '6':
        bTurnOver = true;
        break;
    }
}

int selectSpecies(Player& player) {
    char user;
    int n = 0;
    user = _getch();
    switch (user) {
        case '1':
            n = 0;
            break;
        case '2':
            n = 1;
            break;
        case '3':
            n = 2;
            break;
    }
    return n;
}

int selectTrait(Player& player) {
    char user = 0;
    int n = 0;
    do {
        user = _getch();
        switch (user) {
        case '1':
            n = 0;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '2':
            n = 1;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '3':
            n = 2;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '4':
            n = 3;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '5':
            n = 4;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '6':
            n = 5;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '7':
            n = 6;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '8':
            n = 7;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '9':
            n = 8;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '10':
            n = 9;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                break;
            }
            else n = 11;
            break;
        case '0':
            n = 38;
            break;
        }

        if (n > playersTraitsTotal(player) && n != 38) {
            clearMenuField();
            setCursor(menuX, menuY-1);
            cout << "Please, enter a proper one!";
            drawMenuForPlayer(player, nPhase);
        }
        else break;

    } while (n > playersTraitsTotal(player) && n != 38);
    return n;
}

void compressPlayersDeck(Player& player) {
    int N = playersTraitsTotal(player);
    for (int i = 0; i < 9; i++) {
        for (int j = 0; j < 9; j++) {
            if (player.pl_traits[j].sName == "None" && player.pl_traits[j + 1].sName != "None") swap(player.pl_traits[j + 1], player.pl_traits[j]);
        }
    }
}

void changeRoles(Player& player1, Player& player2) {
    if (player1.bMarker == true) {
        player1.bMarker = false;
        player2.bMarker = true;
    }
    else {
        player1.bMarker = true;
        player2.bMarker = false;
    }
}

void acceptFoodCard(Player player) {
    char user = 0;
    int n = 0;
    do {
        user = _getch();
        switch (user) {
        case '1':
            n = 0;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '2':
            n = 1;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '3':
            n = 2;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '4':
            n = 3;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '5':
            n = 4;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '6':
            n = 5;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '7':
            n = 6;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '8':
            n = 7;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '9':
            n = 8;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        case '10':
            n = 9;
            if (checkIfPlayerHasThatTrait(player, n) == true) {
                nTempFodContain += player.pl_traits[n].nFoodCount;
                player.pl_traits[n].sName = "None";
                player.pl_traits[n].nFoodCount = 0;
                player.pl_traits[n].sProperty = "None";
                player.pl_traits[n].bFacedown = false;
                player.pl_traits[n].bTrigger = false;
                //break;
            }
            else n = 11;
            break;
        }

        if (n > playersTraitsTotal(player)) {
            clearMenuField();
            setCursor(35, 46);
            cout << "Please, enter a proper one!";
            drawMenuForPlayer(player, nPhase);
        }
        else break;

    } while (n > playersTraitsTotal(player));
}

void shuffleLargeDeck() {
    int nCarnivore = 17;
    int nOneTraitCards = 7;

    // those below are counters that are needed to create a deck
    int nAmbushCount = 0;
    int nBurrowingCount = 0;
    int nCarnivoreCount = 0;
    int nClimbingCount = 0;
    int nCooperationCount = 0;
    int nDefensiveHerdingCount = 0;
    int nFatTissueCount = 0;
    int nFertileCount = 0;
    int nForagingCount = 0;
    int nHardShellCount = 0;
    int nHornsCount = 0;
    int nIntelligenceCount = 0;
    int nLongNeckCount = 0;
    int nPackHuntingCount = 0;
    int nScavengerCount = 0;
    int nSymbiosisCount = 0;
    int nWarningCallCount = 0;

    //struct Trait {
    //    string sName;        // the name of a card
    //    string sProperty;    // the property of a trait means what action does this card do;
    //    int nFoodCount;      // determins what amount of food does the trait increase or decrease in the Pool;
    //    bool bFacedown;      // is the card facedown or not;
    //    bool bTrigger;       // has the card got trigger or it is going to implement its property only in the next round;
    //};

    for (int i = 0; i < 129; i++) {
        Trait currentTrait;

        if (nCarnivoreCount < 17) {
            currentTrait.sName = "Carnivore";
            currentTrait.sProperty = propertyCarnivore;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = true;
            nCarnivoreCount++;
        }

        else if (nAmbushCount < 7) {
            currentTrait.sName = "Ambush";
            currentTrait.sProperty = propertyAmbush;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nAmbushCount++;
        }

        else if (nBurrowingCount < 7) {
            currentTrait.sName = "Burrowing";
            currentTrait.sProperty = propertyBurrowing;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nBurrowingCount++;
        }

        else if (nClimbingCount < 7) {
            currentTrait.sName = "Climbing";
            currentTrait.sProperty = propertyClimbing;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nClimbingCount++;
        }

        else if (nCooperationCount < 7) {
            currentTrait.sName = "Cooperation";
            currentTrait.sProperty = propertyCooperation;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nCooperationCount++;
        }

        else if (nDefensiveHerdingCount < 7) {
            currentTrait.sName = "Defensive Herding";
            currentTrait.sProperty = propertyDefensiveHerding;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nDefensiveHerdingCount++;
        }

        else if (nFatTissueCount < 7) {
            currentTrait.sName = "Fat Tissue";
            currentTrait.sProperty = propertyFatTissue;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = true;
            nFatTissueCount++;
        }

        else if (nFertileCount < 7) {
            currentTrait.sName = "Fertile";
            currentTrait.sProperty = propertyFertile;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = true;
            nFertileCount++;
        }

        else if (nForagingCount < 7) {
            currentTrait.sName = "Foraging";
            currentTrait.sProperty = propertyForaging;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nForagingCount++;
        }

        else if (nHardShellCount < 7) {
            currentTrait.sName = "Hard Shell";
            currentTrait.sProperty = propertyHardShell;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nHardShellCount++;
        }

        else if (nHornsCount < 7) {
            currentTrait.sName = "Horns";
            currentTrait.sProperty = propertyHorns;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nHornsCount++;
        }

        else if (nIntelligenceCount < 7) {
            currentTrait.sName = "Intelligence";
            currentTrait.sProperty = propertyIntelligence;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nIntelligenceCount++;
        }

        else if (nLongNeckCount < 7) {
            currentTrait.sName = "Long Neck";
            currentTrait.sProperty = propertyLongNeck;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = true;
            nLongNeckCount++;
        }

        else if (nPackHuntingCount < 7) {
            currentTrait.sName = "Pack Hunting";
            currentTrait.sProperty = propertyPackHunting;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nPackHuntingCount++;
        }

        else if (nScavengerCount < 7) {
            currentTrait.sName = "Scavenger";
            currentTrait.sProperty = propertyScavenger;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nScavengerCount++;
        }

        else if (nSymbiosisCount < 7) {
            currentTrait.sName = "Symbiosis";
            currentTrait.sProperty = propertySymbiosis;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nSymbiosisCount++;
        }

        else if (nWarningCallCount < 7) {
            currentTrait.sName = "Warning Call";
            currentTrait.sProperty = propertyWarningCall;
            currentTrait.nFoodCount = -3 + rand() % 13;
            currentTrait.bFacedown = true;
            currentTrait.bTrigger = false;
            nWarningCallCount++;
        }

        largeDeck[i] = currentTrait;
    }

    // We have filled our deck with cards, now it's time to shuffle it.
    Trait temp;
    for (int i = 0; i < 129; i++) {
        temp = largeDeck[i + rand() % 129 - i];
        largeDeck[i + rand() % 129 - i] = largeDeck[i];
        largeDeck[i] = temp;
    }

    // Now we need to make a small deck just in case.

    for (int i = 0; i < 89; i++) {
        smallDeck[i] = largeDeck[i];
    }

    // And prepare user cards deck.

    for (int i = 0; i < 129; i++) {
        usedCards[i].sName = "None";
        usedCards[i].nFoodCount = 0;
        usedCards[i].sProperty = "None";
        usedCards[i].bFacedown = false;
        usedCards[i].bTrigger = false;
    }

#ifdef showDeck
    space;
    for (int i = 0; i < 129; i++) {
        cout << largeDeck[i].sName << " | ";
        if (i % 5 == 0) cout << "\n";
    }
    space;
#endif
}

void dealCardsTwoPerson(Player& player1, Player& player2) {
    int nTotalCardsCounter = 0;
    for (int i = 0; i < 89; i++) {
        if (smallDeck[i].sName != "Unavailable") nTotalCardsCounter++;
    }

    int nCardsNeeded = 0;
    for (int i = 0; i < 5; i++) {
        if (player1.pl_species[i].nPopulation != 0) nCardsNeeded++;
        if (player1.pl_species[i].nPopulation != 0) nCardsNeeded++;
    }
    nCardsNeeded += 6;

    if (nCardsNeeded < nTotalCardsCounter) {
        bLastRound = false;
        if (playersTraitsTotal(player1) <= playersSpeciesTotal(player1) + 3) {
            for (int i = 0; i < playersSpeciesTotal(player1); i++) {
                giveRandomCardTwoPerson(player1);
            }
            for (int i = 0; i < 3; i++) {
                giveRandomCardTwoPerson(player1);
            }
        }
        else return;
        if (playersTraitsTotal(player2) <= playersSpeciesTotal(player2) + 3) {
            for (int i = 0; i < playersSpeciesTotal(player2); i++) {
                giveRandomCardTwoPerson(player2);
            }
            for (int i = 0; i < 3; i++) {
                giveRandomCardTwoPerson(player2);
            }
        }
        else return;
    }
    else if (nCardsNeeded >= nTotalCardsCounter) {
        bLastRound = true;
        shuffleLargeDeck();
        dealCardsTwoPerson(player1, player2);
    }
}

void giveRandomCardTwoPerson(Player& player) {
    for (int i = 0; i < 89; i++) { // picking next available card in a deck
        if (smallDeck[i].sName != "Unavailable") {
            for (int j = 0; j < 10; j++) { // looking through players traits
                if (player.pl_traits[j].sName == "None") {
                    player.pl_traits[j] = smallDeck[i]; // giving them the trait

                    smallDeck[i].sName = "Unavailable"; // taking the card away makes it unavailable
                    smallDeck[i].nFoodCount = 0;
                    smallDeck[i].sProperty = "None";
                    smallDeck[i].bFacedown = false;
                    smallDeck[i].bTrigger = false;
                    break;
                }
            }
            break;
        }
    }
}
