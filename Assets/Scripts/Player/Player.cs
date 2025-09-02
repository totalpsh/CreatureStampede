using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    PlayerController controller;

    public event Action<float, float> OnChangeHealth;
    public event Action<Player> OnCharacterDie;

    public bool IsDead { get; private set; } = false;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void Init()
    {

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
