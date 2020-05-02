using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 加速度センサーによる判定クラス
/// </summary>
public class AccelerateSensor : MonoBehaviour {

    public Word wordPrefab;         // Wordゲームオブジェクトのプレファブ
    public Text txtDebug;
    public float speed = 5.0f;      // 加速度判定用のこのゲームオブジェクトの移動速度
    private bool isWordCreate;      // Wordを生成したかどうかの判定用
    public Transform canvasTrans;   // Wordの生成する位置

    void Update() {
        // 初期化
        Vector3 dir = Vector3.zero;

        // Y軸はスマホを縦持ちすると反応してしまうので、他の軸のみ判定する
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.z;

        txtDebug.text = dir.x.ToString();
        txtDebug.text += "\n" + dir.y.ToString();
        txtDebug.text += "\n" + dir.z.ToString();

        // 傾きが大きくなって、Wordを生成していないなら
        if (dir.sqrMagnitude > 1 && !isWordCreate) {
            // 傾きを正規化(0か1にする)
            dir.Normalize();

            // Wordを生成          
            StartCoroutine(CreateWord());
        }

        // 傾きを時間で計測
        dir *= Time.deltaTime;

        // 傾き判定用のこのゲームオブジェクトを、傾きに合わせて移動
        transform.Translate(dir * speed);
    }

    /// <summary>
    /// Wordを生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateWord() {
        // 生成フラグを立てる。このフラグがtrueの間は傾きがあっても連続で生成しないようにする
        isWordCreate = true;

        // Wordを生成し、Wordのタイトルを渡す(TODO タイトル以外にも、メッセージやパラメータを渡すようにする)
        Word wordObj = Instantiate(wordPrefab, canvasTrans, false);
        wordObj.InitWord("誕生日");
        
        yield return new WaitForSeconds(1.0f);
        
        // 生成フラグを下す
        isWordCreate = false;
    }
}
