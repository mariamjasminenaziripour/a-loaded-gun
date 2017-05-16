using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;



public class LightOn : MonoBehaviour {

	public Light myLight;
	public bool lightOn;
	// Use this for initialization
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

	}

	void On_TouchStart (Gesture gesture){

		if (gesture.pickedObject == this.gameObject) {
			myLight.enabled = !myLight.enabled;
		}
	}

	void On_TouchDown (Gesture gesture){

	}

	void On_TouchUp (Gesture gesture){

	}
		
}
