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
        // �÷��̾� ����Ʈ ��������
        // ���ڷ� ���� �����Ͱ� �÷��̾� ������ ����Ʈ�� ���ԵǾ� �ִٸ� false�� ��ȯ

        // -> ShowSlot ���� SkillList�� �߰� ���� �ʴ´�.


        return false;
    }
}
