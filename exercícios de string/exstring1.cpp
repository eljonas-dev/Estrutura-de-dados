//João Victor dos Santos, Luis Tiago Ferreira;

#include <iostream>
#include <string>
#include <windows.h>
#include <locale.h>

using namespace std;

void gotoxy(int x, int y) {
	COORD coord;
	coord.X = x;
	coord.Y = y;
	
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}

int CalculaCentro (int tam_string) {
	
	int centro = (80 - tam_string) / 2; //80 é o número de colunas no console
	return centro;
}

int main(int argc, char** argv) {
	
	setlocale(LC_ALL, "");
	string msg = "";
	cout << "Escreva uma mensagem" << endl;
	getline(cin, msg); //usei getline pra permitir que ele leia mais de uma palavra por espaço, ao invés de parar no primeiro espaço vazio.
	int col_inicial = CalculaCentro(msg.length());
	
	system("cls"); //Pra limpar a tela
	gotoxy(col_inicial, 5);
	cout << msg << endl;
	
	//cout << CalculaCentro(msg.length()) << endl;

	
	for (int i = 0; i < msg.length(); i++) { //o primeiro for percorre cada caractere da mensagem
		char letra = msg[i]; //Separa a letra pra depois a gente manipular ela no segundo for
		int col_atual = col_inicial + i;
		for (int j = 5; j < 20; j++) {
			gotoxy(col_atual, j);
			cout << " ";
			
			gotoxy(col_atual, j + 1);
			cout << letra;
			Sleep(50);
		}
	}
	return 0;
}

