using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Slime : MonsterBase
{
    public IObjectPool<Slime> pool;
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
}
