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
    bool beenRoomOne = false;
    bool beenRoomTwo = false;

    public GameObject sign1;
    public GameObject sign2;
    public Material sign1BrightShader;
    public Material sign2BrightShader;
    public Material signdDarkShader;

    Button buttonOne;
    Button buttonTwo;
    public HandChanger roomOneHand;
    public GameObject endingText;

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
        beenRoomOne = true;
        if (!GameEnds)
        {
            sign1.GetComponent<MeshRenderer>().material = signdDarkShader;
            buttonOne.interactable = false;
        }
        roomOneHand.OnRoomEnter();
    }

    public void SetToRoomTwo()
    {
        beenRoomTwo=true;
        if (!GameEnds)
        {
            sign2.GetComponent<MeshRenderer>().material = signdDarkShader;
            buttonTwo.interactable = false;
        }
        
    }

    public void LeaveRoomOne()
    {
        roomOneHand.OnRoomExit();
        SetGameEnds();
    }

    public void LeaveRoomTwo()
    {
        AudioManager.Instance.StopLoopSound();
        SetGameEnds() ;
    }

    public void SetGameEnds()
    {
        if (GameEnds) return;
        if(beenRoomOne && beenRoomTwo)
        {
            gameEnds = true;
            buttonOne.interactable = true;
            buttonTwo.interactable = true;
            sign1.GetComponent<MeshRenderer>().material = sign1BrightShader;
            sign2.GetComponent<MeshRenderer>().material = sign2BrightShader;
            endingText.SetActive(true);
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        buttonOne = GameObject.Find("Sign/Canvas/Button").GetComponent<Button>();
        buttonTwo = GameObject.Find("Sign/Canvas1/Button").GetComponent<Button>();
        roomOneHand = GameObject.Find("Hand").GetComponent<HandChanger>();
        buttonOne.interactable = true;
        buttonTwo.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
