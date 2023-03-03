#include <iostream>

using namespace std;

int main()
{
	float S, L, R;

	cout << "Enter Length of a round:   " << endl;
	cin >> L;

	R = L / (2 * 3.14);
	S = 3.14 * R * R;

	cout << "The area of your circle is: " << S << endl;
}