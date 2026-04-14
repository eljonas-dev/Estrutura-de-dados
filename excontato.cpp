//João Victor dos Santos Pereira

#include <iostream>
#include <locale.h>
#include <ctime>
#include <string>

using namespace std;

class Data {
	private:
		int dia;
		int mes;
		int ano;
		public:
			Data(int dia, int mes, int ano) {
				
				this ->dia = dia;
				this ->mes = mes;
				this ->ano = ano;
			}
			Data() {
				this ->dia = 0;
				this ->mes = 0;
				this ->ano = 0;

			}
			//SET
			void setDia(int dia) {
				this ->dia = dia;
			}
			void setMes(int mes) {
				this ->mes = mes;
			}
			void setAno (int ano) {
				this ->ano = ano;
			}
			//GET
			int getDia() {
				return this ->dia;
			}
			int getMes() {
				return this ->mes;
			}
			int getAno() {
				return this ->ano;
			}
			
};

class Contato {

	private:
		string nome;
		string email;
		string telefone;
		Data dtnasc;
		public:
			Contato() {
				this -> nome = "";
				this -> email = "";
				this -> telefone = "";
			}
			Contato(string nome, string email, string telefone, Data dtnasc) {
				this ->nome = nome;
				this ->email = email;
				this ->telefone = telefone;
				this ->dtnasc = dtnasc;
			}
			//SET
			void setNome(string nome) {
				this ->nome = nome;
			}
			void setEmail(string email) {
				this ->email = email;
			}
			void setTelefone(string telefone) {
				this ->telefone = telefone;
			}
			void setDtnasc(Data dtnasc) {
				this ->dtnasc = dtnasc;
			}
			//GET
			string getNome() {
				return this ->nome;
			}
			string getEmail() {
				return this ->email;
			}
			string getTelefone() {
				return this ->telefone;
			}
			Data getDtnasc() {
				return this ->dtnasc;
			}
			int idade() {
				time_t t = time(0);
				
				tm* timePtr = localtime(&t);
				int anoAtual = timePtr->tm_year + 1900;
				
				int res = anoAtual - dtnasc.getAno();
				return res;
			}
};

int main(int argc, char** argv) {
	Contato lista[5];
	
	for (int i = 0; i < 5; i++) {

		string n, e, t;
		int d, m, a;
		
		cout << "Cadastrode Contato n. " << i + 1 << ": " << endl;
		
		cout << "Insira seu nome: " << endl;
		getline(cin >> ws, n);
		
		cout << "Insira seu E-mail: " << endl;
		cin >> e;
		
		cout << "Insira seu Telefone: " << endl;
		cin >> t;
		
		cout << "Insira a data de nascimento (dia mês ano): " << endl;
		cin >> d >> m >> a;
		
		//Pra limpar o buffer
		cin.ignore(1000, '\n');
		
		Data datat(d, m, a);
		
		lista[i].setNome(n);
		lista[i].setEmail(e);
		lista[i].setTelefone(t);
		lista[i].setDtnasc(datat);
		cout << "--------------------------------------------------------------------------" << endl;
 	}
 	
 	for (int i = 0; i < 5; i++) {
		 cout << "O nome do cadastro " << i + 1 << " é: " << lista[i].getNome() << endl;
		 cout << "O E-mail do cadastro " << i + 1 << "é: " << lista[i].getEmail() << endl;
		 cout << "O Telefone do cadastro " << i + 1 << "é: " << lista[i].getTelefone() << endl;
		 cout << "A data de nascimento do cadastro " << i + 1 << "é: " << lista[i].getDtnasc().getDia()
 	 	     << "/" << lista[i].getDtnasc().getMes()
		 	 << "/" << lista[i].getDtnasc().getAno() << endl;
		 cout << "A idade do cadastro " << i + 1 << "é: " << lista[i].idade() << endl;
		 cout << "--------------------------------------------------------------------------" << endl;
	 }
		
 	return 0;
}