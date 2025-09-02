using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : UIBase
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button titleButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue);
        restartButton.onClick.AddListener(OnRestart);
        titleButton.onClick.AddListener(OnTitle);
    }

    public void OnContinue()
    {
        CloseUI();
        Time.timeScale = 1.0f;
    }

    public void OnRestart()
    {
        SceneLoadManager.Instance.RestartScene();
    } 

    public void OnTitle()
    {
        SceneLoadManager.Instance.LoadScene(SceneType.Intro);
        Time.timeScale = 1.0f;
    }

}
