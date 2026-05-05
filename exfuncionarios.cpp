#include <iostream>
#include <vector>
#include <string>
#include <iomanip>

using namespace std;

struct Funcionario {
    int prontuario;
    string nome;
    double salario;
};

void incluir(vector<Funcionario> &lista) {
    Funcionario novo;
    cout << "\nDigite o prontuario: ";
    cin >> novo.prontuario;

    for (size_t i = 0; i < lista.size(); i++) {
        if (lista[i].prontuario == novo.prontuario) {
            cout << "Erro: Este prontuario ja esta cadastrado!" << endl;
            return;
        }
    }

    cout << "Digite o nome: ";
    cin.ignore();
    getline(cin, novo.nome);
    cout << "Digite o salario: ";
    cin >> novo.salario;

    lista.push_back(novo);
    cout << "Funcionario incluido com sucesso!" << endl;
}

void excluir(vector<Funcionario> &lista) {
    int pront;
    cout << "\nDigite o prontuario para excluir: ";
    cin >> pront;

    for (vector<Funcionario>::iterator it = lista.begin(); it != lista.end(); ++it) {
        if (it->prontuario == pront) {
            lista.erase(it);
            cout << "Funcionario removido!" << endl;
            return;
        }
    }
    cout << "Funcionario nao encontrado." << endl;
}

void pesquisar(const vector<Funcionario> &lista) {
    int pront;
    cout << "\nDigite o prontuario para pesquisa: ";
    cin >> pront;

    for (size_t i = 0; i < lista.size(); i++) {
        if (lista[i].prontuario == pront) {
            cout << "\n--- Dados do Funcionario ---" << endl;
            cout << "Nome: " << lista[i].nome << endl;
            cout << "Salario: R$ " << fixed << setprecision(2) << lista[i].salario << endl;
            return;
        }
    }
    cout << "Funcionario nao encontrado." << endl;
}

void listar(const vector<Funcionario> &lista) {
    double totalSalarios = 0;
    
    if (lista.empty()) {
        cout << "\nLista vazia." << endl;
        return;
    }

    cout << "\n--- Listagem de Funcionarios ---" << endl;
    for (size_t i = 0; i < lista.size(); i++) {
        cout << "Prontuario: " << lista[i].prontuario 
             << " | Nome: " << lista[i].nome 
             << " | Salario: R$ " << fixed << setprecision(2) << lista[i].salario << endl;
        totalSalarios += lista[i].salario;
    }
    
    cout << "--------------------------------" << endl;
    cout << "Total da folha salarial: R$ " << totalSalarios << endl;
}

int main() {
    vector<Funcionario> lista;
    int opcao;

    do {
        cout << "\n--- MENU DE ADMINISTRACAO ---" << endl;
        cout << "0. Sair" << endl;
        cout << "1. Incluir" << endl;
        cout << "2. Excluir" << endl;
        cout << "3. Pesquisar" << endl;
        cout << "4. Listar" << endl;
        cout << "Escolha uma opcao: ";
        cin >> opcao;

        switch (opcao) {
            case 1: incluir(lista); break;
            case 2: excluir(lista); break;
            case 3: pesquisar(lista); break;
            case 4: listar(lista); break;
            case 0: break;
            default: cout << "Opcao invalida!" << endl;
        }
    } while (opcao != 0);

    return 0;
}