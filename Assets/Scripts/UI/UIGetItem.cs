using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class UIGetItem : UIBase
{
    Player _player;

    private UICardSlot slot;
    [SerializeField] private SkillData[] datas;
    [SerializeField] private Transform[] showTransform;

    private void OnEnable()
    {
        _player = PlayerManager.Instance.Player;

        ShowSlot();
    }

    public void ShowSlot()
    {
        List<SkillData> skillList = new List<SkillData>();

        for (int i = 0; i < 3; i++)
        {
            slot = UIManager.Instance.CreateSlotUI<UICardSlot>();
            slot.transform.SetParent(showTransform[i], false);

            SkillData data;

            do
            {
                data = datas[Random.Range(0, datas.Length)];
            }while (skillList.Contains(data));

            if (skillList.Contains(data)) return;

            skillList.Add(data);

            slot.InSlot(data);
            slot.selectEvent += CloseUI;
        }
    }


    public bool GetHasItem(/*SkillData data*/)
    {
        // 플레이어 리스트 가져온후
        // 인자로 들어온 데이터가 플레이어 아이템 리스트에 포함되어 있다면 false를 반환

        // -> ShowSlot 내부 SkillList에 추가 하지 않는다.


        return false;
    }
}
