using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public string option1;
    public string option2;
    public IChanger changer;

    void OnCollisionEnter(Collision other)
    {
        //Check other's tag/name, based on tag name call TriggerChange1 or TriggerChange2
        //TriggerChange1 and TriggerChange2 could call into a different script that can be created based on what each receiver wants to do?
        //We could store a reference to two other scripts/objects as change1 and change2 and assign those on the receiver prefab so we can customize behavior
        if (other.gameObject.name == option1)
        {
            changer.TriggerChange1();
        }
        else if (other.gameObject.name == option2)
        {
            changer.TriggerChange2();
        }
    }
}
