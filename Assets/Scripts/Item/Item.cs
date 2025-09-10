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

            // 오브젝트 이름으로 관리하는 건 위험함
            // 특히 Clone 제거 로직은 버전에 따라 언제 변할지 모름
            // 차라리 아이템 클래스에 멤버를 만들어서 활용 - itemCode, itemId 등
            string itemName = gameObject.name;
            itemName = itemName.Remove(itemName.IndexOf("(Clone)")); // IndexOf 활용 자체는 좋음
            StageManager.Instance.Stage.itemPools[itemName].Release(gameObject);
        }
    }

    // 아이템 효과 적용
    // 상속 활용 좋음
    // 하위 클래스에서 필요한 기능만 집중가능한 구조
    protected abstract void ApplyEffect(GameObject player);
}
