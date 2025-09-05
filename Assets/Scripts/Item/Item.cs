using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Item Collected!");
            ApplyEffect(collision.gameObject);

            AudioManager.Instance.PlaySfx(audioClip);

            string itemName = gameObject.name;
            itemName = itemName.Remove(itemName.IndexOf("(Clone)"));
            StageManager.Instance.Stage.itemPools[itemName].Release(gameObject);
        }
    }

    // 아이템 효과 적용
    protected abstract void ApplyEffect(GameObject player);
}
