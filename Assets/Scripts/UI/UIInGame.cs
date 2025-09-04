using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInGame : UIBase
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Image DashIcon;
    [SerializeField] private Image[] skillIcon = new Image[3];
    [SerializeField] private Image[] skillLevelBG = new Image[3];
    [SerializeField] private TextMeshProUGUI[] skillLevelText = new TextMeshProUGUI[3];
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button pauseButton;
    
    private Player _player;
    private List<BaseWeapon> weapons;   // 플레이어가 가진 스킬 받아올 필드

    public float dashCooldown;
    private float currentCooldown;
    private bool dashEnd = false;

    private int level;
    private int maxExp;
    private int curExp;
    private int score;
     
    private void Awake()
    {
        _player = PlayerManager.Instance.Player;

        pauseButton.onClick.AddListener(OpenPauseUI);

        StageManager.Instance.OnLevelExp += UpdateExp;

        for(int i = 0; i < skillIcon.Length; i++)
        {
            skillIcon[i].color = new Color(255, 255, 255, 0);
            skillLevelBG[i].color = new Color(255, 255, 255, 0);
            skillLevelText[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        weapons = _player.Weapons;
        UpdateWeaponIcon();
        UpdateExp();

        StageManager.Instance.OnExpChange += OnExpChanged;
        StageManager.Instance.OnMaxExpChange += OnMaxExpChanged;
        StageManager.Instance.OnScoreChange += OnScoreChanged;
        StageManager.Instance.OnLevelChanged += OnPlayerLevelUp;

    }

    private void OnDisable()
    {
        StageManager.Instance.OnExpChange -= OnExpChanged;
        StageManager.Instance.OnMaxExpChange -= OnMaxExpChanged;
        StageManager.Instance.OnScoreChange -= OnScoreChanged;
        StageManager.Instance.OnLevelChanged -= OnPlayerLevelUp;
    }

    public void SetCharacter(Player player)
    {
        _player = player;
        _player.controller.dashAction += DashEndEvent; 
        dashCooldown = _player.controller.dashCooldown;

        // 여기서 UI에 들어갈 값들 세팅

        // 업데이트
        //
    }

    private void Update()
    {
        UpdateTimer(StageManager.Instance.StageTime);

        if (dashEnd)
        {
            currentCooldown += Time.deltaTime;
        }
        UpdateDashIcon();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UIManager.Instance.OpenUI<UIGetItem>();
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            UIManager.Instance.OpenUI<UILevelUp>();
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            UIResult ui = UIManager.Instance.GetUI<UIResult>();
            ui.ClearUI();
            Time.timeScale = 0f;
        }
    }

    public void DashEndEvent()
    {
        dashEnd = true;
    }

    private void UpdateDashIcon()
    {
        if (currentCooldown < 1 && dashEnd)
        {
            DashIcon.fillAmount = currentCooldown / dashCooldown;
        }
        else
        {
            currentCooldown = 0;
            DashIcon.fillAmount = 1f;
            dashEnd = false;
        }
    }

    private void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }

    public void UpdateLevel()
    {
        UIManager.Instance.OpenUI<UILevelUp>();
        Time.timeScale = 0f;
    }

    

    public void UpdateExp()
    {
        int maxExp = StageManager.Instance.MaxExp;
        int curExp = StageManager.Instance.CurrentExp;
        int score = StageManager.Instance.Score;

        levelText.text = StageManager.Instance.Level.ToString();
        expSlider.maxValue = StageManager.Instance.MaxExp;
        expSlider.value = StageManager.Instance.CurrentExp;

        expText.text = $"{curExp} / {maxExp}";

        scoreText.text = score.ToString();
    }

    public void UpdateWeaponIcon()
    {
        if(weapons == null)
        {
            Debug.Log("버그 : 장비 없음");
        }

        for(int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] == null) continue;

            skillIcon[i].sprite = weapons[i].Data.WeaponData.icon;
            skillIcon[i].color = new Color(255, 255, 255, 255);
            skillLevelBG[i].color = new Color(255, 255, 255, 255);
            skillLevelText[i].gameObject.SetActive(true);

            if(_player.GetWeaponLevel(weapons[i].Data) < weapons[i].Data.WeaponData.maxLevel)
                skillLevelText[i].text = $"Lv. {_player.GetWeaponLevel(weapons[i].Data)}";
            else
                skillLevelText[i].text = $"Max";
        }
    }

    private void OpenPauseUI()
    {
        UIPause ui = UIManager.Instance.GetUI<UIPause>();
        ui.OpenUI();
        Time.timeScale = 0;
    }

    public void OnPlayerLevelUp(int level)
    {
        levelText.text = level.ToString();

        UIManager.Instance.OpenUI<UILevelUp>();
        Time.timeScale = 0f;
    }

    public void OnExpChanged(int currentExp)
    {
        expText.text = $"{currentExp} / {StageManager.Instance.MaxExp}";
        expSlider.value = currentExp;
    }

    public void OnMaxExpChanged(int maxExp)
    {
        expText.text = $"{StageManager.Instance.CurrentExp} / {maxExp}";
        expSlider.maxValue = maxExp;
    }

    public void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }
}
