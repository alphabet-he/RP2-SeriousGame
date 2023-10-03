using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandChanger : MonoBehaviour, IChanger
{
    public Quaternion handRotation;
    public Vector3 endPosition;
    Vector3 startPosition;

    int questionNum = 0;
    private bool shouldMoveOut = false;
    private bool shouldMoveBack = false;
    private bool isHit = false;
    int hitCount = 0;
    int feedCount = 0;
    GameObject heldItem;
    int handAnswer = 1;
    MeshRenderer meshRenderer;

    void Start()
    {
        startPosition = transform.position;
        handAnswer = GameData.questions[0].questionResponse;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public bool TriggerChange1(GameObject obj)
    {
        //Hit by Hammer
        Debug.Log("Hit by Hammer");
        AudioManager.Instance.PlaySFX("Hammer");
        hitCount++;
        if(obj.transform.parent.transform.parent != null)
        {
            GameObject.Destroy(obj.transform.parent.transform.parent.gameObject);
        }
        GameObject.Destroy(obj);

        if (hitCount >= 3)
        {
            gameObject.AddComponent<Rigidbody>();
            GameData.questions[questionNum].roomPct = GetRoomPercent();
            return true;
        }

        isHit = true;
        Invoke(nameof(StopHandHit), .5f);

        return false;
    }

    public bool TriggerChange2(GameObject obj)
    {
        Debug.Log("Hit by Lollipop");

        if (hitCount >= 3)
        {
            return false;
        }
        //Given Lollipop
        feedCount++;
        //This makes the lollipop part of the hand and removes interactability
        GameObject.Destroy(obj.GetComponent<XRGrabInteractable>());
        GameObject.Destroy(obj.GetComponent<Rigidbody>());
        //Get the child lollipop attach point and place it there
        obj.transform.position = gameObject.GetComponentsInChildren<Transform>()[0].position;
        obj.transform.SetParent(gameObject.transform, true);
        heldItem = obj;

        shouldMoveBack = true;

        if (feedCount >= 3)
        {
            GameData.questions[questionNum].roomPct = GetRoomPercent();
            return true;
        }
        return false;
    }

    public void OnRoomEnter()
    {
        meshRenderer.enabled = true;
        shouldMoveOut = true;
    }

    public void OnRoomExit() 
    {
        meshRenderer.enabled = false;
    }

    float GetRoomPercent()
    {
        //The percentage of the time that they do they say they'll do is
        //the amount of times they chose their option out of the total actions they took
        //Don't hurt the hand will be 1
        if(handAnswer == 1)
        {
            return (float)feedCount / (float)(feedCount + hitCount);
        }
        else
        {
            return (float)hitCount / (float)(feedCount + hitCount);
        }
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
        if(isHit)
        {
            MoveHandHit();
        }
    }

    void MoveHandOut()
    {
        Debug.Log(endPosition);
        Debug.Log(startPosition);
        if (transform.position != endPosition)
        {
            //Debug.Log(Vector3.MoveTowards(transform.position, endPosition, /*movementSpeed * */Time.deltaTime));
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
            Debug.Log(feedCount);
            shouldMoveBack = false;
            if(feedCount < 3)
            {
                shouldMoveOut = true;
            }
            GameObject.Destroy(heldItem);
        }
    }

    void MoveHandHit()
    {
        transform.rotation = Random.rotation;
    }

    void StopHandHit()
    {
        transform.rotation = handRotation;
        isHit = false;
    }
}
