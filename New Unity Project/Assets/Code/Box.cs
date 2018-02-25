using UnityEngine;
using System.Collections;

public class Box {

	public Face Front, Back, Up, Down, Right, Left;
	public GameObject GFront,GBack,GUp,GDown,GRight,GLeft;
	public Box(Face F,Face B,Face U, Face D,Face R, Face L, GameObject GF,GameObject GB,GameObject GU,GameObject GD,GameObject GR,GameObject GL) {
		GFront = GF;
		GBack = GB;
		GUp = GU;
		GDown = GD;
		GRight = GR;
		GLeft = GL;
		Front = F;
		Back = B;
		Up = U;
		Down = D;
		Right = R;
		Left = L;
	}
}
