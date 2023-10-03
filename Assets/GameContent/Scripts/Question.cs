using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string question;
    public string option1;
    public string option2;
    public int questionResponse;
    public int roomResponse;
    public float roomPct;

    public override string ToString()
    {
        return question + "\n\n" + option1 + "\n\n" + option2;
    }

    public string GetResultString()
    {
        return question + "\n\n" + (questionResponse == 1 ? option1 : option2);
    }
}
