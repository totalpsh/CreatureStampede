using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterBase : MonoBehaviour, IDamagable
{
    [SerializeField] SO_Monster monsterData;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Collider2D monsterCollider;


    public string Name { get; private set; }
    public MonsterGrade Grade { get; private set; }
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MoveSpeed { get; private set; }
    public float Damage { get; private set; }
    public int Score { get; private set; }
    public int Exp { get; private set; }

    public event Action<MonsterBase> OnHpZero;
    public event Action OnHpChanged;

    protected Player target;
    protected Vector2 dir;
    protected bool canAttack;
    protected bool canMove;

    Coroutine monsterStopCoroutine;

    protected virtual void Awake()
    {
        InitializeMonster();
        
    }

    protected virtual void Update()
    {
        dir = GetDirectionToTarget(target.transform);

    }

    protected Vector2 GetDirectionToTarget(Transform target)
    {
        Vector2 ret;
        ret = target.transform.position - transform.position;
        ret = ret.normalized;
        return ret;
    }

    protected virtual void FixedUpdate()
    {
        if (canMove)
        {
            ChaseTarget();

        }
    }

    private void LateUpdate()
    {
        
    }

    private void InitializeMonster()
    {
        Name = monsterData.MonsterName;
        Grade = monsterData.Grade;
        MaxHealth = monsterData.MaxHealth;
        CurrentHealth = MaxHealth;
        Damage = monsterData.Damage;
        Score = monsterData.Score;
        Exp = monsterData.Exp;

        target = PlayerManager.Instance.Player;


    }

    protected virtual void OnEnable()
    {
        MoveSpeed = 30f;

        animator.SetBool(MonsterAnimParam.IsMoving, target != null);
        canAttack = true;
        canMove = true;

        monsterCollider.enabled = true;
        rb.simulated = true;
    }

 

    public void SetHealth(float health)
    {
        CurrentHealth = Math.Clamp(health, 0, MaxHealth);

        OnHpChanged?.Invoke();

        if (CurrentHealth == 0)
        {
            Die();
            
            OnHpZero?.Invoke(this);
        }
    }

    protected virtual void SetCharacterDirection()
    {
        if (dir.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = true;

        }
    }

    private void ChaseTarget()
    {
        SetCharacterDirection();

        rb.MovePosition(rb.position + dir * MoveSpeed * Time.fixedDeltaTime);
    }

    IEnumerator MonsterAttackWithDelay(float delay)
    {
        canAttack = false;
        //animator.SetTrigger(MonsterAnimParam.Attack);

        IDamagable player = target as IDamagable;
        if (player != null)
        {
            player.TakePhysicalDamage(Damage);
        }

        yield return new WaitForSeconds(delay);
        canAttack = true;

    }

    protected virtual void Attack(Player player, float delay = 0.5f)
    {
        if (!canAttack)
            return;

        StartCoroutine(MonsterAttackWithDelay(0.5f));

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Attack(target, 0.5f);
    }



    protected void DropItem()
    {
        float randomValue = UnityEngine.Random.value;
        if (Grade == MonsterGrade.Normal)
        {
            float cumulativeValue = 0f;

            cumulativeValue += 0.05f;
            if (randomValue < cumulativeValue)
            {
                var potion = StageManager.Instance.Stage.itemPools["RecoveryPotion"].Get();
                potion.transform.position = transform.position;
                return;
            }
            //cumulativeValue += 0.1f;
            //if (randomValue < cumulativeValue)
            //{
            //    var potion = ResourceManager.Instance.CreateItem<RecoveryPotion>("RecoveryPotion");
            //    potion.transform.position = transform.position;
            //    return;

            //}

        }
        else if (Grade == MonsterGrade.Elite)
        {
            if (randomValue < 0.5f)
            {
                var weaponBox = StageManager.Instance.Stage.itemPools["WeaponBox"].Get();

                weaponBox.transform.position = transform.position;
                return;

            }
        }
    }

    protected void Die()
    {
        animator.SetTrigger(MonsterAnimParam.Die);
        monsterCollider.enabled = false;
        rb.simulated = false;

        
        StartCoroutine(DestroyAfterDelay(0.5f));
    }

    public void StopForDuration(float duration)
    {
        if (monsterStopCoroutine != null)
        {
            StopCoroutine(monsterStopCoroutine);

        }
        monsterStopCoroutine = StartCoroutine(StopMonster(duration));

        
    }

    IEnumerator StopMonster(float duration)
    {
        animator.SetBool(MonsterAnimParam.IsMoving, false);
        canMove = false;
        rb.simulated = false;

        yield return new WaitForSeconds(duration);
        canMove = true;
        rb.simulated = true;

        animator.SetBool(MonsterAnimParam.IsMoving, true);
    }

    protected void HitReaction()
    {
        StopForDuration(0.5f);
        animator.SetTrigger(MonsterAnimParam.Hit);
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        StageManager.Instance.Stage.pools[Name].Release(gameObject);
    }

    public void TakePhysicalDamage(float damage)
    {
        HitReaction();
        SetHealth(CurrentHealth - damage);
        if (CurrentHealth == 0)
        {
            // 플레이어에게 경험치 주기
            StageManager.Instance.AddExp(Exp);
            // 점수 더하기
            StageManager.Instance.AddScore(Score);
            // 드롭하기
            DropItem();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletBoundary"))
        {
            MoveSpeed = monsterData.MoveSpeed;
        }
        

    }
}
