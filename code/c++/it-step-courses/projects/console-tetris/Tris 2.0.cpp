/*
    In case someone would actually happen to appear here: it's a codecamp exam project.
    >> It supposed to be a console tetris or something.
    It's a total spaghette code because the limitations of a task are pretty strict + I'm trying to speedrun it while
    revising and taking other exams.

    Maybe one day I'll fix it, but certainly not today.

    Tomorrow Objectives:
    � redraw figures properly — +
    � do something about collision (rewrite from scratch)
    � X coordinate

    New : glide function;
*/

#include <Windows.h>
#include <thread>
#include <iostream>
#include <conio.h>
#include <ctime>

using namespace std;

/* preprocessors */

#define nl cout<<"\n\n"
#define scale
#define field

/* GLOBAL */

//int** tetromino = new int* [4];

bool GameOver = false;

/* structures */

struct Tetromino {
    int** shape = new int* [4];
    int x = 7;
    int y = 0;
    int index = 60;
    int rotation = 0;
};

struct Screen {

    string name = "void";
    int score = 0;

    const int SW = 45;  // Screen Width
    const int SP = 30;  // pillar
    const int SH = 28;  // Screen Height

    const int FW = 22;  // Field Width
    const int FH = 24;  // Field Height

    int** Field = new int* [FW];
    int** miniField = new int* [4];

    string title = " Tris 2.0 ";
    string menuNewGame = " New Game ";
    string leaderboards = " Leaderboards ";
    string options = " Options ";
    string exit = " Exit ";

};


Screen menu;
Screen game;

/* main */

// screen & field drawing stuff
void makeField();
void drawField(int i, int j);
void drawScreen(Screen& screen);
void showFieldTest();
void setCursor(int x, int y);
// tetromino stuff
Tetromino createTetromino(int index);
void placeTetromino(int x, int y, Tetromino tetro);
void drawTetromino(Tetromino tetro, int x, int y);
bool checkFit(Tetromino tetro);
bool freeFromRight(Tetromino tetro);
void forceDown(Tetromino& currentTetromino);
void forceRight(Tetromino& currentTetromino);

/* auxiliary */

void placeCursor();
void showScale();

int main() {
    srand(time(NULL));

    // game
    game.name = "game";
    menu.name = "menu";

    bool Key[4];
    makeField();
    drawScreen(game);
    bool flag_force = false;
    bool flag_phase = false;
    int RandomTetromino;
    int RandomTetrominoPlus;
    int gameSpeed = 0;
    int speedValue = 20;
    Tetromino currentTetromino;

    // DEBUG =========================
    // The field array is one-tick-slower than screen and it's ok; 

#ifdef field
    showFieldTest();
#endif
    placeCursor();

    RandomTetrominoPlus = 6; // rand() % 7


    do {
        RandomTetromino = RandomTetrominoPlus;
        RandomTetrominoPlus = 6;
        currentTetromino = createTetromino(RandomTetromino);
        currentTetromino.x = 7;
        currentTetromino.y = 0;
        flag_phase = false;
        if (!checkFit(currentTetromino)) GameOver = true;

        do {
            // Time
            this_thread::sleep_for(1000ms);
            gameSpeed++;
            if (gameSpeed = speedValue) {
                flag_force = true;
                //gameSpeed = 0;
            }
             
            // Input

            for (int i = 0; i < 4; i++) {
                Key[i] = (0x8000 & GetAsyncKeyState((unsigned char)("\x27\x25\x28\x20"[i]))) != 0;
            }

            if (Key[0] && freeFromRight(currentTetromino)) forceRight(currentTetromino);

            if (Key[2] && checkFit(currentTetromino)) forceDown(currentTetromino);

            // Logic


            // Visual

            //drawTetromino(currentTetromino, currentTetromino.x, currentTetromino.y);
            showFieldTest();
            placeCursor();
            //cout << currentT;
            //this_thread::sleep_for(50ms);

            if (flag_force == true) {
                gameSpeed = 0;
                forceDown(currentTetromino);
            }

            if (checkFit(currentTetromino) == false) flag_phase = true;
            drawTetromino(currentTetromino, currentTetromino.x, currentTetromino.y);
            placeCursor();
        } while (flag_phase != true);
    } while (!GameOver);

    system("pause>0");
    return 0;
}

