using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorUnlock : MonoBehaviour {

    private AudioSource unlocking;

    // Use this for initialization
    void Start () {
        unlocking = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Interaction()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {

        unlocking.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main");

    }
}
