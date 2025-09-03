using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInGame : UIBase
{
    private Player _player;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Image DashIcon;
    [SerializeField] private Image[] skillIcon = new Image[3];
    [SerializeField] private TextMeshProUGUI[] skillLevel = new TextMeshProUGUI[3];
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button pauseButton;

    public float dashCooldown = 1.2f;
    private float currentCooldown;
    private bool isDash = false;

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
        UpdateTimer(StageManager.Instance.StageTime);
        
        currentCooldown -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.V))
        {
            isDash = true;
        }
        UpdateDashCool();
    }

    private void UpdateDashCool()
    {
        if (currentCooldown > 0 && isDash)
        {
            DashIcon.fillAmount = currentCooldown / dashCooldown;
        }
        else
        {
            currentCooldown = dashCooldown;
            DashIcon.fillAmount = 1f;
            isDash = false;
        }
    }

    private void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    public void UpdateExp(float currentExp, float maxExp)
    {
        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;

        expText.text = $"{currentExp} / {maxExp}";
    }

    

    public void UpdateSkillIcon()
    {
        // 플레이어 보유 스킬 리스트 가져오기
        // 플레이어 리스트 가져오고
        // 반복문 -> 요소가 null이 아니라면
        // 인덱스 0부터 이미지, 텍스트 반영해주기
    }

    private void OpenPauseUI()
    {
        UIPause ui = UIManager.Instance.GetUI<UIPause>();
        ui.OpenUI();
        Time.timeScale = 0;
    }

}
