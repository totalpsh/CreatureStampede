using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILevelUp : UIBase
{
    Player _player;

    [SerializeField] private WeaponSO[] playerWeapons;

    [SerializeField] private UICardSlot[] slots;
    [SerializeField] private SkillData[] hasItemDatas;
    //[SerializeField] private Transform[] showTransform;

    private void OnEnable()
    {
        _player = PlayerManager.Instance.Player;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].CloseUI();
            slots[i].selectEvent += CloseUI;
        }

        ShowSlot();
    }

    public void ShowSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            for(int j = 0; j < playerWeapons.Length; j++)
            {
                if (slots[i].Weapon == playerWeapons[j])
                {
                    if (playerWeapons[j].WeaponData.level == 5) slots[i].CloseUI();
                    else
                    {
                        slots[i].OpenUI();
                        slots[i].playerHasWeapon = playerWeapons[j];
                    }
                }
            }
        }
    }
}
