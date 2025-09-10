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

    List<UICardSlot> openCards = new List<UICardSlot>();

    private void OnEnable()
    {
        // 한번 만들었으면 Clear 하고 사용하는 것을 고려
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
        
    }

    // 슬롯을 보여주기 위해서라기엔.. 반복문이 좀 많은 느낌
    public void ShowSlot()
    {
        openCards.Clear();
        if (weapons.Count >= _player.Data.PlayerData.maxWeaponCount)
        {
            scoreSlot.OpenUI();
            openCards.Add(scoreSlot);
            return;
        }

        List<UICardSlot> openCardList = new List<UICardSlot>();
        for(int i = 0; i < slots.Length; i++)
        {
            bool hasWeapon = playerWeapons.Contains(slots[i].Weapon);
            if(!hasWeapon) openCardList.Add(slots[i]);
        }

        // 목적없는 코드??
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

        if (ran[0] > ran[1])
        {
            int temp = ran[0];
            ran[0] = ran[1];
            ran[1] = temp;
        }

        for(int i= 0; i < ran.Length; i++)
        {
            openCards.Add(openCardList[ran[i]]);
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
