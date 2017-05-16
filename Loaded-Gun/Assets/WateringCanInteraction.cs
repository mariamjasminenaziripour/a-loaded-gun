using UnityEngine;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;

public class WateringCanInteraction : MonoBehaviour {
	private int fingerId = -1;
	private bool drag = true;
	private Vector3 startPosition;
	private Vector3 startSize;
	private Vector3 startAnchor;
	private Sprite startImage;
	//	public GameObject[] candles;
	private Sprite thisSprite;
	private Sprite changeSprite;
	private GameObject[] thesePlants;
	public RectTransform[] plantsRect;


	void OnEnable(){
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchUp += On_TouchUp;
		EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += On_TouchUp2Fingers;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}

	void OnDestroy(){
		UnsubscribeEvent();
	}

	void UnsubscribeEvent(){
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchUp -= On_TouchUp;
		EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= On_TouchUp2Fingers;
	}

	// Use this for initialization
	void Start () {
		startPosition = gameObject.GetComponent<Transform> ().position;
		startSize = gameObject.GetComponent<Transform>().localScale;
		startAnchor = gameObject.GetComponent<RectTransform> ().anchoredPosition;
		startImage = gameObject.GetComponent<Image> ().sprite;
		thesePlants = GameObject.FindGameObjectsWithTag ("plant");
		for (int m = 0; m < thesePlants.Length; m++) {
			print (thesePlants[m]);
		}
		plantsRect= new RectTransform[thesePlants.Length];
		for(int i=0;i<thesePlants.Length;i++){
			plantsRect [i] = thesePlants[i].GetComponent<RectTransform> ();
		}

	}

	// Update is called once per frame
	void Update () {

	}

	void On_TouchStart (Gesture gesture){
		if (gesture.isOverGui && drag){
			if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) && fingerId==-1){
				Camera.main.GetComponent<CameraSwipe> ().uiDragging = true;
				fingerId = gesture.fingerIndex;
				transform.SetAsLastSibling();
				gameObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
			}
		}
	}

	void On_TouchDown (Gesture gesture){

		if (fingerId == gesture.fingerIndex && drag){
			if (gesture.isOverGui ){
				if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) ){
					
					transform.position += (Vector3)gesture.deltaPosition;
				}
			}
		}
	}

	// on touch up, instantiate object. trying screen to world point in triangle but it is instantiating on the edge. fml. 
	void On_TouchUp (Gesture gesture){

		if (fingerId == gesture.fingerIndex) {
			Camera.main.GetComponent<CameraSwipe> ().uiDragging = false;
			fingerId = -1;
			gameObject.transform.localScale = startSize;
			foreach (RectTransform plant in plantsRect) {
				if (gesture.IsOverRectTransform (plant, Camera.main) == true) {
					Debug.Log ("meow meow watering plants");
//					thisSprite = item.GetComponent<SpriteRenderer> ().sprite;
					changeSprite = plant.GetComponent<Plants> ().wateredPlant;
					//thisSprite= changeSprite;
					plant.GetComponent<SpriteRenderer> ().sprite = changeSprite;
					gameObject.transform.position = startPosition;
					gameObject.GetComponent<Image> ().sprite = startImage;
					gameObject.GetComponent<RectTransform> ().anchoredPosition = startAnchor;
					//Destroy (this.gameObject);
				} else {
					gameObject.transform.position = startPosition;
					gameObject.GetComponent<Image> ().sprite = startImage;
					gameObject.GetComponent<RectTransform> ().anchoredPosition = startAnchor;
				}
			}
		}
	}

	void On_TouchStart2Fingers (Gesture gesture)
	{
		if (gesture.isOverGui && drag){
			if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) && fingerId==-1){
				transform.SetAsLastSibling();
			}
		}		
	}


	void On_TouchDown2Fingers (Gesture gesture)
	{
		if (gesture.isOverGui){
			if (gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)){
				if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) ){
					transform.position += (Vector3)gesture.deltaPosition;
				}
				drag = false;
			}
		}
	}

	void On_TouchUp2Fingers (Gesture gesture)
	{
		if (gesture.isOverGui){
			if (gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)){
				drag = true;
			}
		}
	}
}
