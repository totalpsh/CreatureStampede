using System.Collections.Generic;
using UnityEngine;

public class IceBowController : BaseWeapon
{
    float timer;

    Player player;
    List<Scanner> movingWeaponPool;
     
    [SerializeField] GameObject movingWeaponPrefab;


    [SerializeField] private float radius = 1f; // 움직이는 무기 생성 위치

    protected override void Awake()
    {
        base.Awake(); // BaseWeapon의 Awake 호출
        movingWeaponPool = new List<Scanner>();
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {

        MovingWeaponSetting();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate) // fireRate는 BaseWeapon에서 관리됩니다.
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

            Transform bullet = GetBullet().transform;
            bullet.SetParent(player.bulletpoolGO.transform);
            bullet.position = go.transform.position; // 발사 위치 초기화
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 발사 방향 설정
            bullet.GetComponent<Bullet>().Init(damage, Data.WeaponData.isPierce, dir, Data.WeaponData.speed, Data.WeaponData.abilityValue);
        }

    }

    public override void LevelUp()
    {
        base.LevelUp();

        MovingWeaponSetting();
    }

    // 발사할 위치 설정
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
            iceWeapon.Translate(Vector3.up * radius, Space.Self); // 위치 설정
        }
    }

    // 움직이는 무기 풀에서 사용 가능한 무기 가져오기
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
            // 생성 수정 필요
            select = Instantiate(movingWeaponPrefab, transform).GetComponent<Scanner>();
            movingWeaponPool.Add(select);
        }
        return select;
    }
}
