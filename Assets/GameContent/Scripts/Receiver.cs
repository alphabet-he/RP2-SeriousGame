using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public string option1;
    public string option2;

    private IChanger changer;

    void Start()
    {
        changer = gameObject.GetComponent<IChanger>();
        Debug.Log(changer);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check other's tag/name, based on tag name call TriggerChange1 or TriggerChange2
        //TriggerChange1 and TriggerChange2 call into a different script that can be created based on what each receiver wants to do
        if (other.gameObject.name == option1)
        {
            changer.TriggerChange1(other.gameObject);
            //NotifyStatue(change1)
        }
        else if (other.gameObject.name == option2)
        {
            changer.TriggerChange2(other.gameObject);
            //NotifyStatue(change2)
        }
    }
}
