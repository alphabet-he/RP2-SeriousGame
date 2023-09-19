using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ObjectReleased(GameObject interactable)
    {
        //If something else is attached, detach it (or disable the other object, depending on how we want to do things)

        //Check interactable's tag/name
        //Based on tag name call TriggerChange1 or TriggerChange2
        //TriggerChange1 and TriggerChange2 could call into a different script that can be created based on what each receiver wants to do?
        //We could store a reference to two other scripts/objects as change1 and change2 and assign those on the receiver prefab so we can customize behavior
    }
}
