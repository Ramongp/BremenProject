using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Use this for initialization
	public float TimeLeft;
	public Slider TimeSlider, MoneySlider;
	public static bool start,end;
	public static float TimeLevel;
	public static int pointsLevel,points;
	public int currentValue;
	public int moneySpeed; 
	void Start () {
		moneySpeed = 20;
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
			TimeLeft -= Time.deltaTime*moneySpeed/3;
			TimeSlider.value = TimeLeft;
			if (TimeLeft < TimeLevel-TimeLevel/3) {
				points = pointsLevel - pointsLevel / 3;
				ColorBlock c = TimeSlider.colors;
				c.normalColor = Color.red;
				TimeSlider.colors = c;
			}

			if (TimeLeft < TimeLevel/2) {
				points = pointsLevel/2;
				ColorBlock c = TimeSlider.colors;
				c.normalColor = Color.blue;
				TimeSlider.colors = c;
			}
		} 
		if (MoneySlider.value <currentValue) {
			MoneySlider.value += Time.deltaTime*moneySpeed;
		}
	}

	void SetTimer()
	{
		ColorBlock c = TimeSlider.colors;
		c.normalColor = Color.white;
		TimeSlider.colors = c;

		TimeLevel = 60;
		pointsLevel = 10;
		TimeLeft = TimeLevel;
		TimeSlider.maxValue = TimeLeft;
		points = pointsLevel;
	}
	public void Points()
	{
		currentValue += points; //Habrá más cosas
	}
}
