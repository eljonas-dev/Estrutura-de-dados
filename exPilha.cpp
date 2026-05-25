#include <iostream>
#include <climits>

using namespace std;

struct No {
    int dado;
    No* proximo;
};

struct PilhaLista {
    No* topo;

    // Isso é um construtor. Ele garante que 'topo' comece como NULL
    PilhaLista() {
        topo = NULL;
    }

    bool vazia() {
        return topo == NULL;
    }

    void empilhar(int valor) {
        No* novoNo = new No();
        novoNo->dado = valor;
        novoNo->proximo = topo;
        topo = novoNo;
    }

    int desempilhar() {
        if (!vazia()) {
            No* temp = topo;
            int valor = temp->dado;
            topo = topo->proximo;
            delete temp;
            return valor;
        }
        return INT_MIN;
    }

    int espiarTopo() {
        if (!vazia()) {
            return topo->dado;
        }
        return INT_MIN;
    }
};

int main() {
    // Ao criar aqui, o construtor deixa o topo como NULL automaticamente
    PilhaLista pilhaPares;
    PilhaLista pilhaImpares;

    int numeroAtual;
    int numeroAnterior = INT_MIN;
    int contador = 0;

    cout << "=== IMPLEMENTACAO COM LISTA ENCADEADA ===\n";
    cout << "Digite 30 numeros inteiros em ordem estritamente crescente:\n";

    while (contador < 30) {
        cout << "Digite o " << contador + 1 << "o numero: ";
        cin >> numeroAtual;

        if (numeroAtual <= numeroAnterior) {
            cout << "[Erro] O numero deve ser MAIOR que o anterior (" << numeroAnterior << "). Tente novamente.\n";
            continue;
        }

        if (numeroAtual % 2 == 0) {
            pilhaPares.empilhar(numeroAtual);
        } else {
            pilhaImpares.empilhar(numeroAtual);
        }

        numeroAnterior = numeroAtual;
        contador++;
    }

    cout << "\n=== EXIBINDO EM ORDEM DECRESCENTE ===\n";

    while (!pilhaPares.vazia() || !pilhaImpares.vazia()) {
        int topoPar = pilhaPares.espiarTopo();
        int topoImpar = pilhaImpares.espiarTopo();

        if (topoPar > topoImpar) {
            cout << pilhaPares.desempilhar() << " (Par)" << endl;
        } else {
            cout << pilhaImpares.desempilhar() << " (Impar)" << endl;
        }
    }

    return 0;
}