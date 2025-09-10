using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider2D[] targets; // RaycastHit2D[]���� Collider2D[]�� ����Ǿ����ϴ�.
    public Transform nearestTarget;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (mainCamera == null) return;

        // ī�޶��� ���� ��ǥ ���� ũ�� ���
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector2 boxSize = new Vector2(cameraWidth, cameraHeight);

        // ȭ�� ũ�⿡ �´� �簢�� �������� ��� Ÿ���� ����
        // 성능에 부하가 있는 기능 NonAlloc 을 같이 체크해보자
        // 지금은 매번 배열을 새로 만듬
        targets = Physics2D.OverlapBoxAll(mainCamera.transform.position, boxSize, 0f, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = float.MaxValue; // �ִ밪���� �ʱ�ȭ

        // Collider2D �迭�� ��ȸ�ϵ��� ����
        foreach (Collider2D target in targets)
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

    // ����׿�
    private void OnDrawGizmos()
    {
        if (mainCamera == null) return;

        Gizmos.color = Color.red;
        
        // ī�޶� �信 �´� �簢�� �׸���
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 boxSize = new Vector3(cameraWidth, cameraHeight, 0);
        Gizmos.DrawWireCube(mainCamera.transform.position, boxSize);
    }
}
