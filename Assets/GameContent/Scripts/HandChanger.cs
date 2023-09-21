using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandChanger : MonoBehaviour, IChanger
{
    Vector3 startPosition;
    Vector3 endPosition;

    private bool shouldMoveOut = true;
    private bool shouldMoveBack = false;


    public void TriggerChange1()
    {
        //Hit by Hammer
        shouldMoveBack = true;
        //NotifyStatue(hammer)
    }

    public void TriggerChange2()
    {
        //Given Lollipop
        //TODO: Take the Lollipop from the player
        shouldMoveBack = true;
        //NotifyStatue(lollipop)
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
            //transform.position = Vector3.MoveTowards(transform.position, endPosition, movementSpeed * Time.deltaTime);
        }
    }

    void MoveHandBack()
    {

    }
}
