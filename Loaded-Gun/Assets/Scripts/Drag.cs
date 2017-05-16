using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;

public class Drag : MonoBehaviour {
	private Vector2 start;
	private Vector3 thisFinger;
	private Vector3 deltaPosition;
	private Vector3 startSize;
	private Vector3 startPosition;
	private int startLayer;
	public Transform worldSprite;
	public Image iconSprite;
	private bool cameraMoving;

	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchUp += On_TouchEnd;
		EasyTouch.On_DragStart += On_DragStart;
		EasyTouch.On_DragEnd += On_DragEnd;
		EasyTouch.On_Drag += On_Drag;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}

	void OnDestroy(){
		UnsubscribeEvent();
	}

	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchUp -= On_TouchEnd;
		EasyTouch.On_DragStart -= On_DragStart;
		EasyTouch.On_DragEnd -= On_DragEnd;
		EasyTouch.On_Drag -= On_Drag;
	}	

	void Start(){
		startPosition = gameObject.GetComponent<Transform> ().position;
		startSize = gameObject.GetComponent<Transform>().localScale;
		cameraMoving = Camera.main.GetComponent<CameraSwipe> ().cameraSwiping;
	}

	void Update(){
		startLayer = gameObject.GetComponent<SpriteRenderer> ().sortingOrder;
	}

	void On_TouchStart (Gesture gesture){
		if (gesture.pickedObject == this.gameObject) {
			
//			print ("awwwwee");
			gameObject.transform.localScale = new Vector3 (.40f, .40f, .1f);	
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 8;
		}
	}

	void On_TouchEnd (Gesture gesture){
		if (gesture.pickedObject == this.gameObject && cameraMoving == false) {
			gameObject.transform.localScale = startSize;
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = startLayer;
		}
	}

	void On_DragStart (Gesture gesture){
		
		if (gesture.pickedObject == this.gameObject && cameraMoving == false) {
			start = Camera.main.ScreenToWorldPoint(gesture.startPosition);
			Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			deltaPosition = position - transform.position;

		}
	}

	void On_Drag (Gesture gesture){
		if (gesture.pickedObject == this.gameObject) {
			
			print ("hey now!");
			//var offset = transform.position - Camera.main.ScreenToWorldPoint (gesture.position);
			Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			transform.position = position - deltaPosition;
		}
	}

	void On_DragEnd (Gesture gesture){

//		transform.position = startPosition;
//		gameObject.transform.localScale = startSize;
		
	}

//	private void ChangeSize (){
//		print ("awwwwee");
//	    gameObject.transform.localScale = new Vector3 (.56f, .59f, .1f);
//		transform.position.z =
//
//	}
}
