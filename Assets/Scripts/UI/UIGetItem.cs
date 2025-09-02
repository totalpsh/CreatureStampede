using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class UIGetItem : UIBase
{
    private UICardSlot slot;
    [SerializeField] private SkillData[] datas;
    [SerializeField] private Transform[] showTransform;

    // Start is called before the first frame update
    void Start()
    {
        ShowSlot();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowSlot()
    {
        List<SkillData> skillList = new List<SkillData>();

        for (int i = 0; i < 3; i++)
        {
            slot = UIManager.Instance.CreateSlotUI<UICardSlot>();
            slot.transform.SetParent(showTransform[i], false);

            SkillData data = datas[Random.Range(0, datas.Length)];

            if (skillList.Contains(data)) return;
            skillList.Add(data);

            slot.InSlot(data);
        }
    }
}
