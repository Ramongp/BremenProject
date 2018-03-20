using UnityEngine;
using System.Collections;

public class SameCube : MonoBehaviour {

	// Use this for initialization
	string[,] Forient,Rorient,Uorient,Rot;
	Face[] SidesO,SidesF;
	Face Fo,Uo,Ro,Ff,Uf,Rf,S1,S2,S3,T1,T2,T3,So1,So2,So3; //Caras originales, finales, repetidas (sides) y no repetidas (tapadas)
	string[] movs; //Conjunto de strings de movimientos de cada cara
	string S1mov,S2mov,S3mov;
	int SameSymbols,DiffSymbols;
	Box FakeBox;
	public static bool IsSameCube;
	int[,] OrientconRot;
	void Start () { //Tablas con cambios de orientaciones
		
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	public void Compare(Face F1,Face U1,Face R1,Face F2,Face U2,Face R2)
	{	
		Fo = F1; Uo = U1; Ro = R1; Ff = F2; Uf = U2; Rf = R2;
		SidesO =new Face[]{Fo,Uo,Ro};
		SidesF =new Face[]{Ff,Uf,Rf};
		movs = new string[3];
		Forient= new string[4,4] {{" ","Toward-up-right,","Toward-up-right,Toward-up-right,","Toward-up-left,"},{"Toward-up-left,"," ","Toward-up-right,","Toward-up-right,Toward-up-right,"},{"Toward-up-left,Toward-up-left,","Toward-up-left,"," ","Toward-up-right,"},{"Toward-up-right,","Toward-up-left,Toward-up-left","Toward-up-left,"," "}};
		Uorient= new string[4,4] {{" ","Left,","Left,Left,","Right,"},{"Right,"," ","Left,","Left,Left,"},{"Right,Right,","Right,"," ","Left,"},{"Left,","Right,Right,","Right,"," "}};
		Rorient= new string[4,4] {{" ","Up,","Up,Up,","Down,"},{"Down,"," ","Up,","Up,Up,"},{"Down,Down,","Down,"," ","Up,"},{"Up,","Down,Down,","Down,"," "}};
		Rot = new string[3, 3]{ { " ", "Up,", "Right," }, { "Down,", " ", "Toward-up-right," }, { "Left,", "Toward-up-left,", " " } };
		OrientconRot = new int[3, 3]{ {0, 0, 0 }, { 0, 0, -1 }, {0, 1, 0 } };// Está al
		FakeBox =new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("U", 0,2),new Face ("D", 0,3), new Face ("R", 0,4), new Face ("L", 0,5));

		FindSides ();

	}

	 void FindSides()
	{
		SameSymbols = 0;
		DiffSymbols = 0;
		for(int i=0;i<SidesF.Length;i++)
		{
			bool congruent=false;
			for (int u = 0; u < SidesF.Length; u++) 
			{
				if (SidesO [i].symbol.Equals (SidesF [u].symbol)) {

					congruent = true;
					Debug.Log ("Se repite "+SidesO[i].symbol);
					switch (SameSymbols) {
					case 0:
						S1 = SidesF [u];
						So1 = SidesO [i];
						break;
					case 1:
						S2 = SidesF [u];
						So2 = SidesO [i];
						break;
					case 2:
						S3 = SidesF [u];
						So3 = SidesO [i];
						break;
					}
					CreateMov (i, u, SameSymbols);
					SameSymbols++;

					
				}
			}
			if (!congruent) {
				Debug.Log ("No se repite "+SidesO[i].symbol);
				switch (DiffSymbols) {
				case 0:
					T1 = SidesO [i];
					break;
				case 1:
					T2 = SidesO [i];
					break;
				case 2:
					T3 = SidesO [i];
					break;
				}
				DiffSymbols++;
			}
		}

		switch (SameSymbols) {
		case 0:
			Debug.Log ("Ninguna cara es la misma");
			Debug.Log ("Mismo cubo");
			IsSameCube = true;
			break;
		case 1:
			Debug.Log ("Se repite solo el simbolo " + S1.symbol);
			if ((!string.IsNullOrEmpty(S1mov))&&
				(NoVisible2 (T1,T2))) {
				Debug.Log ("Mismo cubo");
				IsSameCube = true;
			} else {
				Debug.Log ("Cubo diferente");
				IsSameCube = false;
			}
			Debug.Log (T1.symbol + " acaba en " + TradLocaton (T1.localization) + " y " + T2.symbol + " acaba en " + TradLocaton (T2.localization));
			break;
		case 2:
			Debug.Log ("Se repiten dos simbolos");
			if ((!string.IsNullOrEmpty(S1mov))&&
				(!string.IsNullOrEmpty(S2mov))&&
				(Compare2Sides ()) && (NoVisible (T1))) {
				Debug.Log ("Mismo cubo");
				IsSameCube = true;
			} 
			else {
				Debug.Log ("Cubo diferente");
				IsSameCube = false;
			}
			break;
		case 3:
			Debug.Log ("Se repiten tres simbolos");
			if ((!string.IsNullOrEmpty(S1mov))&&
				(!string.IsNullOrEmpty(S2mov))&&
				(!string.IsNullOrEmpty(S3mov))&&(Compare3Sides ())) {
				Debug.Log ("Mismo cubo");
				IsSameCube = true;
			} 
			else {
				Debug.Log ("Cubo diferente");
				IsSameCube = false;
			}
			break;

		}
		//Debug.Log ("F " + Fmov + " U " + Umov + " R " + Rmov);
	}

