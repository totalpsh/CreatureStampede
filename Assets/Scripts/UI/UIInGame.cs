using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInGame : UIBase
{
    Player _player;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Image DashIcon;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI skillLevel;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button pauseButton;

    private void Awake()
    {
        pauseButton.onClick.AddListener(OpenPauseUI);
    }

    public void SetCharacter(Player player)
    {
        _player = player;

        // 여기서 UI에 들어갈 값들 세팅

        // 업데이트
        //UpdateExp(_player.currentExp, _player.maxExp);
        //UpdateSkillIcon();
    }

    private void Update()
    {
        float time = StageManager.Instance.StageTime;
        UpdateTimer(time);
    }

    private void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = $"{minutes}:{seconds}";
    }

    void UpdateExp(float currentExp, float maxExp)
    {
        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;

        expText.text = $"{currentExp} / {maxExp}";
    }

    public void UpdateSkillIcon()
    {

    }

    private void OpenPauseUI()
    {
        UIPause ui = UIManager.Instance.GetUI<UIPause>();
        ui.OpenUI();
        Time.timeScale = 0;
    }

}
