using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet
{
    protected override void Ability(MonsterBase target)
    {
        if (target == null) return;

        // 좋음 방법
        // 상태이상이 여러개라면 개별적인 함수를 만드는 것보다 상태이상 코드를 넘기게 됩니다.
        target.StopForDuration(abilityValue, Color.blue);
        
    }
}
