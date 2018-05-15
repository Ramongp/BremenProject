using UnityEngine;
using System.Collections;

public class SameCube : MonoBehaviour {

	// Use this for initialization
	public string[,] Forient,Rorient,Uorient,Rot, NoOrientR3,NoOrientR1;
	Face[] SidesO,SidesF;
	Face Fo,Uo,Ro,Ff,Uf,Rf,S1,S2,S3,T1,T2,So1,So2,So3; //Caras originales, finales, repetidas (sides) y no repetidas (tapadas)
	public string[] movs; //Conjunto de strings de movimientos de cada cara
	string S1mov,S2mov,S3mov;
	public static Face Fx, Tquest;
	public static int SameSymbols,DiffSymbols;
	public static bool IsSameCube;
	int[,] OrientconRot;
	public static string Way;
	void Awake () { //Tablas con cambios de orientaciones

		movs = new string[3];
		/*Forient= new string[4,4] {{" ","Toward-up-right,","Toward-up-right,Toward-up-right,","Toward-up-left,"},{"Toward-up-left,"," ","Toward-up-right,","Toward-up-right,Toward-up-right,"},{"Toward-up-left,Toward-up-left,","Toward-up-left,"," ","Toward-up-right,"},{"Toward-up-right,","Toward-up-left,Toward-up-left","Toward-up-left,"," "}};
		Uorient= new string[4,4] {{" ","Left,","Left,Left,","Right,"},{"Right,"," ","Left,","Left,Left,"},{"Right,Right,","Right,"," ","Left,"},{"Left,","Right,Right,","Right,"," "}};
		Rorient= new string[4,4] {{" ","Up,","Up,Up,","Down,"},{"Down,"," ","Up,","Up,Up,"},{"Down,Down,","Down,"," ","Up,"},{"Up,","Down,Down,","Down,"," "}};
		Rot = new string[3,3]{ { " ","Up,","Right," },{ "Down,"," ","Toward-up-right," },{ "Left,","Toward-up-left,"," " } };
		NoOrientR3 = new string[3,3] { {"","Toward-up-right,Up,","Toward-up-left,Right,"},{"Right,Down,","","Left,Toward-up-right,"},{"Up,Left","Right,Down",""}}; //Table with movements for R=3
		NoOrientR1 = new string[3,3] { {"Toward-up-right,Toward-up-right,","Left,","Down,"},{"Toward-up-left,","Left,Left,","Up,"},{"Toward-up-right,","Right,","Up,Up,"}}; //Table with movements for R=3*/


		//Separador _
		Forient= new string[4,4] {{" ","Toward-up-right_","Toward-up-right_Toward-up-right_","Toward-up-left_"},{"Toward-up-left_"," ","Toward-up-right_","Toward-up-right_Toward-up-right_"},{"Toward-up-left_Toward-up-left_","Toward-up-left_"," ","Toward-up-right_"},{"Toward-up-right_","Toward-up-left_Toward-up-left","Toward-up-left_"," "}};
		Uorient= new string[4,4] {{" ","Left_","Left_Left_","Right_"},{"Right_"," ","Left_","Left_Left_"},{"Right_Right_","Right_"," ","Left_"},{"Left_","Right_Right_","Right_"," "}};
		Rorient= new string[4,4] {{" ","Up_","Up_Up_","Down_"},{"Down_"," ","Up_","Up_Up_"},{"Down_Down_","Down_"," ","Up_"},{"Up_","Down_Down_","Down_"," "}};
		Rot = new string[3,3]{ { " ","Up_","Right_" },{ "Down_"," ","Toward-up-right_" },{ "Left_","Toward-up-left_"," " } };
		NoOrientR3 = new string[3,3] { {"","Toward-up-right_Up_","Toward-up-left_Right_"},{"Right_Down_","","Left_Toward-up-right_"},{"Up_Left","Right_Down",""}}; //Table with movements for R=3
		NoOrientR1 = new string[3,3] { {"Toward-up-right_Toward-up-right_","Left_","Down_"},{"Toward-up-left_","Left_Left_","Up_"},{"Toward-up-right_","Right_","Up_Up_"}}; //Table with movements for R=3


		OrientconRot = new int[3,3]{ {0,0,0 },{ 0,0,-1 },{0,1,0 } };// Está al

	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	public void Compare(Face F1,Face U1,Face R1,Face F2,Face U2,Face R2)
	{	
		
		Fo = F1; Uo = U1; Ro = R1; Ff = F2; Uf = U2; Rf = R2;
		SidesO =new Face[]{Fo,Uo,Ro};
		SidesF =new Face[]{Ff,Uf,Rf};



		FindSides ();

	}

	public string  LockTest(Face F1, Face F2, Face F3)
	{
		Fo=F1;
		Uo=F2;
		Ro=F3;
		int Slock=0;
		SidesO =new Face[]{Fo,Uo,Ro};
		for(int i=0;i<SidesO.Length;i++)
		{ if (SidesO [i].symbol.Equals ("Lock"))
				Slock = i;
			}
	

		SidesF = new Face[] {new Face("Lock", 0, 0)};
		Debug.Log ("Slock " + Slock.ToString ()+ " movs length " + movs.Length.ToString());
		CreateMov (Slock, 0, 0);
	
		Way = S1mov;
			return S1mov;

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
				}
				DiffSymbols++;
			}
		}
		Way = S1mov;
		Fx= new Face (" ",0,0);
		Tquest= new Face (" ",0,0);
		switch (SameSymbols) {
		case 0:
			Debug.Log ("Ninguna cara es la misma");
			Debug.Log ("Mismo cubo");
			Fx.explicacion = "NoFaceSame";
			IsSameCube = true;
			break;
		case 1:
			Face T1f=new Face(T1.symbol,T1.orientation, T1.localization),T2f=new Face(T2.symbol,T2.orientation, T2.localization);
			Debug.Log ("Se repite solo el simbolo " + S1.symbol + "por ahora T1 " + TradLocaton(T1.localization));
			if ((!string.IsNullOrEmpty(S1mov))&&
				(NoVisible2 (T1f,T2f))
				||(S1.orientation.Equals(4))) {

				Tquest.symbol = T1.symbol;Tquest.orientation = T1.localization;Tquest.localization = T1f.localization; //Localization original and final of the T side

				if (S1.orientation.Equals (4)) {
					string temp = NoOrientR1 [listpos(S1.localization),listpos( So1.localization)];
					temp += S1mov;
					Way = temp;
					T1f = new Face (T1.symbol, T1.orientation, T1.localization);
					MoveFace (Way, T1f);
					Tquest.symbol = T1.symbol;Tquest.orientation = T1.localization;Tquest.localization = T1f.localization; //Localization original and final of the T side
				}

				Debug.Log ("Mismo cubo");
				Fx.explicacion = "FollowPath";

				IsSameCube = true;
			} else {
				if (string.IsNullOrEmpty (S1mov))
					Fx = So1;
				Debug.Log ("Cubo diferente");
				IsSameCube = false;
			}
			Debug.Log (T1.symbol + " acaba en " + TradLocaton (T1.localization) + " y " + T2.symbol + " acaba en " + TradLocaton (T2.localization));
			break;
		case 2:
			Debug.Log ("Se repiten dos simbolos");
			if ((string.IsNullOrEmpty (S1mov)) &&
			    (!S1.orientation.Equals (4))) {
				Fx = So1;
				Debug.Log ("Cubo diferente");
				IsSameCube = false;
				break;
			}
			if ((string.IsNullOrEmpty (S2mov)) &&
				(!S2.orientation.Equals (4))) {
				Fx = So2;
				Debug.Log ("Cubo diferente");
				IsSameCube = false;
				break;
			}
				if((S1.orientation.Equals(4))&&(S2.orientation.Equals(4)))
					{

				if (S2.localization.Equals (So1.localization)) {
					switch (So1.localization) {
					case 0: 
						S1mov = Forient [0, 2] + S1mov;
						break;
					case 2:
						S1mov = Uorient [0, 2] + S1mov;
						break;
					default:
						S1mov = Rorient [0, 2] + S1mov;
						break;
					}
				}
				else{
				bool BeforeTranslation = false;
				string[,] Table;
				switch (So1.localization) {
				case 0: 
					Table = Forient;
					break;
				case 2:
					Table = Uorient;
					break;
				default:
					Table = Rorient;
					break;
				}
					for (int i = 0; i < Table.GetLength(1); i++) {
					if (Table [listpos (S1.localization), i].Equals (S2mov)) {
						BeforeTranslation = true;
					}
				}
				if (BeforeTranslation) {
					S1mov = S2mov + S1mov;
				} else {
					S1mov = S1mov+S2mov;
				}
				}
				Face T1f2R =new Face (T1.symbol,T1.orientation,T1.localization);
				if (NoVisible (T1f2R)) {
					Debug.Log ("Mismo cubo");
					Fx.explicacion = "FollowPath";
					Way = S1mov;
					Tquest.symbol = T1.symbol;Tquest.orientation = T1.localization;Tquest.localization = T1f2R.localization; //Localization original and final of the T side
					IsSameCube = true;
					break;
				} else {
					Fx = T1;
					Debug.Log ("Cubo diferente");
					IsSameCube = false;
					break;
				}
					
				}
				if (S2.orientation.Equals (4)) {
					string tempmovS2 = S1mov;
				string[] listmovS2 = tempmovS2.Split ('_');
				S2mov = "_"; 																			// Para evitar error null revisar
					if ((!(So1.orientation.Equals (S1.orientation))&&(listmovS2.Length>2)) ||(So1.orientation.Equals(S1.orientation))&&!(So1.localization.Equals(S1.localization))) {
						string temp = listmovS2[listmovS2.Length-2];
					temp += "_"+S2mov;
						S2mov = temp;
						Debug.Log ("Camino creado para S2 " + S2mov+ " A partir de "+S1mov);
					}
				}
				if (S1.orientation.Equals (4)) { // Create rotation part for S1
							string tempmovS1 = S2mov;
				string[] listmovS1 = tempmovS1.Split ('_');
				S1mov = "_"; 																			// Para evitar error null revisar
					if ((!(So2.orientation.Equals (S2.orientation))&&(listmovS1.Length>2))||(So2.orientation.Equals(S2.orientation))&&!(So2.localization.Equals(S2.localization))) {
						string temp = listmovS1 [listmovS1.Length-2];
					temp += "_"+ S1mov;
						S1mov = temp;
						Debug.Log ("Camino creado para S1 " + S1mov+ " A partir de "+S2mov);
					}
					string change = S2mov;
					S2mov = S1mov;
					S1mov = change;
					Face tempf = S2;
					S2 = S1;
					S1 = tempf;
					Face tempof = So2;
					So2 = So1;
					So1 = tempof;
					Debug.Log ("Ahora sin orient es S2 " + TradLocaton (S2.localization));
				}
				if (Compare2Sides ()) {
					Debug.Log ("Mismo cubo");
				Fx.explicacion = "FollowPath";
					IsSameCube = true;
					break;
				} else {
					Debug.Log ("Cubo diferente");
					IsSameCube = false;
				}
				
			break;
		case 3:
			Debug.Log ("Se repiten tres simbolos");
			if (
				/*((S1.orientation.Equals(4))&&
				(S2.orientation.Equals(4))&&
				(S3.orientation.Equals(4)))||*/
				
				((!string.IsNullOrEmpty(S1mov))&&
				(!string.IsNullOrEmpty(S2mov))&&
				(!string.IsNullOrEmpty(S3mov))&&(Compare3Sides ()))
				) {
				Debug.Log ("Mismo cubo");
				Fx.explicacion = "FollowPath";
				IsSameCube = true;
			} 
			else {
				if (string.IsNullOrEmpty (S1mov))
					Fx = So1;
				if (string.IsNullOrEmpty (S2mov))
					Fx = So2;
				if (string.IsNullOrEmpty (S3mov))
					Fx = So3;
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
		if (So.orientation < 4) {
			Sf.orientation += OrientconRot [i, u];
			switch (Sf.orientation) {
			case 4: 
				Sf.orientation = 0;
				break;
			case -1:
				Sf.orientation = 3;
				break;
			}
			if (!So.orientation.Equals (Sf.orientation)) {

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
		string[] listmov = tempmov.Split ('_');
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
			default:
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
		if ((f.localization.Equals (1)) || (f.localization.Equals (3)) || (f.localization.Equals (5))) {
			return true;
		} 
		else {
			Debug.Log (" Segun Novisible acaba en " + TradLocaton (f.localization));
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
			Fx = So1;
			Debug.Log ("Fx So1 " + TradLocaton (So1.localization));
			if (((f1.localization.Equals (1)) || (f1.localization.Equals (3)) || (f1.localization.Equals (5)))&& !((f2.localization.Equals (1)) || (f2.localization.Equals (3)) || (f2.localization.Equals (5)))) {
				if (Cube.change.Equals (0)) { //The only way to differentiate the Change of symbol from change of orientation in this case is by asking to the code Cube.
					
					Fx = T2;
					Debug.Log ("Fx T2 " + TradLocaton (T2.localization));
				}
				//Debug.Log ("Cara tapada T2 " + Fx.symbol + " en " + TradLocaton(Fx.localization)+" antes " +TradLocaton(T2.localization));
				return false;

			}
			if (!((f1.localization.Equals (1)) || (f1.localization.Equals (3)) || (f1.localization.Equals (5)))&& ((f2.localization.Equals (1)) || (f2.localization.Equals (3)) || (f2.localization.Equals (5)))) {
				if (Cube.change.Equals (0)) {
					Fx = T1;
					Debug.Log ("Fx T1 " + TradLocaton (T1.localization));
				}
				//Debug.Log ("Cara tapada T1 " + Fx.symbol + " en " + TradLocaton(Fx.localization) + " antes " + TradLocaton(T1.localization));
				return false;

			}
		
			//Debug.Log ("Cara " + Fx.symbol + " en " + Fx.orientation.ToString ());
			return false;

		}
	}

	public bool Compare2Sides () // Miramos si ambas caras han hecho caminos equivalentes
	{
		Face F1= new Face (So1.symbol,So1.orientation,So1.localization),F2=new Face (So2.symbol,So2.orientation,So2.localization);
		//Debug.Log ("Antes de mover " + F1.symbol + " en " + TradLocaton( F1.localization) +" "+ F2.symbol + " en " +TradLocaton( F2.localization));

		F1= MoveFace (S2mov, F1);
		Debug.Log("S2mov no da error "+S2mov);
		F2=MoveFace (S1mov, F2);
		Debug.Log("S1mov no da error "+S1mov);

	//	Debug.Log (" Tras mover con R=2 " + F1.symbol + " habría hecho "+S2mov+" Habria acabado en " + TradLocaton( F1.localization)+ " como "+TradLocaton(S1.localization) + " y " + F2.symbol + " habría hecho "+S1mov+ " habria acabado en " +TradLocaton( F2.localization)+ " como "+TradLocaton(S2.localization ));
		if (((F1.localization ==S1.localization))&&((F2.localization == S2.localization))) {
			Debug.Log ("Acaban en el mismo sitio");
			Face T1f = new Face(T1.symbol,T1.orientation, T1.localization);
			if (NoVisible (T1f)) {
				Way = S1mov;
				Tquest.symbol = T1.symbol;Tquest.orientation = T1.localization;Tquest.localization = T1f.localization; //Localization original and final of the T side
				return true;

			} 
			else {
				Fx = T1;
			//	Debug.Log ("Fx T1 " + TradLocaton (So2.localization));
			//	findExpl ();
				return false;
			}

		} 
		else {
			Fx = T1;
			Debug.Log ("Acaban en sitios diferentes");
			Face T1f = new Face(T1.symbol,T1.orientation, T1.localization); //Falla no visible T1f
			Debug.Log("T1f pre en "+ TradLocaton(T1f.localization));
			Face T2f = new Face(So2.symbol,So2.orientation, So2.localization);
			Debug.Log("T2f pre  en "+ TradLocaton(T2f.localization));
			bool NVT1 = NoVisible (T1f), NVS2 = !(NoVisible (T2f));
			if ((NVT1)&& (NVS2)) {
				Debug.Log("T1f pos en "+ TradLocaton(T1f.localization));
				Debug.Log("T2f pos en "+ TradLocaton(T2f.localization));
				Debug.Log("No visible "+NVT1.ToString()+" Dejar ver a S2? "+NVS2.ToString()+" Side "+ TradLocaton(T1f.localization)+" Camino "+S1mov);
				Fx = So2;
				Debug.Log ("Fx So2 " + TradLocaton (So2.localization));
				//findExpl ();
			} 
			else {
				Debug.Log("T1f pos en "+ TradLocaton(T1f.localization));
				Debug.Log("T2f pos en "+ TradLocaton(T2f.localization));
				Debug.Log("No visible "+NVT1.ToString()+" Dejar ver a S2? "+NVS2.ToString()+" Side "+ TradLocaton(T1f.localization)+" Camino "+S1mov);
				Fx = So1;
				Debug.Log ("Fx So1 " + TradLocaton (So1.localization));
			//	findExpl ();
			}
			return false;
		}
	}

	public bool Compare3Sides () // Miramos si ambas caras han hacho caminos equivalentes
	{
		if (S1.orientation.Equals (4)) {
			S1mov = NoOrientR3 [listpos( So1.localization),listpos( S1.localization)];
		}
		if (S2.orientation.Equals (4)) {
			S2mov = NoOrientR3 [listpos(So2.localization),listpos( S2.localization)];
		}
		if (S3.orientation.Equals (4)) {
			S3mov = NoOrientR3 [listpos(So3.localization),listpos(S3.localization)];
		}
		Debug.Log ("R=3 all ori=4 " + S1mov + " " + S2mov + " " + S3mov);
		Face F1 = new Face(So1.symbol,So1.orientation,So1.localization), F2 = new Face(So2.symbol,So2.orientation,So2.localization), F3 =new Face(So3.symbol,So3.orientation,So3.localization);
		F1= MoveFace (S2mov, F1);
		F2= MoveFace (S3mov, F2);
		F3 =MoveFace (S1mov, F3);

		if (((F1.localization.Equals (S1.localization)))&&((F2.localization.Equals (S2.localization)) )
			&&((F3.localization.Equals (S3.localization)))) {
			Way = S1mov;
			return true;
		} 
		else {
			Face F2f = new Face (So2.symbol, So2.orientation, So2.localization), F3f = new Face (So3.symbol, So3.orientation, So3.localization);
			F2f = MoveFace (S1mov, F2f);
			F3f = MoveFace (S1mov, F3f);
			if (!(F2f.localization.Equals (S2.localization)) && !(F3f.localization.Equals (S3.localization))) {
				Fx = So1;
				return false;
			} else {
				if ((F2f.localization.Equals (S2.localization))) {
					Fx = So3;
					return false;
				} else {
					Fx = So2;
					return false;
				}
			}

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

	void findExpl ()
	{
		bool found=false;
		foreach (Face Fo in SidesO) {
			if (Fo.symbol.Equals (Fx.symbol)) {
				Fx.explicacion = "Has changed its orientation";
				found = true;
			}
		}
		if (!found) 
		{
			Fx.explicacion = "has changed its symbol";

			}
		}
		
}
