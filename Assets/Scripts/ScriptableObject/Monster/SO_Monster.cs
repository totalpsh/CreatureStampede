using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterGrade
{
    Normal,
    Elite,

}

[CreateAssetMenu(fileName = "New Monster", menuName = "Monsters/New Monster")]
public class SO_Monster : ScriptableObject
{
    [SerializeField] private string monsterName;
    [SerializeField] private MonsterGrade grade;
    [SerializeField] private float maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private int score;
    [SerializeField] private int exp;

    public string MonsterName => monsterName;
    public MonsterGrade Grade => grade;
    public float MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    public float Damage => damage;
    public int Score => score;
    public int Exp => exp;
}
