using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private static int _score;
    public static int Score {
        get { return _score; }
        set { _score = value; 
            Debug.Log("Score Changed: " + _score); }
    }

    public static void ChangeScore(int amount) {
        Score += amount;
    }
}
