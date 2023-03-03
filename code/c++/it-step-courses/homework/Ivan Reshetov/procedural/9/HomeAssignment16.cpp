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
void TaskFour();

int main()
{
    srand(time(NULL));

    int user;
    char c_con;
    string menu = "\nChoose the number of a task (1-4).\n\nYou: ";
    string s_con = "\t----------------------------------\n\t| Do you want to continue? (y/n) |\n\t----------------------------------\n\nYou: ";

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
        case 3:
        {
            TaskThree();
            break;
        }
        case 4:
        {
            TaskFour();
            break;
        }
        }

        cout << s_con;
        cin >> c_con;
        cout << "\n\n";

    } while (c_con != 'n' && c_con != 'N');

    system("pause");
    return 0;
}

void TaskOne()
{
    int arr_main[10] = {}, arr_sub1[5] = {}, arr_sub2[5] = {};
    cout << "\n\nMain array:\n\n";
    for (int i = 0; i < 10; i++)
    {
        arr_main[i] = 1 + rand() % 9;
        cout << arr_main[i] << " ";
    }
    cout << "\n\nArray one: \n\n";
    int i = 0;
    while (i < 5)
    {
        arr_sub1[i] = arr_main[i];
        cout << arr_sub1[i] << " ";
    }
    cout << "\n\nArray two: \n\n";
    while (i < 10)
    {
        arr_sub2[i] = arr_main[i];
        cout << arr_sub2[i] << " ";
    }
    cout << ".\n\n";
}

void TaskTwo()
{
    int arr_res[10] = {};
    int arr_one[10] = {};
    int arr_two[10] = {};
    cout << "\n\nFirst array:\n\n";
    for (int i = 0; i < 10; i++)
    {
        arr_one[i] = 1 + rand() % 9;
        cout << arr_one[i] << " ";
    }
    cout << "\n\nSecond array:\n\n";
    for (int i = 0; i < 10; i++)
    {
        arr_two[i] = 1 + rand() % 9;
        cout << arr_two[i] << " ";
    }
    cout << "\n\nResault:\n\n";
    for (int i = 0; i < 10; i++)
    {
        arr_res[i] = arr_one[i] + arr_two[i];
        cout << arr_res[i] << " ";
    }
    cout << "\n\n";
}

void TaskThree()
{
    int spend[7] = {};
    string days[7] = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};
    float Mid = 0;
    int Sum = 0, c = 0;
    cout << "\n\nNow you should enter your spending for each day of the week.\n\n";
    for (int i = 0; i < 7; i++)
    {
        cout << days[i] << ": ";
        cin >> spend[i];
        cout << "\n";
        Sum += spend[i];
    }
    Mid = Sum / 7;
    cout << "Average sum is " << Mid <<".\n\nTotal sum is " << Sum << ".\n\nYou've spent more than 100 USD on:\n\n";
    for (int i = 0; i < 7; i++)
    {
        if (spend[i] > 100)
        {
            c++;
            cout << days[i] << "(" << spend[i] << ") ";
        }
    }
    cout << "\n\nTotal: " << c << "\n\n\n";
}

void TaskFour()
{
    string months[12] = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
    float usd_to_eur[12] = {};
    float percentage[12] = {};
    float resault[12] = {};
    int S, j = 0;

    cout << "\n\nPlease, enter the deposit sum in EUR: ";
    cin >> S;

    cout << "\n\nNow, please, enter the interest rate: \n\n";
    for (int i = 0; i < 12; i++)
    {
        cout << months[i] << ": ";
        cin >> percentage[i];
    }
    cout << "\n\nNow, please, enter the USD to EUR ratio: \n\n";
    for (int i = 0; i < 12; i++)
    {
        cout << months[i] << ": ";
        cin >> usd_to_eur[i];
    }
    int v = 0;
    for (int i = 0; i < 12; i++)
    {
        if (S * percentage[i]/100 > 500 * usd_to_eur[i])
        {
            resault[i] = S * percentage[i] / 100 / 2;
        }
    }
    cout << "\n\nSum available: \n";
    for (int i = 0; i < 12; i++)
    {
        if (resault[i] != 0)
        {
            cout << months[i] << ": " << resault[i] << "\n";
        }
    }
    cout << "\n\n";
}