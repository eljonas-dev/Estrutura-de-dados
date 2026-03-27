#include <iostream>
#include <string>
#include <fstream>

using namespace std;
/*find_last_of(' ') -----> pra poder localizar o último espaço do registro.
esse "método", percorre os índices da string do final pro começo buscando algum dígito, no caso usaremos ele
pra buscar um char vazio. Vai retornar a posição dele!

string::npos ---> é o nome do que ele retorna quando nao encontra espaço! (usaremos isso numa condição)

substr() pra cortar a string em duas partes a partir de um determinado índice

usaremos size_t na hora de declarar o tamanho, ao invés de int, pq assim o tal do c++ sabe
que só haverá valores positivos. (uma frase nao pode ter -5 letras né mano)

1- primeiro vamos fazer a lógica de corte
	se ele encontrar um espaço (posicao != string::npos), vai rodar o tal do corte
2- depois vamos focar na questão da persistência.
*/




int main (int argc, char** argv) {
	string nomecompleto = "";
	cout << "Insira o nome completo:  " << endl;
	getline(cin, nomecompleto);
	
	return 0;
	// meu nome é jonas e eu sou um bananão :p
}