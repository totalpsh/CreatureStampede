using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class UIGetItem : UIBase
{
    Player _player;

    [SerializeField] private List<BaseWeapon> weapons;
    [SerializeField] private List<WeaponSO> playerWeapons;

    [SerializeField] private UICardSlot[] slots;
    [SerializeField] private UICardSlot scoreSlot;


    private void OnEnable()
    {
        playerWeapons = new List<WeaponSO>();

        _player = PlayerManager.Instance.Player;
        weapons = _player.Weapons;

        foreach(var weapon in weapons)
        {
            playerWeapons.Add(weapon.Data);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].CloseUI();
            slots[i].selectEvent += CloseUI;
        }

        scoreSlot.selectEvent += CloseUI;
        scoreSlot.CloseUI();

        ShowSlot();
        AudioManager.Instance.EffectBgm(true);
    }

    public void ShowSlot()
    {
        if(weapons.Count >= _player.Data.PlayerData.maxWeaponCount)
        {
            scoreSlot.OpenUI();
            return;
        }

        List<UICardSlot> openCardList = new List<UICardSlot>();
        for(int i = 0; i < slots.Length; i++)
        {
            bool hasWeapon = playerWeapons.Contains(slots[i].Weapon);
            if(!hasWeapon) openCardList.Add(slots[i]);
        }

        for(int i = 0; i < playerWeapons.Count; i++)
        {
            Debug.Log(playerWeapons[i].name);
        }

        Debug.Log(openCardList.Count);
        
        int[] ran = new int[2];
        ran[0] = Random.Range(0, openCardList.Count);
        do
        {
            ran[1] = Random.Range(0, openCardList.Count);
        } while (ran[0] == ran[1]);

        for(int i= 0; i < ran.Length; i++)
        {
            openCardList[ran[i]].OpenUI();
        }
    }


    protected override void OnClose()
    {
        base.OnClose();
        foreach(UICardSlot slot in slots)
        {
            slot.CloseUI();
        }
        AudioManager.Instance.EffectBgm(false);
    }
}
