using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Use this for initialization
	public float TimeLeft, AnimationTime;
	public Slider TimeSlider, MoneySlider;
	public static bool start, end, animation;
	public static float TimeLevel;
	public static int pointsLevel, points;
	public int currentValue, answer;
	public int moneySpeed; 
	public Text Solution, PointText;
	public Image TimeFill,Bag;
	public Button TaparN,TaparE;
	public Image Message;
	public ParticleSystem ParticleF, ParticleR, ParticleU;
	void Start () {
		
		Bag.gameObject.SetActive (false);
		PointText.gameObject.SetActive (false);moneySpeed = 20;
		MoneySlider.value = 0;
		MoneySlider.maxValue = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			SetTimer ();
			start = false;
		}
		if (!end) {
			TimeLeft -= Time.deltaTime;
			TimeSlider.value = TimeLeft;
				points = pointsLevel ;//Ecuacion puntos
				ColorBlock c = TimeSlider.colors;
				c.normalColor = Color.red;
			TimeFill.color= (Color.green/2+(Color.red/TimeLeft));
			} 
		if (MoneySlider.value <currentValue) {
			MoneySlider.value += Time.deltaTime*moneySpeed;
		}
	}

	void SetTimer()
	{
		ParticleF.gameObject.SetActive (false);
		ParticleR.gameObject.SetActive (false);
		ParticleU.gameObject.SetActive (false);
		Message.gameObject.SetActive (false);
		TimeLevel = 60;
		pointsLevel = 10;
		points = pointsLevel;
		TimeLeft = TimeLevel;
		TimeSlider.maxValue = TimeLeft;
		TimeSlider.value = TimeSlider.maxValue;
		TaparN.gameObject.SetActive (false);
		TaparE.gameObject.SetActive (false);
		animation = false;
		end = false;
		Solution.gameObject.SetActive (false);

		points = pointsLevel;
	}

	public void Animation(bool correct,int answer)
	{
		this.answer = answer;
		Message.gameObject.SetActive (true);
		TaparE.gameObject.SetActive (true);
		animation = true;
		AnimationTime = 2;
		if (correct) {
			switch (answer) {
			case 0:
				Solution.text = "Yes, it is the same";
				break;
			case 1:
				Solution.text = "Yes, They are different";
				break;
			}
			Bag.gameObject.SetActive (true);
			Solution.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Correct", true);
			currentValue += points;
			PointText.gameObject.SetActive (true);
			PointText.text = "+ " + points;
		} 
		else {
			switch (answer) {
			case 2:
				Solution.text = "No, They are different";
				break;
			case 3:
				Solution.text = "No, it is the same";
				break;
			}
			Solution.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Wrong", true);
		}
	}

	public void NextBox()
	{
		GameObject.Find ("Camera").GetComponent<Cube> ().Restart ();
	}

	public void Explicacion()
	{
		//animation = false;
		TaparE.gameObject.SetActive (false);
		TaparN.gameObject.SetActive (true);
		Bag.gameObject.SetActive (false);
		PointText.gameObject.SetActive (false);

		switch (answer) {
		case 0:
			Solution.GetComponent<Animator> ().SetBool ("Correct", false);
			Solution.text = "Yes, it is the same";
			break;
		case 1:
			Solution.GetComponent<Animator> ().SetBool ("Correct", false);
			Solution.text = "They are different because of "+SameCube.Fx.symbol;
			break;
		case 2:
			Solution.GetComponent<Animator> ().SetBool ("Wrong", false);
			Solution.text = "They are different, because of "+SameCube.Fx.symbol;
			break;
		case 3:
			Solution.GetComponent<Animator> ().SetBool ("Wrong", false);
			Solution.text = "No, it is the same";
			break;

		}

		
	}
		
}
