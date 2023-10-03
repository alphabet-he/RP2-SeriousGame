using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameController : MonoBehaviour
{
    public static GameController instance;


    InputDevice rController;
    InputDevice lController;

    bool gameEnds = false;

    Button buttonOne;
    Button buttonTwo;
    GameObject roomOneHand;

    public InputDevice RController { get => rController; set => rController = value; }
    public InputDevice LController { get => lController; set => lController = value; }
    public bool GameEnds { get => gameEnds; set => gameEnds = value; }

    private void Awake()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
        else if (instance != null)
        {
            Destroy(gameObject); Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void SetToRoomOne()
    {
        if (!GameEnds)
        {
            buttonOne.interactable = false;
        }
        roomOneHand.SetActive(true);
    }

    public void SetToRoomTwo()
    {
        if (!GameEnds)
        {
            buttonTwo.interactable = false;
        }
        
    }

    public void LeaveRoomOne()
    {
        roomOneHand.SetActive(false);
    }

    public void LeaveRoomTwo()
    {
        AudioManager.Instance.StopLoopSound();
    }

    public void SetGameEnds()
    {
        gameEnds = true;
        buttonOne.interactable = true;
        buttonTwo.interactable = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        buttonOne = GameObject.Find("Sign/Canvas/Button").GetComponent<Button>();
        buttonTwo = GameObject.Find("Sign/Canvas1/Button").GetComponent<Button>();
        roomOneHand = GameObject.Find("LollipopRoom/Hand");
        buttonOne.interactable = true;
        buttonTwo.interactable = true;
        roomOneHand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
