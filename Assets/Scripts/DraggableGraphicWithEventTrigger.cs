using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DraggableGraphicWithEventTrigger : MonoBehaviour {

    //ドラッグ開始地点とこのオブジェクトの原点との差分ベクトル
    private Vector2 mouseOffset;

    void Start() {
        InitEventTrigger();
    }

    /// <summary>
    /// ドラッグが開始されたとき1回呼ばれるメソッド
    /// </summary>
    public void OnBeginDrag() {
        //このGameObjectのpositionとマウスカーソル位置の差分を保存
        mouseOffset = transform.position - Input.mousePosition;
    }

    /// <summary>
    /// ドラッグ中に毎フレーム呼ばれるメソッド
    /// </summary>
    public void OnDrag() {
        //保存した差分ベクトルをマウスカーソルに加算して移動位置を算出
        transform.position = (Vector2)Input.mousePosition + mouseOffset;
    }

    /// <summary>
    /// EventTriggerを登録するメソッド
    /// </summary>
    private void InitEventTrigger() {
        //EventTriggerにOnDragBeginとOnDragのイベントを追加
        EventTrigger evRef = GetComponent<EventTrigger>();

        //OnDragBegin
        EventTrigger.Entry entry_begindrag = new EventTrigger.Entry();
        entry_begindrag.eventID = EventTriggerType.BeginDrag;
        EventTrigger.TriggerEvent event_begindrag = new EventTrigger.TriggerEvent();
        event_begindrag.AddListener((eventData) => OnBeginDrag());
        entry_begindrag.callback = event_begindrag;
        evRef.triggers.Add(entry_begindrag);

        //OnDrag
        EventTrigger.Entry entry_drag = new EventTrigger.Entry();
        entry_drag.eventID = EventTriggerType.Drag;
        EventTrigger.TriggerEvent event_drag = new EventTrigger.TriggerEvent();
        event_drag.AddListener((eventData) => OnDrag());
        entry_drag.callback = event_drag;
        evRef.triggers.Add(entry_drag);
    }
}
