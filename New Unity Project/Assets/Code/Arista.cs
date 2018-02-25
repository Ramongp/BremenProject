using UnityEngine;
using System.Collections;

public class Arista {
	public int coste;
	public Node origen;
	public Node destino;

	Arista(int c,Node o,Node d){
		coste = c;
		origen = o;
		destino = d;
	}
}
