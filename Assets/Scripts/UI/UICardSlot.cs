using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardSlot : UIBase
{
    public enum ButtonMode
    {
        Upgrade,
        GetWeapon
    }

    [SerializeField] private WeaponSO weaponData;
    public WeaponSO Weapon { get { return weaponData; } }

    public WeaponSO playerHasWeapon;

    [SerializeField] private ButtonMode mode;
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

    public event Action<BaseWeapon[]> weaponSelect;
    public event Action selectEvent;

    // Start is called before the first frame update
    protected override void OnOpen()
    {
        base.OnOpen();
        UpdateUI();
    }

    public void UpdateUI()
    {
        selectButton.onClick.AddListener(OnClickButton);
        nameText.text = weaponData.WeaponData.name;
        icon.sprite = weaponData.WeaponData.icon;
        levelText.text = $"Lv. {weaponData.WeaponData.level.ToString()}";
        descriptionText.text = weaponData.WeaponData.description;
    }

    public void OnClickButton()
    {
        switch (mode)
        {
            case ButtonMode.Upgrade:
                UpgradeItem();
                break;
            case ButtonMode.GetWeapon:
                AcquireWeapon();
                break;
        }

        HasAbility();
        // �÷��̾ ������ �ִ� ���� �迭 Ȥ�� ����Ʈ�� �־��ֱ�
        selectEvent?.Invoke();
        Time.timeScale = 1.0f;
    }

    private void AcquireWeapon()
    {
        Debug.Log("������ ȹ��");
    }

    private void UpgradeItem()
    {
        Debug.Log("������ ���� ���׷��̵�");

        playerHasWeapon.WeaponData.level++;

    }

    public bool HasAbility()
    {
        return false;
    }

}
