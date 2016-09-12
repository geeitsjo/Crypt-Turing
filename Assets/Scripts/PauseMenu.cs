using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseUI;
	public GameObject startCanvas;
	public GameObject timerCanvas;
	public GameCanvas_Script sn;
	public KeyBoard timeless;
	//bool main =false;
	private bool pause=false;

	void Start(){
		PauseUI.SetActive (false);
	}

	void Update(){
				if (Input.GetButtonDown ("Pause")) {
					pause = !pause;
				}
				if (pause) {
					PauseUI.SetActive (true);
					Time.timeScale = 0;
				}
				if (!pause) {
					PauseUI.SetActive (false);
					Time.timeScale = 1; //0.3 is slow motion
		}
	}//end of update

	public void Resume(){
		pause = false;
	}
	public void RestartCanvas(){
		pause = false;
		sn = gameObject.GetComponent<GameCanvas_Script>();
		sn.RestartTimer();
		timeless = gameObject.GetComponent<KeyBoard>();
		timeless.RestartCanvas ();
	}
	public void MainMenu(){
		startCanvas.SetActive (true);
		timerCanvas.SetActive (false);
		pause = false;
	}
	public void Quit(){
		Application.Quit ();
	}

	public void StartGame(){
		startCanvas.SetActive (false);
		timerCanvas.SetActive (true);
	}

	public void EndGame(){
		startCanvas.SetActive (true);
		timerCanvas.SetActive (false);
	}
}//end of class
