using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using DG.Tweening;


public class Answer : MonoBehaviour
{
    public TMP_Text txtAnswer;
    public Button btnAnswer;

    [Header("回答の番号")]
    public int answerNo;
    [Header("回答の文章")]
    public string answer;
    [Header("増減するパラメータの種類とその数値")]
    public int[] changeParameters;
    public bool isLastQuestion;
    public string title;
    public bool isSubmit; // 重複タップ防止

    QuestionManager questionManager;

    /// <summary>
    /// 回答を設定する
    /// </summary>
    public void SetupAnswer(int answerNo, string answer, int[] parameters, bool isLastQuestion, string title, QuestionManager questionManager) {
        this.answerNo = answerNo;
        this.answer = answer;
        changeParameters = parameters;
        this.isLastQuestion = isLastQuestion;
        this.title = title;
        this.questionManager = questionManager;

        txtAnswer.text = answer;
        //btnAnswer.onClick.AddListener(() => StartCoroutine(OnClickAnswer()));
    }

    /// <summary>
    /// 回答タップ時
    /// </summary>
    //private IEnumerator OnClickAnswer() {
        //if (!isSubmit) {
        //    // 重複タップ防止
        //    isSubmit = true;

        //    // 残り時間を止める
        //    questionManager.gameState = QuestionManager.GameState.WAIT;

        //    // 他の回答もタップできないようにする
        //    questionManager.InactiveAnswer();

        //    // 回答に合わせたパラメータを変化させる
        //    for (int i = 0; i < changeParameters.Length; i++) {
        //        switch (i) {
        //            case 0:
        //                GameData.instance.shioData.a += changeParameters[0];
        //                break;
        //            case 1:
        //                GameData.instance.shioData.b += changeParameters[1];
        //                break;
        //            case 2:
        //                GameData.instance.shioData.c += changeParameters[2];
        //                break;
        //            case 3:
        //                GameData.instance.shioData.d += changeParameters[3];
        //                break;
        //        }
        //    }

        //    if (isLastQuestion) {
        //        GameData.instance.shioData.title = title;
        //    }
        //    yield return new WaitForSeconds(2.0f);

        //    // 次の質問に移るか、終了かチェック
        //    questionManager.CheckQuestionCount();
        //}
   // }
}
