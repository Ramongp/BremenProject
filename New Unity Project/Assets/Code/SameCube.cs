using UnityEngine;
using System.Collections;

public class SameCube : MonoBehaviour {

	// Use this for initialization
	string[,] Forient,Rorient,Uorient,Rot;
	Face[] SidesO,SidesF;
	Face Fo,Uo,Ro,Ff,Uf,Rf; //Caras originales y finales
	string[] movs; //Conjunto de strings de movimientos de cada cara
	string Fmov,Umov,Rmov;
	void Start () { //Tablas con cambios de orientaciones
		

	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	public void Compare(Face F1,Face U1,Face R1,Face F2,Face U2,Face R2)
	{	Fmov = " ";Umov=" ";Rmov=" ";
		Fo = F1; Uo = U1; Ro = R1; Ff = F2; Uf = U2; Rf = R2;
		SidesO =new Face[]{Fo,Uo,Ro};
		SidesF =new Face[]{Ff,Uf,Rf};
		Forient= new string[4,4] {{" ","TUR,","TUR,TUR,","TUL,"},{"TUL"," ","TUR,","TUR,TUR,"},{"TUL,TUL,","TUL,"," ","TUR,"},{"TUR,","TUL,TUL","TUL,"," "}};
		Uorient= new string[4,4] {{" ","LEFT,","LEFT,LEFT,","RIGHT,"},{"RIGHT,"," ","LEFT,","LEFT,LEFT,"},{"RIGHT,RIGHT,","RIGHT,"," ","LEFT,"},{"LEFT,","RIGHT,RIGHT,","RIGHT,"," "}};
		Rorient= new string[4,4] {{" ","UP,","UP,UP,","DOWN,"},{"DOWN"," ","UP,","UP,UP,"},{"DOWN,DOWN,","DOWN,"," ","UP,"},{"UP,","DOWN,DOWN,","DOWN,"," "}};
		Rot = new string[3, 3]{ { " ", "UP", "RIGHT" }, { "DOWN", " ", "TUR" }, { "LEFT", "TUL", " " } };

		FindSides ();
	}

	public void FindSides()
	{
		movs = new string[]{ Fmov, Umov, Rmov };
		for(int i=0;i<SidesF.Length;i++)
		{
			for (int u = 0; u < SidesF.Length; u++) 
			{
				if (SidesO [i].symbol.Equals (SidesF [u].symbol)) {
					Debug.Log ("Se repite "+SidesO[i].symbol);
					Debug.Log(SidesO[i].symbol+" "+ CreateMov (i,u));
					
				}
			}
		}
		//Debug.Log ("F " + Fmov + " U " + Umov + " R " + Rmov);
	}

	public string CreateMov(int i,int u)
	{
		Face So = SidesO [i];
		Face Sf = SidesF [u];
		string mov = movs [i];
		Debug.Log ("Orient: " +So.symbol+ " So " +So.orientation + " Sf "+Sf.orientation);
		if (!So.orientation.Equals(Sf.orientation)) {
			
			switch (i) {
			case 0:
				mov += Forient [So.orientation, Sf.orientation];
				break;
			case 1:
				mov += Uorient [So.orientation, Sf.orientation];
				break;
			case 2:
				mov += Rorient [So.orientation, Sf.orientation];
				break;
			}
		}
		Debug.Log ("Local: "+So.symbol+" So "+So.localization + " Sf "+Sf.localization);
		if (!So.localization.Equals (Sf.localization)) {
			mov += Rot [i, u];
		}
		return  mov;
	}

}
