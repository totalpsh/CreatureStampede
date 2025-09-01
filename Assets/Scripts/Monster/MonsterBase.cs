using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] SO_Monster monsterData;

    public string Name { get; private set; }
    public MonsterGrade Grade { get; private set; }
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MoveSpeed { get; private set; }
    public float Damage { get; private set; }
    public int Score { get; private set; }
    public int Exp { get; private set; }

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
    }
}