void makeField() {
    for (int i = 0; i < game.FH; i++) {
        game.Field[i] = new int[24];
    }

    for (int i = 0; i < game.FH; i++) {
        for (int j = 0; j < game.FW; j++) {
            if (i == 0 || j == 0 || j == game.FW - 1 || i == game.FH - 1) game.Field[i][j] = 9;
            else game.Field[i][j] = 0;
        }
    }

    for (int i = 0; i < 4; i++) {
        game.miniField[i] = new int[8];
    }
}

void drawField(int i, int j) {

    switch (game.Field[i][j]) {
    case 0:
        cout << " ";
        break;
    case 1:
        cout << "[";
        break;
    case 2:
        cout << "]";
        break;
    case 9:
        cout << char(219);
        break;
    default:
        cout << "#";
        break;
    }

    if (j - 1 == game.FW) cout << "\n";
}

void showFieldTest() {
    int x = 60, y = 5;
    for (int i = 0, cy = y; i < game.FH; i++, cy++) {
        for (int j = 0, cx = x; j < game.FH; j++, cx++) {
            setCursor(cx, cy);
            cout << game.Field[i][j];
        }
        cout << "\n";
    }
}

void drawScreen(Screen& screen) {

    if (screen.name == "game") { ////// GAME SCREEN

        int old_y = 0, old_x = 0;

        /////////////////////////////// UP

        int x = 5, y = 5;
        setCursor(x, y);
        cout << char(201);
        x++;
        for (int i = x; i < screen.SW + x; i++) {
            setCursor(i, y);
            if (i == screen.SP + 6) cout << char(203);
            else cout << char(205);
        }
        setCursor(screen.SW + x, y);
        cout << char(187);

        //////////////////////////////// DOWN

        x = 5, y = screen.SH + 4;
        setCursor(x, y);
        cout << char(200);
        x++;
        for (int i = x; i < screen.SW + x; i++) {
            setCursor(i, y);
            if (i == screen.SP + 6) cout << char(202);
            else cout << char(205);
        }
        setCursor(screen.SW + x, y);
        cout << char(188);

        //////////////////////////////// LEFT

        old_y = 5;
        x = 5, y = old_y + 1;
        setCursor(x, y);
        for (int i = y; i < screen.SH + old_y - 1; i++) {
            setCursor(x, i);
            cout << char(186);
        }

        //////////////////////////////// Pillar

        old_y = 5;
        old_x = 5;
        x = old_x + screen.SP + 1, y = old_y + 1;
        setCursor(x, y);
        for (int i = y; i < screen.SH + old_y - 1; i++) {
            setCursor(x, i);
            cout << char(186);
        }

        //////////////////////////////// RIGHT

        old_y = 5;
        old_x = 5;
        x = old_x + screen.SW + 1, y = old_y + 1;
        setCursor(x, y);
        for (int i = y; i < screen.SH + old_y - 1; i++) {
            setCursor(x, i);
            cout << char(186);
        }

        /////////////////////////////// FIELD

        y = 7;  // starting points
        x = 10;

        setCursor(x, y);
        for (int i = 0, ipos = y; i < game.FH; i++, ipos++) {
            for (int j = 0, jpos = x; j < game.FW; j++, jpos++) {
                setCursor(jpos, ipos);
                drawField(i, j);
            }
        }

        /////////////////////////////// SCORE
        setCursor(game.SP + 11, 7);
        cout << "Score:";
        setCursor(game.SP + 11, 8);
        for (int i = 0; i < 6; i++) {
            cout << "0";
        }

        placeCursor();  // places cursor to its home :p
    }
    else if (screen.name == "menu") { // MENU SCREEN
        int old_y = 0, old_x = 0;

        /////////////////////////////// UP

        int x = 5, y = 5;
        setCursor(x, y);
        cout << char(201);
        x++;
        for (int i = x; i < screen.SW + x; i++) {
            setCursor(i, y);
            cout << char(205);
        }
        setCursor(screen.SW + x, y);
        cout << char(187);

        //////////////////////////////// DOWN

        x = 5, y = screen.SH + 4;
        setCursor(x, y);
        cout << char(200);
        x++;
        for (int i = x; i < screen.SW + x; i++) {
            setCursor(i, y);
            cout << char(205);
        }
        setCursor(screen.SW + x, y);
        cout << char(188);

        //////////////////////////////// LEFT

        old_y = 5;
        x = 5, y = old_y + 1;
        setCursor(x, y);
        for (int i = y; i < screen.SH + old_y - 1; i++) {
            setCursor(x, i);
            cout << char(186);
        }

        //////////////////////////////// RIGHT

        old_y = 5;
        old_x = 5;
        x = old_x + screen.SW + 1, y = old_y + 1;
        setCursor(x, y);
        for (int i = y; i < screen.SH + old_y - 1; i++) {
            setCursor(x, i);
            cout << char(186);
        }
        placeCursor();  // places cursor to its home :p
    }
    else cout << "\n\n\a\t[!] NullPointerException.\n\n";
}

