#include <iostream>

using std::cout;
using std::cin;
using std::endl;
using std::string;

int main()

{
    setlocale(LC_ALL, "Russian");

    int choice, N, M;
    char cont_value;

    string menu_0 = "\nChoose a figure of: \n";
    string menu_1 = "\n 1. �;\n 2. �;\n 3. �;\n 4. �;\n 5. �;\n 6. �;\n 7. �;\n 8. �;\n 9. �;\n10. �. \n\nYou: ";
    string inp = "\nNow, choose a side value : ";
    string cont_message = "Do you want to continue? (y/n)\n\nYou: ";

    do
    {
        cout << menu_0;
        cout << menu_1;
        cin >> choice;

        cout << inp;
        cin >> N;
        cout << "\n\n";

        switch (choice)
        {
        case 1:
            // <�>
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    (j >= i) ? cout << ("* ") : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 2:
            // <�>
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    (j <= i) ? cout << ("* ") : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 3:
            // <�>
        {
            (N % 2 != 0) ? M = N + 1 : M = N; // If we divide not-odd N by 2, we are going to have an issue when the middle element would not be written;

            for (int i = 0; i < M/2; i++)
            {
                for (int j = 0; j < N-i; j++)
                {
                    (j >= i) ? cout << ("* ") : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 4:
            // <�>
        {
            (N % 2 != 0) ? M = N + 1 : M = N; // same (case 3, line 65);

            for (int i = 0; i != N; i++)
            {
                for (int j = 0; j != N; j++)
                {
                    (i >= N - j - 1 && i >= j) ? cout << ("* ") : cout << "  "; 

                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 5:
            // <�>
        {
            for (int i = 0; i != N; i++)
            {
                for (int j = 0; j != N; j++)
                {
                    (i >= N - j - 1 && i >= j || i <= N - j - 1 && i <= j) ? cout << "* " : cout << "  "; // I've dicided to stop odd's check since these logical operators already provide it;
                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 6:
            // <�>
        {
            for (int i = 0; i != N; i++)
            {
                for (int j = 0; j != N; j++)
                {
                    (j <= N - i - 1 && j <= i || j >= N - i - 1 && j >= i) ? cout << "* " : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 7:
            // <�>
        {
            for (int i = 0; i != N; i++)
            {
                for (int j = 0; j != N; j++)
                {
                    (j <= N - i - 1 && j <= i) ? cout << "* " : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 8:
            // <�>
        {
            for (int i = 0; i != N; i++)
            {
                for (int j = 0; j != N; j++)
                {
                    (j >= N - i - 1 && j >= i) ? cout << "* " : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
           // </�>

        case 9:
            // <�>
        {
            for (int i = 0; i != N; i++)
            {
                for (int j = 0; j != N; j++)
                {
                    (j <= N - i - 1) ? cout << "* " : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
            // </�>

        case 10:
            // <�>
        {
            for (int i = 0; i != N; i++)
            {
                for (int j = 0; j != N; j++)
                {
                    (j >= N - i - 1) ? cout << "* " : cout << "  ";
                }
                cout << "\n";
            }
            break;
        }
            // </�>
        

        }
        cout << "\n\n";
        cout << cont_message;
        cin >> cont_value;
        cout << "\n\n";
    } while (cont_value != 'n' && cont_value != 'N');
    cout << "\n\n";

    system("pause");
    return 0;
}


