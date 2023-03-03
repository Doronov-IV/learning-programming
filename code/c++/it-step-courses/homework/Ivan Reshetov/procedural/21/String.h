#pragma once

#include <iostream>
#include <conio.h>
#include <string>
#include <fstream>

#ifndef UNICODE
#define UNICODE
#endif
#ifndef _UNICODE
#define _UNICODE
#endif

#include <Windows.h>

using namespace std;

int mystrcmp(const char* str1, const char* str2) {
	int len1 = 0, len2 = 0, i = 0;
	while (str1[i] != '\0') {
		i++;
	}
	len1 = i;
	i = 0;
	while (str2[i] != '\0') {
		i++;
	}
	len2 = i;
	int dif = len1 - len2;
	if (dif == 0) return 0;
	else if (dif > 0) return 1;
	else if (dif < 0) return -1;
}

int StringToNumber(char* str) {
	long C = 0;
	int rad = 0;

	for (int i = 0; str[i] != '\0'; i++) {
		rad++;
	}
	rad--;
	int j = pow(10, rad);
	for (int i = 0; str[i] != '0'; i++, j /= 10) {
		switch (str[i]) {
		case '1':
			C += 1 * j;
			break;
		case '2':
			C += 2 * j;
			break;
		case '3':
			C += 3 * j;
			break;
		case '4':
			C += 4 * j;
			break;
		case '5':
			C += 5 * j;
			break;
		case '6':
			C += 6 * j;
			break;
		case '7':
			C += 7 * j;
			break;
		case '8':
			C += 8 * j;
			break;
		case '9':
			C += 9 * j;
			break;
		default:
			break;
		}
	}
	return C;
}

char* NumberToString(int number) {
	int r = 0, n = number + 1, temp;
	while (n % 10 != 1) {
		n /= 10;
		r++;
	}
	n = number;
	char* buf = new char[r];
	temp = r;
	int r0 = r + 1;
	for (int i = n, j = 0; temp != -1; temp--) {
		//<digit> gets a current digit of a bin value
		r = temp + 1;
		i = n;
		for (; r != 1; r--) {
			i /= 10;
		}
		i = i % 10;
		if (i == 1) {
			buf[j++] = '1';
		}
		else if (i == 2) {
			buf[j++] = '2';
		}	
		else if (i == 3) {
			buf[j++] = '3';
		}
		else if (i == 4) {
			buf[j++] = '4';
		}
		else if (i == 5) {
			buf[j++] = '5';
		}
		else if (i == 6) {
			buf[j++] = '6';
		}
		else if (i == 7) {
			buf[j++] = '7';
		}
		else if (i == 8) {
			buf[j++] = '8';
		}
		else if (i == 9) {
			buf[j++] = '9';
		}
		else if (i == 0) {
			buf[j++] = '0';
		}
		else continue;
	}
	buf[r0] = '\0';
	return buf;
}

char* Uppercase(char* str1) {
	int l = 0;
	for (int i = 0; str1[i] != '\0'; i++) {
		l++;
	}
	char* buf = new char[l];
	for (int i = 0; str1[i] != '\0'; i++) {
		buf[i] = str1[i];
	}
	for (int i = 0; str1[i] != '\0'; i++) {
		if (int(str1[i]) >= 97 && int(str1[i]) <= 122) buf[i] -= 32;
	}
	buf[l] = '\0';
	return buf;
}

char* Lowercase(char* str1) {
	int l = 0;
	for (int i = 0; str1[i] != '\0'; i++) {
		l++;
	}
	char* buf = new char[l];
	for (int i = 0; str1[i] != '\0'; i++) {
		buf[i] = str1[i];
	}
	for (int i = 0; str1[i] != '\0'; i++) {
		if (int(str1[i]) >= 65 && int(str1[i]) <= 90) buf[i] += 32;
	}
	buf[l] = '\0';
	return buf;
}

char* mystrrev(char* str) {
	int l = 0;
	for (int i = 0; str[i] != '\0'; i++) {
		l++;
	}
	char* buf = new char[l];
	for (int i = 0; i < l/2; i++) {
		swap(buf[i], buf[l - i - 1]);
	}
	buf[l] = '\0';
	return buf;
}
