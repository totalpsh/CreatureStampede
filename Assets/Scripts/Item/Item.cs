using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Item Collected!");
            ApplyEffect(collision.gameObject);

            string itemName = gameObject.name;
            itemName = itemName.Remove(itemName.IndexOf("(Clone)"));
            StageManager.Instance.Stage.itemPools[itemName].Release(gameObject);
            // Destroy(gameObject);
        }
    }

    // 아이템 효과 적용
    protected abstract void ApplyEffect(GameObject player);
}