void setCursor(int x, int y) {
    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
    COORD coord;
    coord.X = x;
    coord.Y = y;
    SetConsoleCursorPosition(hConsole, coord);
}

void placeCursor() {
    setCursor(0, game.SH + 6);
}

void showScale() {
    int x = 6, y = 4, cd = 0;
    for (int i = x, counter = 1; i < menu.SW + 7; i++, counter++) {
        setCursor(i, y);
        if (counter == 10) {
            cd += counter;
            setCursor(i, y - 1);
            cout << cd;
            setCursor(i, y);
            counter = 0;
        }
        else if (counter % 2 != 0) cout << counter << " ";
    }
    x = 3, y = 6, cd = 0;
    for (int i = y, counter = 1; i < menu.SH + 6; i++, counter++) {
        setCursor(x, i);
        if (counter == 10) {
            cd += counter;
            setCursor(x - 3, i);
            cout << cd;
            setCursor(x, i);
            counter = 0;
        }
        else if (counter % 2 != 0) cout << counter << " ";
    }
}

Tetromino createTetromino(int index) {
    //////////////////////////// LOGIC
    Tetromino Unit;
    int** A = new int* [4];
    int local_index = 60;
    for (int i = 0; i < 4; i++) {
        A[i] = new int[8];
    }
    for (int i = 0; i < 4; i++) {
        for (int j = 0; j < 8; j++) {
            A[i][j] = 0;
        }
    }
    switch (index) {
    case 0:
        local_index = 0;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (i == 3) {
                    if (j == 0 || j % 2 == 0) A[i][j] = 1;
                    else if (j % 2 != 0) A[i][j] = 2;
                }
                else A[i][j] = 0;
            }
        }
        break;
    case 1:
        local_index = 1;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (i == 2 || i == 3) {
                    if (j == 0 || j == 2) A[i][j] = 1;
                    else if (j == 1 || j == 3) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else A[i][j] = 0;
            }
        }
        break;
    case 2:
        local_index = 2;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (i == 2) {
                    if (j == 0 || j == 2) A[i][j] = 1;
                    else if (j == 1 || j == 3) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else if (i == 3) {
                    if (j == 2 || j == 4) A[i][j] = 1;
                    else if (j == 3 || j == 5) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else A[i][j] = 0;
            }
        }
        break;
    case 3:
        local_index = 3;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (i == 3) {
                    if (j == 0 || j == 2) A[i][j] = 1;
                    else if (j == 1 || j == 3) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else if (i == 2) {
                    if (j == 2 || j == 4) A[i][j] = 1;
                    else if (j == 3 || j == 5) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else A[i][j] = 0;
            }
        }
        break;
    case 4:
        local_index = 4;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (i == 2) {
                    if (j == 4) A[i][j] = 1;
                    else if (j == 5) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else if (i == 3) {
                    if (j == 0 || j == 2 || j == 4) A[i][j] = 1;
                    else if (j == 1 || j == 3 || j == 5) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else A[i][j] = 0;
            }
        }
        break;


    case 5:
        local_index = 5;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (i == 2) {
                    if (j == 0) A[i][j] = 1;
                    else if (j == 1) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else if (i == 3) {
                    if (j == 0 || j == 2 || j == 4) A[i][j] = 1;
                    else if (j == 1 || j == 3 || j == 5) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else A[i][j] = 0;
            }
        }
        break;

    case 6:
        local_index = 6;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (i == 2) {
                    if (j == 2) A[i][j] = 1;
                    else if (j == 3) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else if (i == 3) {
                    if (j == 0 || j == 2 || j == 4) A[i][j] = 1;
                    else if (j == 1 || j == 3 || j == 5) A[i][j] = 2;
                    else A[i][j] = 0;
                }
                else A[i][j] = 0;
            }
        }
        break;
    default:
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                A[i][j] = 0;
            }
        }
        break;
    }
    Unit.shape = A;
    Unit.index = local_index;
    return Unit;
}

