using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bat : MonsterBase
{
    bool isChasing = true;
    float lifeTime = 0f;

    protected override void Awake()
    {
        base.Awake();


    }
    protected override void Update()
    {
        lifeTime += Time.deltaTime;
        if (isChasing)
        {
            dir = GetDirectionToTarget(target.transform);
        }

        if (lifeTime > 5f)
        {
            Die();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        dir = GetDirectionToTarget(target.transform);
        isChasing = true;
        lifeTime = 0f;
    }

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

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        isChasing = false;
    }

    
}
