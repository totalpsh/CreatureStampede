using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    PlayerController controller;
    public Scanner scanner;
    [field: SerializeField] public PlayerSO Data { get; private set; }

    public event Action<float, float> OnChangeHealth;
    public event Action<Player> OnCharacterDie;


    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float CurrentHealth { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    public bool IsDead { get; private set; } = false;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        scanner = GetComponent<Scanner>();
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
        //CurrentHealth += damage;
        //CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        //CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        //OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        //if (CurrentHealth <= 0f)
        //{
        //    Death();
        //}
    }

    private void Death()
    {
        //if (IsDead) return;
        //IsDead = true;
        //animator.SetTrigger(DeathHash);

        //OnCharacterDie?.Invoke(this);
    }
}
