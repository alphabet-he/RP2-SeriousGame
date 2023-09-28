using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
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
    private bool isCuttingR;
    private bool isCuttingL;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        isCuttingL = false;
        isCuttingR = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) // if the scissor is upon the robe
        {
            //Debug.Log("Bool triggered is true!");
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

                //Debug.Log(selectedByRDevice);

                // test whether the device of the hand is pushed trigger button
                
                if(!selectedByRDevice) // left hand hold the scissors
                {
                    GameController.instance.LController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
                    if(triggerValue > 0.2f)
                    {
                        
                        isCuttingL = true;
                    }
                    else
                    {
                        if (isCuttingL) // originally pressing trigger button, now released
                        {
                            isCuttingL = false;
                            Debug.Log("Cut robe by left hand!");
                            Cut();
                        }
                    }
                }
                else // right
                {
                    GameController.instance.RController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
                    if (triggerValue > 0.2f)
                    {
                        
                        isCuttingR = true;
                    }
                    else
                    {
                        if (isCuttingR) // originally pressing trigger button, now released
                        {
                            isCuttingR = false;
                            Debug.Log("Cut robe by right hand!");
                            Cut();
                        }
                    }
                }
                
            }
        }
        
    }

    void Cut()
    {
        health--;
        SpiderRoomController.instance.CutOnce();
        if(health <= 0)
        {
            gameObject.SetActive(false);
            SpiderRoomController.instance.CutOutRobe();
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
