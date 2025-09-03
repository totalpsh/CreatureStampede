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
        isBulletBoundary = false; // ��� üũ ��Ȱ��ȭ
    }

    protected void OnEnable()
    {
        if (boomerangCoroutine != null)
        {
            StopCoroutine(boomerangCoroutine);
        }
        boomerangCoroutine = StartCoroutine(BoomerangRoundTrip());
    }

    // �պ� ��� ó���ϴ� �ڷ�ƾ
    private IEnumerator BoomerangRoundTrip()
    {
        isReturning = false;

        // abilityValue�� �պ� �ð��̹Ƿ�, ������ �ð���ŭ �����մϴ�.
        float outwardTime = abilityValue / 2f;
        yield return new WaitForSeconds(outwardTime);

        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(0.01f);
        circleCollider2D.enabled = true;

        // ���� �÷��̾�� ���ư��ϴ�.
        isReturning = true;
        if (player != null && _rigidbody != null)
        {
            // ������Ʈ�� ��Ȱ��ȭ�� ������ ��� �÷��̾ ���� �̵��մϴ�.
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
        // ���ƿ��� ���̰� �÷��̾�� �浹�ߴٸ� ��Ȱ��ȭ�մϴ�.
        if (isReturning && collision.CompareTag("Player"))
        {
            BulletSetActive();
            return;
        }

        // �� ���� �浹�� �⺻ ������ �����ϴ�.
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

    // �Ѿ� �ɷ�    
    protected override void Ability(MonsterBase target)
    {
        // �ڽ� Ŭ�������� ����
    }
}
