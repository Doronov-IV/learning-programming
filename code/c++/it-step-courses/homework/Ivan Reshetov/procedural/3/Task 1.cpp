#include <iostream>

using namespace std;

int main()
{
    float R1, R2, R3, R0;

    R1 = 2;
    R2 = 4;
    R3 = 8;

    cout << "Calculating R0 resistance ..." << endl;

    R0 = 1 / (1 / R1 + 1 / R2 + 1 / R3);

    cout << "R0 = " << R0 << endl;
}