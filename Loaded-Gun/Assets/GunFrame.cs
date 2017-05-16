using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class GunFrame : MonoBehaviour {
	public GameObject gunFrame;
	public GameObject bulletFrame;
	public Sprite fingerGun;
	public bool gunSpriteChanged;
	public AudioClip audioChanged;
	public bool audioReversed;

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
		//audioReversed = false;
		
	}

	// Update is called once per frame
	void Update () {
		if (audioReversed == true) {
			print (audioReversed);
		}


	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		print ("bork bork");
		if (collision.gameObject == bulletFrame) {
			this.gameObject.GetComponent <SpriteRenderer>().sprite = fingerGun;
			gameObject.GetComponent<AudioSource> ().enabled = true;
			Destroy (collision.gameObject);
		}
			
	}

	void On_TouchStart (Gesture gesture){
		if (gesture.pickedObject == this.gameObject){
			gameObject.GetComponent<AudioSource>().Play();

		}
	}
}
