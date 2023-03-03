#include <iostream>
#include <ctime>

using std::cout;
using std::cin;
using std::rand;
using std::swap;

#define space cout << "\n\n"

void quickSort(int* pA, int low, int high);
int part(int* pA, int low, int high);
int getArraySize(int* pA);
void displayArray(int* pA);

int main()
{
	srand(time(NULL));

	int C;
	space;
	cout << "Enter the array Capacity: ";
	cin >> C;
	space;
	int* pA = new int[C];

	for (int i = 0; i < C; i++) {
		pA[i] = 1 + rand() % 75;
	}
	
	cout << "\tStarting array:";
	space;
	displayArray(pA);
	space;

	quickSort(pA, 0, C-1);

	cout << "\tSorted array:";
	space;
	displayArray(pA);

	system("pause>0");
	return 0;
}

int getArraySize(int* pA) {
	int size = _msize(pA) / sizeof(pA[0]);
	return size;
}

void displayArray(int* pA) {
	int size = getArraySize(pA);
	space;
	for (int i = 0; i < size; i++) {
		cout << " " << pA[i];
	}
	space;
}

int part(int* pA, int low, int high) {
    int p = pA[high];
    int N = (low - 1);

    for (int i = low; i < high; i++) {

        if (pA[i] <= p) {
            N++;   
            swap(pA[N], pA[i]);
        }
    }
    swap(pA[N + 1], pA[high]);
    return (N + 1);
}

void quickSort(int* pA, int low, int high) {
    if (low < high) {
        int pi = part(pA, low, high);
        quickSort(pA, low, pi - 1);
        quickSort(pA, pi + 1, high);
    }
}