//João Victor dos Santos, Luis Tiago Ferreira.

#include <string>
#include <iostream>
#include <windows.h>

using namespace std;

int main(int argc, char** argv) {
	//guardando a mensagem numa variável
	setlocale(LC_ALL, "");
	string msg = "";
	cout << "Escreva uma mensagem!" << endl;
	getline(cin, msg);
	
	//loop pra remover os espaços da mensagem (e colocar em caixa alta)
	string msglimpa = ""; 
	for (int i = 0; i < msg.length(); i++) {
		char letra = toupper(msg[i]);
		
		if (letra != ' ') {
			msglimpa.push_back(letra); //pega o char e joga ele pro fim da flia.
		}
	}
	
	//For pra comparar
	bool ehpalindromo = true;
	for (int i = 0, j = msglimpa.length() - 1; i < j; i++, j--) {
		char indo = msglimpa[i], voltando = msglimpa[j];
		
		if (indo != voltando) {;
			ehpalindromo = false;
			break;
		}
	
	}
	
	//teste pro resultado
	if (ehpalindromo == true) {
		cout << "É um palindromo ;D" << endl;
	} 
	else {
		cout << "Não é um palíndromo :(" << endl;
	}
}