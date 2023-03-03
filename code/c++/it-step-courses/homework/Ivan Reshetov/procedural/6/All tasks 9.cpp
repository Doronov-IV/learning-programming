
#include <iostream>

using namespace std;

//definition

int TaskOne(int var_a);
int TaskTwo(int var_a, int var_b);
int TaskThree(); // I've used a while-loop though the task was obviously designed with for-loop;
int TaskFour(int var_a);
int TaskFive(int var_a);



int main()
{
    /*int  x, y, z;*/
    int x, y, a, v, k, value; // a:1st; x,y:2nd; v:4th; ;   
    char con;

    do {
        con = 0;
        cout << "\nEnter a number of a task you want to see. (five in total)\nUse digits. Task number: ";
        cin >> value;
        cout << " \n";
        switch (value) {
        case 1:

            // an imput check; 

            cout << "Starting Task #1." << endl;
            do {
                cout << "\nEnter a value: ";
                cin >> a;
                if (a >= 500) cout << "Value must be less than 500." << endl;
            } while (a >= 500);
            cout << " \n";

            TaskOne(a); // function;
            break;

        case 2:

            cout << "\nEnter two values: \n";
            cin >> x;
            cin >> y;
            cout << " \n";
            TaskTwo(x, y);
            break;

        case 3:

            TaskThree();
            break;
            // case 4: TaskFour(); break;
            // case 5: TaskFive(); break;

        case 4:

            do {
                cout << "\nEnter a value: ";
                cin >> v;
                if (v > 20 || v < 1) cout << "Value must be less than 20 and more than 1." << endl;
            } while (v > 20 || v < 1);
            cout << " \n";
            TaskFour(v);
            break;

        case 5: 

             do {
                cout << "\nEnter a value: ";
                cin >> k;
                if (k > 9 || k < 1) cout << "Value must be less than 10 and more than 1." << endl;
            } while (k > 9 || k < 1);
            cout << " \n";
            TaskFive(k);
            break;

        default: cout << "Choose a proper task (1-5)";
        }
        cout << " \n";
        cout << "Do you want to continue? (y/n) ";
        cin >> con;
    } while (con == 'Y' || con == 'y');

}

// declaration;

int TaskOne(int var_a) {
    int Sum = 0;
    do {
        Sum = Sum + var_a;
        var_a++;
    } while (var_a <= 500);
    cout << "Sum is " << Sum << endl;

    return 0;

}

int TaskTwo(int var_a, int var_b) {
    int Mul = 1;
    do {
        Mul = Mul * var_a;
        var_b--;
    } while (var_b != 0);
    cout << "x power y is " << Mul << endl;

    return 0;

}

int TaskThree() {
    float Res = 1.0;
    int var_a = 2;
    do {
        Res += var_a;
        var_a++;
    } while (var_a != 1001);
    Res = Res / 1000;
    cout << "Resault is " << Res << endl;

    return 0;
}

int TaskFour(int var_a) {
    int Mul = 1;
    while (var_a <= 20) Mul = Mul * var_a++;
    cout << "Multiplication is " << Mul << ".";
    return 0;
}

int TaskFive(int var_a) {
    int Mul = var_a, i=1;

    while (i!=11) {
        cout << " " << var_a << " x " << i << " = " << Mul << endl;
        i++;
        Mul = var_a * i;
    }
    return 0;
}



