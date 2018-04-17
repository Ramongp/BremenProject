using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Points : MonoBehaviour {


	// Use this for initialization
	public Slider TimeSlider, MoneySlider;
	public Text points,TimeText,MoneyText;
	public Image bag, star0, star1,star2;
	public Color32 StarOff, StarOn;
	public float speed;
	public static bool exchange;
	public Button next;
	void Start () {
		Set ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (exchange) {
			if (TimeSlider.value > 0) {
				TimeSlider.value -= Time.deltaTime * speed;
				MoneySlider.value += Time.deltaTime * speed;
				points.text = ((int)(MoneySlider.value*10)).ToString () + LangTest.LMan.getString ("Points");
				if ((MoneySlider.value > 0) && (star0.color==StarOff)) {
					star0.color = StarOn;
					Debug.Log ("Pasa");
				}
				if ((MoneySlider.value > 20) && (star1.color==StarOff)) {
					star1.color = StarOn;
				}
				if ((MoneySlider.value > 40) && (star2.color==StarOff)) {
					star2.color = StarOn;
				}
			}
			else {
				exchange = false;
			}
		}
	}

	public void RewardAnimation (float timeLeft) //Show the reward only with the timeLeft
	{
		TimeSlider.gameObject.SetActive (true);
		MoneySlider.gameObject.SetActive (true);
		bag.gameObject.SetActive (true);
		star0.gameObject.SetActive (true);
		star1.gameObject.SetActive (true);
		star2.gameObject.SetActive (true);
		next.gameObject.SetActive (true);
		star0.color = StarOff;
		star1.color = StarOff;
		star2.color = StarOff;
		TimeSlider.value = timeLeft;
		exchange = true;
		
	}
	public void Set()
	{
		TimeText.text = LangTest.LMan.getString ("Time");
		MoneyText.text = LangTest.LMan.getString ("Money");
		exchange = false;
		speed = 8;
		next.gameObject.SetActive (false);
		StarOff = new Color32 (255, 255, 153, 50);
		StarOn = new Color32 (255, 255, 153, 255);
		TimeSlider.maxValue = 60;
		MoneySlider.maxValue = 60;
		TimeSlider.value = 0;
		MoneySlider.value = 0;
		TimeSlider.gameObject.SetActive (false);
		MoneySlider.gameObject.SetActive (false);
		bag.gameObject.SetActive (false);
		star0.gameObject.SetActive (false);
		star1.gameObject.SetActive (false);
		star2.gameObject.SetActive (false);
	}
}
