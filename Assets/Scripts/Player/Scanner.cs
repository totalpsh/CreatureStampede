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
        // ĳ���� ���� ��ġ, ������, ����, �Ÿ�, ���̾�
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0f, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100; // ����� ū ������ �ʱ�ȭ

        foreach (RaycastHit2D target in targets)
        {
            Vector2 myPos = transform.position;
            Vector2 targetPos = target.transform.position;

            float curDiff = Vector2.Distance(myPos, targetPos); // ���� Ÿ�ٰ��� �Ÿ�

            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }


        return result;
    }
}
