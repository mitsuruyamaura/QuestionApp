using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Word : MonoBehaviour
{
    public Text txtWord;
    public string titleWord;
    public string message;
    public int[] palameters;

    void Start() {
        // Debug
        message = "ポケモン、ゲットだぜ\n" + "うひょーーーーー！<>" + "以上で、文字テスト終わります。";
    }

    public void InitWord(string word) {
        titleWord = word;       
        txtWord.text = titleWord;
    }
}
