

#include <iostream>

using namespace std;

int main()
{
    //#1

    cout << "Task One" << endl;
    cout << " \n";

    int C, in, hrs0, min0, sec0, hrs1, min1, sec1;

    cout << "Input time, sec: ";
    cin >> in;
    cout << " \n";

    hrs0 = in / 3600;
    C = in % 3600;
    min0 = C / 60;
    sec0 = C % 60;

    cout << "Current time: " << hrs0 << ":" << min0 << ":" << sec0 << endl;


    C = 86400 - in; // 86400 seconds = 24 hours precisely ;
    hrs1 = C / 3600;
    C = C % 3600;
    min1 = C / 60;
    sec1 = C % 60;

    cout << "Countdown till midnight: " << hrs1 << ":" << min1 << ":" << sec1 << endl;
    cout << " \n";
    cout << " \n";


    //#2

    cout << "Task Two" << endl;
    cout << " \n";

    int t, h2, tmp;
    cout << "Enter the time passed: " << endl;
    cin >> t;

    tmp = 28800 - t; // 28800 is a 8-hours work day ;
    h2 = tmp / 3600;

    cout << "You got " << h2 << " hours untill the end of your shift." << endl;


}

