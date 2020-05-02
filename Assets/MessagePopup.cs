using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                    // 追加
using System.Text.RegularExpressions;    // 追加

/// <summary>
/// Wordのメッセージ情報を表示するクラス
/// </summary>
public class MessagePopup : MonoBehaviour
{
    public Text txtMessage;      // 吹き出しメッセージ表示
    public Image imgTapIcon;     // メッセージが全部表示されたらタップを促すアイコン表示 

    [TextArea(1, 20)]
    public string allMassage;    // メッセージ全文。段落に分かれていない長文のもの。

    public string splitString = "<>";   // allMessageをページ区切りするための文字列
    private string[] splitMessage;      // ページ区切りされた文字列が入る配列
    private int messageNum;             // メッセージの現在のページ数
    public float textSpeed = 0.05f;     // 1文字表示する際の文字スピード
    private float elapsedTime = 0;      // 文字表示用の経過時間
    private int nowTextNum = 0;         // 現在のページの文字数
    public float clickFlachTime = 0.2f; // タップを促すアイコンの点滅時間
    private bool isOneMessage = false;  // 1ページの表示が終わるとtrueになるフラグ
    private bool isEndMAssage = false;　// すべてのページの表示が終わるとtrueになるフラグ。このフラグがたったときにタップするとウインドウが消える。

    void Start() {
        // 各データを初期化する
        allMassage = null;
        imgTapIcon.enabled = false;
        txtMessage.text = "";
        //SetMessage(allMassage);
    }

    void Update() {
        // すべてのメッセージ表示が終わっているか、表示するメッセージがないなら処理しない
        if (isEndMAssage || allMassage == null) {
            return;
        }

        // １回に表示するメッセージ(1ページ分)を表示していない場合
        if (!isOneMessage) {
            // 時間を計測する
            elapsedTime += Time.deltaTime;

            // 計測している時間が、テキスト表示時間を経過したらメッセージ(1文字ずつ)を追加していく
            if (elapsedTime >= textSpeed) {
                txtMessage.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;

                // 1ページ分のメッセージを全部表示、または行数が最大数表示されたら
                if (nowTextNum >= splitMessage[messageNum].Length) {
                    // 1ページ分表示終了のフラグを立てる
                    isOneMessage = true;
                }
            }
            
            // メッセージ表示中にマウスの左ボタンか画面タップされたら一括表示
            if (Input.GetMouseButtonDown(0)) {
                // ここまでに表示しているテキストに残りのメッセージを追加
                txtMessage.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }

        // 1回に表示するメッセージを表示した場合
        } else {
            // 時間を計測する
            elapsedTime += Time.deltaTime;

            // タップアイコンを表示する時間を超えるたびに点滅させる
            if (elapsedTime >= clickFlachTime) {
                imgTapIcon.enabled = !imgTapIcon.enabled;
                elapsedTime = 0f;
            }

            // タップされたら次の文字表示処理
            if (Input.GetMouseButtonDown(0)) {
                // 表示した文字数を0に戻す
                nowTextNum = 0;

                // ページ数を1つ増やす
                messageNum++;

                // 表示中のメッセージを破棄する
                txtMessage.text = "";

                // タップを促すアイコンを非表示
                imgTapIcon.enabled = false;

                // 計測用の時間を0に戻す
                elapsedTime = 0f;

                // 1ページのメッセージをすべて表示した判定をするフラグを解除
                isOneMessage = false;

                // Word内のメッセージが全部表示されたら
                if (messageNum >= splitMessage.Length) {
                    // すべてのメッセージを表示したフラグをたてる
                    isEndMAssage = true;

                    // メッセージポップアップを非表示にする
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Wordから届いた情報を設定して表示する準備をする
    /// </summary>
    /// <param name="message"></param>
    private void SetMessage(string message) {
        // Wordクラスからメッセージ全文を受け取る
        this.allMassage = message;

        // 1列になっている文字列を文字列単位で、1回に表示するメッセージに分割する
        splitMessage = Regex.Split(allMassage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);

        // 初期化処理
        nowTextNum = 0;
        messageNum = 0;
        txtMessage.text = "";
        isOneMessage = false;
        isEndMAssage = false;            
    }

    /// <summary>
    /// Wordのメッセージを受け取り、メッセージポップアップを表示する
    /// </summary>
    /// <param name="message"></param>
    public void SetMessagePopup(string message) {
        // 受け取ったメッセージの表示を準備する
        SetMessage(message);

        // メッセージポップアップを表示する
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
