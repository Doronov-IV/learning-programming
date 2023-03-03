/*

 Home Assignment №34.
 
 2021.05.11

  

 Задание 1. Дано два текстовых файла. Выяснить, совпадают ли их строки. Если нет, то вывести несовпадающую 
    строку из каждого файла.

  Задание 2. Дан текстовый файл. Необходимо создать 
    новый файл и записать в него следующую статистику по
    исходному файлу:
    Количество символов;
    Количество строк;
     Количество гласных букв;
    Количество согласных букв;
     Количество цифр.

Задание 3. Шифр Цезаря — один из древнейших шифров.
    При шифровании каждый символ заменяется другим,
    отстоящим от него в алфавите на фиксированное число
    позиций.

*/

#include <iostream>
#include <conio.h>
#include <string>
#include <fstream>

#ifndef UNICODE
#define UNICODE
#endif
#ifndef _UNICODE
#define _UNICODE
#endif

#include <Windows.h>

using namespace std;

#define space cout<<"\n\n"
#define testChar for(int i = 0; i < 256; i++) { cout <<"\n\n" << i << " = " << char(i); } // testChars
#define testAlphas // testAlpha
#define testUnis for(int i = 256; i < 1000; i++) {cout <<"\n" << i << " = " << char(i); }

int BS = 1000;

/* main */

void TaskOne();
void TaskTwo();
void TaskThree();

char* createAlphabet();
char toLowerCase(char c);
bool isCapital(char c);
char** createFileBuffer();
void code(char**& buf, int key, char* A);
void uncode(char**& buf, int key, char* A);

char* createAlphabetLocal();
bool isCapitalLocal(char c);
char toLowerCaseLocal(char c);
void codeLocal(char**& buf, int key, char* A);
void uncodeLocal(char**& buf, int key, char* A);

/*auxiliary */

void showContinue();
void showSubContinue(int task_number);

int main() {
    char user;
    char c_con;
    std::string menu = "\nChoose the number of a task (1-3).\n\nYou: ";
    //setlocale(LC_CTYPE, ".1251");
    //setlocale(LC_CTYPE, ".866");
    //setlocale(LC_ALL, "Russian");

    do
    {
        std::cout << menu;
        user = _getch();

        switch (user)
        {
        case '1':
        {
            TaskOne();
            break;
        }
        case '2':
        {
            TaskTwo();
            break;
        }
        case '3':
        {
            TaskThree();
            break;
        }
        default:
            break;
        }
        showContinue();
        c_con = _getch();
        std::cout << "\n\n";
        system("cls");
    } while (c_con != 'n' && c_con != 'N');
    std::cout << "\n\n\tYou've quit. Have a nice day!\n\t   (Press any key...)\n\n\n";

    system("pause>0");
    return 0;

}

// main 

void TaskOne() {
    cout << "\n\nTask One.\n\n";


    string st_path = "temp0.txt";
    string nd_path = "temp1.txt";


    string* buf = new string[1000]; // current buffer
    bool flag = false, gf = false; // global flag

    for (int i = 0; i < 1000; i++) { // we'll need it later
        buf[i] = "";
    }

    string line = "";
    ifstream first;
    int i = 0; // iterator
    first.open(st_path);
    if (first.is_open()) {
        while (getline(first, line)) {
            buf[i] = line;
            i++;
        }
    }
    int buf_size = i;
    i = 0;
    line = "";
    first.close();

    int j = 0;
    ifstream second;
    second.open(nd_path);
    if (second.is_open()) {
        while (getline(second, line)) {
            flag = false;
            for (int v = 0; v < buf_size; v++) {
                if (line == buf[v]) {
                    buf[v] = "";
                    break;
                }
            }
        }
    }
    second.close();

    for (int i = 0; i < 1000; i++) {
        if (buf[i] != "") {
            gf = true;
            break;
        }
    }

    if (gf == false) {
        cout << "The two files are even.\n\n";
    }
    else {
        // displaying the unique strings
        cout << "   There are some unique strings:";
        space;
        for (int i = 0; i < 1000; i++) {
            if (buf[i] != "") {
                cout << buf[i] << "\n";
            }
        }
        space;
    }
    delete[] buf;
}

