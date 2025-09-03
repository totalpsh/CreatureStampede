using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData
{
    [Header("Weapon Settings")]

    [Tooltip("이름")]
    public string name; // 이름
    //[SerializeField] private string skillIconPath;
    [Tooltip("아이콘")]
    public Sprite icon; // 무기 아이콘
    [Tooltip("설명")] // 설명
    public string description;

    [Tooltip("레벨")]
    public int level; // 레벨

    [Tooltip("공격력")]
    public float damage = 5f; // 공격력
    [Tooltip("발사 개수")]
    public int count = 1; // 발사 개수
    [Tooltip("발사 속도, 회전 속도")]
    public float speed = 20f; // 발사 속도, 회전 속도
    [Tooltip("발사 속도 (초당 발사 횟수)")]
    public float fireRate = 0.5f; // 발사 속도 (초당 발사 횟수)
    [Tooltip("능력 값")]
    public float abilityValue = 0f; // 능력 값

    [Tooltip("관통 여부")]
    public bool isPierce = false; // 관통 여부    

    [Header("Level Up Settings")]

    [Tooltip("공격력 증가량")]
    public float damageIncrease = 2f; // 공격력 증가량
    [Tooltip("발사 개수 증가량")]
    public int countIncrease = 1; // 발사 개수 증가량
    [Tooltip("발사 속도, 회전 속도 증가량")]
    public float speedIncrease = 2f; // 발사 속도, 회전 속도 증가량
    [Tooltip("발사 속도 (초당 발사 횟수) 증가량")]
    public float fireRateDecrease = 0.05f; // 발사 속도 (초당 발사 횟수) 증가량
    [Tooltip("능력 증가량")]
    public float abilityValueIncrease = 1f; // 능력 증가량


    [Header("Projectile Settings")]

    [Tooltip("발사체 프리팹")]
    public GameObject projectilePrefab; // 발사체 프리팹
    [Tooltip("발사체 수명")]
    public float projectileLifetime = 5f; // 발사체 수명
    [Tooltip("발사체 퍼짐 각도")]
    public float spreadAngle = 15f; // 발사체 퍼짐 각도
    [Tooltip("발사 사운드")]
    public AudioClip shootSound; // 발사 사운드
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField] public WeaponData WeaponData { get; private set; }
}
