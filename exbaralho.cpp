#include <iostream>
#include <string>
#include <algorithm>
#include <ctime>
#include <vector>

using namespace std;
string carta = "";
int main (int argc, char** argv) {
	setlocale(LC_ALL, "Portuguese_Brazil");
	vector<string> baralho;
	for (int b = 1; b < 3; b++) {
		for (int n = 1; n < 5; n++) {
			for (int v = 1; v < 14; v++){
				carta = (to_string(n) + "-" + to_string(v) + "-" + to_string(b));
				baralho.push_back(carta); //adiciona no fim da listaa;
			}
		}
	}
	srand(time(0));
	/*
	srand se trata da semente que é usada pro cálculo do sorteio. Se definirmos uma semente fixa, ele vai sempre gerar os mesmos valores.
	Porém o que estamos fazendo aqui é usar o tempo que o próprio computador conta em segundos, de maneira contínua!
	Como o tempo nunca para ou volta, então a capacidade de geração randômica dele é infinita!
	*/
	random_shuffle(baralho.begin(), baralho.end());

	string mao[4][11];
	int contador = 0;
	for (int j = 0; j < 4; j++) {
		for (int c = 0; c < 11; c++) {
			mao[j][c] = baralho[contador];
			contador++;
		}
	}
	
	for (int j = 0; j< 4; j++) {
		
		cout << "----------------------------------" << endl;
		cout << "MÃO DO JOGADOR " << (j + 1) << " :" << endl;
		for (int c = 0; c < 11; c++) {
			cout << mao[j][c] << endl;
		}
	}
	
	
}

