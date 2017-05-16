using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class CameraSwipe : MonoBehaviour {

	public Camera myCamera;
	public bool uiDragging;
	public bool worldDragging;
	public bool cameraSwiping;
	public bool colliding;
	private Vector3 bumper;
	private Vector3 camStartPosition;

	void OnEnable(){
		EasyTouch.On_SwipeStart += On_SwipeStart;
		EasyTouch.On_DragEnd += On_SwipeEnd;
		EasyTouch.On_Swipe += On_Swipe;
	}

	void OnDisable(){
		UnsubscribeEvent();
	}

	void OnDestroy(){
		UnsubscribeEvent();
	}

	void UnsubscribeEvent(){
		EasyTouch.On_SwipeStart -= On_SwipeStart;
		EasyTouch.On_SwipeEnd -= On_SwipeEnd;
		EasyTouch.On_Swipe -= On_Swipe;
	}	

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		bumper = new Vector3(.3f,.3f,0);
	
	}

	private void On_SwipeStart(Gesture gesture)
	{
		if(uiDragging == false && worldDragging == false){
		camStartPosition=Camera.main.transform.localPosition;
		//startPosition=gesture.GetTouchToWordlPoint(Camera.main.transform.localPosition.z);
		}
	}

	private void On_Swipe(Gesture gesture)
	{
		if (uiDragging == false && worldDragging == false) {
			cameraSwiping = true;
			Vector2 tmpVector = new Vector3 ();
			Vector2 currentPos = new Vector3 ();

			currentPos = gesture.GetTouchToWorldPoint (Camera.main.transform.localPosition.z);
			tmpVector = gesture.deltaPosition * (Time.deltaTime * .04f);  //.GetTouchToWordlPoint(Camera.main.transform.localPosition.z);
			//	Debug.Log(tmpVector);
			//Debug.Log(currentPos);
			myCamera.transform.localPosition -= new Vector3 (tmpVector.x, myCamera.transform.localPosition.y - 1.59f, 0);
			//Debug.Log (currentPos);
			//Debug.Log ("Swiping!");
		}
	}

	private void On_SwipeEnd(Gesture gesture)
	{
		cameraSwiping = false;
		Debug.Log ("Done!");
	}

	private void OnTriggerEnter(Collider fuckyou){
		colliding = true;
		RaycastHit hit;
		if(fuckyou.gameObject.CompareTag("wall")){
			print("meow meow");
		if(Physics.Raycast(transform.position, transform.forward, out hit)){
			print("fucking kill me");
				transform.position -= bumper;
		}
		}
	}

	private void OnTriggerExit(Collider fuckyou){
		colliding = false;
	}
}
