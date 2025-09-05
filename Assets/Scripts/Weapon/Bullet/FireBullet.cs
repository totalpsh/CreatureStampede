using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : Bullet
{
    Animator animator;
    CircleCollider2D circleCollider2D;
    BoxCollider2D boxCollider2D;
    [SerializeField] AudioClip explosionSfx;

    // ������ ������ ����
    bool isExploding = false;


    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
        boxCollider2D.enabled = true;
        circleCollider2D.enabled = false;

    }
    protected override void Ability(MonsterBase target)
    {
        if(isExploding)
        {
            return;
        }

        if(abilityValue == 0)
        {
            abilityValue = 1f;
        }
        isExploding = true;

        

        // �ִϸ��̼� ���̸�ŭ ��� �� ��Ȱ��ȭ
        StartCoroutine(DisableAfterAnimation());
    }

    protected override void BulletSetActive()
    {
        if(isExploding)
        {
            return;
        }


        base.BulletSetActive();
    }

    IEnumerator DisableAfterAnimation()
    {
        // 0.1�� ���
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.PlaySfx(explosionSfx);
        // ũ�� ����
        Vector3 scale = transform.localScale;
        scale *= abilityValue;
        transform.localScale = scale;


        boxCollider2D.enabled = false;
        circleCollider2D.enabled = true;
        _rigidbody.velocity = Vector2.zero;


        animator.SetTrigger("IsExplosion");
        yield return new WaitForSeconds(0.1f);
        // �ִϸ��̼� ���̸�ŭ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        // �Ѿ� ��Ȱ��ȭ
        animator.SetTrigger("IsReuse");
        isExploding = false;

        scale /= abilityValue;
        transform.localScale = scale;
        boxCollider2D.enabled = true;
        circleCollider2D.enabled = false;

        BulletSetActive();
    }
}
