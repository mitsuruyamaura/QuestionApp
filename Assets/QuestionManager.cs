using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public int questionCount;
    public int maxQuestionCount;
    public float limitTime;
    float timer;

    public TMP_Text txtInfo;
    public TMP_Text txtTimer;

    public enum GameState {
        WAIT,
        PLAY,
        RESULT
    }
    public GameState gameState;

    // 質問リスト
    List<QuestionDataList.QuestionData> normalQuestions = new List<QuestionDataList.QuestionData>();
    List<QuestionDataList.QuestionData> lastQuestions = new List<QuestionDataList.QuestionData>();

    public Question questionPrefab;
    public Answer answerPrefab;

    public Transform questionPlace;
    public Transform answerPlace;

    [Header("質疑応答リスト")]
    public List<GameObject> questionsAndAnswers;

    void Start() {
        gameState = GameState.WAIT;           // Updateが動かないようにする
        questionCount = 0;
        timer = limitTime;                    // 回答できる時間の設定
        txtInfo.gameObject.SetActive(false);

        // 質問リストを作成
        CreateQuestionList();
    }

    /// <summary>
    /// 質問リストを作成
    /// </summary>
    private void CreateQuestionList() {              
        foreach (QuestionDataList.QuestionData question in GameData.instance.questionDataList.questionDatas) {
            // 通常の問題はレア０か１だけ
            if (question.rarerity < 2) {
                normalQuestions.Add(question);
            }
            // 最後の問題はレア２のものだけ
            if (question.rarerity == 2) {
                lastQuestions.Add(question);
            }
        }
        // 最初の問題を作成
        SetQuestionAndAnswer();
    }

    void Update() {
        if (gameState == GameState.PLAY) {
            // 残り時間のカウントダウンとカウントダウン表示
            timer -= Time.deltaTime;
            txtTimer.text = timer.ToString("0.0");
            if (timer <= 0) {
                // 残り時間
                gameState = GameState.WAIT;
                StartCoroutine(TimeUp());            
            }
        }
    }

    /// <summary>
    /// 時間切れ
    /// </summary>
    /// <returns></returns>
    public IEnumerator TimeUp() {
        // タップできないようにする
        InactiveAnswer();

        // 時間切れ表示
        txtInfo.gameObject.SetActive(true);
        txtInfo.text = "Time Up!";

        yield return new WaitForSeconds(2.0f);
        // 表示を消す
        txtInfo.gameObject.SetActive(false);

        // 質問数のチェック
        CheckQuestionCount();
    }

    /// <summary>
    /// 質問数のチェック
    /// </summary>
    public void CheckQuestionCount() {
        // 質問と回答をクリアする
        if (questionsAndAnswers.Count > 0) {
            for (int i = 0; i < questionsAndAnswers.Count; i++) {
                Destroy(questionsAndAnswers[i]);
            }
            questionsAndAnswers.Clear();
        }
        // 残り時間を元に戻す
        timer = limitTime;

        // 表示した質問数が設定数以上か
        if (maxQuestionCount <= questionCount) {
            // 設定数以上なら終了
            Result();
        } else {
            // 設定数未満の場合には、次の質問を呼び出す
            SetQuestionAndAnswer();
        }
    }

    /// <summary>
    /// 質問と回答を作成
    /// </summary>
    public void SetQuestionAndAnswer() {
        // 表示した質問の数のトータルを加算
        questionCount++;

        // 質問リストから質問をランダムに選ぶ
        int questionNumber = 0;
        QuestionDataList.QuestionData questionData = new QuestionDataList.QuestionData();

        if (maxQuestionCount == questionCount) {
            // 最後の問題
            questionNumber = Random.Range(0, lastQuestions.Count);
            questionData = lastQuestions[questionNumber];
            lastQuestions.Remove(lastQuestions[questionNumber]);
        } else {
            // 通常の問題
            questionNumber = Random.Range(0, normalQuestions.Count);
            questionData = normalQuestions[questionNumber];
            normalQuestions.Remove(normalQuestions[questionNumber]);
        }

        // 質問をインスタンスして表示設定
        Question questionObj = Instantiate(questionPrefab, questionPlace, false);
        questionObj.SetupQuestion(questionData.questionNo, questionData.questionText);

        // 質疑応答リストに追加 
        questionsAndAnswers.Add(questionObj.gameObject);

        // 最後の問題か確認
        bool isLastQuestion = maxQuestionCount <= questionCount;

        // 回答をインスタンス
        for (int i = 0; i < questionData.answers.Length; i++) {
            Answer answerObj = Instantiate(answerPrefab, answerPlace, false);
            int[] parameter = new int[1];
            switch (i) {
                case 0:
                    parameter = questionData.answerParameters_0;
                    break;
                case 1:
                    parameter = questionData.answerParameters_1;
                    break;
                case 2:
                    parameter = questionData.answerParameters_2;
                    break;
            }
            // 回答の表示設定
            answerObj.SetupAnswer(i, questionData.answers[i], parameter, isLastQuestion, questionData.title, this);

            // 質疑応答リストに追加
            questionsAndAnswers.Add(answerObj.gameObject);
        }

        // ゲームを再開する
        gameState = GameState.PLAY;      
    }

    /// <summary>
    /// 回答をタップできないようにする
    /// </summary>
    public void InactiveAnswer() {
        for (int i = 0; i < questionsAndAnswers.Count; i++) {
            if (questionsAndAnswers[i].GetComponent<Answer>()) {
                questionsAndAnswers[i].GetComponent<Answer>().btnAnswer.interactable = false;
            }
        }
    }

    /// <summary>
    /// 結果発表
    /// </summary>
    public void Result() {
        gameState = GameState.RESULT;

        // TODO リザルト処理
        txtInfo.gameObject.SetActive(true);
        txtInfo.text = "Result";
        txtTimer.text = "";
    }
}
