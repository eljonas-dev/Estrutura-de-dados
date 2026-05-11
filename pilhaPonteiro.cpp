#include <iostream>
using namespace std;

struct No { //É um nó
	float dado;
	struct No *ant; //ponteiro do nó anterior a esse
};

struct Pilha { //é um  empilhamento de nós
	No *topo;
	//O topo aponta para os que estão abaixo também!
	
};

Pilha* init() {
	Pilha *p = new Pilha;
	p->topo = NULL;
	return p;
};

int isEmpty(Pilha *p) {
	return (p->topo == NULL);
}

int count(Pilha *p) {
	int i = 0;
	No *no; //Um nó auxiliar, pra nao perdermos o topo de vista em nenhum momento. "Parãmetros são passados por referencia.
	no = p->topo;
	while (no != NULL) {
		i++;
		no = no->ant;
		return i;
	}
}

int print(Pilha *p) {
	No *no; //Um nó auxiliar, pra nao perdermos o topo de vista em nenhum momento. "Parãmetros são passados por referencia.
	no = p->topo;
	while (no != NULL) {
		
		
		return i;
	}
}

void push(Pilha *p, float v) {
	No *no = new No;
	no ->dado = v;
	no->ant = p->topo;
	p->topo = no; // novo topo é o endereço do no
	//A cada novo nó criado, o topo também atualiza!
}

push(pilhaFloat, 10)

print(pilhaFloat);
push(pilhaFloat, 15);


//O tipo da pilha é sempre o tipo do elemento que ele vai retornar né cara
float pop(Pilha *p) {
	float v = -1;
	int podeDesempilhar = (! isEmpty(p));
	if (podeDesempilhar) {
		No *no = p->topo;
		v = no->dado;
		p->topo = no->ant; //O novo topo é o nó anterior (pq vc removeu o de cima né)
		free(no);
	}
	return v;
 }
 
 void freePilha(pPilha *p) {
	 //Vai pegar todos os elementos e remover da memória começando pelo topo (o último). Sua estrutura de repetição se assekelha ao count;
	 No *no;
	 no = p->topo;
	 while (no != NULL) {
	 	No *temp = no->ant; //variavel auxiliar do nó
	 	free(no);
		 no = temp;
		 //tem que guardar o endereço anterior pra destruir o próximo
		 
	 }
	 free(p);
	 
 }

int main (int argc, char** argv){
	Pilha *pilhaFloat;
	pilhaFloat = init();
	
	cout << "Pilha vazia: "
		 << (isEmpty(pilha)?"Sim":"Nao")
		 << endl;
	print(pilhaFloat);
		 
	return 0;
}

//No ponteiro não precisamos de uma restrição que me impeça de fazer o empilhamento!!!

/*Em nenhuma das soluções ele sou uma busca (find);

essa versão é c ponteiros, ao invés de usar um array, o que dá mais liberdade, por não se limitar ao tamanho dedicado ao array na memória.
*/