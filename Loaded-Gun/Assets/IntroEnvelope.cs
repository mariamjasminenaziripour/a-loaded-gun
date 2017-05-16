using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class IntroEnvelope : MonoBehaviour {
	
	public GameObject introNote;
	public Light halllight;

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
			introNote.SetActive (true);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void lightOnClose () {
		introNote.SetActive (false);
		halllight.enabled = true;
	}
}
