using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : BaseWeapon
{
    float timer;



    protected override void Awake()
    {
        base.Awake(); // BaseWeapon의 Awake 호출
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0f;
            BulletSetting();
        }
    }


    protected override void BulletSetting()
    {
        // 기첫 번째 랜덤 방향을 설정
        Vector2 initialDirection = Random.insideUnitCircle.normalized;

        for (int i = 0; i < count; i++)
        {
            Transform bullet = GetBullet().transform;
            bullet.SetParent(player.bulletpoolGO.transform);
            bullet.position = player.transform.position;

            // 각 부메랑이 발사될 각도 계산
            float angle = 360f * i / count;

            // 초기 랜덤 방향에서 계산된 각도만큼 회전시킨 최종 방향 구함
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 fireDirection = rotation * initialDirection;
            
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, fireDirection, speed, abilityValue);
        }
        ShootSound();
    }
}
