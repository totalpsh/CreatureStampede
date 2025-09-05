using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonsterBase
{
    protected override void Attack(Player player, float delay = 0.5F)
    {
        base.Attack(player, delay);

        animator.SetTrigger(MonsterAnimParam.Attack);
    }
}
