using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionMap : MonoBehaviour
{
    PlayerController playerController;

    bool isAvailable = false;

    private void Start()
    {
        playerController = PlayerManager.Instance.Player.GetComponent<PlayerController>();
        PlayerManager.Instance.Player.OnCharacterDie += OnPlayerDie;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isAvailable)
            return;
        if (!collision.gameObject.CompareTag("Area"))
            return;

        Vector2 playerPos = PlayerManager.Instance.Player.transform.position;
        Vector2 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = playerController.MovementDirection;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        if (diffX > diffY)
        {
            transform.Translate(Vector3.right * dirX * 80);
        }
        else if (diffX < diffY)
        {
            transform.Translate(Vector3.up * dirY * 80);

        }
    }

    public void OnPlayerDie()
    {
        isAvailable = false;
    }
}
