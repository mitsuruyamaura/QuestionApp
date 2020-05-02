using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public static GameData instance;

    [System.Serializable]
    public class ShioData{
        public int a;
        public int b;
        public int c;
        public int d;
        public string title;

        public ShioData(int a, int b, int c, int d, string title) {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.title = title;
        }
    }
    
    public ShioData shioData = new ShioData(50, 50, 50, 50, "");

    public QuestionDataList questionDataList;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
