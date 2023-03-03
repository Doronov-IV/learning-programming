#include <iostream>

using namespace std;

int main()
{
    float S, V, T, A;


    cout << "Enter your Velocity, Time and Acceleration   " << endl;

    cin >> V >> T >> A;

    S = (V * T) + (A * T * T) / 2;

    cout << "The distance is " << S << endl;
}