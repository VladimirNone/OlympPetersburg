#include <iostream>
#include <stdio.h>
#include <omp.h>
#include <ctime>

using namespace std;

#define _OPENMP = 5;
int maxLengthOfArray = 290;

int* randomArray(int mn) {
	srand(time(NULL));
	int* arr = new int [mn];
	for (int i = 0; i < mn; i++) {
		arr[i] = rand() % 10;
	}
	return arr;
}

void ParallelFunc() {

	for (int n = 2; n <= maxLengthOfArray; n++) {
		int m = n + 1,* A = randomArray(m*n), * B = randomArray(n*m), * C  = new int [m*m];

		#pragma omp parallel for shared(A, B, C, n, m)
		for (int i = 0; i < m; ++i)
		{
			int* c = C + i * m;
			for (int j = 0; j < m; j++)
				c[j] = 0;
			for (int k = 0; k < n; k++)
			{
				const int* b = B + k * m;
				int a = A[i * n + k];
				for (int j = 0; j < m; j++)
					c[j] += a * b[j];
			}
		}
	}
}

int main()
{
	unsigned int start_timeAll = clock(); // начальное время

	ParallelFunc();

	unsigned int end_timeAll = clock(); // конечное время
	unsigned int search_time = end_timeAll - start_timeAll; // искомое время
	cout << endl << search_time / 1000.0;
	return 0;
}

