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
    string menu_1 = "\n 1. à;\n 2. á;\n 3. â;\n 4. ã;\n 5. ä;\n 6. å;\n 7. æ;\n 8. ç;\n 9. è;\n10. ê. \n\nYou: ";
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
            // <à>
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
            // </à>

        case 2:
            // <á>
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
            // </á>

        case 3:
            // <â>
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
            // </â>

        case 4:
            // <ã>
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
            // </ã>

        case 5:
            // <ä>
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
            // </ä>

        case 6:
            // <å>
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
            // </å>

        case 7:
            // <æ>
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
            // </æ>

        case 8:
            // <ç>
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
           // </ç>

        case 9:
            // <è>
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
            // </è>

        case 10:
            // <ê>
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
            // </ê>
        

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


