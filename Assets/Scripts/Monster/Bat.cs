using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonsterBase
{
    protected override void SetCharacterDirection()
    {
        if (dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = false;

        }
    }

    protected override void Attack(Player player, float delay = 0.5F)
    {
        base.Attack(player, delay);

        animator.SetTrigger(MonsterAnimParam.Attack);
    }
}
