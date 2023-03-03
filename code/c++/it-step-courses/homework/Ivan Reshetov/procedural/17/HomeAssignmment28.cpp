#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

// main
void TaskOne();
void TaskTwo();
void TaskThree();

// auxiliary
int* createArray(int size);
int getArraySize(int* p);
void displayArray(int* p);

int main() {
    srand(time(NULL));

    int user;
    char c_con;
    string menu = "\nChoose the number of a task (1-3).\n\nYou: ";
    string s_con = "\t----------------------------------\n\t| Do you want to continue? (y/n) |\n\t----------------------------------\n\nYou: ";

    do
    {
        cout << menu;
        cin >> user;

        switch (user)
        {
        case 1:
        {
            TaskOne();
            break;
        }
        case 2:
        {
            TaskTwo();
            break;
        }
        case 3:
        {
            TaskThree();
            break;
        }
        default:
            break;
        }
        cout << s_con;
        cin >> c_con;
        cout << "\n\n";
        system("cls");
    } while (c_con != 'n' && c_con != 'N');
    cout << "\n\n\tYou've quit. Have a nice day!\n\t   (Press any key...)\n\n\n";

    system("pause>0");
    return 0;
}

void TaskOne() {
    int cap;
    cout << "\n\nTask One.\n\nEnter arrays capacity.\n\nYou: ";
    cin >> cap;
    int* pA = new int[cap];
    int* pB = new int[cap] {};
    pA = createArray(cap);
    cout << "\n\n   Array number 1:";
    displayArray(pA);
    //pB = pA; // NullPointerException
    for (int i = 0; i < getArraySize(pA); i++) {
        pB[i] = pA[i];
    }
    delete [] pA;
    cout << "\n\n   Array number 2:";
    displayArray(pB);
    cout << "\n\n";
}

void TaskTwo() {
    int cap;
    cout << "\n\nTask Two.\n\nEnter arrays capacity.\n\nYou: ";
    cin >> cap;
    int* pA = new int[cap];
    int* pB = new int[cap] {};
    pA = createArray(cap);
    cout << "\n\n   Array number 1:";
    displayArray(pA);
    for (int i = 0, v = getArraySize(pA)-1; i < getArraySize(pA); i++, v--) {
        pB[i] = pA[v];
    }
    delete [] pA;
    cout << "\n\n   Array number 2:";
    displayArray(pB);
    cout << "\n\n";
}

void TaskThree() {
    int cap;
    cout << "\n\nTask Three.\n\nEnter arrays capacity.\n\nYou: ";
    cin >> cap;
    int* pA = new int[cap];
    int* pB = new int[cap] {};
    pA = createArray(cap);
    cout << "\n\n   Array number 1:";
    displayArray(pA);
    for (int i = 0, v = getArraySize(pA) - 1; i < getArraySize(pA); i++, v--) {
        pB[i] = pA[v];
    }
    delete[] pA;
    cout << "\n\n   Array number 2:";
    displayArray(pB);
    cout << "\n\n";
}


// auxiliary

int* createArray(int size) {
    int* pA = new int[size];
    for (int i = 0; i < size; i++) {
        pA[i] = 1 + rand() % 9;
    }
    return pA;
}

int getArraySize(int* p) {
    int size = _msize(p) / sizeof(p[0]);
    return size;
}

void displayArray(int* p) {
    cout << "\n\n";
    for (int i = 0; i < getArraySize(p); i++) {
        cout << " " << p[i];
    }
    cout << "\n\n";
}