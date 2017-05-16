using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class BlowOut : MonoBehaviour {
	private Light thisLight;

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
		thisLight = gameObject.GetComponent<Light> ();
	
	}

	void On_TouchStart (Gesture gesture){

		if (gesture.pickedObject == this.gameObject) {
			thisLight.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
