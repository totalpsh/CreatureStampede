using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonsterBase
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
}
