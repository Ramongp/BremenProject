using UnityEngine;
using System.Collections;

public class Grafo  {
	public Node[] nodos;
	public Arista[][] Aristas;

	Grafo(){
	nodos= new Node[5];
	Aristas = new Arista[5][];
	}


	public void AddNodo(Node n){
		nodos [n.cara]= n;
	}
	public void AddArco(Arista a){
		Aristas [a.origen.cara] [a.destino.cara] = a;
	}
}
