using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : BaseWeapon
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
        //랜덤 방향으로 발사
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GetBullet().transform;
            bullet.SetParent(player.bulletpoolGO.transform);
            bullet.position = player.transform.position;

            // 랜덤 방향 생성
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, randomDir); // 발사 방향 설정
            // 총알 초기화
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, randomDir, speed, abilityValue);
        }
    }

}
