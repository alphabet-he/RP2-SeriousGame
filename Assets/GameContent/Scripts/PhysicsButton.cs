using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Credit goes to https://www.youtube.com/watch?v=HFNzVMi5MSQ
 */

public class PhysicsButton : MonoBehaviour
{
    public float threshold = 0.2f; // the percentage of how much the press/release can spare
    public float deadzone = 0.025f; // the percentage of how much the press can be considered not pressed at all

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y > startPos.y + 0.01f)
        {
            transform.localPosition = startPos;
        }
        if (!isPressed && GetValue() + threshold >=1) // player just pressed the button
        {
            Pressed();
        }
        if (isPressed && GetValue() - threshold <= 0) // player just pressed the button
        {
            Release();
        }
    }

    float GetValue()
    {
        float value = Vector3.Distance( startPos, transform.localPosition ) / joint.linearLimit.limit;

        if(Mathf.Abs( value ) < deadzone) // the press is too slight to be considered a press
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1, 1);

    }

    void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    void Release()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
