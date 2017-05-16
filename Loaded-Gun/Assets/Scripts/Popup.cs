using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class Popup : MonoBehaviour {

	public GameObject thisPanel;

	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}

	void OnDestroy(){
		UnsubscribeEvent();
	}

	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart;
	}	


	// Use this for initialization
	void Start () {
		//thisPanel.SetActive (false);
		Gesture current = EasyTouch.current;
	}
		
	void On_TouchStart (Gesture gesture){
		if (gesture.pickedObject == this.gameObject) {
			thisPanel.SetActive (true);
			print (gesture.pickedObject);
		}
	}

	void On_TouchDown (Gesture gesture){

	}

	void On_TouchUp (Gesture gesture){

	}
		
	
	// Update is called once per frame
//	void Update () {
//	
//	}
//
	public void closeIt () {
		thisPanel.SetActive (false);
	}
}
