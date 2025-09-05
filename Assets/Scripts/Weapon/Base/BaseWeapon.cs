using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [field: SerializeField] public WeaponSO Data { get; private set; }

    protected List<Bullet> bulletPool;

    protected int count;
    protected float damage;
    protected float speed;
    protected float fireRate; // 발사 속도 (초당 발사 횟수)
    protected float abilityValue; // 능력 값
    public int Level { get; private set; }


    protected Player player;
    protected virtual void Awake()
    {
        bulletPool = new List<Bullet>();
        count = Data.WeaponData.count;
        damage = Data.WeaponData.damage;
        speed = Data.WeaponData.speed;
        fireRate = Data.WeaponData.fireRate;
        abilityValue = Data.WeaponData.abilityValue;
        Level = Data.WeaponData.level;
    }

    protected virtual void Start()
    {
        player = PlayerManager.Instance.Player;
    }

    [SerializeField] protected Transform firePoint; // 발사 위치
    protected Bullet GetBullet()
    {
        Bullet select = null;

        foreach(var bullet in bulletPool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                select = bullet;
                select.gameObject.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            // 생성 수정 필요
            select = Instantiate(Data.WeaponData.projectilePrefab).GetComponent<Bullet>();
            bulletPool.Add(select);
        }
        return select;
    }

    protected abstract void BulletSetting();

    public virtual void LevelUp()
    {
        if(Level >= Data.WeaponData.maxLevel) return;

        count += Data.WeaponData.countIncrease;
        damage += Data.WeaponData.damageIncrease;
        speed += Data.WeaponData.speedIncrease;
        fireRate = Mathf.Max(0.1f, fireRate - Data.WeaponData.fireRateDecrease); // 최소 발사 속도 제한
        abilityValue += Data.WeaponData.abilityValueIncrease;
        Level++;
    }

    protected void ShootSound()
    {
        if(Data.WeaponData.shootSound != null)  
            AudioManager.Instance.PlaySfx(Data.WeaponData.shootSound);
    }
}