void TaskTwo() {
    cout << "\n\nTask Two.\n\n";


    string st = "temp2.txt";
    string nd = "temp3.txt";


    string* res = new string[5];
    string* buf = new string[1000];

    res[0] = "Symbols = ";
    res[1] = "Lines = ";
    res[2] = "Vowels = ";
    res[3] = "Consonants = ";
    res[4] = "Digits = ";

    int s = 0, l = 0, v = 0, c = 0, d = 0;
    int* A = new int[5];

    ifstream in;
    in.open(st);

    string line;
    if (in.is_open()) {
        while (getline(in, line)) {
            l++;
            for (int i = 0; i < line.length(); i++) {
                s++;
                if (int(line[i]) > 48 && int(line[i]) < 58) d++;
                else if (line[i] == 'a' || line[i] == 'e' || line[i] == 'i' || line[i] == 'o' || line[i] == 'u' || line[i] == 'y' || line[i] == 'A' || line[i] == 'E' || line[i] == 'I' || line[i] == 'O' || line[i] == 'U' || line[i] == 'Y') v++;
                else if (line[i] == 'b' || line[i] == 'c' || line[i] == 'd' || line[i] == 'f' || line[i] == 'g' || line[i] == 'h' || line[i] == 'j' || line[i] == 'k' || line[i] == 'l' || line[i] == 'm' || line[i] == 'n' || line[i] == 'o' || line[i] == 'p' || line[i] == 'q' || line[i] == 's' || line[i] == 't' || line[i] == 'v' || line[i] == 'x' || line[i] == 'z' || line[i] == 'r' || line[i] == 'w' || line[i] == 'B' || line[i] == 'C' || line[i] == 'D' || line[i] == 'F' || line[i] == 'G' || line[i] == 'H' || line[i] == 'J' || line[i] == 'K' || line[i] == 'L' || line[i] == 'M' || line[i] == 'N' || line[i] == 'P' || line[i] == 'Q' || line[i] == 'S' || line[i] == 'T' || line[i] == 'V' || line[i] == 'X' || line[i] == 'Z' || line[i] == 'R' || line[i] == 'W') c++;//B, C, D, F, G, J, K, L, M, N, P, Q, S, T, V, X, Z and often H, R, W, Y.
            }
        }
    }

    in.close();
    A[0] = s;
    A[1] = l;
    A[2] = v;
    A[3] = c;
    A[4] = d;

    ofstream out;
    out.open(nd);

    for (int i = 0; i < 5; i++) {
        out << res[i] << " " << A[i] << "\n";
    }
    out.close();
    delete[] res;
    delete[] A;
}

void TaskThree() {
    cout << "\n\nTask Three.\n\n";

#ifdef testChars
    testChars
#endif

#ifdef testUni
        testUni
#endif

#ifdef testAlpha
        char* B = createAlphabetLocal();
    for (int i = 0; i < 32; i++) {
        cout << i << " - " << B[i] << endl;
    }
#endif
    char** buf = createFileBuffer();
    char* A = createAlphabetLocal();
    int filesize = 0, i = 0;

    string st = "temp4.txt";
    string nd = "temp5.txt";

    string line = "";
    string subline = "";
    ifstream in;
    in.open(st);
    if (in.is_open()) {
        while (getline(in, line)) {
            for (int j = 0; j < line.length(); j++) {
                buf[i][j] = line[j];
            }
            i++;
        }
    }
    in.close();

    /*if (in.is_open()) {
        for (int i = 0; i < filesize;) {
            while (getline(in, line)) {
                for (int j = 0; j < line.length(); j++) {
                    buf[i][j] = line[j];
                }
                buf[i][line.length()] = '\0';
                i++;
            }
        }
    }*/
    in.close();

    codeLocal(buf, 5, A);

    ofstream out;
    out.open(nd);
    if (out.is_open()) {
        for (int i = 0; i < BS; i++) {
            if (buf[i][0] == '0') break;
            for (int j = 0; buf[i][j] != '\0'; j++) {
                if (buf[i][j] != '0') out << buf[i][j];
                else break;
            }
            out << "\n";
        }
    }

    cout << "\n\n\t>> Done\n\n";


}

