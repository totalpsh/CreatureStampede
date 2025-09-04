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



    // ��� ������ �����
    [SerializeField] private BaseWeapon[] baseWeapons;

    // ���� ����ϴ� ����
    [field: SerializeField] public List<BaseWeapon> Weapons { get; private set; }

    // ���� ����
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
            // �ǰ� �ִϸ��̼� ������� ����
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
        // �ִϸ��̼� ��� �� �̺�Ʈ ȣ��
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

    // ���� ����
    public void EquipWeapons(WeaponSO data)
    {
        if(Weapons.Count >= Data.PlayerData.maxWeaponCount)
        {
            Debug.Log("�ִ� ���� ���� �ʰ�");
            return;
        }

        // ���� ������ �ִ� ������ data�� ���� ���Ⱑ �ִ� �� Ȯ��
        for (int i = 0; i < baseWeapons.Length; i++)
        {
            if(baseWeapons[i].Data == data)
            {
                // ���� ���Ⱑ ������ Ȱ��ȭ �� ���� ������ �ִ� ���⿡ �߰�
                baseWeapons[i].gameObject.SetActive(true);
                Weapons.Add(baseWeapons[i]);
                return;
            }
        }
    }

    // ���� ������
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
    
    // ���� ���⸦ ������ �ִ��� Ȯ�� �� ������ ������ ���� ��ȯ
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

    // �׾����� ���� ��Ȱ��ȭ
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
