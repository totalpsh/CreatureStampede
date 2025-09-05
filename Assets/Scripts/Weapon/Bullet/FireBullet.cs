using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : Bullet
{
    Animator animator;
    CircleCollider2D circleCollider2D;
    BoxCollider2D boxCollider2D;
    [SerializeField] AudioClip explosionSfx;

    // 터지는 중인지 여부
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

        

        // 애니메이션 길이만큼 대기 후 비활성화
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
        // 0.1초 대기
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.PlaySfx(explosionSfx);
        // 크기 조절
        Vector3 scale = transform.localScale;
        scale *= abilityValue;
        transform.localScale = scale;


        boxCollider2D.enabled = false;
        circleCollider2D.enabled = true;
        _rigidbody.velocity = Vector2.zero;


        animator.SetTrigger("IsExplosion");
        yield return new WaitForSeconds(0.1f);
        // 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        // 총알 비활성화
        animator.SetTrigger("IsReuse");
        isExploding = false;

        scale /= abilityValue;
        transform.localScale = scale;
        boxCollider2D.enabled = true;
        circleCollider2D.enabled = false;

        BulletSetActive();
    }
}
