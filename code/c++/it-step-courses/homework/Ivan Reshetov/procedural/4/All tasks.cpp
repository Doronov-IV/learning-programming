
// Home assignment #6
// Doronov I.V.

#include <iostream>

using namespace std;

int main()
{

    // #1
    
    cout << "\tTask One " << endl;
    cout << " \n";

    float Tf, S, V;
    
    cout << "Enter the amount of kilometers: " << endl;
    cin >> S;
    cout << "Enter the travel time in mins: " << endl;
    cin >> Tf;

    V = S / Tf;

    cout << "Your velocity is:  " << V << " " << "km/min" << endl;
    cout << " \n";
    cout << " \n";


    // #2

    cout << "\tTask Two " << endl;
    cout << " \n";

    float H1, M1, S1, H2, M2, S2, cost;    // H, M and S variables stand for the time user enters: 1st set stands for the start, while snd one is for the end;
    int T1, T2, Ts, Total, Rub;   // Time is going to be measured with minutes for the price is settled with minutes;

    cost = 2;

    cout << "Enter the time of a start: \nEnter the hours: ";
    cin >> H1;
    cout << " \n";
    cout << "Enter the minutes: ";
    cin >> M1;
    cout << " \n";
    cout << "Enter the seconds: ";
    cin >> S1;
    cout << " \n";

    cout << "Enter the time of an end: \nEnter the hours: ";
    cin >> H2;
    cout << " \n";
    cout << "Enter the minutes: ";
    cin >> M2;
    cout << " \n";
    cout << "Enter the seconds: ";
    cin >> S2;
    cout << " \n";

    T1 = (H1 * 60) + M1 + (S1 / 60);

    T2 = (H2 * 60) + M2 + (S2 / 60);

    Ts = T2 - T1;

    Total = cost * Ts;

    Rub = Total * 2.74;

    cout << "The total travel price is: " << Total << " hr." <<" or " << Rub <<" rub." << endl;
    cout << " \n";
    cout << " \n";


    // #3

    cout << "\tTask Three " << endl;
    cout << " \n";

    float St, PriceOne, PriceTwo, PriceThree, Fuel, TS1, TS2, TS3;   // "St" is a distance var for a third task, TS stands for "Total Sum";

    cout << "Enter the distance in km: ";
    cin >> St;
    cout << " \n";

    cout << "Enter the price for 92nd: ";
    cin >> PriceOne;
    cout << " \n";

    cout << "Enter the price for 95th: ";
    cin >> PriceTwo;
    cout << " \n";

    cout << "Enter the price for Disel: ";
    cin >> PriceThree;
    cout << " \n";

    cout << "Enter the amount for Fuel needed: ";
    cin >> Fuel;
    cout << " \n";

    TS1 = St * PriceOne * Fuel / 100;
    TS2 = St * PriceTwo * Fuel / 100;
    TS3 = St * PriceThree * Fuel / 100;

    cout << "\t|Fuel brand     | Total cost|" << endl;
    cout << "\t|92nd \t\t|\t " << TS1 << "|" << endl;
    cout << "\t|95th \t\t|\t " << TS2 <<"|" << endl;
    cout << "\t|Disel\t\t|\t " << TS3 <<"|" << endl;


}   
   

