using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WordDropPlace : MonoBehaviour
{
    public ShioManager shioManager;
    private bool isDroped;

    private void OnTriggerEnter2D(Collider2D col) {
        if (isDroped) {
            return;
        }

        if (col.gameObject.tag == "Word") {
            Word dropWord = col.gameObject.GetComponent<Word>();
            if (dropWord != null) {
                Debug.Log("Get Word");
                col.GetComponent<BoxCollider2D>().enabled = false;
                isDroped = true;
                StartCoroutine(ChooseDropWord(dropWord));               
            }
        }
    }

    private IEnumerator ChooseDropWord(Word dropWord) {
        shioManager.TalkFromChooseWord(dropWord);

        Sequence seq = DOTween.Sequence();
        seq.Append(dropWord.gameObject.GetComponent<RectTransform>().
            DOAnchorPos(new Vector2(0.5f, 0.5f), 1.0f));
        seq.Join(dropWord.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 1070), 1.0f, RotateMode.FastBeyond360));
        seq.Join(dropWord.gameObject.transform.DOScale(0, 1.0f));

        yield return new WaitForSeconds(1.0f);
        Destroy(dropWord.gameObject);
        isDroped = false;
    }
}
