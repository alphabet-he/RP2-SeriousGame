using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject optionButton1;
    public GameObject optionButton2;
    public GameObject continueButton;
    public GameObject startText;
    public List<Question> questions;

    int questionNum = 0;
    bool fadeOut, fadeIn = false;

    void Start()
    {
        Button btn1 = optionButton1.GetComponent<Button>();
        btn1.onClick.AddListener(OnClick1);
        Button btn2 = optionButton2.GetComponent<Button>();
        btn2.onClick.AddListener(OnClick2);
        Button continueBtn = continueButton.GetComponent<Button>();
        continueBtn.onClick.AddListener(ContinuePressed);

        optionButton1.SetActive(false);
        optionButton2.SetActive(false);

        gameObject.GetComponent<TextMeshPro>().text = "";
    }

    void ContinuePressed()
    {
        gameObject.GetComponent<TextMeshPro>().text = questions[questionNum].ToString();
        startText.GetComponent<TextMeshPro>().text = "";
        continueButton.SetActive(false);
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
    }

    void Update()
    {
        if (fadeOut)
        {
            Color textColor = gameObject.GetComponent<TextMeshPro>().color;
            float fadeAmount = textColor.a - (Time.deltaTime);

            textColor = new Color(textColor.r, textColor.g, textColor.b, fadeAmount);
            gameObject.GetComponentInChildren<TextMeshPro>().color = textColor;

            if (fadeAmount <=0) 
            {
                if (questionNum >= questions.Count)
                {
                    //Store the questions/answers for later
                    GameData.questions = questions;

                    //Move to new scene
                    SceneManager.LoadScene("Anastasiia");
                }
                else
                {
                    fadeOut = false;
                    changeText();
                }
            }
        }
        else if (fadeIn)
        {
            Color textColor = gameObject.GetComponent<TextMeshPro>().color;
            float fadeAmount = textColor.a + (Time.deltaTime);

            textColor = new Color(textColor.r, textColor.g, textColor.b, fadeAmount);
            gameObject.GetComponent<TextMeshPro>().color = textColor;

            if (fadeAmount >= 255)
            {
                fadeIn = false;
            }
        }
    }

    void changeText()
    {
        gameObject.GetComponent<TextMeshPro>().text = questions[questionNum].ToString();
        fadeIn = true;
    }

    void NextQuestion()
    {
        fadeOut = true;
        questionNum++;
    }

    void OnClick1()
    {
        //Send Option 1 to the results
        if ( questionNum < questions.Count ) 
        {
            questions[questionNum].questionResponse = 1;
        }

        NextQuestion();
    }

    void OnClick2()
    {
        //Send Option 2 to the results
        if (questionNum < questions.Count)
        {
            questions[questionNum].questionResponse = 2;
        }

        NextQuestion();
    }
}
