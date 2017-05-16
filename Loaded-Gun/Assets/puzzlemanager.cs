using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class puzzlemanager : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void newScene (){
		SceneManager.LoadScene("StartScreen");

	}
}
