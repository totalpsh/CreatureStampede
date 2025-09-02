using System.Collections.Generic;
using UnityEngine;

public class IceBowController : BaseWeapon
{
    float timer;
    Player player;
    PlayerController playerController;

    List<Scanner> movingWeaponPool;
     
    [SerializeField] GameObject movingWeaponPrefab;
    // ���� ��ġ
    [SerializeField] private float radius = 1f;

    protected override void Awake()
    {
        base.Awake(); // BaseWeapon�� Awake ȣ��
        player = GetComponentInParent<Player>();
        playerController = GetComponentInParent<PlayerController>();
        movingWeaponPool = new List<Scanner>();
    }

    private void Start()
    {

        MovingWeaponSetting();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate) // fireRate�� BaseWeapon���� �����˴ϴ�.
        {
            timer = 0f;
            BulletSetting();
        }

        transform.Rotate(Vector3.forward * Data.WeaponData.spreadAngle * Time.deltaTime);
    }

    protected override void BulletSetting()
    {
        

        foreach (var go in movingWeaponPool)
        {
            if (!go.nearestTarget)
                return;

            Vector3 targetPos = go.nearestTarget.position;
            Vector3 dir = (targetPos - go.transform.position).normalized;

            Transform serveBullet = GetBullet().transform;
            serveBullet.position = go.transform.position; // �߻� ��ġ �ʱ�ȭ
            serveBullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // �߻� ���� ����
            serveBullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, dir, Data.WeaponData.speed);
        }

    }

    public override void LevelUp()
    {
        base.LevelUp();

        MovingWeaponSetting();
    }

    // �߻��� ��ġ ����
    void MovingWeaponSetting()
    {
        for (int i = 0; i < count; i++)
        {
            Transform iceWeapon;

            if (i < transform.childCount)
            {
                iceWeapon = transform.GetChild(i);
            }
            else
            {
                iceWeapon = GetMovingWeapon().transform;
                iceWeapon.SetParent(transform);
            }




            Vector3 rotVec = Vector3.forward * 360 * i / (count);
            iceWeapon.Rotate(rotVec);
            iceWeapon.Translate(Vector3.up * radius, Space.Self); // ��ġ ����
        }
    }

    Scanner GetMovingWeapon()
    {
        Scanner select = null;

        foreach (var go in movingWeaponPool)
        {
            if (!go.gameObject.activeSelf)
            {
                select = go;
                select.gameObject.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            // ���� ���� �ʿ�
            select = Instantiate(movingWeaponPrefab, transform).GetComponent<Scanner>();
            movingWeaponPool.Add(select);
        }
        return select;
    }
}
