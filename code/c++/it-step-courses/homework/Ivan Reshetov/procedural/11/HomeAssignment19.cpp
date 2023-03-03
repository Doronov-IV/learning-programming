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
    char c_con = 'n';

    string menu = "\nChoose the number of a task (1-4).\n\n(Task 3 was succefuly failed.)\n\nYou: ";
    string s_con = "\t----------------------------------------------\n\t| Do you want to continue the program? (y/n) |\n\t----------------------------------------------\n\nYou: ";

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
        default:
        {
            break;
        }
        }

        cout << s_con;
        cin >> c_con;
        cout << "\n-----------------------------------------------------------------\n";

    } while (c_con != 'n' && c_con != 'N');

    system("pause");
    return 0;
}

void TaskOne()
{
    long long Mob[10] = { 12377430451, 12349480985, 12364751728, 12348865875, 12347639751, 12367620731, 12369742982, 12397110066, 12357413825, 12364590028 };
    long long Stat[10] = { 12126948782, 12121316588, 12123131021, 12127115389, 12121841410, 12121656803, 12125478766, 12127243635, 12123359231, 12123986347 };

    int user, id;
    char c_con = 'n';
    string menu = "\nChoose the action (1-4).\n\n1. Sort with mobile phone numbers. \n2. Sort with home numbers. \n3. Show specific info. \n4. Exit. \n\nYou: ";
    string s_con = "\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\t| Do you want to continue Task One? (y/n) |\n\t~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\nYou: ";

    do
    {
        cout << "\n\nTask One.\n\n\tPhone book:\n\n";
        for (int i = 0; i < 10; i++)
        {
            if (i + 1 <= 9)
                cout << i + 1 << ".  " << "Mobile: " << Mob[i] << "; Home: " << Stat[i] << ".";
            else cout << i + 1 << ". " << "Mobile: " << Mob[i] << "; Home: " << Stat[i] << ".";
            cout << "\n";
        }


        cout << menu;
        cin >> user;

        switch (user)
        {
        case 1:
        {
            int j;
            long long key_1, key_0;
            for (int i = 1; i < 10; i++)
            {
                key_0 = Mob[i];
                key_1 = Stat[i];
                j = i;
                while (j > 0 && Mob[j - 1] > key_0)
                {
                    Mob[j] = Mob[j - 1];
                    Stat[j] = Stat[j - 1];
                    j--;
                }
                Mob[j] = key_0;
                Stat[j] = key_1;
            }
            cout << "\n\n\tNew Phone book : \n\n";
                for (int i = 0; i < 10; i++)
                {
                    if (i+1 < 10)
                        cout << i + 1 << ".  " << "Mobile: " << Mob[i] << "; Home: " << Stat[i] << ".";
                    else cout << i + 1 << ". " << "Mobile: " << Mob[i] << "; Home: " << Stat[i] << ".";
                    cout << "\n";
                }
            cout << "\n\n";
            break;
        }
        case 2:
        {
            int j;
            long long key_1, key_0;
            for (int i = 1; i < 10; i++)
            {
                key_0 = Mob[i];
                key_1 = Stat[i];
                j = i;
                while (j > 0 && Stat[j - 1] > key_1)
                {
                    Mob[j] = Mob[j - 1];
                    Stat[j] = Stat[j - 1];
                    j--;
                }
                Mob[j] = key_0;
                Stat[j] = key_1;
            }
            cout << "\n\n\tNew Phone book : \n\n";
            for (int i = 0; i < 10; i++)
            {
                if (i + 1 <= 9)
                    cout << i + 1 << ".  " << "Mobile: " << Mob[i] << "; Home: " << Stat[i] << ".";
                else cout << i + 1 << ". " << "Mobile: " << Mob[i] << "; Home: " << Stat[i] << ".";
                cout << "\n";
            }
            cout << "\n\n";
            break;
        }
        case 3:
        {
            do
            {
                cout << "\n\nEnter user's id (1-10)\n\nYou: ";
                cin >> id;
            } while (id <= 1 && id >= 10);
            cout << "\n\nUser #" << id << " - " << "Mobile: " << Mob[id-1] << "; Home: " << Stat[id-1] << ".\n\n";
            break;
        }
        case 4:
        {
            break;
        }
        default:
            break;
        }

        cout << s_con;
        cin >> c_con;
        cout << "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";

    } while (c_con != 'n' && c_con != 'N');

}

void TaskTwo()
{
    cout << "\n\nTask Two.\n\nStarting array:\n\n";
    int A[10] = {};
    bool flag;
    const int N = 10;
    for (int i = 0; i < N; i++)
    {
        A[i] = 1 + rand() % 9;
        cout << A[i] << " ";
    }

    for (int i = 0; i < N - 1; i++)
    {
        flag = false;
        for (int j = 0; j < N - i - 1; j++)
        {
            if (A[j + 1] < A[j])
            {
                flag = true;
                swap(A[j], A[j + 1]);
            }
        }
        if (flag == false) break;
    }

    cout << "\n\nSorted array:\n\n";
    for (int i = 0; i < N; i++)
    {
        cout << A[i] << " ";
    }
    cout << "\n\n";
}

void TaskThree()
{
    cout << "\n\n  I've been writing charts and paintings for about an hour.\n";
    cout << "   I've decided to look for the solution on the Internet.\n";
    cout << "   Since I've found it much more simple than I expected,\n";
    cout << " I've made up my mind to work harder and trying different ways\n";
    cout << "     of understanding the problem from now on. \n\n";
}

void TaskFour()
{
    cout << "\n\n\tTask Four.\n\n";

    bool flag;
    int counter_0 = 0, counter_1 = 0;

    int A[10][1000] = {}; // bubble
    int B[10][1000] = {}; // selection

    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 1000; j++)
        {
            A[i][j] = 1 + rand() % 9;
            B[i][j] = A[i][j];
        }
    }

    cout << "   > Proceeding bubble sort ..........";

    for (int temp = 0; temp < 10; temp++)
    {
        for (int i = 0; i < 1000 - 1; i++)
        {
            flag = false;
            for (int j = 0; j < 1000 - i - 1; j++)
            {
                if (A[temp][j + 1] < A[temp][j])
                {
                    flag = true;
                    swap(A[temp][j], A[temp][j + 1]);
                }
            }
            if (flag == false) break;
            else (counter_0++);
        }
    }

    cout << " Completed.\n   > Proceeding selection sort .......";

    for (int temp = 0, min; temp < 10; temp++)
    {
        for (int i = 0; i < 1000 - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < 1000; j++)
            {
                if (B[temp][j] < B[temp][min])
                    min = j;
            }
            if (B[temp][min] != B[temp][i])
            {
                swap(B[temp][min], B[temp][i]);
                counter_1++;
            }
        }
    }
    cout << " Completed.\n\n";
    cout << "Resaults:\n\n\tBubble: " << counter_0 << " swaps;\n\n" << "\tSelection: " << counter_1 << " swaps;\n\n "<< "\tDifference: " << abs(counter_0 - counter_1) << ".\n\n" << "\tTask finished.\n\n";

}
