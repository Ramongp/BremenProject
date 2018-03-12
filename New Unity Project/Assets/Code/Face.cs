using UnityEngine;
using System.Collections;

public class Face {

	// Use this for initialization
	public string symbol;
	public int orientation; //0,1,2,3
	public int localization; //0 Front,1 Back,2 Up,3 Down,4 Right, 5 Left
	public Face(string s,int o, int l) {
		symbol = s;
		orientation = o;
		localization=l;
	}
	

}
