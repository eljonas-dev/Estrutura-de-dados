#include <iostream>
#include <locale.h>
using namespace std;
int fileira = 0;
int poltrona = 0;
float preco;
char mapa[15][40];
int lugar_ocupado = 0;
float bilheteria = 0;

float CalculaPreco(int a) {
	if (a > 0 && a <= 5) {
		preco = 50;
		return preco;
	}
	else if (a >= 6 && a <= 10) {
		preco = 30;
		return preco; 
	}
	else if (a >= 11 && a <= 15) {
		preco = 15;
		return preco;
	}
	else {
		cout << "valor inválido!" << endl;
		return 0;
	}
}

int main(int argc, char** argv) {
	setlocale(LC_ALL, "Portuguese_Brazil");
	int escolha;
	
	for (int i = 0; i < 15; i++){
		for (int j = 0; j < 40; j++) {
		mapa[i][j] = '.';
		}
		cout << endl;
	} 
	
	while (true) {
		cout << "0. Finalizar" << endl;
		cout << "1. Reservar poltrona" << endl;
		cout << "2. Mapa de ocupação" << endl;
		cout << "3. Faturamento" << endl;
		cout << "Insira o valor referente a ação que deseja realizar..." << endl;
		cin >> escolha;
		
		if (escolha == 0) {
			break;
		}
		else if (escolha == 1) {
			cout << "Selecione a fileira que deseja..." << endl;
			cin >> fileira;
			cout << "Agora selecione a poltrona que deseja..." << endl;
			cin >> poltrona;
			if (poltrona <= 0 || poltrona > 40) {
				cout << "Poltrona inexisente, favor tente novamente :)" << endl;
			}
			else if (mapa[fileira -1][poltrona -1] == '#') {
				cout << "Lugar está ocupado! Por favor tente novamente" << endl;
			}
			else {
				cout << "Reserva concluída com sucesso!" << endl;
				lugar_ocupado++;
				mapa[fileira -1][poltrona -1] = '#';
				bilheteria = bilheteria + CalculaPreco(fileira);
				cout << "O valor a ser pago é de..." << CalculaPreco(fileira) << endl;
			}
						
			
		}
		else if (escolha == 2) {
			for (int i = 0; i < 15; i++){
				for (int j = 0; j < 40; j++) {
					cout << mapa[i][j] << " ";
				}
				cout << endl;
			}
		}
		else if (escolha == 3) {
			cout << "O número de lugares ocupados é de " << lugar_ocupado << endl;
			cout << "O faturamento da bilheteria é de " << bilheteria << endl;
		}
		
		for (int x = 0; x < 6; x++) {
 	   		cout << "." << endl;
		}
		
	}
	return 0;
	

}