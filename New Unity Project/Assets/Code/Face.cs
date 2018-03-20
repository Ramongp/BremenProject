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
	
	public void MoveFUp()
	{
		switch (localization) {
		case 0:
			localization = 2;
			break;
		case 1:
			localization = 3;
			break;
		case 2:
			localization = 1;
			break;
		case 3:
			localization = 0;
			break;
		}
	}
	public void MoveFDown()
	{		
		switch (localization) {
		case 0:
			localization = 3;
			break;
		case 1:
			localization = 2;
			break;
		case 2:
			localization = 0;
			break;
		case 3:
			localization = 1;
			break;
		}

	}
	public void MoveFRight()
	{
		switch (localization) {
		case 0:
			localization = 4;
			break;
		case 5:
			localization = 0;
			break;
		case 1:
			localization = 5;
			break;
		case 4:
			localization = 1;
			break;
		}
	}
	public void MoveFLeft()
	{
		switch (localization) {
		case 0:
			localization = 5;
			break;
		case 5:
			localization = 1;
			break;
		case 1:
			localization = 4;
			break;
		case 4:
			localization = 0;
			break;
		}
	}
	public void MoveFUpRight()
	{
		switch (localization) {
		case 2:
			localization = 4;
			break;
		case 3:
			localization = 5;
			break;
		case 4:
			localization = 3;
			break;
		case 5:
			localization = 2;
			break;
		}

	}
	public void MoveFUpLeft()
	{
		switch (localization) {
		case 2:
			localization = 5;
			break;
		case 3:
			localization = 4;
			break;
		case 4:
			localization = 2;
			break;
		case 5:
			localization = 3;
			break;
		}
	}
}
