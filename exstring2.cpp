/* 
1- guardar a frase em uma variável (usaremos getline pra capturar a frase completa) (FEITO)

2- Vamos colocar a frase num for que vai percorrer todos os caracteres até identificar um espaço (aqui usaremos if)
ele vai tentar reescrever a frase em outra variável, desta vez sem os espaços. (FEITO)

3-Então colocaremos tudo dentro de um mesmo loop, onde iremos comparar cada letra da variável só que em direções opostas. 
E então, comparando um por um, usaremos uma condição (booleana), onde toda vez que forem iguais,
 ele continua identando, enquanto que a partir do momento em que os caracteres não batem, 
 ele simplesmente quebra o ciclo e escreve que não é um palíndromo. 
 	 Atenção!!! Pra fazer o for percorrer o inverso da string ele deve usar a função texto.size() pra saber quantos caracteres possui 
	  e subtrair 1
 	 (pois se trata de um array)
 	 e então fazemos ele identar ao contrário com i-- (ele vai subtrair do total, ou seja, vai vir pra trás)
*/

#include <string>
#include <iostream>

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
	for (int i = 0; int j = msglimpa.length() - 1; i < j; i++; j--) {}
}

