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

    bool saveCat = false;
    bool giveLollipop = false;

    GameObject statueCat;
    GameObject statueSpider;
    GameObject statueHammer;
    GameObject statueLollipop;

    public InputDevice RController { get => rController; set => rController = value; }
    public InputDevice LController { get => lController; set => lController = value; }
    public bool GameEnds { get => gameEnds; set => gameEnds = value; }
    public bool SaveCat { get => saveCat; set => saveCat = value; }
    public bool GiveLollipop { get => giveLollipop; set => giveLollipop = value; }

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
        if (GameEnds) return;
        SetGameEnds();
        if(giveLollipop) { statueLollipop.SetActive(true); }
        else { statueHammer.SetActive(true); }
    }

    public void LeaveRoomTwo()
    {
        AudioManager.Instance.StopLoopSound();
        if (GameEnds) return;
        SetGameEnds();
        //Check if you saved the cat and notify the statue
        if (SaveCat)
        {
            GameData.questions[0].roomResponse = 1;
            statueCat.SetActive(true);
        }
        else
        {
            GameData.questions[0].roomResponse = 2;
            statueSpider.SetActive(true);

            //If they didn't cut any ropes
            if(GameData.questions[0].roomPct == -1)
            {
                //If they said they'd save the cat
                if (GameData.questions[0].questionResponse == 1)
                {
                    //They didn't save it so 0%
                    GameData.questions[0].roomPct = 0;
                }
                else
                {
                    //That's what they said so 100%
                    GameData.questions[0].roomPct = 1;
                }
            }
        }
        GameObject.Find("QuestionDisplay").GetComponent<QuestionDisplay>().Refresh();
    }

    public void SetGameEnds()
    {
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
        statueCat = GameObject.Find("Statue/Cat");
        statueSpider = GameObject.Find("Statue/Spider");
        statueHammer = GameObject.Find("Statue/Hammer");
        statueLollipop = GameObject.Find("Statue/Lollipop");
        statueCat.SetActive(false);
        statueSpider.SetActive(false);
        statueHammer.SetActive(false);
        statueLollipop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
