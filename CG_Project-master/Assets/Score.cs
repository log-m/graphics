using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text UI;
    public static int score = 0;
    public int maxScore;

    void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("Collision!");
        score++;
        UI.text = "Orbs found: " + score + "/" + maxScore;
        Destroy(gameObject);
        if (score == maxScore)
            SceneManager.LoadScene(0);
    }
}
