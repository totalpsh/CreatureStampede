using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelUp : UIBase
{
    private UICardSlot slot;
    [SerializeField] private SkillData[] datas;
    [SerializeField] private Transform[] showTransform;

    private void OnEnable()
    {
        ShowSlot();
        
    }

    public void ShowSlot()
    {
        List<SkillData> skillList = new List<SkillData>();

        for (int i = 0; i < 3; i++)
        {
            slot = UIManager.Instance.CreateSlotUI<UICardSlot>();
            slot.transform.SetParent(showTransform[i], false);

            // �÷��̾� ���� ��ų ����Ʈ �����ͼ� �ݿ����ֱ�
            //SkillData data;

            //do
            //{
            //    data = datas[Random.Range(0, datas.Length)];
            //} while (skillList.Contains(data));

            //if (skillList.Contains(data)) return;

            //skillList.Add(data);

            //slot.InSlot(data);

            slot.selectEvent += CloseUI;
        }
    }
}
