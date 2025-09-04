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

    private Player player;


    private void Start()
    {
        selectButton.onClick.AddListener(OnClickButton);
    }

    private void OnEnable()
    {
        player = PlayerManager.Instance.Player;
    }
    
    // Start is called before the first frame update
    protected override void OnOpen()
    {
        base.OnOpen();
        UpdateUI();
    }

    public void UpdateUI()
    {
        
        nameText.text = weaponData.WeaponData.name;
        icon.sprite = weaponData.WeaponData.icon;
        descriptionText.text = weaponData.WeaponData.description;

        int level = player.GetWeaponLevel(weaponData);

        levelText.text = $"Lv. {level.ToString()}";
    }

    public void OnClickButton()
    {
        Debug.Log($"{weaponData.name} 선택됨");
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
        // 플레이어가 가지고 있는 무기 배열 혹은 리스트에 넣어주기
        selectEvent?.Invoke();
        Time.timeScale = 1.0f;
    }

    private void AcquireWeapon()
    {
        Debug.Log("장비상자 획득");
        player.EquipWeapons(Weapon);
    }

    private void UpgradeItem()
    {
        Debug.Log("레벨업 무기 업그레이드");
        player.LevelUpWeapon(Weapon);
    }

    public bool HasAbility()
    {
        return false;
    }

}
