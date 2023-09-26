using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 RoomOneTeleport;
    public Vector3 RoomTwoTeleport;

    void MovePosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    public void ToRoomOne()
    {
        MovePosition(RoomOneTeleport);
    }

    public void ToRoomTwo()
    {
        MovePosition(RoomTwoTeleport);
    }
}
