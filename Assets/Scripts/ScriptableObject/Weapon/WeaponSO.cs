using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    [Header("Weapon Settings")]

    
    // 툴팁으로 자세하게 정보를 공유하고 있어서 좋다
    //[SerializeField] private string skillIconPath;
    [Tooltip("������")]
    public Sprite icon; // ���� ������
    [Tooltip("����")] // ����
    public string description;

    [Tooltip("����")]
    public int level; // ����

    [Tooltip("�ִ� ����")]
    public int maxLevel = 5; // �ִ� ����

    [Tooltip("���ݷ�")]
    public float damage = 5f; // ���ݷ�
    [Tooltip("�߻� ����")]
    public int count = 1; // �߻� ����
    [Tooltip("�߻� �ӵ�, ȸ�� �ӵ�")]
    public float speed = 20f; // �߻� �ӵ�, ȸ�� �ӵ�
    [Tooltip("�߻� �ӵ� (�ʴ� �߻� Ƚ��)")]
    public float fireRate = 0.5f; // �߻� �ӵ� (�ʴ� �߻� Ƚ��)
    [Tooltip("�ɷ� ��")]
    public float abilityValue = 0f; // �ɷ� ��

    // 관통 횟수도 많이 관리함
    public bool isPierce = false; // ���� ����    

    [Header("Level Up Settings")]

    [Tooltip("���ݷ� ������")]
    public float damageIncrease = 2f; // ���ݷ� ������
    [Tooltip("�߻� ���� ������")]
    public int countIncrease = 1; // �߻� ���� ������
    [Tooltip("�߻� �ӵ�, ȸ�� �ӵ� ������")]
    public float speedIncrease = 2f; // �߻� �ӵ�, ȸ�� �ӵ� ������
    [Tooltip("�߻� �ӵ� (�ʴ� �߻� Ƚ��) ������")]
    public float fireRateDecrease = 0.05f; // �߻� �ӵ� (�ʴ� �߻� Ƚ��) ������
    [Tooltip("�ɷ� ������")]
    public float abilityValueIncrease = 1f; // �ɷ� ������


    [Header("Projectile Settings")]

    [Tooltip("�߻�ü ������")]
    public GameObject projectilePrefab; // �߻�ü ������
    [Tooltip("�߻�ü ����")]
    public float projectileLifetime = 5f; // �߻�ü ����
    [Tooltip("�߻�ü ���� ����")]
    public float spreadAngle = 15f; // �߻�ü ���� ����
    [Tooltip("�߻� ����")]
    public AudioClip shootSound; // �߻� ����
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField] public WeaponData WeaponData { get; private set; }
}
