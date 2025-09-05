using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet
{
    protected override void Ability(MonsterBase target)
    {
        if (target == null) return;

        target.StopForDuration(abilityValue, Color.blue);
        
    }
}
