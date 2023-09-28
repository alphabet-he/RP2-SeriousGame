using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class QuestionDisplay : MonoBehaviour
{
    public TextMeshPro text;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        Debug.Log("Refresing");
        List<Question> questions = GameData.questions;
        string newText = "";
        foreach (Question question in questions)
        {
            newText += question.GetResultString() + "\n\n";
        }

        Debug.Log(gameObject.GetComponent<TextMeshPro>());
        Debug.Log(gameObject.GetComponent<TextMeshPro>().text);
        gameObject.GetComponent<TextMeshPro>().text = newText;
    }

    void Update()
    {
        
    }
}
