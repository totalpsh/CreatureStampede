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

    List<UICardSlot> openCards = new List<UICardSlot>();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            openCards[0]?.OnClickButton();
            

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            openCards[1]?.OnClickButton();


        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            openCards[2]?.OnClickButton();


        }
    }

    public void ShowSlot()
    {
        openCards.Clear();
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
                        openCards.Add(slots[i]);
                    }
                    hasWeapons.Add(weapon);
                }
            }
        }

        if (maxLevelCnt == hasWeapons.Count)
        {
            scoreSlot.OpenUI();
            openCards.Add(scoreSlot);

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
