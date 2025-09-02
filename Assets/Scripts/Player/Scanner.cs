using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public LayerMask targetLayer;
    public Collider2D[] targets; // RaycastHit2D[]에서 Collider2D[]로 변경되었습니다.
    public Transform nearestTarget;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (mainCamera == null) return;

        // 카메라의 월드 좌표 기준 크기 계산
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector2 boxSize = new Vector2(cameraWidth, cameraHeight);

        // 화면 크기에 맞는 사각형 영역으로 모든 타겟을 감지
        targets = Physics2D.OverlapBoxAll(mainCamera.transform.position, boxSize, 0f, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = float.MaxValue; // 최대값으로 초기화

        // Collider2D 배열을 순회하도록 수정
        foreach (Collider2D target in targets)
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

    // 디버그용
    private void OnDrawGizmos()
    {
        if (mainCamera == null) return;

        Gizmos.color = Color.red;
        
        // 카메라 뷰에 맞는 사각형 그리기
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 boxSize = new Vector3(cameraWidth, cameraHeight, 0);
        Gizmos.DrawWireCube(mainCamera.transform.position, boxSize);
    }
}
