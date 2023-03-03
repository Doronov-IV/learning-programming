#include <ctime>
#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::rand;
using std::string;
using std::swap;

void Insert();
void Bubble();
void Gnome();

int main()
{
    srand(time(NULL));
    Bubble();
    Insert();
    Gnome();

    system("pause");
    return 0;
}

void Bubble()
{
    cout << "\n\n<Bubble>\n\nStarting array:\n\n";
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

void Insert()
{
    cout << "\n\n<Insert>\n\n";
    cout << "\n\nStarting array:\n\n";
    int A[10] = {};
    const int N = 10;

    for (int i = 0; i < N; i++)
    {
        A[i] = 1 + rand() % 9;
        cout << A[i] << " ";
    }
    cout << "\n\n";
    int key, j;
    for (int i = 1; i < N; i++)
    {
        key = A[i];
        j = i;
        while (j > 0 && A[j - 1] > key) 
        {
            A[j] = A[j - 1];
            j--;
        }
        A[j] = key;

    }


    cout << "\n\nSorted array:\n\n";
    for (int i = 0; i < N; i++)
    {
        cout << A[i] << " ";
    }
    cout << "\n\n";
}

void Gnome()
{
    cout << "\n\n<Gnome>\n\n";
    int A[20] = {};
    const int N = 10;
    cout << "\n\nStarting array:\n\n";
    for (int i = 0; i < N; i++)
    {
        A[i] = 1 + rand() % 9;
        cout << A[i] << " ";
    }
    cout << "\n\n";
    for (int i = 0; i < N;)
    {
        if (A[i] >= A[i - 1])
        {
            i++;
        }
        else
        {
            swap(A[i], A[i - 1]);
            i--;
        }

    }

    cout << "\n\nSorted array:\n\n";
    for (int i = 0; i < N; i++)
    {
        cout << A[i] << " ";
    }
    cout << "\n\n";
}