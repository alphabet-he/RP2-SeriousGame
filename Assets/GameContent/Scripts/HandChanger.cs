using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandChanger : MonoBehaviour, IChanger
{
    public Vector3 endPosition;
    Vector3 startPosition;

    private bool shouldMoveOut = true;
    private bool shouldMoveBack = false;
    FixedJoint fixedJoint;

    void Start()
    {
        startPosition = transform.position;
    }

    public void TriggerChange1(GameObject obj)
    {
        //Hit by Hammer
        shouldMoveBack = true;        
    }

    public void TriggerChange2(GameObject obj)
    {
        //Given Lollipop
        //This makes the lollipop part of the hand and removes interactability
        GameObject.Destroy(obj.GetComponent<XRGrabInteractable>());
        GameObject.Destroy(obj.GetComponent<Rigidbody>());
        //Get the child lollipop attach point and place it there
        obj.transform.position = gameObject.GetComponentsInChildren<Transform>()[0].position;
        obj.transform.SetParent(gameObject.transform, true);

        shouldMoveBack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMoveOut)
        {
            MoveHandOut();
        }
        else if (shouldMoveBack)
        {
            MoveHandBack();
        }
    }

    void MoveHandOut()
    {
        if (transform.position != endPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, /*movementSpeed * */Time.deltaTime);
        }
        else
        {
            shouldMoveOut = false;
        }
    }

    void MoveHandBack()
    {
        if (transform.position != startPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, /*movementSpeed * */Time.deltaTime);
        }
        else
        {
            shouldMoveBack = false;
        }
    }
}
