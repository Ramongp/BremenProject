using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour {

	// Use this for initialization
	public GameObject Reward;
	public static bool animation;
	void Start () {
		Reward.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
		if (animation) {
			animation = false;
			Reward.SetActive (true);
		}
	
	}

	public void RewardAnimation (float timeLeft) //Show the reward only with the timeLeft
	{
		
	}
}
