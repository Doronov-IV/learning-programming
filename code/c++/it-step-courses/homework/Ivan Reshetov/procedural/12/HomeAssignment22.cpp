#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

int main() {
    srand(time(NULL));


    const int n1 = 10;
    const int n2 = 4;
    float array2[10];
    int array[n1][n2] = {
        {4,4,4,3},
        {3,4,3,4},
        {5,4,5,4},
        {5,5,5,5},
        {4,3,4,5},
        {3,4,3,4},
        {4,4,4,4},
        {5,5,5,5},
        {2,3,2,2},
        {4,3,4,4}
    };
    int sequence[n1] = {1,2,3,4,5,6,7,8,9,10};

    cout << " Starting sequence: \n\n";

    cout << "\t\tI\tII\tIII\tIV" << endl;
    float min = 5, max = 0;
    for (int i = 0, summ; i < n1; i++) {
        summ = 0;
        cout << "student " << sequence[i] << " :\t";
        for (int j = 0; j < n2; j++) {
            summ += array[i][j];
            cout << array[i][j] << "\t";
        }
        cout << ": av = " << (float)summ / 4;
        array2[i] = (float)summ / 4;
        if (min > (float)summ / 4) min = (float)summ / 4;
        if (max < (float)summ / 4) max = (float)summ / 4;
        cout << endl;
    }
    cout << "\nThe best: ";
    for (int i = 0, summ; i < n1; i++) {
        summ = 0;
        for (int j = 0; j < n2; j++) {
            summ += array[i][j];
        }
        if (max == (float)summ / 4) cout << "st" << sequence[i] << " ";
    }
    cout << "  av: " << max;
    cout << endl;

    cout << "The worst: ";
    for (int i = 0, summ; i < n1; i++) {
        summ = 0;
        for (int j = 0; j < n2; j++) {
            summ += array[i][j];
        }
        if (min == (float)summ / 4) cout << "st" << sequence[i] << " ";
    }
    cout << "  av: " << min;
    cout << endl;

    for (int i = 0; i < 10; i++) {
        cout << array2[i] << " ";
    }

    // sorting
    bool flag;
    for (int i = 0; i < 9; i++) {
        flag = true;
        for (int j = 0; j < 9 - i; j++) {
            if (array2[j] < array2[j + 1]) {
                swap(array2[j], array2[j + 1]);
                swap(sequence[j], sequence[j + 1]);
                for (int k = 0; k < 4; k++) {
                    swap(array[j][k], array[j + 1][k]);
                }
                flag = false;
            }
        }
        if (flag == true) break;
    }

    // displaying resault
    cout << endl;
    cout << "\n\n Sorted sequence: \n\n";
    for (int i = 0, summ; i < n1; i++) {
        summ = 0;
        cout << "student " << sequence[i] << " :\t";
        for (int j = 0; j < n2; j++) {
            summ += array[i][j];
            cout << array[i][j] << "\t";
        }
        cout << ": av = " << (float)summ / 4;
        cout << endl;
    }


    system("pause > 0");
    return 0;
}