void placeTetromino(int x, int y, Tetromino tetro) {
    if (checkFit(tetro)) {
        for (int i = y, ti = 0; i < 4 + y; i++, ti++) {
            for (int j = x, tj = 0; j < 8 + x; j++, tj++) {
                if (game.Field[i][j] == 0 && tetro.shape[ti][tj] != 0) game.Field[i][j] = tetro.shape[ti][tj];
                else if (game.Field[i][j] != 0 && tetro.shape[ti][tj] != 0 && tetro.y == 0) GameOver = true;
            }
        } // the bug is probably somewhere here
    }
}

void drawTetromino(Tetromino tetro, int x, int y) {
    // start y & start x are the coordinates in logical array
    /*game.FW / 2 - 6*/; // where 6 is an unprecise length of a tetromino itself or sth idk
    placeTetromino(x+1, y+1, tetro);

    for (int i = 0, ipos = y + 7; i < 4; i++, ipos++) {           // where 8 is the distance from console edge
        for (int j = 0, jpos = x + 10; j < 8; j++, jpos++) {      // same
            setCursor(jpos, ipos+1); // +
            drawField(i + y + 1, j + x + 1);
        }
    }
}

bool checkFit(Tetromino tetro) {
    int low = 0; // lowest point of tetro
    bool flag = false;
    int side; // 
    for (int i = 0; i < 4; i++) {
        flag == false;
        side = 0;
        for (int j = 0; j < 8; j++) {
            if (tetro.shape[i][j] != 0) flag = true;
        }
        if (flag == false) {
            low = i;
            break;
        }
    }
    flag = false;
    for (int i = tetro.x; i < tetro.x + 6; i++) {
        if (game.Field[tetro.y + 4][i] == 0) flag = true;
        else if (game.Field[tetro.y + 5][i] == 9 || game.Field[tetro.y + 5][i] == 1 || game.Field[tetro.y + 5][i] == 2) return false;
    }
    if (flag == true) return true;
}