// auxiliary

void showContinue() {
    const int len = 47;
    cout << "\t" << char(201);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(187);
    cout << "\n\t" << char(186);

    cout << "  Do you want to continue the programme? (y/n) ";

    cout << char(186) << "\n";
    cout << "\t" << char(200);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(188) << "\n";
    cout << "\t You: ";
}

void showSubContinue(int task_number) {
    const int len = 47;
    cout << "\t" << char(218);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(191);
    cout << "\n\t" << char(179);

    cout << "     Do you want to continue Task " << task_number << "? (y/n)     ";

    cout << char(179) << "\n";
    cout << "\t" << char(192);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(217) << "\n";
    cout << "\t You: ";
}

char* createAlphabet() {
    char* A = new char[32]; // 160 - 175 from "a" to "п", 224 - 239 from "п" to "я";

    A[0] = '\u0430';
    A[1] = '\u0431';
    A[2] = '\u0432';
    A[3] = '\u0433';
    A[4] = '\u0434';
    A[5] = '\u0435';
    A[6] = '\u0436';
    A[7] = '\u0437';
    A[8] = '\u0438';
    A[9] = '\u0439';
    A[10] = '\u043A';
    A[11] = '\u043B';
    A[12] = '\u043C';
    A[13] = '\u043D';
    A[14] = '\u043E';
    A[15] = '\u043F';
    A[16] = '\u0440';
    A[17] = '\u0441';
    A[18] = '\u0442';
    A[19] = '\u0443';
    A[20] = '\u0444';
    A[21] = '\u0445';
    A[22] = '\u0446';
    A[23] = '\u0447';
    A[24] = '\u0448';
    A[25] = '\u0449';
    A[26] = '\u044A';
    A[27] = '\u044B';
    A[28] = '\u044C';
    A[29] = '\u044D';
    A[30] = '\u044E';
    A[31] = '\u044F';
    A[32] = '\0';

    return A;
}

char* createAlphabetLocal() {
    char* A = new char[32];

    for (int i = 160, v = 0; i < 176; v++, i++) {
        A[v] = char(i);
    }

    for (int i = 224, v = 16; i < 240; v++, i++) {
        A[v] = char(i);
    }

    A[32] = '\0';

    return A;
}

bool isCapital(char c) {
    if (int(c) == '\u0410' || int(c) == '\u0411' || int(c) == '\u0412' || int(c) == '\u0413' || int(c) == '\u0414' || int(c) == '\u0415' || int(c) == '\u0416' || int(c) == '\u0417' || int(c) == '\u0418' || int(c) == '\u0419' || int(c) == '\u041A' || int(c) == '\u041B' || int(c) == '\u041C' || int(c) == '\u041D' || int(c) == '\u041E' || int(c) == '\u041F' || int(c) == '\u0420' || int(c) == '\u0421' || int(c) == '\u0422' || int(c) == '\u0423' || int(c) == '\u0424' || int(c) == '\u0425' || int(c) == '\u0426' || int(c) == '\u0427' || int(c) == '\u0428' || int(c) == '\u0429' || int(c) == '\u042A' || int(c) == '\u042B' || int(c) == '\u042C' || int(c) == '\u042D' || int(c) == '\u042E' || int(c) == '\u042F') {
        return true;
    }
    else return false;
}

bool isCapitalLocal(char c) {
    for (int i = 128; i < 159; i++) {
        if (c == char(i)) return true;
    } 
    return false;
}

