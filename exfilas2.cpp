#include <iostream>

using namespace std;

struct Node {
    int dado;
    Node* proximo;
};

struct Fila {
    Node* frente;
    Node* tras;
    int total;
};

struct Guiche {
    int id;
    Fila senhasAtendidas;
    Guiche* proximo;
};

void inicializarFila(Fila& f) {
    f.frente = NULL;
    f.tras = NULL;
    f.total = 0;
}

void enfileirar(Fila& f, int valor) {
    Node* novo = new Node;
    novo->dado = valor;
    novo->proximo = NULL;
    if (f.tras == NULL) {
        f.frente = novo;
        f.tras = novo;
    } else {
        f.tras->proximo = novo;
        f.tras = novo;
    }
    f.total++;
}

int desenfileirar(Fila& f) {
    if (f.frente == NULL) return -1;
    Node* temp = f.frente;
    int valor = temp->dado;
    f.frente = f.frente->proximo;
    if (f.frente == NULL) {
        f.tras = NULL;
    }
    delete temp;
    f.total--;
    return valor;
}

void inserirGuiche(Guiche*& lista, int id) {
    Guiche* novo = new Guiche;
    novo->id = id;
    inicializarFila(novo->senhasAtendidas);
    novo->proximo = NULL;

    if (lista == NULL) {
        lista = novo;
    } else {
        Guiche* atual = lista;
        while (atual->proximo != NULL) {
            atual = atual->proximo;
        }
        atual->proximo = novo;
    }
}

Guiche* buscarGuiche(Guiche* lista, int id) {
    Guiche* atual = lista;
    while (atual != NULL) {
        if (atual->id == id) return atual;
        atual = atual->proximo;
    }
    return NULL;
}

int contarGuiches(Guiche* lista) {
    int cont = 0;
    Guiche* atual = lista;
    while (atual != NULL) {
        cont++;
        atual = atual->proximo;
    }
    return cont;
}

void exibirFila(Fila f) {
    Node* atual = f.frente;
    while (atual != NULL) {
        cout << atual->dado << " ";
        atual = atual->proximo;
    }
    cout << endl;
}

int main() {
    Fila senhasGeradas;
    inicializarFila(senhasGeradas);
    
    Guiche* listaGuiches = NULL;
    int proximaSenha = 1;
    int totalAtendidasGeral = 0;
    int opcao;

    do {
        cout << "\n=========================================" << endl;
        cout << "Senhas aguardando atendimento: " << senhasGeradas.total << endl;
        cout << "Guiches abertos: " << contarGuiches(listaGuiches) << endl;
        cout << "=========================================" << endl;
        cout << "0. Sair" << endl;
        cout << "1. Gerar senha" << endl;
        cout << "2. Abrir guiche" << endl;
        cout << "3. Realizar atendimento" << endl;
        cout << "4. Listar senhas atendidas" << endl;
        cout << "Escolha uma opcao: ";
        cin >> opcao;

        if (opcao == 0) {
            if (senhasGeradas.total > 0) {
                cout << "Nao eh possivel encerrar. Ainda ha senhas aguardando atendimento." << endl;
                opcao = -1;
            } else {
                cout << "Total de senhas atendidas: " << totalAtendidasGeral << endl;
            }
        } 
        else if (opcao == 1) {
            enfileirar(senhasGeradas, proximaSenha);
            cout << "Senha " << proximaSenha << " gerada com sucesso!" << endl;
            proximaSenha++;
        } 
        else if (opcao == 2) {
            int id;
            cout << "Digite o ID do guiche: ";
            cin >> id;
            if (buscarGuiche(listaGuiches, id) != NULL) {
                cout << "Guiche com esse ID ja existe!" << endl;
            } else {
                inserirGuiche(listaGuiches, id);
                cout << "Guiche " << id << " aberto com sucesso!" << endl;
            }
        } 
        else if (opcao == 3) {
            if (senhasGeradas.total == 0) {
                cout << "Nao ha senhas aguardando atendimento." << endl;
            } else {
                int id;
                cout << "Digite o ID do guiche: ";
                cin >> id;
                Guiche* g = buscarGuiche(listaGuiches, id);
                if (g == NULL) {
                    cout << "Guiche nao encontrado!" << endl;
                } else {
                    int senhaChamada = desenfileirar(senhasGeradas);
                    enfileirar(g->senhasAtendidas, senhaChamada);
                    totalAtendidasGeral++;
                    cout << "Senha " << senhaChamada << " atendida no guiche " << id << "!" << endl;
                }
            }
        } 
        else if (opcao == 4) {
            int id;
            cout << "Digite o ID do guiche: ";
            cin >> id;
            Guiche* g = buscarGuiche(listaGuiches, id);
            if (g == NULL) {
                cout << "Guiche nao encontrado!" << endl;
            } else {
                cout << "Senhas atendidas pelo guiche " << id << ": ";
                if (g->senhasAtendidas.total == 0) {
                    cout << "Nenhuma senha atendida ainda.";
                } else {
                    exibirFila(g->senhasAtendidas);
                }
                cout << endl;
            }
        }
    } while (opcao != 0);

    return 0;
}