void forceDown(Tetromino& currentTetromino) {
    if (currentTetromino.rotation == 0) {
        if (checkFit(currentTetromino)) {
            switch (currentTetromino.index) {
            case 0:
                if (checkFit(currentTetromino) == true) currentTetromino.y += 1;
                for (int i = currentTetromino.x; i < currentTetromino.x + 9; i++) {
                    game.Field[currentTetromino.y + 1][i] = 0;
                    game.Field[currentTetromino.y + 2][i] = 0;
                    game.Field[currentTetromino.y + 3][i] = 0;
                }
                break;
            case 1:
                if (checkFit(currentTetromino) == true) currentTetromino.y += 1;
                for (int i = currentTetromino.x; i < currentTetromino.x + 9; i++) {
                    game.Field[currentTetromino.y + 1][i] = 0;
                    game.Field[currentTetromino.y + 2][i] = 0;
                }
                break;
            case 2:
                if (checkFit(currentTetromino) == true) currentTetromino.y += 1;
                for (int i = currentTetromino.x; i < currentTetromino.x + 9; i++) {
                    game.Field[currentTetromino.y + 1][i] = 0;
                    game.Field[currentTetromino.y + 2][i] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 5] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 6] = 0; 
                }
                break;
            case 3:
                if (checkFit(currentTetromino) == true) currentTetromino.y += 1;
                for (int i = currentTetromino.x; i < currentTetromino.x + 9; i++) {
                    game.Field[currentTetromino.y + 1][i] = 0;
                    game.Field[currentTetromino.y + 2][i] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 1] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 2] = 0;
                }
                break;
            case 4:
                if (checkFit(currentTetromino) == true) currentTetromino.y += 1;
                for (int i = currentTetromino.x; i < currentTetromino.x + 9; i++) {
                    game.Field[currentTetromino.y + 1][i] = 0;
                    game.Field[currentTetromino.y + 2][i] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 1] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 2] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 3] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 4] = 0;
                }
                break;
            case 5:
                if (checkFit(currentTetromino) == true) currentTetromino.y += 1;
                for (int i = currentTetromino.x; i < currentTetromino.x + 9; i++) {
                    game.Field[currentTetromino.y + 1][i] = 0;
                    game.Field[currentTetromino.y + 2][i] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 3] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 4] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 5] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 6] = 0;
                }
                break;
            case 6:
                if (checkFit(currentTetromino) == true) currentTetromino.y += 1;
                for (int i = currentTetromino.x; i < currentTetromino.x + 9; i++) {
                    game.Field[currentTetromino.y + 1][i] = 0;
                    game.Field[currentTetromino.y + 2][i] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 1] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 2] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 5] = 0;
                    game.Field[currentTetromino.y + 3][currentTetromino.x + 6] = 0;
                }
                break;
            }
        }
        else return;
    }
}

void forceRight(Tetromino& currentTetromino) {
    if (currentTetromino.rotation == 0) {
        if (freeFromRight(currentTetromino)) {
            switch (currentTetromino.index) {
            case 0:
                if (freeFromRight(currentTetromino)) currentTetromino.x += 2;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 2] = 0;
                drawField(currentTetromino.y + 3, currentTetromino.x - 1);
                drawField(currentTetromino.y + 3,currentTetromino.x - 2);
                break;
            case 1:
                if (freeFromRight(currentTetromino)) currentTetromino.x += 2;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 2] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x - 2] = 0;
                drawField(currentTetromino.y + 3, currentTetromino.x - 1);
                drawField(currentTetromino.y + 3, currentTetromino.x - 2);
                drawField(currentTetromino.y + 2, currentTetromino.x - 1);
                drawField(currentTetromino.y + 2, currentTetromino.x - 2);
                break;
            case 2:
                if (freeFromRight(currentTetromino)) currentTetromino.x += 2;
                game.Field[currentTetromino.y + 2][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x - 2] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x + 1] = 0;
                break;
            case 3:
                if (freeFromRight(currentTetromino)) currentTetromino.x += 2;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 2] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x + 1] = 0;
                break;
            case 4:
                if (freeFromRight(currentTetromino)) currentTetromino.x += 2;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 2] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x + 2] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x + 3] = 0;
                break;
            case 5:
                if (freeFromRight(currentTetromino)) currentTetromino.x += 2;
                game.Field[currentTetromino.y + 4][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 4][currentTetromino.x - 2] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 3][currentTetromino.x - 2] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x - 1] = 0;
                game.Field[currentTetromino.y + 2][currentTetromino.x - 2] = 0;
                break;
            case 6:
                if (freeFromRight(currentTetromino)) currentTetromino.x += 2;
                for (int i = currentTetromino.y-1; i < currentTetromino.y + 5; i++) {
                    for (int j = currentTetromino.x-2; j < currentTetromino.x + 9; j++) {
                        game.Field[i][j] = 0;
                    }
                }
                drawTetromino(currentTetromino, currentTetromino.x, currentTetromino.y);
                break;
            }
        }
        else return;
    }
}

