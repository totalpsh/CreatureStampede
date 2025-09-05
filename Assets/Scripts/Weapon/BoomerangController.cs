using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : BaseWeapon
{
    float timer;



    protected override void Awake()
    {
        base.Awake(); // BaseWeapon�� Awake ȣ��
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
        // ��ù ��° ���� ������ ����
        Vector2 initialDirection = Random.insideUnitCircle.normalized;

        for (int i = 0; i < count; i++)
        {
            Transform bullet = GetBullet().transform;
            bullet.SetParent(player.bulletpoolGO.transform);
            bullet.position = player.transform.position;

            // �� �θ޶��� �߻�� ���� ���
            float angle = 360f * i / count;

            // �ʱ� ���� ���⿡�� ���� ������ŭ ȸ����Ų ���� ���� ����
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 fireDirection = rotation * initialDirection;
            
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, fireDirection, speed, abilityValue);
        }
        ShootSound();
    }
}
