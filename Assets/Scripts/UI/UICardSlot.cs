using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardSlot : UIBase
{
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private string skillName;
    //[SerializeField] private string skillIconPath;
    [SerializeField] private Sprite skillIcon;
    [SerializeField] private int skillLevel;
    [SerializeField] private string skillDescription;

    public event Action SelectClose;

    private void Awake()
    {
        selectButton.onClick.AddListener(OnClickCard);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InSlot(SkillData skill)
    {
        skillName = skill.SkillName;
        skillIcon = skill.SkillIcon;
        skillLevel = skill.SkillLevel;
        skillDescription = skill.SkillDescription;
        UpdateUI();
    }

    public void UpdateUI()
    {
        nameText.text = skillName;
        icon.sprite = skillIcon;
        levelText.text = $"Lv. {skillLevel.ToString()}";
        descriptionText.text = skillDescription;
    }

    public void OnClickCard()
    {
        HasAbility();
        SelectClose?.Invoke();
        CloseUI();
    }

    public bool HasAbility()
    {
        return false;
    }
}