bool freeFromRight(Tetromino tetro) {
    bool flag = false;
    int side; // 
    for (int i = 0; i < 4; i++) {
        flag == false;
        side = 0;
        for (int j = 0; j < 8; j++) {
            if (tetro.shape[i][j] != 0) flag = true;
        }
        if (flag == false) {
            break;
        }
    }

    switch (tetro.index) {
    case 0:
        flag = false;
        if (game.Field[tetro.y+4][tetro.x + 8] == 0) flag = true;
        else if (game.Field[tetro.y+4][tetro.x + 8] == 9 || game.Field[tetro.y+3][tetro.x + 8] == 1 || game.Field[tetro.y+3][tetro.x + 8] == 2) return false;
        
        if (flag == true) return true;
        break;
    case 1:
        flag = false;
        if (game.Field[tetro.y + 3][tetro.x + 6] == 0 && game.Field[tetro.y + 2][tetro.x + 6] == 0) flag = true;
        else if (game.Field[tetro.y + 3][tetro.x + 4] == 9 || game.Field[tetro.y + 3][tetro.x + 4] == 1 || game.Field[tetro.y + 3][tetro.x + 4] == 2) return false;

        if (flag == true) return true;
        break;
    case 2:
        flag = false;
        if (game.Field[tetro.y + 1][tetro.x + 6] == 0 && game.Field[tetro.y + 2][tetro.x + 6] == 0) flag = true;
        else if (game.Field[tetro.y + 1][tetro.x + 6] == 9 || game.Field[tetro.y + 1][tetro.x + 6] == 1 || game.Field[tetro.y + 1][tetro.x + 6] == 2) return false;

        if (flag == true) return true;
        break;
    case 3:
        flag = false;
        if (game.Field[tetro.y + 2][tetro.x + 6] == 0 && game.Field[tetro.y + 3][tetro.x + 6] == 0) flag = true;
        else if (game.Field[tetro.y + 2][tetro.x + 6] == 9 || game.Field[tetro.y + 2][tetro.x + 6] == 1 || game.Field[tetro.y + 2][tetro.x + 6] == 2) return false;

        if (flag == true) return true;
        break;
    case 4:
        flag = false;
        if (game.Field[tetro.y + 2][tetro.x + 6] == 0 && game.Field[tetro.y + 3][tetro.x + 6] == 0) flag = true;
        else if (game.Field[tetro.y + 2][tetro.x + 6] == 9 || game.Field[tetro.y + 2][tetro.x + 6] == 1 || game.Field[tetro.y + 2][tetro.x + 6] == 2) return false;

        if (flag == true) return true;
        break;
    case 5:
        flag = false;
        if (game.Field[tetro.y + 2][tetro.x + 6] == 0 && game.Field[tetro.y + 3][tetro.x + 6] == 0) flag = true;
        else if (game.Field[tetro.y + 2][tetro.x + 6] == 9 || game.Field[tetro.y + 2][tetro.x + 6] == 1 || game.Field[tetro.y + 2][tetro.x + 6] == 2) return false;

        if (flag == true) return true;
        break;
    case 6:
        flag = false;
        if (game.Field[tetro.y + 2][tetro.x + 6] == 0 && game.Field[tetro.y + 3][tetro.x + 6] == 0) flag = true;
        else if (game.Field[tetro.y + 2][tetro.x + 6] == 9 || game.Field[tetro.y + 2][tetro.x + 6] == 1 || game.Field[tetro.y + 2][tetro.x + 6] == 2) return false;

        if (flag == true) return true;
        break;
    
    }
}