char toLowerCase(char c) {
    char* low = createAlphabet();
    char* A = new char[32];

    A[0] = '\u0410';
    A[1] = '\u0411';
    A[2] = '\u0412';
    A[3] = '\u0413';
    A[4] = '\u0414';
    A[5] = '\u0415';
    A[6] = '\u0416';
    A[7] = '\u0417';
    A[8] = '\u0418';
    A[9] = '\u0419';
    A[10] = '\u041A';
    A[11] = '\u041B';
    A[12] = '\u041C';
    A[13] = '\u041D';
    A[14] = '\u041E';
    A[15] = '\u041F';
    A[16] = '\u0420';
    A[17] = '\u0421';
    A[18] = '\u0422';
    A[19] = '\u0423';
    A[20] = '\u0424';
    A[21] = '\u0425';
    A[22] = '\u0426';
    A[23] = '\u0427';
    A[24] = '\u0428';
    A[25] = '\u0429';
    A[26] = '\u042A';
    A[27] = '\u042B';
    A[28] = '\u042C';
    A[29] = '\u042D';
    A[30] = '\u042E';
    A[31] = '\u042F';
    A[32] = '\0';

    for (int i = 0; i < 33; i++) {
        if (A[i] == c) return low[i];
    }
}

char toLowerCaseLocal(char c) {
    char* low = createAlphabetLocal();
    char* A = new char[32];

    for (int i = 128, v = 0; i < 159; v++, i++) {
        A[v] = char(i);
    }

    for (int i = 0; i < 33; i++) {
        if (A[i] == c) return low[i];
    }
}

char** createFileBuffer() {
    char** buf = new char* [BS];
    for (int i = 0; i < BS; i++) {
        buf[i] = new char[BS];
    }
    for (int i = 0; i < BS; i++) {
        for (int j = 0; j < BS; j++) {
            buf[i][j] = '0';
        }
    }
    return buf;
}

void code(char**& buf, int key, char* A) {
    int temp = 0;
    for (int i = 0; i < BS; i++) {
        for (int j = 0; j < BS; j++) {
            for (int k = 0; k < 31; k++) {
                if (isCapital(buf[i][j])) buf[i][j] = toLowerCase(buf[i][j]);
                if (int(buf[i][j]) == int(A[k])) {
                    if (k + key > 33) {
                        temp = abs(32 - k + key);
                        buf[i][j] = A[temp];
                    }
                    else buf[i][j] = A[k + key];
                }
            }
        }
    }
}

void uncode(char**& buf, int key, char* A) {
    int temp = 0;
    for (int i = 0; i < BS; i++) {
        for (int j = 0; j < BS; j++) {
            for (int k = 0; k < 31; k++) {
                //if (isCapital(buf[i][j])) buf[i][j] = toLowerCase(buf[i][j]);
                if (int(buf[i][j]) == int(A[k])) {
                    if (k - key < 0) {
                        temp = abs(31 - k - key);
                        buf[i][j] = A[temp];
                    }
                    else buf[i][j] = A[k - key];
                }
            }
        }
    }
}

void codeLocal(char**& buf, int key, char* A) {
    int temp = 0;
    for (int i = 0; i < BS; i++) {
        for (int j = 0; j < BS; j++) {
            for (int k = 0; k < 31; k++) {
                if (isCapitalLocal(buf[i][j])) buf[i][j] = toLowerCaseLocal(buf[i][j]);
                if (int(buf[i][j]) == int(A[k])) {
                    if (k + key > 33) {
                        temp = abs(32 - k + key);
                        buf[i][j] = A[temp];
                    }
                    else buf[i][j] = A[k + key];
                }
            }
        }
    }
}

void uncodeLocal(char**& buf, int key, char* A) {
    int temp = 0;
    for (int i = 0; i < BS; i++) {
        for (int j = 0; j < BS; j++) {
            for (int k = 0; k < 31; k++) {
                //if (isCapital(buf[i][j])) buf[i][j] = toLowerCase(buf[i][j]);
                if (int(buf[i][j]) == int(A[k])) {
                    if (k - key < 0) {
                        temp = abs(31 - k - key);
                        buf[i][j] = A[temp];
                    }
                    else buf[i][j] = A[k - key];
                }
            }
        }
    }
}
