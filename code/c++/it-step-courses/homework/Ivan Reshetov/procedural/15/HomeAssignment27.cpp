#include <ctime>
#include <iostream>

const int M = 10, N = 10;

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

void TaskOne(int[N][M]);
void TaskOne(double[N][M]);
void TaskOne(char[N][M]);

int TaskTwo(int, int, int);
void TaskThree();
int TaskThreeRec(int[], int, int, int);


int main() {
    srand(time(NULL));
    int user;
    int M_i[N][M] = {};
    double M_d[N][M] = {};
    char M_c[N][M] = {};
    char c_con = 'n';
    string menu = "\nChoose the number of a task (1-3).\n\nYou: ";
    string s_con = "\t----------------------------------------------\n\t| Do you want to continue the program? (y/n) |\n\t----------------------------------------------\n\nYou: ";
    char c_con_1 = 'n';
    int user_1;
    string menu_1 = "\nChoose the data type (1-int, 2-double, 3-char).\n\nYou: ";
    string s_con_1 = "\t------------------------------------------\n\t| Do you want to try another type? (y/n) |\n\t------------------------------------------\n\nYou: ";
    do {
        cout << menu;
        cin >> user;
        switch (user) {
        case 1: {
            //<1>
            do {
                cout << menu_1;
                cin >> user_1;
                cout << "\n\n";
                switch (user_1) {
                case 1: {
                    TaskOne(M_i);
                    break;
                }
                case 2: {
                    TaskOne(M_d);
                    break;
                }
                case 3: {
                    TaskOne(M_c);
                    break;
                }
                default:
                    break;
                }
                
                cout << s_con_1;
                cin >> c_con_1;
                system("cls");

            } while (c_con_1 != 'n' && c_con_1 != 'N');

            break;
            //</1>
        }
        case 2: {
            
            cout << "\n\nTask Two.\n\n";
            int X, Y;
            cout << "Enter a value. \n\nYou: ";
            cin >> X;
            cout << "\n\nEnter another value. \n\nYou: ";
            cin >> Y;
            cout << "\n\n";
            
            cout << "The greatest common devisor for " << X << " and " << Y <<" is: " << TaskTwo(X, Y, 0) << "\n\n";
            break;
        }
        case 3: {
            TaskThree();
            break;
        }
        default: {
            break;
        }
        }

        cout << s_con;
        cin >> c_con;
        system("cls");

    } while (c_con != 'n' && c_con != 'N');
    if (c_con == 'n' || c_con == 'N') {
        cout << "\n\n\tYou've quit. Have a nice day! (Press any key)\n\n";
    }

    system("pause>0");
    return 0;
}

void TaskOne(int A[N][M]) {
    cout << "\n   Starting matrix: \n\n";
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            A[i][j] = 1 + rand() % 9;
            cout << A[i][j] << " ";
        }
        cout << "\n";
    }
    cout << "\n\n";

    int min = 10, max = 0;
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            if (i == j) {
                if (A[i][j] > max) max = A[i][j];
                if (A[i][j] < min) min = A[i][j];
            }
        }
    }

    cout << " Min = " << min << "\n Max = " << max <<"\n\n";
    //<selection.sort>
    for (int temp = 0, min; temp < N; temp++)
    {
        for (int i = 0; i < M - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < M; j++)
            {
                if (A[temp][j] < A[temp][min])
                    min = j;
            }
            if (A[temp][min] != A[temp][i])
            {
                swap(A[temp][min], A[temp][i]);
            }
        }
    }
    //</selection.sort>
    cout << "   Sorted matrix:\n\n";
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            cout << A[i][j] << " ";
        }
        cout << "\n";
    }
    cout << "\n\n";
}

