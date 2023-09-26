using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics; // which input device
    public GameObject handModelPrefab; 

    private InputDevice targetDevice;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if(devices.Count > 0 )
        {
            targetDevice = devices[0];
        }

        spawnedHandModel = Instantiate(handModelPrefab, transform);
        spawnedHandModel.SetActive(true);
        handAnimator = spawnedHandModel.GetComponent<Animator>();
        
    }
    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHandAnimation();
        /*        if (!targetDevice.isValid)
                {
                    TryInitialize();
                }
                else
                {
                    spawnedHandModel.SetActive(true);
                    //UpdateHandAnimation();
                }*/
    }
}