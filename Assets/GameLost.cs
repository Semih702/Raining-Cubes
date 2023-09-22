using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameLost : MonoBehaviour
{
    public int score;
    public int record;
    public TMP_Text scoreText;
    public TMP_Text recordText;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("lastScore");
        record = PlayerPrefs.GetInt("record");
        scoreText.text = "Score: " + score.ToString();
        recordText.text = "Record: " + record.ToString();
    }

    public void StartAgain()
    {
        SceneManager.LoadScene("InGame");
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
