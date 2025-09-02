using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        // 캐스팅 시작 위치, 반지름, 방향, 거리, 레이어
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0f, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100; // 충분히 큰 값으로 초기화

        foreach (RaycastHit2D target in targets)
        {
            Vector2 myPos = transform.position;
            Vector2 targetPos = target.transform.position;

            float curDiff = Vector2.Distance(myPos, targetPos); // 현재 타겟과의 거리

            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }


        return result;
    }
}
