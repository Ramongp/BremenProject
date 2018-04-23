using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoRotation : MonoBehaviour {

	// Use this for initialization
	public Text text;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Assigned: "+ TrainingCube.Assigned.ToString()+ " Z angle: " + TrainingCube.Zangle + " OrigAngel: " + TrainingCube.Tangle +
			" PrevAngle: "+TrainingCube.prevAngle +" RestAngle: "+TrainingCube.restAngle+" RefAgle: "+TrainingCube.RefAngle;

	}
}
