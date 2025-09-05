using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : Item
{
    protected override void ApplyEffect(GameObject player)
    {
        UIManager.Instance.OpenUI<UIGetItem>();
        Time.timeScale = 0f;
    }
}
