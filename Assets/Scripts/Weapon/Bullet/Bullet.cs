using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected bool isPierce;
    [SerializeField] protected float speed;
    [SerializeField] protected float abilityValue;

    protected Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
       _rigidbody = GetComponent<Rigidbody2D>();
    }


    public void Init(float damage, bool isPierce, Vector3 dir, float speed = 0, float abilityValue = 0)
    {
        this.damage = damage;
        this.isPierce = isPierce;
        this.speed = speed;
        this.abilityValue = abilityValue;

        if (_rigidbody != null)
        {
            _rigidbody.velocity = dir * this.speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log($"Hit Enemy {damage}");
            collision.GetComponent<IDamagable>()?.TakePhysicalDamage(damage);
            Ability(collision.GetComponent<MonsterBase>());
            if (!isPierce)
            {
                BulletSetActive();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("BulletBoundary"))
        {
            BulletSetActive();
        }
    }

    // 총알 비활성화 및 속도 초기화
    protected virtual void BulletSetActive()
    {
        _rigidbody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    // 총알 능력    
    protected virtual void Ability(MonsterBase target)
    {
        // 자식 클래스에서 구현
    }
}
