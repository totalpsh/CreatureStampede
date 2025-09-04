using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class UIResult : UIBase
{
    [SerializeField] private TextMeshProUGUI successText;
    [SerializeField] private TextMeshProUGUI failedText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button titleButton;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestart);
        titleButton.onClick.AddListener(OnTitle);

        StageManager.Instance.OnStageClear += ClearUI;
        StageManager.Instance.OnGameOver += GameOverUI;
    }

    public void ClearUI()
    {
        OpenUI();
        SetScore();
        failedText.enabled = false;
        successText.enabled = true;
        Time.timeScale = 0f;
    }

    public void GameOverUI()
    {
        OpenUI();
        SetScore();
        successText.enabled = false;
        failedText.enabled = true;
        Time.timeScale = 0f;
    }

    public void SetScore()
    {
        scoreText.text = StageManager.Instance.Score.ToString();
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
