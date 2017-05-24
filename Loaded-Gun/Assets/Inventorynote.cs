﻿using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;

public class Inventorynote : MonoBehaviour {
	public GameObject thisPanel;
	public GameObject introText;
	public GameObject thisLetter;


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
//		thisDoor.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void On_TouchStart (Gesture gesture){
		if (gesture.pickedObject == thisLetter) {
				print("mew mew");
			thisPanel.SetActive (true);
			thisLetter.SetActive (false);
				
			}
	}

	public void closeIt () {
		thisPanel.SetActive (false);
	}


	public void openLetter(){
		thisLetter.SetActive (false);

	}

//	IEnumerator openLetter(){
////		introText.GetComponent<Text>().enabled = false;
////		yield return new WaitForSeconds(1);
//		//this is how I am getting the letter button to become active after a period of time
//		thisLetter.SetActive (false);
//
//	}
}
