//João Victor dos Santos, Luis Tiago Ferreira.

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <cctype>

using namespace std;

int main() {
    ifstream arq("nomes.txt");
    string linha;

    while (getline(arq, linha)) {

        vector<string> partes;
        string atual = "";

        for (int i = 0; i < linha.length(); i++) {
            if (linha[i] != ' ') {
                atual.push_back(linha[i]);
            } else {
                partes.push_back(atual);
                atual = "";
            }
        }
        partes.push_back(atual);

        int n = partes.size();

        for (int i = 0; i < partes[n - 1].length(); i++) {
            cout << (char)toupper(partes[n - 1][i]);
        }

        cout << ", ";

        cout << partes[0] << " ";

        for (int i = 1; i < n - 1; i++) {
            cout << (char)toupper(partes[i][0]) << ".";
            if (i < n - 2) cout << " ";
        }

        cout << endl;
    }

    arq.close();
    return 0;
}