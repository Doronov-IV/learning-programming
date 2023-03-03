
// DISCLAIMER: all functions were tested with calc. Even #4 was tested on integers before it went decimal;
// DISCLAIMER: I've made it with functions just because it's easier to read all those instructions out of switch-case. Pressing '+' and '-' really helps.

#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::string;
using std::rand;
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
    cout << "\n\n\tTask one.\n\n";
    int A[20] = {};
    int F_min, F_max;

    for (int i = 0; i < 20; i++)
    {
        A[i] = 1 + rand() % 20;
    }

    cout << "rand() array:\n\n";

    for (int i = 0; i < 20; i++)
    {
        cout << A[i] << " ";
    }

    F_max = F_min = A[0];

    cout << "\n\n";
    for (int i = 0; i < 20; i++)
    {
        if (A[i] > F_max)
            F_max = A[i];
        if (A[i] < F_min)
            F_min = A[i];
    }

    cout << "Min = " << F_min << ";\n";
    cout << "Max = " << F_max << ".\n\n";

}

void TaskTwo()
{
    cout << "\n\n\tTask two.\n\n";
    int A[20] = {};
    int user_int, user_min, user_max, S = 0;
    
    cout << "Enter a min border:\n\nYou: ";
    cin >> user_min;
    cout << "\n\n";
    cout << "Enter a max border:\n\nYou: ";
    cin >> user_max;
    cout << "\n\n";
    cout << "Enter a value ("<< user_min << "-" << user_max << ")" << ": \n\nYou: ";
    cin >> user_int;
    cout << "\n\n";

    if (user_min > user_max) 
        swap(user_min, user_max);

    for (int i = 0; i < 20; i++)
    {
        A[i] = user_min + rand() % (user_max-user_min);
    }

    cout << "Default array:\n\n";

    for (int i = 0; i < 20; i++)
    {
        cout << A[i] << " ";
    }

    for (int i = 0; i < 20; i++)
    {
        if (A[i] < user_int) S += A[i];
    }

    cout << "Sum = " << S << ".\n\n";
}

void TaskThree()
{
    cout << "\n\n\tTask three.\n\n";
    int A[12] = {};
    string M[12] = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    int user_min, user_max, find_min, find_max, i_min = 0, i_max = 0;

    cout << "\n\nNow you should enter an income of an enterprise month by month.\n\n";
    for (int i = 0; i < 12; i++)
    {
        string ord;
        if (i + 1 == 1) ord = "st";
        else if (i + 1 == 2) ord = "nd";
        else if (i + 1 == 3) ord = "rd";
        else ord = "th";

        cout << "Enter " << i + 1 << ord << " month" << " (" << M[i] << ") " << "income: ";
        cin >> A[i];
    }

    do {
        cout << "\n\nNow, please, enter the min border (1-12): ";
        cin >> user_min;
        cout << "\n\n";
        cout << "Now, please, enter the max border (1-12): ";
        cin >> user_max;
        cout << "\n\n";
    } while (user_max - user_min < 2);

    find_max = A[user_max];
    find_min = A[user_min];
    i_max = user_max;
    i_min = user_min;
    for (int i = user_min-1; i < user_max-1; i++)
    {
        if (A[i] > find_max)
        {
            find_max = A[i];
            i_max = i;
        }
            
        if (A[i] < find_min)
        {
            find_min = A[i];
            i_min = i;
        }
    }
    cout << "Min income is for " << M[i_min] << ";\nMax income is for " << M[i_max] << ".\n\n";
}

void TaskFour()
{
    // If you haven't, please, read the disclaimer at line #2 of the code.

    int N, i_min, i_max;
    bool flag;
    float A[100] = {};
    float temp,fst_neg = 0, lst_neg, f_min, f_max, min, max, sum_neg, sum_bet = 0, mul = 1.0, mul_odd = 1.0;
    cout << "\n\nPlease, enter array capacity.\n\nYou: ";
    cin >> N;
    cout << "\n\nThe array follows: \n\n";
    for (int i = 0; i < N; i++)
    {
        A[i] = 20 - rand() % 39 + ((1 +rand() % 10)/10.0);
        cout << A[i] << " | ";
    }
    cout << "\n\n";

    //<1>
    sum_neg = 0;
    for (int i = 0; i < N; i++)
    {
        if (A[i] < 0) sum_neg += A[i];
    }

    cout << "1) " << "Sum of negatives: " << sum_neg << ";\n";
    //</1>

    //<2>
    f_min = f_max = A[0];
    i_min = i_max = 0;
    for (int i = 0; i < N; i++)
    {
        if (A[i] > f_max)
        {
            f_max = A[i];
            i_max = i;
        }
            
        if (A[i] < f_min)
        {
            f_min = A[i];
            i_min = i;
        }
    }

    if (i_min != i_max)
    {
        for (int i = i_min + 1; i < i_max; i++)
        {
            mul *= A[i];
        }
        cout << "\n2) Three values:\n Max = " << f_max << " with index [" << i_max << "];\n Min = " << f_min << " with index [" << i_min << "];\n Multiplication of those in between = " << mul << ".\n\n";
    }
    else cout << "Value exeption: min and max elements are even.\n\n";
    //</2>
   
    //<3>
    for (int i = 0; i < N; i++)
    {
        if (i % 2 == 0) mul_odd *= A[i];
    }
    cout << "3) Multiplication of elements with odd index: " << mul_odd << ".\n\n";
    //</3>

    //<4>
    sum_neg = 0;
    for (int v = 0; v < N; v++)
    {
        if (A[v] < 0)
        {
            fst_neg = A[v];
            i_min = v;
            break;
        }
    }
    if (fst_neg == 0) cout << "Value exeption: no negative numbers were generated. (insufficient amount)\n\n";
    lst_neg = fst_neg;
    for (int v = 0; v < N+1; v++)
    {
        if (A[v+1] < 0)
        {
            lst_neg = A[v + 1];
            i_max = v + 1;
        }
    }
    if (fst_neg == lst_neg)
    {
        cout << "Value exeption: one negative number was generated. (insufficient amount)\n\n";
    }
    else
    {
        for (int i = i_min+1; i < i_max; i++)
        {
            sum_neg += A[i];
        }
        cout << "4) Sum of those between [" << i_min << "] and [" << i_max << "] is equal to: " << sum_neg << ".\n\n";
    }
}