#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

/* main */
void TaskOne(); // 1 main loop 1 flag
void TaskTwo(); // 2 main loops 2 flags each

/* auxiliary */
int* createArray(int size); // puts 1 + r % 9 values in the array
int getArraySize(int* p);   // gets _msize
void displayArray(int* p);  // cout's the array

int main() {
    srand(time(NULL));

    int user;
    char c_con;
    string menu = "\nChoose the number of a task (1-2).\n\nYou: ";
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
    cout << "\n\nTask One.";
    int M, N, c = 0;
    cout << "\n\nEnter M: ";
    cin >> M;
    cout << "\n\nEnter N: ";
    cin >> N;
    int* pA = new int[M];
    int* pB = new int[N];
    pA = createArray(M);
    pB = createArray(N);
    cout << "\n\nArray A: \n\n";
    displayArray(pA);
    cout << "\n\nArray B: \n\n";
    displayArray(pB);

    int temp;
    if (M > N) temp = M;
    else temp = N;

    bool flag = false;
    int* pBuff = new int[temp] {};

    for (int i = 0, m = 0; i < getArraySize(pA); i++) {         // look through array A
        for (int j = 0; j < getArraySize(pB); j++) {            // look through array B
            flag = false;                                       // set flag as "yes"
            if (pA[i] == pB[j]) {                               // if A[i] equals B[j]
                for (int v = 0; v < getArraySize(pBuff); v++) { // look through array Buffer
                    if (pA[i] == pBuff[v]) {                    // if we already got that value
                        flag = true;                            // say "no"
                    }                                           //
                }                                               // 
                if (flag == false) {                            // if flag is "yes"
                    pBuff[m] = pA[i];                           // add current value to Buffer array
                    m++;
                    c++;
                }
            }
        }
    }
    
    int* pC = new int[c] {};
    for (int tmp = 0, v = 0; pBuff[tmp] != 0; v++, tmp++) {
        pC[v] = pBuff[tmp];
    }
    cout << "\n\nResult:\n\n";
    displayArray(pC);
    delete[]pA;
    delete[]pB;
    delete[]pBuff;
    delete[]pC;

    cout << "\n\n";
}

void TaskTwo() {
    cout << "\n\nTask Two.";
    int M, N, c = 0;
    cout << "\n\nEnter M: ";
    cin >> M;
    cout << "\n\nEnter N: ";
    cin >> N;
    int* pA = new int[M];
    int* pB = new int[N];
    pA = createArray(M);
    pB = createArray(N);
    cout << "\n\nArray A: \n\n";
    displayArray(pA);
    cout << "\n\nArray B: \n\n";
    displayArray(pB);

    int temp;
    if (M > N) temp = M;
    else temp = N;

    bool flag0 = false;
    bool flag1 = false;
    int* pBuff = new int[temp] {};

    int m = 0;
    for (int i = 0; i < getArraySize(pA); i++) {                  // look through array A
        flag0 = false;
        flag1 = false;
        for (int j = 0; j < getArraySize(pB); j++) {              // if we can meet the current element in array B, say "no"
            if (pA[i] == pB[j]) {
                flag0 = true;
            }
        }
        if (flag0 == false) {                                     // if there's no such element in array B
            for (int v = 0; v < getArraySize(pBuff); v++) {
                if (pA[i] == pBuff[v]) {                          // if there's already such element in our Buffer array, say "no"
                    flag1 = true;
                }
            }
            if (flag1 == false) {                                 // if there's none, add it
                pBuff[m] = pA[i];
                m++;
                c++;
            }
        }
    }

    for (int i = 0; i < getArraySize(pB); i++) {                 // repeat the same procedure for array B for those
        flag0 = false;                                           // values that are in it, but not in array A
        flag1 = false;
        for (int j = 0; j < getArraySize(pA); j++) {
            if (pB[i] == pA[j]) {
                flag0 = true;
            }
        }
        if (flag0 == false) {
            for (int v = 0; v < getArraySize(pBuff); v++) {
                if (pB[i] == pBuff[v]) {
                    flag1 = true;
                }
            }
            if (flag1 == false) {
                pBuff[m] = pB[i];
                m++;
                c++;
            }
        }
    }

    int* pC = new int[c] {};
    for (int tmp = 0, v = 0; pBuff[tmp] != 0; v++, tmp++) {
        pC[v] = pBuff[tmp];
    }

    cout << "\n\nResult:";
    displayArray(pC);
    if (getArraySize(pC) < 1) cout << " There's no such elements. \n Try decreasing arrays' capacity.";
    delete[]pA;
    delete[]pB;
    delete[]pBuff;
    delete[]pC;

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
