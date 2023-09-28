using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Robe : MonoBehaviour
{
    public int maxHealth = 3;
    public GameObject scissors;

    public XRDirectInteractor rHand;
    public XRDirectInteractor lHand;

    public XRRayInteractor rRay;
    public XRRayInteractor lRay;

    private int health;
    private bool triggered;
    private bool selectedByRDevice;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) // if the scissor is upon the robe
        {
            Debug.Log("Bool triggered is true!");
            if (scissors.GetComponent<XRGrabInteractable>().isSelected) // if the scissor is selected by interactor
            {
                // check which hand is holding it

                //IXRInteractor interactor = scissors.GetComponent<XRGrabInteractable>().interactorsSelecting[0];
                if(rRay.interactablesSelected.Contains(scissors.GetComponent<XRGrabInteractable>()) ||
                    rHand.interactablesSelected.Contains(scissors.GetComponent<XRGrabInteractable>()))
                {
                    selectedByRDevice = true;
                }
                else if(lRay.interactablesSelected.Contains(scissors.GetComponent<XRGrabInteractable>()) ||
                    lHand.interactablesSelected.Contains(scissors.GetComponent<XRGrabInteractable>()))
                {
                    selectedByRDevice = false;
                }
                else
                {
                    Debug.Log("No device is holding the scissor");
                    return;
                }


                // test whether the device of the hand is pushed trigger button
                {
                    if(!selectedByRDevice)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered!");
        triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }

}
