using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "CreateScriptableObject/QuestionDataList")]
public class QuestionDataList : ScriptableObject {

    public List<QuestionData> questionDatas = new List<QuestionData>();

    [System.Serializable]
    public class QuestionData {
        public int questionNo;
        public string questionText; 
        public string[] answers;
        public int rarerity;
        public string title = "";
        public int[] answerParameters_0;
        public int[] answerParameters_1;
        public int[] answerParameters_2;
    }
}
