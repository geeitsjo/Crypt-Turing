using UnityEngine;
using UnityEngine.UI; // this is important to use the functios for the UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class KeyBoard : MonoBehaviour {

	EventSystem system;
	List<string> users = new List<string>();
	List<string> binary = new List<string>();
	//int count=0; //debugging purposes

	public static int rounds = 3;
	string one;
	string two; 
	string three;
	string four;

	int mistakes;
	int highMis=0;
	int limit=0; //16 possible answers only
	int cou=0;
	int on;


	[SerializeField]
	private Text msg;

	[SerializeField]
	private Text oneAns;

	[SerializeField]
	private Text twoAns;

	[SerializeField]
	private Text threeAns;

	[SerializeField]
	private Text fourAns;

	[SerializeField]
	private Text errors;

	[SerializeField]
	private Text bigErrors;

//	[SerializeField]
//	private Text smallErrors

	void Start(){
		system = EventSystem.current;// EventSystemManager.currentSystem;
	}
	// Update is called once per frame
	void Update(){
		if (Input.GetKeyDown(KeyCode.Tab)){
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			if (next != null){
				InputField inputfield = next.GetComponent<InputField>();
				if (inputfield != null){
					inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret
				}
				system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
			}
			//else{ Debug.Log("next nagivation element not found");}
		}
	}//end of update fnc

	public void GetInput(string number){
		//Debug.Log ("Binary" + number);//input the numbers
		if (limit < 16) {
			if (number.Equals ("0") || number.Equals ("1")) {
				msg.color = Color.white;
				msg.text=("READING INPUT");
				binary.Add (number);
//				Debug.Log (binary [count]);
//				count++;
				limit++;
			if(one==null) one=number;
			else{
				if(two==null) two=number;
				else{
					if(three==null) three=number;
					else{
							if (four == null) {
								four = number;
								ASCII (one, two, three, four);
								one = null;
								two = null;
								three = null;
								four = null;
								//makng sure that the rounds increased
							}
						}
					}
				}
			} 
			else {
				mistakes++;
				errors.text =(""+mistakes);
				if (mistakes > highMis) {
					highMis = mistakes;
					bigErrors.text =("Most Errors: "+highMis);
				}
				msg.color = Color.red;
				msg.text = ("ERROR: NON-BINARY");
				//Debug.Log("mistakes " + mistakes);
			}
		} 
		else {
			//Debug.Log ("shat");
			GameObject[] objFind = GameObject.FindGameObjectsWithTag ("A");
			for (int i = 0; i < objFind.Length; i++) {
				InputField objText = objFind[i].GetComponent<InputField> ();
				objText.text = string.Empty;
			}
			mistakes = 0;
			errors.text =(""+mistakes);
			oneAns.text = ("");
			twoAns.text = ("");
			threeAns.text = ("");
			fourAns.text= ("");
			limit = 0;
			cou = 0;
			one = null;
			two = null;
			three = null;
			four = null;
			msg.color = Color.yellow;
			msg.text = ("ERROR: AUTO-CLEAR");
		}
	}//end of method GetInput

	public void GetUser(string name){
		//Debug.Log ("Binary" + name);
		if (!users.Contains (name)) {
			users.Add (name);
			//Debug.Log ("added" + users [count]);
			//count++;
		}
	}//end of userMethod

	public void Clear(){
		msg.color = Color.white;
		msg.text = ("STATUS REPORT");
		mistakes--;
		GameObject[] objFind = GameObject.FindGameObjectsWithTag ("A");
		for (int i = 0; i < objFind.Length; i++) {
			InputField objText = objFind[i].GetComponent<InputField> ();
			objText.text = string.Empty;
		}
		oneAns.text = ("");
		twoAns.text = ("");
		threeAns.text = ("");
		fourAns.text= ("");
		limit = 0;
		cou = 0;
		one = null;
		two = null;
		three = null;
		four = null;
	}

	private void ASCII(string one, string two, string three, string four){
		//Debug.Log ("array" + one + two + three + four);
		//string[] ans = new string[4] { one, two, three, four };
		//Debug.Log ("In:" + one+two+three+four + " Out:" + Convert.ToInt32(one+two+three+four, 2));
		on=(Convert.ToInt32(one+two+three+four, 2));
		if (on <=9) {
			if (cou == 0) {
				//Debug.Log ("In:" + one+two+three+four + " Out:" + Convert.ToInt32(one+two+three+four, 2));
				oneAns.text = ("" + on);
				cou++;
			}
			else{
				if (cou == 1) {
					twoAns.text = (""+on);
					cou++;
				}
				else{
					if (cou == 2) {
						threeAns.text =(""+on);;
						cou++;
					}
					else{
						if (cou == 3) {
							fourAns.text=(""+on);
							cou = 0;
							rounds++;
							//Debug.Log ("Rounds: "+rounds);

						}
					}
				}
			}
		}//end of main if
		else{
			msg.color = Color.red;
			msg.text = ("ERROR: INCORRECT");
			oneAns.text = ("");
			twoAns.text = ("");
			threeAns.text = ("");
			fourAns.text = ("");
		}
	}//end of method

	public void RestartCanvas(){
		Clear ();
		rounds = 3;
		mistakes=0;
		highMis=0;
		on=0;
	}
}//end of class
