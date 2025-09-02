using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    [Header("Player Stets")]
    public float maxHealth = 100f; // 최대 체력
    public float moveSpeed = 3f; // 이동 속도
    public float damage = 5f; // 공격력

    [Header("Dash Settings")]
    public float dashSpeed = 20f;     // 대시 속도
    [Range(0.1f, 2.0f)] public float dashDuration = 0.2f; // 대시 지속 시간
    public float dashCooldown = 1f;   // 대시 쿨타임
}



[CreateAssetMenu(fileName = "New Player", menuName = "Players/New Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }
}
