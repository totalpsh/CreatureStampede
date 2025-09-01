using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] SO_Monster monsterData;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;

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

    private Player target;
    private Vector2 dir;

    private void Awake()
    {
        InitializeMonster();
    }

    private void Update()
    {
        dir = target.transform.position - transform.position;
        dir = dir.normalized;
    }

    private void FixedUpdate()
    {
        ChaseTarget();
    }

    private void InitializeMonster()
    {
        Name = monsterData.MonsterName;
        Grade = monsterData.Grade;
        MaxHealth = monsterData.MaxHealth;
        CurrentHealth = MaxHealth;
        MoveSpeed = monsterData.MoveSpeed;
        Damage = monsterData.Damage;
        Score = monsterData.Score;
        Exp = monsterData.Exp;

        target = PlayerManager.Instance.Player;
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

    private void ChaseTarget()
    {
        if (dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = false;

        }

        rb.MovePosition(rb.position + dir * MoveSpeed * Time.fixedDeltaTime);
    }
}
