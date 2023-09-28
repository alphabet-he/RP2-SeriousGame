using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandChanger : MonoBehaviour, IChanger
{
    public Vector3 endPosition;
    Vector3 startPosition;

    int questionNum = 0;
    private bool shouldMoveOut = true;
    private bool shouldMoveBack = false;
    int hitCount = 0;
    int feedCount = 0;
    GameObject heldItem;

    void Start()
    {
        startPosition = transform.position;
    }

    public bool TriggerChange1(GameObject obj)
    {
        //Hit by Hammer
        hitCount++;
        GameObject.Destroy(obj);

        if(hitCount >= 3)
        {
            gameObject.AddComponent<Rigidbody>();
            return true;
        }
        if(hitCount + feedCount >= 3)
        {
            GameData.questions[questionNum].roomPct = hitCount / feedCount;
            return true;
        }
        return false;
    }

    public bool TriggerChange2(GameObject obj)
    {
        //Given Lollipop
        //This makes the lollipop part of the hand and removes interactability
        GameObject.Destroy(obj.GetComponent<XRGrabInteractable>());
        GameObject.Destroy(obj.GetComponent<Rigidbody>());
        //Get the child lollipop attach point and place it there
        obj.transform.position = gameObject.GetComponentsInChildren<Transform>()[0].position;
        obj.transform.SetParent(gameObject.transform, true);
        heldItem = obj;

        shouldMoveBack = true;

        if (hitCount + feedCount >= 3)
        {
            GameData.questions[questionNum].roomPct = hitCount / feedCount;
            return true;
        }
        return false;
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
            shouldMoveOut = true;
            GameObject.Destroy(heldItem);
        }
    }
}
