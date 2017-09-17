using UnityEngine;
using System.Collections;


public static class Popup {

	public static void openPanel (GameObject panel){
			panel.SetActive (true);
		}
	
	

    public static void  closePanel (GameObject panel) {
		panel.SetActive (false);
	}
}
