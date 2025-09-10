using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonsterBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        // 컬러 바꿀때 기본 로직
        Color newColor = spriteRenderer.color;
        newColor.a = 1f;
        spriteRenderer.color = newColor;
    }

    protected override void Update()
    {
        base.Update();
        if (spriteRenderer.color.a != 1f)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
