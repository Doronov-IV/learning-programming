#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

float TaskOne(float var_a, float var_b);
int TaskTwo(int var_a, int var_b);
int TaskThree(int var_a, int var_b);
void TaskFour(char, char); // ?
void TaskFive(int var_a);

int main() {
    srand(time(NULL));

    int user;
    char c_con = 'n';

    string menu = "\nChoose the number of a task (1-5).\n\nYou: ";
    string s_con = "\t----------------------------------------------\n\t| Do you want to continue the program? (y/n) |\n\t----------------------------------------------\n\nYou: ";

    float task_1_var_0, task_1_var_1;
    int task_2_var_0, task_2_var_1, task_3_var_0, task_3_var_1, task_5_var_0;
    char task_4_var_0, task_4_var_1;

    do {
        cout << menu;
        cin >> user;

        switch (user) {
        case 1: {
            cout << "\n\nEnter a base:\n\nYou: ";
            cin >> task_1_var_0;
            cout << "\n\nEnter degree:\n\nYou: ";
            cin >> task_1_var_1;
            cout << "\n\n " << task_1_var_0 << "power" << task_1_var_1 << " is " << TaskOne(task_1_var_0, task_1_var_1);
            break;
        }
        case 2: {
            cout << "\n\nEnter min value:\n\nYou: ";
            cin >> task_2_var_0;
            cout << "\n\nEnter max value:\n\nYou: ";
            cin >> task_2_var_1;
            cout << "\n\nS = " << TaskTwo(task_2_var_0, task_2_var_1);
            break;
        }
        case 3: {
            cout << "\n\nEnter min value:\n\nYou: ";
            cin >> task_3_var_0;
            cout << "\n\nEnter max value:\n\nYou: ";
            cin >> task_3_var_1;
            if (TaskThree(task_3_var_0, task_3_var_1) > 1) {
                cout << "\n\n" << TaskThree(task_3_var_0, task_3_var_1) << " values were found.\n\n";
            }
            else if (TaskThree(task_3_var_0, task_3_var_1) == 1) {
                cout << "\n\nOnly one value was found.\n\n";
            }
            else cout << "\n\nNo such values were found.\n\n";
            break;
        }
        case 4: {
            do {
                cout << "\n\nEnter a suit (S, D, H or C):\n\nYou: ";
                cin >> task_4_var_0;
                cout << "\n\nEnter a value (2-10, J, Q, K, A):\n\nYou: ";
                cin >> task_4_var_1;
            } while (task_4_var_0 != 'S' && task_4_var_0 != 'D' && task_4_var_0 != 'H' && task_4_var_0 != 'C' && task_4_var_1 != '2' && task_4_var_1 != '3' && task_4_var_1 != '4' && task_4_var_1 != '5' && task_4_var_1 != '6' && task_4_var_1 != '7' && task_4_var_1 != '8' && task_4_var_1 != '9' && task_4_var_1 != '10' && task_4_var_1 != 'J' && task_4_var_1 != 'K' && task_4_var_1 != 'Q' && task_4_var_1 != 'A');
            TaskFour(task_4_var_0, task_4_var_1);
            break;
        }
        case 5: {
            cout << "\n\nEnter a 6-digit number:\n\nYou: ";
            cin >> task_5_var_0;
            TaskFive(task_5_var_0);
            break;
        }
        default: {
            break;
        }
        }

        cout << s_con;
        cin >> c_con;
        cout << "\n-----------------------------------------------------------------\n";

    } while (c_con != 'n' && c_con != 'N');

    system("pause>0");
    return 0;
}

float TaskOne(float var_a, float var_b) {
    int temp0 = var_a;
    float Res = 1;
    for (int i = 0; i < var_b; i++) {
        Res *= temp0;
    }
    return Res;
}

int TaskTwo(int var_a, int var_b) {
    int S = 0;
    if (var_a > var_b) swap(var_a,var_b);
    for (int i = var_a; i < var_b; i++) {
        S += i;
    }
    return S;
}

int TaskThree(int var_a, int var_b) {
    int Sum = 0, counter = 0;
    for (int i = var_a; i < var_b; i++) {
        for (int j = 1; j < i; j++) {
            if (i % j == 0) {
                Sum += j;
            }
        }
        if (Sum == i) {
            counter++;
        }
        Sum = 0;
    }
    return counter;
}

void TaskFour(char var_a, char var_b) {
    cout << "\n\n Your card:\n\n";
    cout << "  ---------\n";
    cout << " | "<< var_b <<"       |\n";
    cout << " |         |\n";
    cout << " |  "<< var_a <<"      |\n";
    cout << " |      "<< var_a <<"  |\n";
    cout << " |         |\n";
    cout << " |       "<< var_b <<" |\n";
    cout << "  ---------\n\n\n";
}

void TaskFive(int var_a) {
    int temp = 0;
    int S0 = 0, S1 = 0;
    for (int i = 0; i < 3; i++) {
        temp = var_a % 10;
        var_a = var_a / 10;
        S0 += temp;
    }
    temp = 0;
    for (int i = 0; i < 6; i++) {
        temp = var_a % 10;
        var_a = var_a / 10;
        S1 += temp;
    }
    if (S0 == S1) cout << "\n\n Yes!\n\n";
    else cout << "\n\n Better luck next time.\n\n";
}
