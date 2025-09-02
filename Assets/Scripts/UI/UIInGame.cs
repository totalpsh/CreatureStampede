using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public void SetCharacter(Player player)
    {
        _player = player;

        // ���⼭ UI�� �� ���� ����



        // ������Ʈ
        //UpdateExp(_player.currentExp, _player.maxExp);
        //UpdateSkillIcon();
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
}
