using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 RoomOneTeleport;
    public Vector3 RoomOneRotation;

    public Vector3 RoomTwoTeleport;
    public Vector3 RoomTwoRotation;

    void MovePosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    public void MoveRotation(Vector3 rotation) 
    {
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void ToRoomOne()
    {
        MovePosition(RoomOneTeleport);
        MoveRotation(RoomOneRotation);
    }

    public void ToRoomTwo()
    {
        MovePosition(RoomTwoTeleport);
        MoveRotation(RoomTwoRotation);
    }
}
