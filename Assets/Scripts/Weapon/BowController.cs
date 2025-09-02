using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : BaseWeapon
{
    float timer;
    Player player;
    PlayerController playerController;

    

    protected override void Awake()
    {
        base.Awake(); // BaseWeapon의 Awake 호출
        player = GetComponentInParent<Player>();
        playerController = GetComponentInParent<PlayerController>();
        
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= fireRate) // fireRate는 BaseWeapon에서 관리됩니다.
        {
            timer = 0f;
            BulletSetting();
        }
    }

    protected override void BulletSetting()
    {
        //if (!player.scanner.nearestTarget)
        //    return;

        // 발사 방향 설정
        Vector2 direction = playerController.MovementDirection;
        if (direction == Vector2.zero)
        {
            // 플레이어가 멈춰있을 경우, 바라보는 방향으로 발사
            direction = playerController.LastMovementDirection;
        }

        float startAngle = -(count - 1) * Data.WeaponData.spreadAngle / 2; // 시작 각도 계산

        for (int i = 0; i < count; i++)
        {
            Transform bullet = GetBullet().transform;
            bullet.position = player.transform.position; // 발사 위치 초기화

            // 현재 각도 계산
            float angle = startAngle + i * Data.WeaponData.spreadAngle;
            // 방향 벡터 회전
            Vector2 fireDirection = Quaternion.Euler(0, 0, angle) * direction;

            bullet.position = transform.position; // 발사 위치 설정
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, fireDirection); // 발사 방향 설정
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, fireDirection, Data.WeaponData.speed);
            bullet.SetParent(player.bulletpoolGO.transform);
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}
