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
    private List<SkillData> _skills;   // �÷��̾ ���� ��ų �޾ƿ� �ʵ�
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

    public float dashCooldown;
    private float currentCooldown;
    private bool dashEnd = false;

    private void Awake()
    {
        pauseButton.onClick.AddListener(OpenPauseUI);

        for(int i = 0; i < skillIcon.Length; i++)
        {
            skillIcon[i].color = new Color(255, 255, 255, 0);
            skillLevelBG[i].color = new Color(255, 255, 255, 0);
            skillLevelText[i].gameObject.SetActive(false);
        }
    }

    public void SetCharacter(Player player)
    {
        _player = player;
        _player.controller.dashAction += DashEndEvent; 
        dashCooldown = _player.controller.dashCooldown;

        // ���⼭ UI�� �� ���� ����

        // ������Ʈ
        //UpdateExp(_player.currentExp, _player.maxExp);
        //UpdateSkillIcon();
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

    // ���Ͱ� ����� �� �ÿ� �̺�Ʈ�� ȣ��
    public void GetExpEvent()
    {

    }

    public void UpdateExp(float currentExp, float maxExp)
    {
        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;

        expText.text = $"{currentExp} / {maxExp}";
    }

    

    public void UpdateSkillIcon()
    {
        if(_skills == null)
        {
            
        }

        // �÷��̾� ���� ��ų ����Ʈ ��������
        // �÷��̾� ����Ʈ ��������
        // �ݺ��� -> ��Ұ� null�� �ƴ϶��
        // �ε��� 0���� �̹���, �ؽ�Ʈ �ݿ����ֱ�
    }

    private void OpenPauseUI()
    {
        UIPause ui = UIManager.Instance.GetUI<UIPause>();
        ui.OpenUI();
        Time.timeScale = 0;
    }

}
