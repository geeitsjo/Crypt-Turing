using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCanvas_Script : MonoBehaviour {

	public Text timerText; 
	private int startCountDown =80;
	private float countDownTime;
	public GameObject myButton;
	private bool play=false;

	//Progress Bar
	public Transform LoadingBar;
	public Transform TextIndicator;
	public Transform TextLoading;

	public static int roundy;
	int muso;

	[SerializeField]
	private float currentAmount;

	private float speed=3;

	// Use this for initialization
	void Start () {
		countDownTime= startCountDown;
	}
	// Update is called once per frame
	void Update () {
		if (play) {
			countDownTime = countDownTime - Time.deltaTime;
			timerText.text = countDownTime.ToString ("0.0");
			if (countDownTime <= 0.0) {
				Fin ();
			}

				//ProgressBar
			roundy=KeyBoard.rounds;
			if (roundy>5){
					speed = 4;
				}
			if (roundy>10){
					speed = 6;
				}
			if (roundy>20){
					speed = 10;
				}
				//Debug.Log ("Speed: "+speed);

				if (currentAmount < 100) {
					currentAmount += speed * Time.deltaTime;
					TextIndicator.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%";
					TextLoading.gameObject.SetActive (true);
				} else {
					GameObject[] objFind = GameObject.FindGameObjectsWithTag ("A");
					for (int i = 0; i < objFind.Length; i++) {
						InputField objText = objFind [i].GetComponent<InputField> ();
						objText.text = string.Empty;
						objText.interactable = false;
						}
						while(muso<10){
							GetComponent<AudioSource> ().Play ();
							muso++;
						}
					TextLoading.gameObject.SetActive (false);
					TextIndicator.GetComponent<Text> ().text = "SENT";
				}
				LoadingBar.GetComponent<Image> ().fillAmount = currentAmount / 100;
		}
	}//end of update
	public void Play(){
		GameObject[] objFind = GameObject.FindGameObjectsWithTag ("A");
		for (int i = 0; i < objFind.Length; i++) {
			InputField objText = objFind[i].GetComponent<InputField> ();
			objText.text = string.Empty;
			if (!play) {
				objText.interactable = true;
			} else {
				objText.interactable = false;
			}
		}
		play = !play;
	}//end of play

	void Fin(){
		currentAmount = 0;
		muso=0;
		timerText.text =("40.0");
		countDownTime= startCountDown;
		play = true;
		Play ();
	}
	public void RestartTimer(){
		countDownTime= startCountDown;
	}
}//end of class

//
//AudioSource correctAudio;
//AudioSource errorAudio;
//
//void Start() {
//	AudioSource[] audios = GetComponents<AudioSource>();
//	errorAudio = audios[0];
//	correctAudio = audios[1];
//}
//
//void OnGUI() {
//	if (answer == currAnswer) 
//		correctAudio.Play();
//	else
//		errorAudio.Play();
//}
