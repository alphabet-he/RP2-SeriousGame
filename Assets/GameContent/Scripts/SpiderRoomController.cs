using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpiderRoomController : MonoBehaviour
{
    public static SpiderRoomController instance;

    public GameObject spiderPrefab;
    public Vector3 spiderInitLoc;
    public Vector3 spiderMoveSpeed;

    int robeNums;
    List<GameObject> spiderRows = new List<GameObject>();
    public GameObject buttonTextObject;
    TextMeshProUGUI buttonText;

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

    // Start is called before the first frame update
    void Start()
    {
        robeNums = transform.childCount;
        buttonText = buttonTextObject.GetComponent<TextMeshProUGUI>();
        //Debug.Log(robeNums);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CutOnce()
    {
        if(spiderRows.Count == 0)
        {
            AudioManager.Instance.PlayLoopSound("Spider");
            buttonText.text = "Spiders...????\r\nLeave the damn cat\r\n↓↓↓";
        }
        // move existing spiders
        foreach (GameObject go in spiderRows)
        {
            Vector3 newPos = go.transform.position + spiderMoveSpeed;
            go.transform.position = newPos;
        }
        // spiders appear 
        GameObject newRow = Instantiate(spiderPrefab, spiderInitLoc, Quaternion.identity);
        spiderRows.Add(newRow);
    }

    public void CutOutRobe() 
    {
        robeNums--;
        if(robeNums <= 0) // all robes are cut out
        {
            SaveCat();
        }
    }

    void SaveCat()
    {
        foreach (GameObject go in spiderRows)
        {
            Destroy(go);
        }
        spiderRows.Clear();
        AudioManager.Instance.StopLoopSound();
        buttonText.text = "You saved the cat!\r\nIt will meet you outside\r\n↓↓↓";
        //Debug.Log("Dog saved!");
    }
}
