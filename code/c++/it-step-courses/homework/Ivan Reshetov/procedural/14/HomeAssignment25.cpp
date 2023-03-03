#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

void TaskOne();
void TaskTwo();
void TaskThree();

int main() {
    srand(time(NULL));
    int user;
    char c_con = 'n';
    string menu = "\nChoose the number of a task (1-3).\n\nYou: ";
    string s_con = "\t----------------------------------------------\n\t| Do you want to continue the program? (y/n) |\n\t----------------------------------------------\n\nYou: ";
    do {
        cout << "\n\nWarning! The arrays are assembled in reversed order. I made it to understand bin. search better.\n\n";
        cout << menu;
        cin >> user;

        switch (user) {
        case 1: {
            TaskOne();
            break;
        }
        case 2: {
            TaskTwo();
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

void TaskOne() {
    cout << "\n\n Task One. Linear search.\n\n";
    int key;
    int A[1000] = {};
    for (int i = 0; i < 1000; i++) {
        A[i] = 1000-i;
    }
    cout << "Enter a key.\n\n You: ";
    cin >> key;
    cout << "\n\n";
    if (A[0] == key) cout << "You've entered the very first element. (Steps: 1)\n\n";
    else {
        for (int i = 1; A[i-1] != key; i++) {
            if (A[i] == key) cout << "The key-element is at position " << i << ". (Steps: " << i << ")\n\n";
        }
    }
}

void TaskTwo() {
    cout << "\n\n Task Two. Binary search.\n\n";
    int min = 0, max = 999, c = 0;
    int mid;
    int key;
    int A[1000] = {};
    for (int i = 0; i < 1000; i++) {
        A[i] = 1000 - i;
    }
    cout << "Enter a key.\n\n You: ";
    cin >> key;
    cout << "\n\n";

    while (min<=max) {
        mid = (min + max) / 2;
        c++;
        if (A[mid] == key) {
            cout << "The key-element is at position " << mid << ". (Steps: " << c << ")\n\n";
            break;
        }
        else if (A[mid] < key) {
            max = mid + 1;
        }
        else if (A[mid] > key) {
            min = mid - 1;
        }
    }
    
}

void TaskThree() {
    int bin, dec = 0, temp;
    cout << "\n\nTask Three.\n\n\tInput a binary value: \n\nYou: ";
    cin >> bin;
    cout << "\n\n";
    int j = bin + 1, c = 1;

    while (j != 1) { // finding out the number of bits of a binary value
        j/=10;
        c++;
    }
    temp = c-1;
    for (int i = bin; temp != -1; temp--) {
        //<digit> gets a current digit of a bin value
        c = temp+1;
        i = bin;
        for (; c != 1; c--) {
            i /= 10;
        }
        i = i % 10;
        //</digit>
        if (i == 1) {
            dec += i * pow(2, temp);
        }
        else continue;
        
    }
    cout << "Your value: " << bin << ". Result: " << dec << ".\n\n";

}