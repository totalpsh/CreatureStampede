using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : BaseWeapon
{
    float timer;
    Player player;



    protected override void Awake()
    {
        base.Awake(); // BaseWeapon�� Awake ȣ��
        player = GetComponentInParent<Player>();

    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate) // fireRate�� BaseWeapon���� �����˴ϴ�.
        {
            timer = 0f;
            BulletSetting();
        }
    }

    protected override void BulletSetting()
    {
        //���� �������� �߻�
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GetBullet().transform;
            bullet.SetParent(player.bulletpoolGO.transform);
            bullet.position = player.transform.position;

            // ���� ���� ����
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, randomDir); // �߻� ���� ����
            // �Ѿ� �ʱ�ȭ
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, randomDir, speed, abilityValue);
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}
