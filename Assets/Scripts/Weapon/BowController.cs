using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : BaseWeapon
{
    float timer;

    PlayerController playerController;

    

    protected override void Awake()
    {
        base.Awake(); // BaseWeapon�� Awake ȣ��
        
    }

    protected override void Start()
    {
        base.Start(); // BaseWeapon�� Start ȣ��
        playerController = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= fireRate) // fireRate�� BaseWeapon���� �����˴ϴ�.
        {
            timer = 0f;
            BulletSetting();
        }
    }

    protected override void BulletSetting()
    {
        //if (!player.scanner.nearestTarget)
        //    return;

        // �߻� ���� ����
        Vector2 direction = playerController.MovementDirection;
        if (direction == Vector2.zero)
        {
            // �÷��̾ �������� ���, �ٶ󺸴� �������� �߻�
            direction = playerController.LastMovementDirection;
        }

        float startAngle = -(count - 1) * Data.WeaponData.spreadAngle / 2; // ���� ���� ���

        for (int i = 0; i < count; i++)
        {
            // 트랜스폼 말고 Bullet 으로 가져오고
            // 트랜스폼 조작은 Bullet 의 메서드를 통해 초기화 고려
            Transform bullet = GetBullet().transform;
            bullet.position = player.transform.position; // �߻� ��ġ �ʱ�ȭ

            // ���� ���� ���
            float angle = startAngle + i * Data.WeaponData.spreadAngle;
            // ���� ���� ȸ��
            Vector2 fireDirection = Quaternion.Euler(0, 0, angle) * direction;

            bullet.position = transform.position; // �߻� ��ġ ����
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, fireDirection); // �߻� ���� ����
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, fireDirection, Data.WeaponData.speed);
            bullet.SetParent(player.bulletpoolGO.transform);
        }
        ShootSound();
    }


}
