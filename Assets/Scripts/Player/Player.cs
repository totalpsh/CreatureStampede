using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public interface IDamagable
{
    void TakePhysicalDamage(float damage);
}

public class Player : MonoBehaviour, IDamagable
{

    PlayerAnimator playerAnimator;
    public PlayerController controller;
    [field: SerializeField] public PlayerSO Data { get; private set; }

    public event Action<float, float> OnChangeHealth;
    public event Action OnCharacterDie;

    public GameObject bulletpoolGO;
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float CurrentHealth { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    public bool IsDead { get; private set; } = false;



    // 사용 가능한 무기들
    [SerializeField] private BaseWeapon[] baseWeapons;

    // 현재 사용하는 무기
    [field: SerializeField] public List<BaseWeapon> Weapons { get; private set; }

    // 시작 무기
    [SerializeField] private BaseWeapon startWeapon;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        bulletpoolGO = new GameObject("bulletpoolGO");
    }

    public void Init()
    {
        DataInitialization();
        controller.DataInitialization(Data.PlayerData.moveSpeed, Data.PlayerData.dashSpeed, Data.PlayerData.dashDuration, Data.PlayerData.dashCooldown);
        foreach (var weapon in baseWeapons)
        {
            weapon.gameObject.SetActive(false);
        }

        Weapons = new List<BaseWeapon>();
        EquipWeapons(startWeapon.Data);
    }


    void DataInitialization()
    {
        MaxHealth = Data.PlayerData.maxHealth;
        CurrentHealth = MaxHealth;
        Damage = Data.PlayerData.damage;
        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        IsDead = false;
    }


    public virtual void Stop()
    {
        StopAllCoroutines();
    }

    public void SetPosition(Transform targetTr)
    {
        transform.position = targetTr.position;
    }

    public void ChangeHealth(float damage)
    {
        if(IsDead) return;

        CurrentHealth += damage;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            Death();
        }
        else
        {
            playerAnimator.TriggerIsShot();
            // 피격 애니메이션 재생동안 무적
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(playerAnimator.animator.GetCurrentAnimatorStateInfo(0).length);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    private void Death()
    {
        if (IsDead) return;
        IsDead = true;
        
        playerAnimator.TriggerIsDead();
        DisableAll();
        controller.enabled = false;
        // 애니메이션 재생 후 이벤트 호출
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        yield return new WaitForSeconds(playerAnimator.animator.GetCurrentAnimatorStateInfo(0).length);
        OnCharacterDie?.Invoke();
    }

    public void TakePhysicalDamage(float damage)
    {
        ChangeHealth(-damage);
    }

    // 무기 장착
    public void EquipWeapons(WeaponSO data)
    {
        if(Weapons.Count >= Data.PlayerData.maxWeaponCount)
        {
            Debug.Log("최대 무기 개수 초과");
            return;
        }

        // 현재 가지고 있는 무기중 data와 같은 무기가 있는 지 확인
        for (int i = 0; i < baseWeapons.Length; i++)
        {
            if(baseWeapons[i].Data == data)
            {
                // 같은 무기가 있으면 활성화 후 현재 가지고 있는 무기에 추가
                baseWeapons[i].gameObject.SetActive(true);
                Weapons.Add(baseWeapons[i]);
                return;
            }
        }
    }

    // 무기 레벨업
    public void LevelUpWeapon(WeaponSO data)
    {
        foreach(var weapon in Weapons)
        {
            if (weapon.Data == data)
            {
                weapon.LevelUp();
                return;
            }
        }
    }
    
    // 현재 무기를 가지고 있는지 확인 후 가지고 있으면 레벨 반환
    public int GetWeaponLevel(WeaponSO data)
    {
        foreach (var weapon in Weapons)
        {
            if (weapon.Data == data)
            {
                return weapon.Level;
            }
        }
        return 0;
    }

    // 죽었을때 무기 비활성화
    public void DisableAll()
    { 
        foreach (var weapon in Weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        GetComponent<PlayerInput>().gameObject.SetActive(false);
        bulletpoolGO.SetActive(false);
    }
}
