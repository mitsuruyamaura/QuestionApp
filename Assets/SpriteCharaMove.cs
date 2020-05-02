using UnityEngine;
using System.Collections;

/// <summary>
/// ２Ｄスプライト用のマウス移動処理
/// </summary>
public class SpriteCharaMove : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;

    /// <summary>
    /// Unityの持つメソッド
    /// </summary>
    private void OnMouseDown() {
        // マウスカーソルはスクリーン座標のため、対象のオブジェクトもスクリーン座標に変換して計算
        // オブジェクトの位置をスクリーン座標に変換
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log(screenPoint);
        // ワールド座標上の、マウスカーソルと対象の位置の差分
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    /// <summary>
    /// Unityの持つメソッド
    /// </summary>
    private void OnMouseDrag() {
        // 現在のオブジェクトの位置を取得
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        // 現在値をスクリーン座標に変換して差分を加算
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;


        // オブジェクトの位置を更新して移動
        transform.position = currentPosition;
        Debug.Log(this.transform.position);
    }
}
