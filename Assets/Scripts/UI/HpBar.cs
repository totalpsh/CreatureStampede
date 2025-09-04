using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : UIBase
{
    Player player;

    Slider hpSlider;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateHpBar(player.CurrentHealth, player.MaxHealth);
        player.OnChangeHealth += UpdateHpBar;
    }

    // hp 바 업데이트
    public void UpdateHpBar(float currentHp, float maxHp)
    {
        hpSlider.value = currentHp / maxHp;
    }
}
