using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public GameObject slot;
	private Image worldSprite;
	public GameObject[] mySlots;
	private GameObject currentSlot;
	public List<Image> Items;


	// Use this for initialization
	void Start () {
		Items = new List<Image> ();
		mySlots = GameObject.FindGameObjectsWithTag ("slot");
		for(int i = 0; i < mySlots.Length; i++)
		{
	//	Debug.Log ("Slot:" + i);
		}
	
	
	}
	
	// Update is called once per frame
	void Update () {

		foreach(GameObject item in mySlots)
		{
			if (item.transform.childCount > 0) {
				item.GetComponent<slots> ().occupied = true;
				Debug.Log ("Slot" + item + "has a kid!");
			}
			else {
				item.GetComponent<slots> ().occupied = false;
//				currentSlot = item;
//				Debug.Log (currentSlot);
//				return;
				for (int i = 0; i < mySlots.Length; i++) {
					GameObject tempSlot = mySlots [i];
					bool occupiedCheck = tempSlot.GetComponent<slots> ().occupied;
					if( occupiedCheck == false){
						currentSlot = tempSlot;
						Debug.Log ("Slot" + currentSlot + " is tha current slot yo");
						break;
					}
				}
			
			}
		}


		
	
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		print ("bork bork");
		if (collider.gameObject.CompareTag("collectable")){
			
			worldSprite = collider.GetComponent<Drag> ().iconSprite;
		//	Items.Add (worldSprite);
			print (Items);
			Image newIcon = Instantiate(worldSprite) as Image;
			newIcon.transform.SetParent (currentSlot.transform, false);
			Destroy (collider.gameObject);
		}


	}
}
