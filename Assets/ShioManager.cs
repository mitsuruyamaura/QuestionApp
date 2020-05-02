using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShioManager : MonoBehaviour
{
    public MessagePopup messagePopup;

    void Start() {
        // MessagePopupを非表示にする
        messagePopup.transform.GetChild(0).gameObject.SetActive(false);
    }

    /// <summary>
    /// Wordクラスのメソッドから呼び出される
    /// Wordの持つメッセージを受け取る
    /// </summary>
    public void TalkFromChooseWord(Word dropWord) {
        Debug.Log(dropWord.message);

        // MessagePopupのメソッドを呼び出してWordのメッセージ情報を渡す
        messagePopup.SetMessagePopup(dropWord.message);
    }
}
