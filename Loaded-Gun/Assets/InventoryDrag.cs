using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;

public class InventoryDrag : MonoBehaviour {
	private Vector2 start;
	private Vector3 thisFinger;
	private Vector3 deltaPosition;
	public GameObject roomSprite;
	public GameObject homeRoom;

	void OnEnable(){
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_DoubleTap += On_DoubleTap;
		EasyTouch.On_DragStart += On_DragStart;
		EasyTouch.On_DragEnd += On_DragEnd;
		EasyTouch.On_Drag += On_Drag;
	}

	void On_DoubleTap (Gesture gesture)
	{
		if (gesture.isOverGui){
			print ("meow meow");
				GameObject newRoomSprite = Instantiate(roomSprite) as GameObject;
				newRoomSprite.transform.SetParent (homeRoom.transform, false);
				Destroy (this.gameObject);
		}
	}

	void OnDisable(){
		UnsubscribeEvent();
	}

	void OnDestroy(){
		UnsubscribeEvent();
	}

	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_DoubleTap -= On_DoubleTap;
		EasyTouch.On_DragStart -= On_DragStart;
		EasyTouch.On_DragEnd -= On_DragEnd;
		EasyTouch.On_Drag -= On_Drag;
	}

	void On_TouchStart (Gesture gesture){
		if (gesture.isOverGui){
			print ("meow meow");
		//	Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.position.x, gesture.position.y, -1.16f));
		//	GameObject newSprite = Instantiate(roomSprite, newPos, new Quaternion()) as GameObject;
		//	newSprite.transform.SetParent (homeRoom.transform, false);

		//	Destroy (this.gameObject);
			}
		}


	void On_DragStart (Gesture gesture){
		if (gesture.isOverGui){
			if (gesture.pickedUIElement == this.gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) {
				start = Camera.main.ScreenToWorldPoint (gesture.startPosition);
				Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedUIElement.transform.position);
				deltaPosition = position - transform.position;
			}
		}
	}

	void On_Drag (Gesture gesture){
		if (gesture.isOverGui){
			if (gesture.pickedUIElement == this.gameObject || gesture.pickedUIElement.transform.IsChildOf( transform)) {
				print ("hey now!");
				Vector3 pos = gesture.GetTouchToWorldPoint(gesture.pickedUIElement.transform.position);
				transform.position = pos - deltaPosition;
			}
		}

	}

	void On_DragEnd (Gesture gesture){
		if (gesture.isOverGui) {
			if (gesture.pickedUIElement == this.gameObject || gesture.pickedUIElement.transform.IsChildOf (transform)) {
				print ("it's done yall");

			}
		}


	}
}
