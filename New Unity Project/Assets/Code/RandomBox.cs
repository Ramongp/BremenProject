using UnityEngine;
using System.Collections;

public class RandomBox {

	public Box b;
	public int[] change ,moves;
	public RandomBox (Box B, int[] Moves,int[] Change)
	{
		b = B;
		change = Change;
		moves = Moves;
	}
}
