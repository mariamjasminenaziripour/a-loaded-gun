using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    void OnEnable()
    {
        EasyTouch.On_SimpleTap += On_SimpleTap;
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
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void On_SimpleTap(Gesture gesture)
    {
        if (gesture.isOverGui)
        {
            if ((gesture.pickedUIElement == gameObject || gesture.pickedUIElement.transform.IsChildOf(transform))) { 
                Popup.closePanel(this.gameObject);
            }

        }
    }
}
