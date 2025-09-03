using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIResult : UIBase
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button titleButton;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestart);
        titleButton.onClick.AddListener(OnTitle);
    }

    public void ScoreUpdate()
    {
        // 점수 받아오기 -> 반영
        //scoreText.text = 
    }

    private void OnRestart()
    {
        SceneLoadManager.Instance.RestartScene();
        Time.timeScale = 1.0f;
    }

    private void OnTitle()
    {
        SceneLoadManager.Instance.LoadScene(SceneType.Intro);
        Time.timeScale = 1.0f;
    }


}
