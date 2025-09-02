using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    [Header("Player Stets")]
    public float maxHealth = 100f; // �ִ� ü��
    public float moveSpeed = 3f; // �̵� �ӵ�
    public float damage = 5f; // ���ݷ�

    [Header("Dash Settings")]
    public float dashSpeed = 20f;     // ��� �ӵ�
    [Range(0.1f, 2.0f)] public float dashDuration = 0.2f; // ��� ���� �ð�
    public float dashCooldown = 1f;   // ��� ��Ÿ��
}



[CreateAssetMenu(fileName = "New Player", menuName = "Players/New Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }
}