void TaskOne(double A[N][M]) {
    cout << "\n   Starting matrix: \n\n";
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            A[i][j] = (1 + rand() % 9) + ((1.0 + rand() % 9)/10);
            cout << A[i][j] << " ";
        }
        cout << "\n";
    }
    cout << "\n\n";

    double min = 10, max = 0;
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            if (i == j) {
                if (A[i][j] > max) max = A[i][j];
                if (A[i][j] < min) min = A[i][j];
            }
        }
    }

    cout << " Min = " << min << "\n Max = " << max << "\n\n";
    //<selection.sort>
    for (int temp = 0, min; temp < N; temp++)
    {
        for (int i = 0; i < M - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < M; j++)
            {
                if (A[temp][j] < A[temp][min])
                    min = j;
            }
            if (A[temp][min] != A[temp][i])
            {
                swap(A[temp][min], A[temp][i]);
            }
        }
    }
    //</selection.sort>
    cout << "   Sorted matrix:\n\n";
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            cout << A[i][j] << " ";
        }
        cout << "\n";
    }
    cout << "\n\n";

}

void TaskOne(char A[N][M]) {
    cout << "\n   Starting matrix: \n\n";
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            A[i][j] = char(1 + rand() % 9);
            cout << A[i][j] << " ";
        }
        cout << "\n";
    }
    cout << "\n\n";

    char min = (char)10, max = (char)0;
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            if (i == j) {
                if ((int)A[i][j] > (int)max) max = A[i][j];
                if ((int)A[i][j] < (int)min) min = A[i][j];
            }
        }
    }

    cout << " Min = " << min << "\n Max = " << max << "\n\n";

    //<selection.sort>
    for (int temp = 0, min; temp < N; temp++)
    {
        for (int i = 0; i < M - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < M; j++)
            {
                if ((int)A[temp][j] < (int)A[temp][min])
                    min = j;
            }
            if ((int)A[temp][min] != (int)A[temp][i])
            {
                swap(A[temp][min], A[temp][i]);
            }
        }
    }
    //</selection.sort>
    cout << "   Sorted matrix:\n\n";
    for (int i = 0; i < N; i++) {
        for (int j = 0; j < M; j++) {
            cout << A[i][j] << " ";
        }
        cout << "\n";
    }
    cout << "\n\n";
}

int TaskTwo(int n, int m, int temp) {
    temp = n % m;
    if (temp == 0) return m;
    else TaskTwo(m,temp,0);
}

void TaskThree() {
    char c_con = 'n';
    string s_con = "\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\t| Do you want to try play again? (y/n) |\n\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\nYou: ";
    do {
        int user, tries = 0;
        int value[4] = {};
        for (int i = 0; i < 4; i++) {
            value[i] = 1 + rand() % 9;
        }
        cout << "\n\n   Task Three.\n\nI've made a four-digit value. Try guessing it.\n\n";

        if (TaskThreeRec(value, 0, 0, 0) == 4) {
            cout << "\n\nYour've won! (";
            for (int i = 0; i < 4; i++) {
                cout << value[i];
            }
            cout << ")\n\n";
        }
        cout << s_con;
        cin >> c_con;
        system("cls");
        if (c_con == 'n' || c_con == 'N') {
            cout << "\n\n\tYou've quit the Task Three (the game).\n\n\n";
        }
    } while (c_con != 'n' && c_con != 'N');

}

int TaskThreeRec(int value[], int user, int score, int tries) {
    if (tries > 1) cout << "\n\n           (Tries: " << tries << ")\n\n\n";
    cout << "Your guess: ";
    cin >> user;
    score = 0;

    int j = user + 1, c = 0;

    while (j != 0) { // finding out the number of bits of a value
        j /= 10;
        c++;
    }
    int temp = c - 1;
    for (int i = user, alpha = 4 - temp-1; temp != -1; temp--, alpha++) {
        //<digit> gets a current digit of a value
        c = temp + 1;
        i = user;
        for (; c != 1; c--) {
            i /= 10;
        }
        i = i % 10;
        //</digit>
        if (i == value[alpha]) score++;
    }
    if (score != 4) {
        tries++;
        cout << "\n\nYour were wrong.\n\n";
        cout << " >> " << score << "/4\n\n";
        cout << "--------------------------------------\n\n";
        TaskThreeRec(value, 0, score, tries);
    }
    else return score;
}
