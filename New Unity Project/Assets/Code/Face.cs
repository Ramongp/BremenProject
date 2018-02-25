using UnityEngine;
using System.Collections;

public class Face {

	// Use this for initialization
	public string symbol;
	public int orientation; //12,3,6,9
	public int localization; //0 Front,1 Back,2 Up,3 Down,4 Right, 5 Left
	public Texture image;
	public Face(string s,int o,Texture i, int l) {
		symbol = s;
		orientation = o;
		image = i;
		l = localization;
	}
	

}
