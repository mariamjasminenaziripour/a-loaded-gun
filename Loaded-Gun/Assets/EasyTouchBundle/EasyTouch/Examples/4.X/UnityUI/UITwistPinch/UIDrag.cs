using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class UIDrag : MonoBehaviour {

	private int fingerId = -1;
	private bool drag = true;
	private Vector3 startPosition;
	private Vector3 startSize;
	private Vector3 startAnchor;
	private int startLayer;
	public GameObject homeRoom;
	public GameObject roomSprite;
	public GameObject placement;
	public GameObject interaction;
	private RectTransform interactionRect;
	private RectTransform homeRect;
	private bool cameraMoving;

	
	void OnEnable(){
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchUp += On_TouchUp;
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
	}

	void Start(){
		startPosition = gameObject.GetComponent<Transform> ().position;
		startSize = gameObject.GetComponent<Transform>().localScale;
		startAnchor = gameObject.GetComponent<RectTransform> ().anchoredPosition;
		homeRect = placement.GetComponent<RectTransform> ();
		interactionRect = interaction.GetComponent<RectTransform> ();
		cameraMoving = Camera.main.GetComponent<CameraSwipe> ().cameraSwiping;

	}


	
	void On_TouchStart (Gesture gesture){

		if (gesture.isOverGui && drag && cameraMoving == false){
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

		if (fingerId == gesture.fingerIndex){
			Camera.main.GetComponent<CameraSwipe> ().uiDragging = false;
			//Debug.Log ("Dragging iz " + uiDragging);
			fingerId = -1;
			gameObject.transform.localScale = startSize;
			if (gesture.IsOverRectTransform (interactionRect, Camera.main) == true) {
				Destroy (this.gameObject);
			} else if (gesture.IsOverRectTransform (homeRect, Camera.main) == true){
				print ("meow meow");
				GameObject newRoomSprite = Instantiate (roomSprite) as GameObject;
				newRoomSprite.transform.SetParent (homeRoom.transform, false);
				Destroy (this.gameObject);
			}else {
				gameObject.transform.position = startPosition;
				gameObject.GetComponent<RectTransform> ().anchoredPosition = startAnchor;
			}
		}
	}




}
