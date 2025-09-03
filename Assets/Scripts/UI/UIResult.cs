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

    private void ScoreUpdate()
    {

    }

    private void OnRestart()
    {

    }

    private void OnTitle()
    {

    }


}
