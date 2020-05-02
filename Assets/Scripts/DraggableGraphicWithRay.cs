using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableGraphicWithRay : MonoBehaviour {

    //ドラッグ開始地点とこのオブジェクトの原点との差分ベクトル
    private Vector2 mouseOffset;

    public bool isDragging;   // ドラッグ中かどうかの判定用
    public Rigidbody2D rb;    // ドラッグ中なら重力を無視する処理を追加するために使用

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // このゲームオブジェクトをタップしたら、PointerEventDataを新しく作る
            // PointerEventDataを使うことで、オブジェクトをタップしたイベント情報を取得できる
            PointerEventData pointer = new PointerEventData(EventSystem.current);

            // タップした位置にRayを飛ばす
            pointer.position = Input.mousePosition;

            // RaycastResultという型のリストを新規に作成する
            // 上記のタップで、ヒットしたゲームオブジェクトをすべて保存する
            List<RaycastResult> result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, result);

            // ヒットしたリストの最初のゲームオブジェクトと、このゲームオブジェクトが一緒なら
            if (result[0].gameObject == gameObject) {
                // このゲームオブジェクトのpositionとマウスカーソル位置の差分をmouseOffsetとして保存
                mouseOffset = transform.position - Input.mousePosition;

                // タップ状態とする
                isDragging = true;
            }
        }
        
        if (!Input.GetMouseButton(0)) {
            // このゲームオブジェクトがタップされなくなったら、タップ状態を解除する
            isDragging = false;
        }

        if (isDragging) {
            // タップ状態なら、重力を無視するようにする
            rb.isKinematic = true;

            // ゲームオブジェクトの位置情報をタップ位置に変換する
            transform.position = (Vector2)Input.mousePosition + mouseOffset;
        } else {
            // タップ状態が解除されたら、重力が働くようにする
            rb.isKinematic = false;
        }
    }
}
