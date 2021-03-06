﻿using UnityEngine;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lighter : MonoBehaviour {
	private int fingerId = -1;
	private bool drag = true;
	private Vector3 startPosition;
	private Vector3 startSize;
	private Vector3 startAnchor;
	private Sprite startImage;
	public Sprite lighterActive;
	private Transform thisLight;
	private GameObject[] candles;
	public RectTransform[] candlesRect;
	private GameObject gunFrame;
	private RectTransform gunRect;
	private bool isAudioChanged;
	public GameObject newCage;
	public GameObject homeRoom;
	public Scene currentScene;
    public GameObject fingerLight;
    private Transform currentLight;
    private Vector3 deltaPosition;
    private int fingerIndex;

    void OnEnable(){
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchUp += On_TouchUp;
		EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += On_TouchUp2Fingers;
        EasyTouch.On_DragStart += On_DragStart;
        EasyTouch.On_Drag += On_Drag;
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
        EasyTouch.On_DragStart -= On_DragStart;
        EasyTouch.On_Drag -= On_Drag;
    }

	// Use this for initialization
	void Start () {
		currentScene = SceneManager.GetActiveScene ();
		startPosition = gameObject.GetComponent<Transform> ().position;
		startSize = gameObject.GetComponent<Transform>().localScale;
		startAnchor = gameObject.GetComponent<RectTransform> ().anchoredPosition;
		startImage = gameObject.GetComponent<Image> ().sprite;

		
			candles = GameObject.FindGameObjectsWithTag ("candle");
			candlesRect = new RectTransform[candles.Length];
			for (int j = 0; j < candles.Length; j++) {
				candlesRect [j] = candles [j].GetComponent<RectTransform> ();

			
			//gunFrame = GameObject.Find ("Gun Frame");
			//gunRect = gunFrame.GetComponent<RectTransform> ();
		}


	}
	
	// Update is called once per frame
	void FixedUpdate () {
	//	if (currentScene.name == "Main") {
	//		isAudioChanged = gunFrame.GetComponent<GunFrame> ().audioReversed;
	//	}
      
    }

    void Update()
    {
        
    }
    void On_TouchStart (Gesture gesture){
		if (gesture.isOverGui && drag){
			if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) && fingerId==-1){
				if (currentScene.name == "Main") {
					Camera.main.GetComponent<CameraSwipe> ().uiDragging = true;
				}
				//print ("working!");
				fingerId = gesture.fingerIndex;
				transform.SetAsLastSibling();
				gameObject.GetComponent<Image> ().sprite = lighterActive;
				gameObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1f);
                
                Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedUIElement.transform.position);
                deltaPosition = position - transform.position;
                fingerLight.SetActive(true);
                    //  currentLight = Instantiate(fingerLight, gesture.pickedUIElement.transform.position, Quaternion.identity);
                print("lighter: " + transform.position);

            }
        }
	}

	void On_TouchDown (Gesture gesture){

		if (fingerId == gesture.fingerIndex && drag){
			if (gesture.isOverGui ){
				if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) ){
                    Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedUIElement.transform.position);
                    transform.position = position - deltaPosition;
                  //  currentLight.transform.position = position - deltaPosition;
                }
			}
		}
	}

	void On_TouchUp (Gesture gesture){
	// on touch up, instantiate object. trying screen to world point in triangle but it is instantiating on the 

		if (fingerId == gesture.fingerIndex) {
			fingerId = -1;
			gameObject.transform.localScale = startSize;
			foreach (RectTransform item in candlesRect) {
				if (gesture.IsOverRectTransform (item, Camera.main) == true) {
					Camera.main.GetComponent<CameraSwipe> ().uiDragging = false;
					thisLight = item.transform.GetChild(0);
					thisLight.GetComponent<Light>().enabled = true;
					gameObject.transform.position = startPosition;
					gameObject.GetComponent<Image> ().sprite = startImage;
					gameObject.GetComponent<RectTransform> ().anchoredPosition = startAnchor;
					//Destroy (this.gameObject);
				} else {
					if (currentScene.name == "Main") {
						Camera.main.GetComponent<CameraSwipe> ().uiDragging = false;
					}
					gameObject.transform.position = startPosition;
					gameObject.GetComponent<Image> ().sprite = startImage;
					gameObject.GetComponent<RectTransform> ().anchoredPosition = startAnchor;
				}
			}
			if (gesture.IsOverRectTransform (gunRect, Camera.main) == true && isAudioChanged == true && currentScene.name == "Main") {
				Camera.main.GetComponent<CameraSwipe> ().uiDragging = false;
				newCage.GetComponent<Renderer> ().enabled = true;
				newCage.GetComponent<AudioSource> ().Play ();
				Destroy (gunFrame);
			}
            fingerLight.SetActive(false);
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


    void On_DragStart(Gesture gesture)
    {

        // Verification that the action on the object
        if (gesture.pickedObject == gameObject)
        {
            fingerIndex = gesture.fingerIndex;

            // the world coordinate from touch
            Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
            deltaPosition = position - transform.position;

        }
    }

    void On_Drag (Gesture gesture)
    {


        if (gesture.isOverGui && drag)
        {
            if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf(transform)) && fingerId == -1)
            {

                // the world coordinate from touch
                Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
                transform.position = position - deltaPosition;


                // Get the drag angle
                float angle = gesture.GetSwipeOrDragAngle();
            }
        }
        

    }
}
