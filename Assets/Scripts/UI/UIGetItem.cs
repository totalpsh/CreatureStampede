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

    [SerializeField] private WeaponSO[] playerWeapons;

    [SerializeField] private UICardSlot[] slots;
    [SerializeField] private SkillData[] hasItemDatas;

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
        List<UICardSlot> openCardList = new List<UICardSlot>();
        for(int i = 0; i < slots.Length; i++)
        {
            bool hasWeapon = playerWeapons.Contains(slots[i].Weapon);
            if(!hasWeapon) openCardList.Add(slots[i]);
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
    }
}
