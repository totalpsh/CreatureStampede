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

    public float dashCooldown;
    private float currentCooldown;
    private bool dashEnd = false;

    private void Awake()
    {
        
        pauseButton.onClick.AddListener(OpenPauseUI);
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
