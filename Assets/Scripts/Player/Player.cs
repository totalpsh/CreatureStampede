using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public interface IDamagable
{
    void TakePhysicalDamage(float damage);
}

public class Player : MonoBehaviour, IDamagable
{
    PlayerController controller;

    PlayerAnimator playerAnimator;
    [field: SerializeField] public PlayerSO Data { get; private set; }

    public event Action<float, float> OnChangeHealth;
    public event Action OnCharacterDie;

    public GameObject bulletpoolGO;
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float CurrentHealth { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    public bool IsDead { get; private set; } = false;
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
}
