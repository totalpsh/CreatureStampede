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
 
        // ���⼭ UI�� �� ���� ����

        // ������Ʈ
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