	public void CreateMov(int i,int u,int s)
	{
		Face So = SidesO [i];
		Face Sf = SidesF [u];
		string mov = movs [i];
		Debug.Log ("Orient: " +So.symbol+ " So " +So.orientation + " Sf "+Sf.orientation);
		Sf.orientation+= OrientconRot [i, u];
		switch (Sf.orientation) 
		{
		case 4: 
			Sf.orientation = 0;
			break;
		case -1:
			Sf.orientation = 3;
			break;
		}
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
		Debug.Log ("Local: "+So.symbol+" So "+TradLocaton( So.localization )+ " Sf "+TradLocaton( Sf.localization));
		if (!So.localization.Equals (Sf.localization)) {
			mov += Rot [i, u];
		}
		Debug.Log (So.symbol+" " + mov);
		switch (s) {
		case 0:
			S1mov = mov;
			break;
		case 1:
			S2mov = mov;
			break;
		case 2:
			S3mov = mov;
			break;
		}
	
	
	}
		


		public Face MoveFace (string mov, Face f)
	{
		string tempmov = mov;
		string[] listmov = tempmov.Split (',');
		for (int i = 0; i < listmov.Length; i++) {
			switch (listmov [i]) {
			case "Up":
				f.MoveFUp ();
				break;
			case "Down":
				f.MoveFDown();
				break;
			case "Left":
				f.MoveFLeft();
				break;
			case "Right":
				f.MoveFRight();
				break;
			case "Toward-up-right":
				f.MoveFUpRight();
				break;
			case "Toward-up-left":
				f.MoveFUpLeft();
				break;
			}
		}
		return f;
	}

	string TradLocaton(int l)
	{
		switch (l)
		{
		case 0:
			return "Front";
		case 1:
			return "Back";
		case 2:
			return "Up";
		case 3:
			return "Down";
		case 4:
			return "Right";
		case 5:
			return "Left";
		}
		return " ";
	}

	public bool NoVisible(Face f) //Miramos si se deshace de sus vecinos
	{
		f= MoveFace(S1mov,f);
		Debug.Log ("tras moveFace "+f.symbol + " acaba en " + TradLocaton( f.localization));
		if ((f.localization.Equals (1)) || (f.localization.Equals (3)) || (f.localization.Equals (5))) {
			return true;
		} 
		else {
			return false;
		}
	}

	public bool NoVisible2(Face f1, Face f2) //Miramos si se deshace de sus vecinos
	{
		f1 =MoveFace(S1mov,f1);
		f2 =MoveFace (S1mov, f2);
		if (((f1.localization.Equals (1)) || (f1.localization.Equals (3)) || (f1.localization.Equals (5)))&&((f2.localization.Equals (1)) || (f2.localization.Equals (3)) || (f2.localization.Equals (5)))) {
			return true;
		} 
		else {
			return false;
		}
	}

	public bool Compare2Sides () // Miramos si ambas caras han hacho caminos equivalentes
	{
		Face F1=So1,F2=So2;
		Debug.Log ("Antes de mover " + F1.symbol + " en " + TradLocaton( F1.localization) +" "+ F2.symbol + " en " +TradLocaton( F2.localization));
		F1= MoveFace (S2mov, F1);
		F2=MoveFace (S1mov, F2);

		Debug.Log (" Tras mover con R=2 " + F1.symbol + " habría hecho "+S2mov+" Habria acabado en " + TradLocaton( F1.localization)+ " como "+TradLocaton(S1.localization) + " y " + F2.symbol + " habría hecho "+S1mov+ " habria acabado en " +TradLocaton( F2.localization)+ " como "+TradLocaton(S2.localization ));
		if (((F1.localization ==S1.localization))&&((F2.localization == S2.localization))) {
			Debug.Log ("Acaban en el mismo sitio");
			return true;
		} 
		else {
			Debug.Log ("Acaban en sitios diferentes");
			return false;
		}
	}

	public bool Compare3Sides () // Miramos si ambas caras han hacho caminos equivalentes
	{
		Face F1 = So1, F2 = So2, F3 =So3;
		F1= MoveFace (S2mov, F1);
		F2= MoveFace (S1mov, F2);
		F3 =MoveFace (S1mov, F3);

		if (((F1.localization.Equals (S1.localization)))&&((F2.localization.Equals (S2.localization)) )
			&&((F3.localization.Equals (S3.localization)))) {
			return true;
		} 
		else {
			return false;
		}
	}

	int listpos(int n)
	{
		switch (n) {
		case 0:
			return 0;
		case 2:
			return 1;
		case 4:
			return 2;
		}
		return 0;
	}
}
