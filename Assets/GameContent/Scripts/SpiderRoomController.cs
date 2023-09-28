using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRoomController : MonoBehaviour
{
    public static SpiderRoomController instance;

    int robeNums;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CutOutRobe() 
    {
        robeNums--;
        if(robeNums <= 0)
        {
            SaveDog();
        }
    }

    void SaveDog()
    {
        Debug.Log("Dog saved!");
    }
}
