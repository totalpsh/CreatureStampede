using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : BaseWeapon
{
    // ���� ��ġ
    [SerializeField] private float radius = 1.5f;

    protected override void Start()
    {
        base.Start(); // BaseWeapon�� Start ȣ��
        BulletSetting();
    }


    private void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

    protected override void BulletSetting()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GetBullet().transform;
                bullet.SetParent(transform);
            }

           
            bullet.localPosition = Vector3.zero; // �߻� ��ġ �ʱ�ȭ
            bullet.localRotation = Quaternion.identity; // �߻� ���� �ʱ�ȭ

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(Vector3.up * radius, Space.Self); // �߻� ��ġ ����
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, Vector3.zero);
        }
        ShootSound();
    }

    public override void LevelUp()
    {
        base.LevelUp();

        BulletSetting();
    }
}
