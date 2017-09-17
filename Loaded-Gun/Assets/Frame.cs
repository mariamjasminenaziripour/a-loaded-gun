using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

public class Frame : MonoBehaviour {
    public Sprite[] frameSprites = new Sprite[4];
    public GameObject[] framePanels = new GameObject[4];
    public Sprite currentSprite;
    private int spriteIterator;
    private GameObject currentPanel;

    void OnEnable()
    {
        EasyTouch.On_SimpleTap += On_SimpleTap;
        EasyTouch.On_LongTapEnd += On_LongTapEnd;
    }

    void OnDisable()
    {
        UnsubscribeEvent();
    }

    void OnDestroy()
    {
        UnsubscribeEvent();
    }

    void UnsubscribeEvent()
    {
        EasyTouch.On_SimpleTap -= On_SimpleTap;
        EasyTouch.On_LongTapEnd -= On_LongTapEnd;
    }


    // Use this for initialization
    void Start () {
        currentSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

    }
	
	// Update is called once per frame
	void Update () {
      


    }



    void On_SimpleTap(Gesture gesture)
    {
        if (gesture.pickedObject == this.gameObject)
        {
            spriteIterator++;
            switchSprite(spriteIterator);
        }
    }

    void switchSprite (int i)
    {

        if (spriteIterator <= 4)
        {
            switch (i)
            {
                case 0:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = frameSprites[0];
                    currentSprite = frameSprites[0];
                    print("work");
                    break;
                case 1:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = frameSprites[1];
                    currentSprite = frameSprites[1];
                    break;
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = frameSprites[2];
                    currentSprite = frameSprites[2];
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = frameSprites[3];
                    currentSprite = frameSprites[3];
                    break;
                case 4:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = frameSprites[4];
                    currentSprite = frameSprites[4];
                    break;

            }
        }
        else
        {
            spriteIterator = 0;
            switchSprite(spriteIterator);
        }
    }

    void On_LongTapEnd(Gesture gesture)
    {
        if (gesture.pickedObject == this.gameObject)
        {
            currentPanel = framePanels[spriteIterator];
            Popup.openPanel(currentPanel);
            print("ayyee");
        }
    }
}
