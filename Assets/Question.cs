using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question : MonoBehaviour
{
    public TMP_Text txtQuestion;

    public int questionNo;
    public string questionText;

    public void SetupQuestion(int questionNo, string questionText) {
        this.questionNo = questionNo;
        this.questionText = questionText;

        txtQuestion.text = questionText;
    }
}
