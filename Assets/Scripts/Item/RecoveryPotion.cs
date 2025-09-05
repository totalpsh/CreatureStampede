using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryPotion : Item
{
    int healingAmount = 20;

    protected override void ApplyEffect(GameObject player)
    {
        player.GetComponent<Player>().Heal(healingAmount);
    }
}
