using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : Bullet
{
    CircleCollider2D circleCollider2D;
    Player player;

    private Coroutine boomerangCoroutine;
    private bool isReturning = false;


    protected override void Awake()
    {
        base.Awake();
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = true;
        player = PlayerManager.Instance.Player;
        isBulletBoundary = false; // 경계 체크 비활성화
    }

    protected void OnEnable()
    {
        if (boomerangCoroutine != null)
        {
            StopCoroutine(boomerangCoroutine);
        }
        boomerangCoroutine = StartCoroutine(BoomerangRoundTrip());
    }

    // 왕복 운동을 처리하는 코루틴
    private IEnumerator BoomerangRoundTrip()
    {
        isReturning = false;

        // abilityValue는 왕복 시간이므로, 절반의 시간만큼 직진합니다.
        float outwardTime = abilityValue / 2f;
        yield return new WaitForSeconds(outwardTime);

        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(0.01f);
        circleCollider2D.enabled = true;

        // 이제 플레이어에게 돌아갑니다.
        isReturning = true;
        if (player != null && _rigidbody != null)
        {
            // 오브젝트가 비활성화될 때까지 계속 플레이어를 향해 이동합니다.
            while (gameObject.activeSelf)
            {
                Vector2 dirToPlayer = (player.transform.position - transform.position).normalized;
                _rigidbody.velocity = dirToPlayer * speed;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // 돌아오는 중이고 플레이어와 충돌했다면 비활성화합니다.
        if (isReturning && collision.CompareTag("Player"))
        {
            BulletSetActive();
            return;
        }

        // 그 외의 충돌은 기본 로직을 따릅니다.
        base.OnTriggerEnter2D(collision);
    }

    protected override void BulletSetActive()
    {
        if (boomerangCoroutine != null)
        {
            StopCoroutine(boomerangCoroutine);
            boomerangCoroutine = null;
        }
        base.BulletSetActive();
    }

    // 총알 능력    
    protected override void Ability(MonsterBase target)
    {
        // 자식 클래스에서 구현
    }
}
