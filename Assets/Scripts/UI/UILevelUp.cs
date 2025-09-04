using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILevelUp : UIBase
{
    Player _player;

    // [SerializeField] private WeaponSO[] playerWeapons;
    [SerializeField] private List<BaseWeapon> playerWeapons;

    [SerializeField] private UICardSlot[] slots;
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
        playerWeapons = _player.Weapons;
        for (int i = 0; i < slots.Length; i++)
        {
            foreach(var weapon in playerWeapons)
            {
                if (slots[i].Weapon == weapon.Data)
                {
                    if (weapon.Level == weapon.Data.WeaponData.maxLevel) 
                        slots[i].CloseUI();
                    else
                    {
                        slots[i].OpenUI();
                        // slots[i].playerHasWeapon = playerWeapons[j];
                    }
                }
            }
        }
    }
}
