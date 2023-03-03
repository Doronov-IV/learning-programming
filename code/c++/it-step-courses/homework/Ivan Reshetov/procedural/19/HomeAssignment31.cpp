#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;


/* GLOBAL VARIABLES */

float sqrtI = -1.0;

/* structures */

struct ComplexNumber {
    float r = 1.0;
    float i = 1.0;
};

struct Auto {
    float len = 0;
    float clearance = 0;
    float V = 0;
    float P = 0;
    float wheelD = 0;
    string color = "colorless";
    string tm = "handle";
};

/* main */
void TaskOne();
void TaskTwo();

ComplexNumber complexSum(ComplexNumber var_a, ComplexNumber var_b);
ComplexNumber complexDif(ComplexNumber var_a, ComplexNumber var_b);
ComplexNumber complexMul(ComplexNumber var_a, ComplexNumber var_b);
ComplexNumber complexDiv(ComplexNumber var_a, ComplexNumber var_b);

void showMyCar(Auto unit);
Auto rockMyCar();

/* auxiliary */

void showContinue();
void showSubContinue(int task_number);
void showComplexNumber(ComplexNumber a);

float getLen(Auto unit);
float getClearance(Auto unit);
float getV(Auto unit);
float getP(Auto unit);
float getWheelD(Auto unit);
string getColor(Auto unit);
string getTransmissionType(Auto unit);

void setLen(Auto unit, float value);
void setClearance(Auto, float value);
void setV(Auto, float value);
void setP(Auto, float value);
void setWheelD(Auto, float value);
void setColor(Auto, string value);
void setTransmissionType(Auto, string value);


int main() {
    srand(time(NULL));
    int user;
    char c_con;
    string menu = "\n Choose the number of a task (1-2).\n\nYou: ";

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
        showContinue();
        cin >> c_con;
        cout << "\n\n";
        system("cls");
    } while (c_con != 'n' && c_con != 'N');
    cout << "\n\n\tYou've quit. Have a nice day!\n\t   (Press any key...)\n\n\n";

    system("pause>0");
    return 0;
}



// main functions


void TaskOne() {
    cout << "\n\nTask One.\n\n";
    ComplexNumber x;
    ComplexNumber y;
    char user0;
    string action = "And now, please select an action. (+ , - , * , /)\n\nYou: ";
    char c_con0 = 'n';
    
    do
    {
        cout << "\n Please, input a real part of a first complex number.\n\nYou: ";
        cin >> x.r;
        cout << "\n\n";
        cout << "\n Now please, input an imaginary part of a first complex number.\n\nYou: ";
        cin >> x.i;
        cout << "\n\n";
        cout << "\n Now please, input a real part of a second complex number.\n\nYou: ";
        cin >> y.r;
        cout << "\n\n";
        cout << "\n Now please, input an imaginary part of a second complex number.\n\nYou: ";
        cin >> y.i;
        cout << "\n\n";
        cout << action;
        cin >> user0;
        cout << "\n\n";

        switch (user0)
        {
        case '+':
        {
            showComplexNumber(complexSum(x, y));
            break;
        }
        case '-':
        {
            showComplexNumber(complexDif(x, y));
            break;
        }
        case '*':
        {
            showComplexNumber(complexMul(x, y));
            break;
        }
        case '/':
        {
            showComplexNumber(complexDiv(x, y));
            break;
        }
        default:
            break;
        }
        showSubContinue(1);
        cin >> c_con0;
        cout << "\n\n";
        system("cls");
    } while (c_con0 != 'n' && c_con0 != 'N');
    cout << "\n\n\tYou've quit Task One.\n\n\n";
    
    cout << "\n\n";

}

void TaskTwo() {
    cout << "\n\n Task Two.\n\n";
    Auto car95;
    car95 = rockMyCar();
    cout << "I've made a new car. It is ...";
    showMyCar(car95);
    
}

ComplexNumber complexSum(ComplexNumber var_a, ComplexNumber var_b) {
    ComplexNumber res;
    res.r = var_a.r + var_b.r;
    res.i = var_a.i + var_b.i;
    return res;
}

ComplexNumber complexDif(ComplexNumber var_a, ComplexNumber var_b) {
    ComplexNumber res;
    res.r = var_a.r - var_b.r;
    res.i = var_a.i - var_b.i;
    return res;
}

ComplexNumber complexMul(ComplexNumber var_a, ComplexNumber var_b) {
    ComplexNumber res;
    res.r = (var_a.r * var_b.r) + (var_a.i * var_b.i * sqrtI);
    res.i = (var_a.r * var_b.i) + (var_a.i * var_b.r);
    return res;
}

