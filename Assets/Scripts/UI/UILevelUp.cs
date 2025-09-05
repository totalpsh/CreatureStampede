using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILevelUp : UIBase
{
    Player _player;

    // [SerializeField] private WeaponSO[] playerWeapons;
    [SerializeField] private List<BaseWeapon> playerWeapons;
    [SerializeField] private List<BaseWeapon> hasWeapons;
    [SerializeField] private UICardSlot[] slots;
    [SerializeField] private UICardSlot scoreSlot;
    //[SerializeField] private Transform[] showTransform;

    [SerializeField] private bool openScoreCard = false;

    private void OnEnable()
    {
        _player = PlayerManager.Instance.Player;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].CloseUI();
            slots[i].selectEvent += CloseUI;
        }
        scoreSlot.CloseUI();
        scoreSlot.selectEvent += CloseUI;

        //if(!openScoreCard)
        //{
        //    ShowSlot();
        //}
        //else
        //{

        //}
        ShowSlot();
        AudioManager.Instance.EffectBgm(true);
    }

    public void ShowSlot()
    {
        int maxLevelCnt = 0;
        playerWeapons = _player.Weapons;
        hasWeapons.Clear();
        for (int i = 0; i < slots.Length; i++)
        {
            foreach (var weapon in playerWeapons)
            {
                if (slots[i].Weapon == weapon.Data)
                {
                    if (weapon.Level == weapon.Data.WeaponData.maxLevel)
                    {   
                        slots[i].CloseUI();
                        maxLevelCnt++;
                    }
                    else
                    {
                        slots[i].OpenUI();
                    }
                    hasWeapons.Add(weapon);
                }
            }
        }

        if (maxLevelCnt == hasWeapons.Count)
        {
            scoreSlot.OpenUI();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].selectEvent -= CloseUI;
        }
        scoreSlot.selectEvent -= CloseUI;

        AudioManager.Instance.EffectBgm(false);
    }
}
