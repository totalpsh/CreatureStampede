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

        // 애님 키값 상수 활용하는 것 좋음
        // 애니메이션 성능면에서는 해시로 활용하는게 빠름 -> 문자열 비교 안해도 되기 때문
        animator.SetTrigger(MonsterAnimParam.Attack);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("BulletBoundary"))
        {
            isChasing = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // 구조상으로는 타겟을 출동했을때 찾는게 개념상으로는 맞음
            // 대상을 이미 정해두고 진행할거라면 타겟을 매개변수로 활용할 필요가 없음
            Attack(target, 0.5f);
        }

    }


}