ComplexNumber complexDiv(ComplexNumber var_a, ComplexNumber var_b) {
    ComplexNumber res;
    ComplexNumber aux;
    aux.r = var_b.r;
    aux.i = var_b.i * (-1.0);

    res.r = ((var_a.r * aux.r) + (var_a.i * aux.i * sqrtI)) / ((var_b.r * var_b.r) + (var_b.i * var_b.i));
    res.i = ((var_a.r * aux.i) + (var_a.i * aux.r)) / ((var_b.r * var_b.r) + (var_b.i * var_b.i));
    return res;
}

/*
    float len = 0;
    float clearance = 0;
    float V = 0;
    float P = 0;
    float wheelD = 0;
    string color = "colorless";
    string tm = "handle";
*/

float getLen(Auto unit) {
    return unit.len;
}

float getClearance(Auto unit) {
    return unit.clearance;
}

float getV(Auto unit) {
    return unit.V;
}

float getP(Auto unit) {
    return unit.P;
}

float getWheelD(Auto unit) {
    return unit.wheelD;
}

string getColor(Auto unit) {
    return unit.color;
}

string getTransmissionType(Auto unit) {
    return unit.tm;
}

void showMyCar(Auto unit) {
    cout << "\n\nYour car:\n\n";
    cout << "Length " << "\t\t\t" << unit.len << "\n";
    cout << "Clearannce " << "\t\t" << unit.clearance << "\n";
    cout << "Engine Volume " << "\t\t" << unit.V << "\n";
    cout << "Engine Power " << "\t\t" << unit.P << "\n";
    cout << "Wheel Diameter " << "\t\t" << unit.wheelD << "\n";
    cout << "Color " << "\t\t\t" << unit.color << "\n";
    cout << "Transmission Type " << "\t" << unit.tm << "\n\n";
}

Auto rockMyCar() {
    Auto car;
    car.len = 0.1 + rand() % 4;
    car.clearance = 0.1 + rand() % 2;
    car.V = 1.0 + rand() % 50;
    car.P = 1.0 + rand() % 1500;
    car.wheelD = 1.0 + rand() % 300;
    
    int col = 0;
    col = 1 + rand() % 9;
    switch (col) {
    case 1:
        car.color = "Red";
        break;
    case 2:
        car.color = "Orange";
        break;
    case 3:
        car.color = "Yellow";
        break;
    case 4:
        car.color = "Green";
        break;
    case 5:
        car.color = "Cyan";
        break;
    case 6:
        car.color = "Blue";
        break;
    case 7:
        car.color = "Purple";
        break;
    case 8:
        car.color = "White";
        break;
    case 9:
        car.color = "Black";
        break;
    default:
        car.color = "Blue";
        break;
    }

    int trans = 0;
    trans = rand() % 2;
    switch (trans) {
    case 0: 
        car.tm = "Handle";
        break;
    case 1:
        car.tm = "Automatic";
        break;
    default :
        car.tm = "Handle";
        break;
    }
    return car;
}

string getTransmission(Auto unit) {
    return unit.tm;
}

void showComplexNumber(ComplexNumber a) {
    cout << "\n\n";
    if (a.r == 0 && a.i == 0) {
        cout << "The result is 0.\n\n";
    }
    else {
        if (a.r == 0) {
            cout << "The result follows: " << a.i << "i.\n\n";
        }
        else {
            if (a.i > 0) {
                cout << "The result follows: " << a.r << " + " << a.i << "i.\n\n";
            }
            else if (a.i < 0) {
                cout << "The result follows: " << a.r << " " << a.i << "i.\n\n";
            }
            else cout << "The result follows: " << a.r << ".\n\n";
        }
    }
}

void showContinue() {
    const int len = 47;
    cout << "\t" << char(201);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(187);
    cout << "\n\t" << char(186);

    cout << "  Do you want to continue the programme? (y/n) ";

    cout << char(186) << "\n";
    cout << "\t" << char(200);
    for (int i = 0; i < len; i++) {
        cout << char(205);
    }
    cout << char(188) << "\n";
    cout << "\t You: ";
}

void showSubContinue(int task_number) {
    const int len = 47;
    cout << "\t" << char(218);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(191);
    cout << "\n\t" << char(179);

    cout << "     Do you want to continue Task "<< task_number << "? (y/n)     ";

    cout << char(179) << "\n";
    cout << "\t" << char(192);
    for (int i = 0; i < len; i++) {
        cout << char(196);
    }
    cout << char(217) << "\n";
    cout << "\t You: ";
}

