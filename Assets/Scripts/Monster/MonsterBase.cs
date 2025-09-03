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

    public event Action OnHpZero;
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
        dir = target.transform.position - transform.position;
        dir = dir.normalized;
        
       
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            HitReaction();
        }
    }

    private void InitializeMonster()
    {
        Name = monsterData.MonsterName;
        Grade = monsterData.Grade;
        MaxHealth = monsterData.MaxHealth;
        CurrentHealth = MaxHealth;
        MoveSpeed = 30f;
        Damage = monsterData.Damage;
        Score = monsterData.Score;
        Exp = monsterData.Exp;

        target = PlayerManager.Instance.Player;

        animator.SetBool(MonsterAnimParam.IsMoving, target != null);
        animator.SetBool(MonsterAnimParam.Die, false);
        canAttack = true;
        canMove = true;

        OnHpZero += Die;

    }

    public void SetHealth(float health)
    {
        CurrentHealth = Math.Clamp(health, 0, MaxHealth);

        OnHpChanged?.Invoke();

        if (CurrentHealth == 0)
        {
            OnHpZero?.Invoke();
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

        Debug.Log($"{Name}이/가 플레이어를 공격");
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

        yield return new WaitForSeconds(duration);
        canMove = true;
        animator.SetBool(MonsterAnimParam.IsMoving, true);
    }

    protected void HitReaction()
    {
        animator.SetTrigger(MonsterAnimParam.Hit);
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Destroy(gameObject);
    }

    public void TakePhysicalDamage(float damage)
    {
        SetHealth(CurrentHealth - damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("BulletBoundary"))
        {
            return;
        }
        MoveSpeed = monsterData.MoveSpeed;

    }
}
